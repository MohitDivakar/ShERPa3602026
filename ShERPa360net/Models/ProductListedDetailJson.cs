using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShERPa360net.Models
{
    public class ProductListedDetailJson
    {
        public string PURQTY                    { get; set; }
        public Decimal MOBEXPRICE               { get; set; }
        public Decimal MOBILENEWRATE            { get; set; }
        public Decimal MOBILEPURCHASEPERCENTAGE { get; set; }
        public string NGEAPRV                   { get; set; }
        public int NEGAPRVBY                    { get; set; }  
        public int ISAPPROVEDFK                 { get; set; }
        public int ISAPPROVEDAMZ                { get; set; }
        public int ISAPPROVEDWEB                { get; set; }
        public Decimal FKAMT                    { get; set; }
        public Decimal AMZAMT                   { get; set; }
        public Decimal WEBAMT                   { get; set; }
        public Decimal FKPER                    { get; set; }
        public Decimal AMZPER                   { get; set; }
        public Decimal WEBPER                   { get; set; }
        public Decimal BASICPURRATE             { get; set; }
        public int STATUS                       { get; set; }
        public int ID                           { get; set; }
        public string   REJECTREASON            { get; set; } = "";
        public int      ITEMID                  { get; set; } = 0;
        public string   ITEMCODE                { get; set; } = "";
        public Decimal PURFKAMT                 { get; set; } = 0;
        public Decimal PURAMZAMT                { get; set; } = 0;
        public Decimal PURWEBAMT                { get; set; } = 0;
        public Decimal PURFKPER                 { get; set; } = 0;
        public Decimal PURAMZPER                { get; set; } = 0;
        public Decimal PURWEBPER                { get; set; } = 0;
        public Decimal PURCHASEPERONVENDORPRICE { get; set; } = 0;
        public int ISPURAPPROVEDFK              { get; set; } = 0;
        public int ISPURAPPROVEDAMZ             { get; set; } = 0;
        public int ISPURAPPROVEDWEB             { get; set; } = 0;
        public Decimal FinalApproveListingAmount { get; set; } = 0;
    }
}