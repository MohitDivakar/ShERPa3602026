using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShERPa360net.Class
{
    public class BlueDartWB
    {
    }

    public class WayBillGenerationRequest
    {
        public Consignee Consignee { get; set; }

        public bool IsUpdateAPI { get; set; }

        public ReturnAddress Returnadds { get; set; }

        public Services Services { get; set; }

        public Shipper Shipper { get; set; }
    }

    public class Consignee
    {
        public string AvailableDays { get; set; }
        public string AvailableTiming { get; set; }
        public string ConsigneeAddress1 { get; set; }
        public string ConsigneeAddress2 { get; set; }
        public string ConsigneeAddress3 { get; set; }
        public string ConsigneeAddressType { get; set; }
        public string ConsigneeAddressinfo { get; set; }
        public string ConsigneeAttention { get; set; }
        public string ConsigneeBusinessPartyTypeCode { get; set; }
        public string ConsigneeCityName { get; set; }
        public string ConsigneeCountryCode { get; set; }
        public string ConsigneeEmailID { get; set; }
        public string ConsigneeFiscalID { get; set; }
        public string ConsigneeFiscalIDType { get; set; }
        public string ConsigneeFullAddress { get; set; }
        public string ConsigneeGSTNumber { get; set; }
        public string ConsigneeID { get; set; }
        public string ConsigneeIDType { get; set; }
        public string ConsigneeLatitude { get; set; }
        public string ConsigneeLongitude { get; set; }
        public string ConsigneeMaskedContactNumber { get; set; }
        public string ConsigneeMobile { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsigneePincode { get; set; }
        public string ConsigneeStateCode { get; set; }
        public string ConsigneeTelephone { get; set; }
        public string ConsingeeFederalTaxId { get; set; }
        public string ConsingeeRegistrationNumber { get; set; }
        public string ConsingeeRegistrationNumberIssuerCountryCode { get; set; }
        public string ConsingeeRegistrationNumberTypeCode { get; set; }
        public string ConsingeeStateTaxId { get; set; }
    }

    public class ReturnAddress
    {
        public string ManifestNumber { get; set; }
        public string ReturnAddress1 { get; set; }
        public string ReturnAddress2 { get; set; }
        public string ReturnAddress3 { get; set; }
        public string ReturnAddressinfo { get; set; }
        public string ReturnContact { get; set; }
        public string ReturnEmailID { get; set; }
        public string ReturnLatitude { get; set; }
        public string ReturnLongitude { get; set; }
        public string ReturnMaskedContactNumber { get; set; }
        public string ReturnMobile { get; set; }
        public string ReturnPincode { get; set; }
        public string ReturnTelephone { get; set; }
    }


    public class Services
    {
        public string AWBNo { get; set; }
        public double ActualWeight { get; set; }
        public string AdditionalDeclaration { get; set; }
        public string AuthorizedDealerCode { get; set; }
        public string BankAccountNumber { get; set; }
        public string BillToAddressLine1 { get; set; }
        public string BillToCity { get; set; }
        public string BillToCompanyName { get; set; }
        public string BillToContactName { get; set; }
        public string BillToCountryCode { get; set; }
        public string BillToCountryName { get; set; }
        public string BillToFederalTaxID { get; set; }
        public string BillToPhoneNumber { get; set; }
        public string BillToPostcode { get; set; }
        public string BillToState { get; set; }
        public string BillToSuburb { get; set; }
        public string BillingReference1 { get; set; }
        public string BillingReference2 { get; set; }
        public double CessCharge { get; set; }
        public double CollectableAmount { get; set; }
        public CommodityDetail Commodity { get; set; }
        public string CreditReferenceNo { get; set; }
        public string CreditReferenceNo2 { get; set; }
        public string CreditReferenceNo3 { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime CustomerEDD { get; set; }
        public double DeclaredValue { get; set; }
        public int DeferredDeliveryDays { get; set; }
        public string DeliveryTimeSlot { get; set; }
        public ArrayOfDimension Dimensions { get; set; }
        public ArrayOfDynamicQCDetails DynamicQCDetails { get; set; }
        public string ECCN { get; set; }
        public string EsellerPlatformName { get; set; }
        public string ExchangeWaybillNo { get; set; }
        public string ExportImportCode { get; set; }
        public string ExportReason { get; set; }
        public string ExporterAddressLine1 { get; set; }
        public string ExporterAddressLine2 { get; set; }
        public string ExporterAddressLine3 { get; set; }
        public string ExporterBusinessPartyTypeCode { get; set; }
        public string ExporterCity { get; set; }
        public string ExporterCompanyName { get; set; }
        public string ExporterCountryCode { get; set; }
        public string ExporterCountryName { get; set; }
        public string ExporterDivision { get; set; }
        public string ExporterDivisionCode { get; set; }
        public string ExporterEmail { get; set; }
        public string ExporterFaxNumber { get; set; }
        public string ExporterMobilePhoneNumber { get; set; }
        public string ExporterPersonName { get; set; }
        public string ExporterPhoneNumber { get; set; }
        public string ExporterPostalCode { get; set; }
        public string ExporterRegistrationNumber { get; set; }
        public string ExporterRegistrationNumberIssuerCountryCode { get; set; }
        public string ExporterRegistrationNumberTypeCode { get; set; }
        public string ExporterSuiteDepartmentName { get; set; }
        public string FavouringName { get; set; }
        public string ForwardAWBNo { get; set; }
        public string ForwardLogisticCompName { get; set; }
        public double FreightCharge { get; set; }
        public string GovNongovType { get; set; }
        public string IncotermCode { get; set; }
        public double InsuranceAmount { get; set; }
        public string InsurancePaidBy { get; set; }
        public double InsurenceCharge { get; set; }
        public string InvoiceNo { get; set; }
        public bool IsCargoShipment { get; set; }
        public string IsChequeDD { get; set; }
        public bool IsCommercialShipment { get; set; }
        public bool IsDedicatedDeliveryNetwork { get; set; }
        public bool IsDutyTaxPaidByShipper { get; set; }
        public string IsEcomUser { get; set; }
        public bool IsForcePickup { get; set; }
        public bool IsIntlEcomCSBUser { get; set; }
        public bool IsPartialPickup { get; set; }
        public bool IsReversePickup { get; set; }
        public int ItemCount { get; set; }
        public string MarketplaceName { get; set; }
        public string MarketplaceURL { get; set; }
        public bool NFEIFlag { get; set; }
        public string NotificationMessage { get; set; }

        //<xs:element xmlns:q1="http://schemas.datacontract.org/2004/07/SAPI.Entities.Enums.AWBGeneration" minOccurs="0" name="OTPBasedDelivery" type="q1:OTPBasedDelivery"/>

        public string OTPCode { get; set; }
        public string Officecutofftime { get; set; }
        public string OrderURL { get; set; }
        public bool PDFOutputNotRequired { get; set; }
        public string PDFPrintContent { get; set; } //base64
        public string PackType { get; set; }
        public string ParcelShopCode { get; set; }
        public string PayableAt { get; set; }
        public double PayerGSTVAT { get; set; }
        public DateTime PickupDate { get; set; }
        public string PickupMode { get; set; }
        public string PickupTime { get; set; }
        public string PickupType { get; set; }
        public int PieceCount { get; set; }
        public DateTime PreferredDeliveryDate { get; set; }
        public string PreferredPickupTimeSlot { get; set; }

        //<xs:element xmlns:q2="http://schemas.datacontract.org/2004/07/SAPI.Entities.Enums.AWBGeneration" minOccurs="0" name="PrinterLableSize" type="q2:PrinterLableSize"/>

        public string ProductCode { get; set; }
        public string ProductFeature { get; set; }

        //<xs:element xmlns:q3="http://schemas.datacontract.org/2004/07/SAPI.Entities.Enums.AWBGeneration" minOccurs="0" name="ProductType" type="q3:ProductType"/>

        public bool RegisterPickup { get; set; }
        public double ReverseCharge { get; set; }
        public string SignatureName { get; set; }
        public string SignatureTitle { get; set; }
        public string SpecialInstruction { get; set; }
        public string SubProductCode { get; set; }
        public string SupplyOfIGST { get; set; }
        public string SupplyOfwoIGST { get; set; }
        public string TermsOfTrade { get; set; }
        public double TotalCashPaytoCustomer { get; set; }
        public double Total_IGST_Paid { get; set; }

        //public string itemImg" nillable="true" type="tns:ArrayOfItemImage"/>

        public ArrayOfItemDetails itemdtl { get; set; }
        public int noOfDCGiven { get; set; }
    }


    public class CommodityDetail
    {
        public string CommodityDetail1 { get; set; }

        public string CommodityDetail2 { get; set; }

        public string CommodityDetail3 { get; set; }
    }


    public class ArrayOfDimension
    {
        public Dimension Dimension { get; set; }
    }

    public class Dimension
    {
        public double Breadth { get; set; }

        public int Count { get; set; }

        public double Height { get; set; }

        public double Length { get; set; }
    }

    public class ArrayOfDynamicQCDetails
    {
        public DynamicQCDetails DynamicQCDetails { get; set; }
    }

    public class DynamicQCDetails
    {
        public string ExpectedAnswers { get; set; }

        public bool IsQCMandate { get; set; }

        public string ItemID { get; set; }

        public string QuestionDescription { get; set; }

        public string QuestionId { get; set; }

        public string QuestionValue { get; set; }

    }

    public class ArrayOfItemDetails
    {
        public double CGSTAmount { get; set; }
        public string CommodityCode { get; set; }
        public double Discount { get; set; }
        public string HSCode { get; set; }
        public double IGSTAmount { get; set; }
        public double IGSTRate { get; set; }
        public string Instruction { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string IsMEISS { get; set; }
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public double ItemValue { get; set; }
        public int Itemquantity { get; set; }
        public DateTime LicenseExpiryDate { get; set; }
        public string LicenseNumber { get; set; }
        public string ManufactureCountryCode { get; set; }
        public string ManufactureCountryName { get; set; }
        public double PerUnitRate { get; set; }
        public string PieceID { get; set; }
        public double PieceIGSTPercentage { get; set; }
        public string PlaceofSupply { get; set; }
        public string ProductDesc1 { get; set; }
        public string ProductDesc2 { get; set; }
        public string ReturnReason { get; set; }
        public double SGSTAmount { get; set; }
        public string SKUNumber { get; set; }
        public string SellerGSTNNumber { get; set; }
        public string SellerName { get; set; }
        public string SubProduct1 { get; set; }
        public string SubProduct2 { get; set; }
        public string SubProduct3 { get; set; }
        public string SubProduct4 { get; set; }
        public double TaxableAmount { get; set; }
        public double TotalValue { get; set; }
        public string Unit { get; set; }
        public double Weight { get; set; }
        public double cessAmount { get; set; }
        public string countryOfOrigin { get; set; }
        public string docType { get; set; }
        public DateTime eWaybillDate { get; set; }
        public long eWaybillNumber { get; set; }
        public int subSupplyType { get; set; }
        public string supplyType { get; set; }
    }

    public class Shipper
    {
        public string CustomerAddress1 { get; set; }
        public string CustomerAddress2 { get; set; }
        public string CustomerAddress3 { get; set; }
        public string CustomerAddressinfo { get; set; }
        public string CustomerBusinessPartyTypeCode { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerEmailID { get; set; }
        public string CustomerFiscalID { get; set; }
        public string CustomerFiscalIDType { get; set; }
        public string CustomerGSTNumber { get; set; }
        public string CustomerLatitude { get; set; }
        public string CustomerLongitude { get; set; }
        public string CustomerMaskedContactNumber { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPincode { get; set; }
        public string CustomerRegistrationNumber { get; set; }
        public string CustomerRegistrationNumberIssuerCountryCode { get; set; }
        public string CustomerRegistrationNumberTypeCode { get; set; }
        public string CustomerTelephone { get; set; }
        public bool IsToPayCustomer { get; set; }
        public string OriginArea { get; set; }
        public string Sender { get; set; }
        public string VendorCode { get; set; }
    }

    public class WayBillGenerationResponse
    {
        public string AWBNo { get; set; }
        //public string AWBPrintContent" nillable="true" type="xs:base64Binary"/>
        public double AvailableAmountForBooking { get; set; }
        public double AvailableBalance { get; set; }
        public string CCRCRDREF { get; set; }
        public string ClusterCode { get; set; }
        public string DestinationArea { get; set; }
        public string DestinationLocation { get; set; }
        public bool IsError { get; set; }
        public bool IsErrorInPU { get; set; }
        public DateTime ShipmentPickupDate { get; set; }
        public ArrayOfWayBillGenerationStatus Status { get; set; }
        public string TokenNumber { get; set; }
        public double TransactionAmount { get; set; }
    }

    public class ArrayOfWayBillGenerationStatus
    {
        public WayBillGenerationStatus WayBillGenerationStatus { get; set; }
    }

    public class WayBillGenerationStatus
    {
        public string StatusCode { get; set; }

        public string StatusInformation { get; set; }
    }

    public class UserProfile
    {
        public string Api_type { get; set; }        //S
        public string Area { get; set; }            //Null
        public string Customercode { get; set; }    //BLR91677
        public string IsAdmin { get; set; }         //Null 
        public string LicenceKey { get; set; }      //nsgolko3stjunoqhuhmupqmuqophqkqh
        public string LoginID { get; set; }         //BLR91677
        public string Password { get; set; }        //Nulll
        public string Version { get; set; }         //1.3
    }


}