using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShERPa360net.Models
{
    public class AcrossAssistanceRequestModel
    {
        public int planVariantId { get; set; }
        public string partnerRefId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string mobileNumber { get; set; }
        public string email { get; set; }
        public string streetAddress1 { get; set; }
        public string streetAddress2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string pincode { get; set; }
        public int deviceId { get; set; }
        public string serialNumber { get; set; }
        public string imei1 { get; set; }
        public object imei2 { get; set; }
        public int storageCapacityInGB { get; set; }
        public int ramInGB { get; set; }
        public string colour { get; set; }
        public string purchaseDate { get; set; }
        public int purchasePrice { get; set; }
        public string invoiceNumber { get; set; }
        public string invoiceImageUrl { get; set; }
        public string imeiOrSerialNumberImageUrl { get; set; }
        public int planPurchasePrice { get; set; }
        public int manufacturerWarrantyInMonths { get; set; }
        public string manufacturerWarrantyImageUrl { get; set; }
        public string dealerAccountUsername { get; set; }
    }

    public class AcrossAssistanceResponseSuccessModel
    {
        public bool success { get; set; }

        public string message { get; set; }

        public string activationCode { get; set; }
    }

    public class AcrossAssistanceResponseErrorModel
    {
        public bool success         { get; set; }
        public string message       { get; set; }
        public List<string> errors  { get; set; }
    }
}