using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ShERPa360net.Class
{
    public class JobSheetMaster
    {

        public int CMPID { get; set; }
        public string JOBID { get; set; }
        public string JOBDT { get; set; }
        public int JSTYPE { get; set; }
        public string SHIPTOPARTY { get; set; }
        public string BILLTOPARTY { get; set; }
        public string ENDCUST { get; set; }
        public string CUSTPONO { get; set; }
        public string CUSTPODT { get; set; }
        public int ADDID { get; set; }
        public string REMARK { get; set; }
        public int JOBSTATUS { get; set; }
        public string STATRES { get; set; }
        public string STATUPDDT { get; set; }
        public int STATUPDBY { get; set; }
        public string SEGMENT { get; set; }
        public string DISTCHNL { get; set; }
        public string ISRETURN { get; set; }
        public string REFJOBID { get; set; }
        public int CREATEBY { get; set; }
        public string CREATEDATE { get; set; }
        public int UPDATEBY { get; set; }
        public string UPDATEDATE { get; set; }
        public int STAGEID { get; set; }
        public string ONHOLD { get; set; }
        public string ONHOLDREASON { get; set; }
        public string ONHOLDDT { get; set; }
        public string HOLDRELDT { get; set; }
        public string JDAREF { get; set; }
        public string JDAREFDT { get; set; }
        public string PICKUPFROM { get; set; }
        public string SHIPTO { get; set; }
        public string APRVFLAG { get; set; }
        public int PICKUPADDID { get; set; }
        public int DROPADDID { get; set; }
        public string AGENTCD { get; set; }
        public int INQNO { get; set; }
        public int OOW { get; set; }
        public string JWREFNO { get; set; }
        public string JWREFNO2 { get; set; }
        public string JWREFDT { get; set; }
        public string JWREFNO3 { get; set; }
        public string JWREFDT2 { get; set; }
        public string JWREFDT3 { get; set; }
        public string JWREFNO4 { get; set; }
        public string JWREFDT4 { get; set; }
        public string PONO { get; set; }
        public string SLRNO { get; set; }
        public int LISTINGID { get; set; }
        public string ITEMCODE { get; set; }
        public List<JobSheetDetail> lstJobDetail { get; set; }
        public AddressDetail AddressDetail { get; set; }

    }

    public class JobSheetDetail
    {
        public int ID { get; set; }
        public int CMPID { get; set; }
        public string JOBID { get; set; }
        public int SRNO { get; set; }
        public int ITEMID { get; set; }
        public string ITEMDESC { get; set; }
        public Decimal QTY { get; set; }
        public int UOM { get; set; }
        public Decimal RATE { get; set; }
        public Decimal ITEMVALUE { get; set; }
        public string PLANTCD { get; set; }
        public string LOCCD { get; set; }
        public string EXTWARNO { get; set; }
        public string PRODMAKE { get; set; }
        public string PRODMODEL { get; set; }
        public string IMEINO { get; set; }
        public string JOBTYPE { get; set; }
        public string JOBDESC { get; set; }
        public string REFINVNO { get; set; }
        public string REFINVDT { get; set; }
        public decimal REFINVAMT { get; set; }
        public string INSUCO { get; set; }
        public string NOTE { get; set; }
        public string PRODCOND { get; set; }
        public string WAYBILLNO { get; set; }
        public string REVDCNO { get; set; }
        public string FWAYBILLNO { get; set; }
        public string DCNO { get; set; }
        public string BATTERYNO { get; set; }
        public string REVTRANNAME { get; set; }
        public string FTRANNAME { get; set; }
        public string WAYBILLSTATUS { get; set; }
        public string FWAYBILLSTATUS { get; set; }
        public string DELICONFDT { get; set; }
        public string LOCKCODE { get; set; }
        public string REVPICKUPDT { get; set; }
        public string BACKCOVERFLAG { get; set; }
        public string REVDELIDT { get; set; }
        public string FPICKUPDT { get; set; }
        public string PHYIMEINO { get; set; }
        public string FESTIDELDT { get; set; }
        public string REVESTIDELDT { get; set; }
        public string IMEINO2 { get; set; }
        public string CODWAYBILLNO { get; set; }
        public int FEDEXPICKUP { get; set; }
        public string PRODCOLOR { get; set; }
        public string DELIVERYTO { get; set; }
        public string GRADE { get; set; }
        public Decimal BILLAMT { get; set; }
    }

    public class AddressDetail
    {
        public int ID { get; set; }
        public int CMPID { get; set; }
        public string REFID { get; set; } = "";
        public string REFTYPE { get; set; } = "";
        public string ADDOF { get; set; } = "";
        public string ADDR1 { get; set; } = "";
        public string ADDR2 { get; set; } = "";
        public string ADDR3 { get; set; } = "";
        public string CITY { get; set; } = "";
        public int STCD { get; set; }
        public string CNCD { get; set; } = "";
        public string POSTALCODE { get; set; } = "";
        public string CONTACTPERSON { get; set; } = "";
        public string CONTACTNO { get; set; } = "";
        public string CONTACTPERSON2 { get; set; } = "";
        public string CONTACTNO2 { get; set; } = "";
        public string CONTACTPERSON3 { get; set; } = "";
        public string CONTACTNO3 { get; set; } = "";
        public string MOBILENO { get; set; } = "";
        public string MOBILENO2 { get; set; } = "";
        public string MOBILENO3 { get; set; } = "";
        public string FAXNO { get; set; } = "";
        public string EMAILID { get; set; } = "";
        public string WEBSITE { get; set; } = "";
        public string LAT { get; set; } = "";
        public string LONG { get; set; } = "";
    }


    public class JobSheetResponse
    {
        public string JOBNO { get; set; }
        public string MESSAGE { get; set; }
    }

    public class ReverseWaybillUpdate
    {

        public int CMPID { get; set; }

        public string JOBID { get; set; }

        public int STAGEID { get; set; }

        public int JOBSTATUS { get; set; }

        public string STATRES { get; set; }

        public int CREATEBY { get; set; }

        public string REVTRANNAME { get; set; }

        public string WAYBILLNO { get; set; }

        public string WAYBILLSTATUS { get; set; }

        public string DOCTYPE { get; set; }

    }

    public class JobCardMaster
    {
        public int CMPID { get; set; }

        public string JCNO { get; set; }

        public string JCDT { get; set; }

        public string JOBID { get; set; }

        public int JOBSTATUS { get; set; }

        public int JOBIDSRNO { get; set; }

        public string WRKCNT { get; set; }

        public int ITEMID { get; set; }

        public int QTY { get; set; }

        public int UOM { get; set; }

        public string PLANTCD { get; set; }

        public string LOCCD { get; set; }

        public int CREATEBY { get; set; }

        public string CREATEDATE { get; set; }

        public int STAGEID { get; set; }

        public string BACKCOVERFLAG { get; set; }

    }

    public class JobCardResponse
    {
        public string JCNO { get; set; }
        public string MESSAGE { get; set; }
    }


    public class EstimateMaster
    {
        public int CMPID { get; set; }

        public int ESTIAMT_PART { get; set; }

        public int ESTIAMT_SERV { get; set; }

        public int COSTAMT_PART { get; set; }

        public int NWREASON { get; set; }

        public int RITEMID { get; set; }

        public int PAYMODE { get; set; }

        public int TRANCHGPCT { get; set; }

        public int TRANCHG { get; set; }

        public int LOGICHG { get; set; }

        public int ASCCHG { get; set; }

        public int DISCAMT { get; set; }

        public int JOBIDSRNO { get; set; }

        public int STATUS { get; set; }

        public int STAGEID { get; set; }

        public int CREATEBY { get; set; }

        public string ESTINO { get; set; }

        public DateTime ESTIDT { get; set; }

        public string JOBID { get; set; }

        public string REMARK { get; set; }

        public string STATRES { get; set; }

        public string PURREF { get; set; }

        public DateTime PURDT { get; set; }

        public string PARTDESC { get; set; }

        public string SERVDESC { get; set; }

        public string CREATEDATE { get; set; }

        public string LIQUIDDAMAGE { get; set; }

        public string ISRETURN { get; set; }

        public string TOTALLOSS { get; set; }

        public string RPRODMAKE { get; set; }

        public string RPRODMODEL { get; set; }

        public string RIMEINO { get; set; }

        public string RBATTERYNO { get; set; }

        public string HSNEW { get; set; }

        public DateTime ETD { get; set; }


    }

    public class EstimateResponse
    {
        public string ESTINO { get; set; }
        public string MESSAGE { get; set; }
    }

    public class EstimateApproval
    {
        public int CMPID { get; set; }

        public int STATUS { get; set; }

        public int STAGEID { get; set; }

        public int APRVBY1 { get; set; }

        public int APRVFLAG { get; set; }

        public int PAYMODE { get; set; }

        public int REJRES { get; set; }

        public int UPDATEBY { get; set; }

        public string ESTINO { get; set; }

        public string APRVNO1 { get; set; }

        public string CUSTAPRVBY { get; set; }

        public string APRVNOTE { get; set; }

        public DateTime APRVDT1 { get; set; }

        public string UPDATEDATE { get; set; }


    }

    public class EstimateApprovalResponse
    {
        public string ESTINO { get; set; }
        public string MESSAGE { get; set; }
    }

    public class JobCardDetails
    {

        public int CMPID { get; set; }
        public string JCNO { get; set; }
        public int STAGEID { get; set; }
        public DateTime STARTDT { get; set; }
        public DateTime ENDDT { get; set; }
        public string PROBLEM { get; set; }
        public int JOBPROBID { get; set; }
        public string PROBLEM1 { get; set; }
        public int JOBPROBID1 { get; set; }
        public string PROBLEM2 { get; set; }
        public int JOBPROBID2 { get; set; }
        public string PROBLEM3 { get; set; }
        public int JOBPROBID3 { get; set; }
        public string JOBDONE { get; set; }
        public int RESULT { get; set; }
        public string PARTREQ { get; set; }
        public int PARTREQID { get; set; }
        public string PARTREPLACED { get; set; }
        public string NOTE { get; set; }
        public int JOBDONEBY { get; set; }
        public int NEXTSTAGEREQ { get; set; }
        public int CREATEBY { get; set; }
        public string CREATEDATE { get; set; }
        public string ASCPARTCODE { get; set; }
        public string NEWIMEINO { get; set; }
        public string JOBID { get; set; }
    }

    public class UpdateJCMater
    {
        public int CMPID { get; set; }

        public string JCNO { get; set; }

        public int STAGEID { get; set; }

        public int JOBSTATUS { get; set; }

        public string STATRES { get; set; }

        public int STATUPDBY { get; set; }

        public DateTime STATUPDDT { get; set; }

        public int JCDTLID { get; set; }
    }


    public class JobSpecification
    {
        public int CMPID { get; set; }
        public string JOBID { get; set; }
        public string DISPLAYSIZE { get; set; }
        public string FRONT_CAMERA { get; set; }
        public string FRONT_CAMERA2 { get; set; }
        public string BACK_CAMERA { get; set; }
        public string BACK_CAMERA2 { get; set; }
        public string RAMSIZE { get; set; }
        public string ROMSIZE { get; set; }
        public string COLOR { get; set; }
        public string VOLTE_4G { get; set; }
        public string PRODGRADE { get; set; }
        public string MODELDESC { get; set; }
        public string SERIALNO { get; set; }
        public string SKU { get; set; }
        public decimal MRP { get; set; }
        public string CABELTYPE { get; set; }
        public int CREATEBY { get; set; }
        public DateTime CREATEDATE { get; set; }
        public int UPDATEBY { get; set; }
        public DateTime UPDATEDATE { get; set; }
        public string LCDCOLOR { get; set; }
        public string PURGRADE { get; set; }
        public int PITEMID { get; set; }
        public string PITEMDESC { get; set; }
        public string ITEMCODE { get; set; }
        public int ADJUSTREQ { get; set; }
        public int OLDITEMID { get; set; }
        public string OLDITEMCODE { get; set; }
        public string ACTION { get; set; }
    }

    public class JobSpecificationResponse
    {
        public string MESSAGE { get; set; }

        public DataTable Data { get; set; }

    }


    public class PRMaster
    {
        public int CMPID { get; set; }
        public string PRTYPE { get; set; }
        public string PRNO { get; set; }
        public DateTime PRDT { get; set; }
        public string REMARK { get; set; }
        public int STATUS { get; set; }
        public int DEPTID { get; set; }
        public int CREATEBY { get; set; }
        public DateTime CREATEDATE { get; set; }
        public int LISTINGID { get; set; }
        public string VENDCODE { get; set; }
        public int ISPRSTO { get; set; }
        public List<PRDetails> PRDATA { get; set; }
    }

    public class PRDetails
    {
        public int CMPID { get; set; }
        public string PRNO { get; set; }
        public int SRNO { get; set; }
        public int ITEMID { get; set; }
        public string ITEMDESC { get; set; }
        public string PLANTCD { get; set; }
        public string LOCCD { get; set; }
        public string TRNUM { get; set; }
        public int ITEMGRPID { get; set; }
        public decimal PRQTY { get; set; }
        public int UOM { get; set; }
        public decimal RATE { get; set; }
        public decimal CAMOUNT { get; set; }
        public DateTime DELIDT { get; set; }
        public string GLCD { get; set; }
        public string CSTCENTCD { get; set; }
        public string PRFCNT { get; set; }
        public string ASSETCD { get; set; }
        public string PRBY { get; set; }
        public string ITEMTEXT { get; set; }
        public int PARTREQNO { get; set; }
        public int STATUS { get; set; }
        public string IMEINO { get; set; }
    }

    public class PRRespsonse
    {
        public string PRNO { get; set; }

        public string MESSAGE { get; set; }
    }


    public class POMaster
    {
        public int CMPID { get; set; }
        public string POTYPE { get; set; }
        public string PONO { get; set; }
        public DateTime PODT { get; set; }
        public string VENDCODE { get; set; }
        public string TRANCODE { get; set; }
        public string PMTTERMS { get; set; }
        public string REMARK { get; set; }
        public int STATUS { get; set; }
        public decimal NETMATVALUE { get; set; }
        public decimal NETTAXAMT { get; set; }
        public decimal DISCOUNT { get; set; }
        public decimal NETPOAMT { get; set; }
        public int CREATEBY { get; set; }
        public DateTime CREATEDATE { get; set; }
        public string PMTTERMSDESC { get; set; }
        public string AGENTNAME { get; set; }
        public decimal ADJAMT { get; set; }
        public decimal ADVAMT { get; set; }
        public decimal PENDINGAMT { get; set; }
        public int APRVBY { get; set; }
        public DateTime APRVDATE { get; set; }
        public decimal OLDPOAMT { get; set; }
        public int DEPTID { get; set; }
        public int PURTYPE { get; set; }

        public List<PODetails> PODetail { get; set; }

        public List<POTaxation> POTax { get; set; }

        public List<POCharges> POCharge { get; set; }
    }

    public class PODetails
    {
        public int CMPID { get; set; }
        public string PONO { get; set; }
        public int SRNO { get; set; }
        public string PRNO { get; set; }
        public int PRSRNO { get; set; }
        public int ITEMID { get; set; }
        public string ITEMDESC { get; set; }
        public string PLANTCD { get; set; }
        public string LOCCD { get; set; }
        public int ITEMGRPID { get; set; }
        public decimal POQTY { get; set; }
        public int UOM { get; set; }
        public decimal RATE { get; set; }
        public decimal CAMOUNT { get; set; }
        public decimal DISCAMT { get; set; }
        public DateTime DELIDT { get; set; }
        public string GLCD { get; set; }
        public string CSTCENTCD { get; set; }
        public string PRFCNT { get; set; }
        public string ASSETCD { get; set; }
        public string ITEMTEXT { get; set; }
        public decimal TAXAMT { get; set; }
        public string TRNUM { get; set; }
        public string REFNO { get; set; }
        public string IMEINO { get; set; }
        public decimal BRATE { get; set; }
        public string FROMPLANTCD { get; set; }
        public string FROMLOCCD { get; set; }
        public string DEVREASON { get; set; }
        public int APRVSTATUS { get; set; }
        public string REJREASON { get; set; }
        public int APRVBY { get; set; }
        public DateTime APRVDATE { get; set; }
        public decimal LOCKAMT { get; set; }
    }

    public class POTaxation
    {
        public int CMPID { get; set; }
        public int CONDORDER { get; set; }
        public int SRNO { get; set; }
        public string PONO { get; set; }
        public int POSRNO { get; set; }
        public int CONDID { get; set; }
        public string CONDTYPE { get; set; }
        public string GLCODE { get; set; }
        public decimal RATE { get; set; }
        public decimal BASEAMT { get; set; }
        public int PID { get; set; }
        public decimal TAXAMT { get; set; }
        public string OPERATOR { get; set; }
    }

    public class POCharges
    {
        public int CMPID { get; set; }
        public string PONO { get; set; }
        public int SRNO { get; set; }
        public string CHGTYPE { get; set; }
        public decimal CHGAMT { get; set; }
        public decimal CONDID { get; set; }
        public string CONDTYPE { get; set; }
        public string GLCODE { get; set; }
        public decimal RATE { get; set; }
        public int PID { get; set; }
        public decimal TAXAMT { get; set; }
        public string OPERATOR { get; set; }
    }

    public class PORespsonse
    {
        public string PONO { get; set; }

        public string MESSAGE { get; set; }
    }

    public class CommissionMaster
    {
        public int ITEMCATEGORY { get; set; }
        public int PERORFIX { get; set; }
        public decimal RATE { get; set; }
        public int STATUS { get; set; }
        public int CREATEBY { get; set; }
        public DateTime CREATEDATE { get; set; }
        public int UPDATEBY { get; set; }
        public DateTime UPDATEDATE { get; set; }
        public string ACTION { get; set; }
        public string VENDCODE { get; set; }
    }


    public class CommissionResponse
    {
        public string MESSAGE { get; set; }

        public DataTable Data { get; set; }

    }

    public class GRNMaster
    {
        public int CMPID { get; set; }
        public int FINYEAR { get; set; }
        public string DOCNO { get; set; }
        public string DOCTYPE { get; set; }
        public DateTime DOCDATE { get; set; }
        public string CHLNNO { get; set; }
        public DateTime CHLNDT { get; set; }
        public DateTime POSTDATE { get; set; }
        public string TRANCODE { get; set; }
        public string REFDOCNO { get; set; }
        public int REFDOCYEAR { get; set; }
        public string REFNO { get; set; }
        public string REMARK { get; set; }
        public int DEPTCD { get; set; }
        public string EMPNAME { get; set; }
        public int CREATEBY { get; set; }
        public List<GRNDetials> GRNData { get; set; }
    }

    public class GRNDetials
    {
        public int CMPID { get; set; }
        public int FINYEAR { get; set; }
        public string DOCNO { get; set; }
        public string DOCTYPE { get; set; }
        public int SRNO { get; set; }
        public string PONO { get; set; }
        public int POSRNO { get; set; }
        public int ITEMID { get; set; }
        public int UOM { get; set; }
        public decimal QTY { get; set; }
        public decimal CHLNQTY { get; set; }
        public decimal RATE { get; set; }
        public decimal CAMOUNT { get; set; }
        public string ITEMTEXT { get; set; }
        public string CSTCENTCD { get; set; }
        public string PRFCNT { get; set; }
        public string PLANTCD { get; set; }
        public string LOCCD { get; set; }
        public string GLCD { get; set; }
        public string ASSETCD { get; set; }
        public string ITEMDESC { get; set; }
        public int ITEMGRPID { get; set; }
        public string TRACKNO { get; set; }
    }

    public class GRNRespsonse
    {
        public string GRNNO { get; set; }

        public string MESSAGE { get; set; }
    }

    public class PBMaster
    {
        public int CMPID { get; set; }
        public string PBTYPE { get; set; }
        public string PBNO { get; set; }
        public DateTime PBDT { get; set; }
        public string VENDCODE { get; set; }
        public string BILLNO { get; set; }
        public DateTime BILLDT { get; set; }
        public string PMTTERMS { get; set; }
        public string PMTTERMSDESC { get; set; }
        public decimal NETMATVALUE { get; set; }
        public decimal NETTAXAMT { get; set; }
        public decimal DISCOUNT { get; set; }
        public decimal NETPBAMT { get; set; }
        public string REMARK { get; set; }
        public int STATUS { get; set; }
        public int CREATEBY { get; set; }
        public decimal ADJAMT { get; set; }
        public decimal PAIDAMT { get; set; }
        public decimal PENDINGAMT { get; set; }
        public decimal BILLAMT { get; set; }
        public List<PBDetails> PBDetail { get; set; }
        public List<PBTaxation> PBTax { get; set; }
        public List<PBCharges> PBCharge { get; set; }
    }

    public class PBDetails
    {
        public int CMPID { get; set; }
        public string PBNO { get; set; }
        public int SRNO { get; set; }
        public string PONO { get; set; }
        public int POSRNO { get; set; }
        public string MIRNO { get; set; }
        public int MIRSRNO { get; set; }
        public int ITEMID { get; set; }
        public string ITEMDESC { get; set; }
        public string PLANTCD { get; set; }
        public string LOCCD { get; set; }
        public int ITEMGRPID { get; set; }
        public decimal PBQTY { get; set; }
        public int UOM { get; set; }
        public decimal BRATE { get; set; }
        public decimal CAMOUNT { get; set; }
        public decimal DISCAMT { get; set; }
        public string GLCD { get; set; }
        public string CSTCENTCD { get; set; }
        public string PRFCNT { get; set; }
        public string ASSETCD { get; set; }
        public string ITEMTEXT { get; set; }
        public string TRNUM { get; set; }
        public string REFNO { get; set; }
        public decimal RATE { get; set; }
        public string CHALLANNO { get; set; }
        public decimal TAXAMT { get; set; }
    }

    public class PBTaxation
    {
        public int CMPID { get; set; }
        public int CONDORDER { get; set; }
        public int SRNO { get; set; }
        public string PBNO { get; set; }
        public int PBSRNO { get; set; }
        public int CONDID { get; set; }
        public string CONDTYPE { get; set; }
        public string GLCODE { get; set; }
        public decimal RATE { get; set; }
        public decimal BASEAMT { get; set; }
        public int PID { get; set; }
        public decimal TAXAMT { get; set; }
        public string OPERATOR { get; set; }
    }

    public class PBCharges
    {
        public int CMPID { get; set; }
        public string PBNO { get; set; }
        public int SRNO { get; set; }
        public string CHGTYPE { get; set; }
        public decimal CHGAMT { get; set; }
        public int CONDID { get; set; }
        public string CONDTYPE { get; set; }
        public string GLCODE { get; set; }
        public decimal RATE { get; set; }
        public int PID { get; set; }
        public decimal TAXAMT { get; set; }
        public string OPERATOR { get; set; }
    }

    public class PBRespsonse
    {
        public string PBNO { get; set; }

        public string MESSAGE { get; set; }
    }


    public class RTVResponse
    {
        public DataTable DTRTV { get; set; }

        public string MESSAGE { get; set; }
    }

}