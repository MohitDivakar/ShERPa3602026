using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShERPa360net.Class
{
    public class NewBluedartAPIClass
    {
    }


    public class JWTTokenClass
    {
        public string JWTToken { get; set; }
    }

    public class PinCodeCheck
    {
        public string pinCode { get; set; }

        public ProfileClass profile { get; set; }

    }

    public class ProfileClass
    {
        public string LoginID { get; set; }
        public string LicenceKey { get; set; }
        public string Api_type { get; set; }
    }

    public class BlueDartHoliday
    {
        public string Description { get; set; }
        public DateTime HolidayDate { get; set; }
    }

    public class GetServicesforPincodeResult
    {
        public string AdditionalTTDays { get; set; }
        public int AirValueLimit { get; set; }
        public int AirValueLimiteTailPrePaid { get; set; }
        public int ApexCODIntraStateValLimit { get; set; }
        public string ApexDODServiceInbound { get; set; }
        public string ApexDODServiceOutbound { get; set; }
        public string ApexEDLAddDays { get; set; }
        public string ApexEDLDist { get; set; }
        public string ApexETailTDD10Inbound { get; set; }
        public string ApexETailTDD10Outbound { get; set; }
        public string ApexETailTDD12Inbound { get; set; }
        public string ApexETailTDD12Outbound { get; set; }
        public string ApexEconomyInbound { get; set; }
        public string ApexEconomyOutbound { get; set; }
        public string ApexEtailRVP { get; set; }
        public string ApexFODServiceInbound { get; set; }
        public string ApexFODServiceOutbound { get; set; }
        public string ApexInbound { get; set; }
        public string ApexOutbound { get; set; }
        public int ApexPrepaidIntraStateValLimit { get; set; }
        public string ApexTDD { get; set; }
        public string ApexZone { get; set; }
        public string AreaCode { get; set; }
        public string BharatDartCODInbound { get; set; }
        public string BharatDartCODOutbound { get; set; }
        public string BharatDartPrePaidInbound { get; set; }
        public string BharatDartPrePaidOutbound { get; set; }
        public string BharatDartRVP { get; set; }
        public List<BlueDartHoliday> BlueDartHolidays { get; set; }
        public string CCServiceInbound { get; set; }
        public string CCServiceOutbound { get; set; }
        public string CityDescription { get; set; }
        public string DPCODServiceInbound { get; set; }
        public string DPCODServiceOutbound { get; set; }
        public int DPDutsValueLimit { get; set; }
        public string DPNewZone { get; set; }
        public string DPTDD10Inbound { get; set; }
        public string DPTDD10Outbound { get; set; }
        public string DPTDD12Inbound { get; set; }
        public string DPTDD12Outbound { get; set; }
        public string DPZone { get; set; }
        public string DSPServiceInbound { get; set; }
        public string DSPServiceOutbound { get; set; }
        public string DartPlusRVP { get; set; }
        public string DomesticPriorityInbound { get; set; }
        public string DomesticPriorityOutbound { get; set; }
        public string DomesticPriorityTDD { get; set; }
        public string ECOMZone { get; set; }
        public string EDLAddDays { get; set; }
        public string EDLDist { get; set; }
        public string EDLProduct { get; set; }
        public string Embargo { get; set; }
        public string ErrorMessage { get; set; }
        public string ExchangeService { get; set; }
        public string GroundDODServiceInbound { get; set; }
        public string GroundDODServiceOutbound { get; set; }
        public string GroundEDLAddDays { get; set; }
        public string GroundEDLDist { get; set; }
        public string GroundFODServiceInbound { get; set; }
        public string GroundFODServiceOutbound { get; set; }
        public string GroundInbound { get; set; }
        public string GroundOutbound { get; set; }
        public string GroundRVP { get; set; }
        public int GroundValueLimit { get; set; }
        public int GroundValueLimiteTailPrePaid { get; set; }
        public string GroundZone { get; set; }
        public bool IsError { get; set; }
        public string PinCode { get; set; }
        public string PincodeDescription { get; set; }
        public string RVPEmbargo { get; set; }
        public string Region { get; set; }
        public string RemoteApex { get; set; }
        public string RemoteGround { get; set; }
        public string ServiceCenterCode { get; set; }
        public string State { get; set; }
        public string TCLDriveWayZone { get; set; }
        public string TCLServiceInbound { get; set; }
        public string TCLServiceOutbound { get; set; }
        public string eTailCODAirInbound { get; set; }
        public string eTailCODAirOutbound { get; set; }
        public string eTailCODGroundInbound { get; set; }
        public string eTailCODGroundOutbound { get; set; }
        public string eTailExpressCODAirInbound { get; set; }
        public string eTailExpressCODAirOutbound { get; set; }
        public string eTailExpressPrePaidAirInbound { get; set; }
        public string eTailExpressPrePaidAirOutound { get; set; }
        public string eTailPrePaidAirInbound { get; set; }
        public string eTailPrePaidAirOutound { get; set; }
        public string eTailPrePaidGroundInbound { get; set; }
        public string eTailPrePaidGroundOutbound { get; set; }
    }

    public class PinCodeResponse
    {
        public GetServicesforPincodeResult GetServicesforPincodeResult { get; set; }
    }

    public class Commodity
    {
        public string CommodityDetail1 { get; set; }
        public string CommodityDetail2 { get; set; }
        public string CommodityDetail3 { get; set; }
    }


    public class ConsigneeNew
    {
        public string AvailableDays { get; set; }
        public string AvailableTiming { get; set; }
        public string ConsigneeAddress1 { get; set; }
        public string ConsigneeAddress2 { get; set; }
        public string ConsigneeAddress3 { get; set; }
        public string ConsigneeAddressType { get; set; }
        public string ConsigneeAddressinfo { get; set; }
        public string ConsigneeAttention { get; set; }
        public string ConsigneeEmailID { get; set; }
        public string ConsigneeFullAddress { get; set; }
        public string ConsigneeGSTNumber { get; set; }
        public string ConsigneeLatitude { get; set; }
        public string ConsigneeLongitude { get; set; }
        public string ConsigneeMaskedContactNumber { get; set; }
        public string ConsigneeMobile { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsigneePincode { get; set; }
        public string ConsigneeTelephone { get; set; }
    }


    public class DimensionNew
    {
        public double Breadth { get; set; }
        public int Count { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }
    }


    public class Itemdtl
    {
        public double CGSTAmount { get; set; }
        public string HSCode { get; set; }
        public double IGSTAmount { get; set; }
        public double IGSTRate { get; set; }
        public string Instruction { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public double ItemValue { get; set; }
        public int Itemquantity { get; set; }
        public string PlaceofSupply { get; set; }
        public string ProductDesc1 { get; set; }
        public string ProductDesc2 { get; set; }
        public string ReturnReason { get; set; }
        public double SGSTAmount { get; set; }
        public string SKUNumber { get; set; }
        public string SellerGSTNNumber { get; set; }
        public string SellerName { get; set; }
        public int TaxableAmount { get; set; }
        public double TotalValue { get; set; }
        public double cessAmount { get; set; }
        public string countryOfOrigin { get; set; }
        public string docType { get; set; }
        public int subSupplyType { get; set; }
        public string supplyType { get; set; }
    }


    public class Request
    {
        public ConsigneeNew Consignee { get; set; }
        public Returnadds Returnadds { get; set; }
        public ServicesNew Services { get; set; }
        public ShipperNew Shipper { get; set; }
    }


    public class Returnadds
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


    public class WayBillGenClass
    {
        public Request Request { get; set; }
        public ProfileClass Profile { get; set; }
    }


    public class ServicesNew
    {
        public string AWBNo { get; set; }
        public string ActualWeight { get; set; }
        public double CollectableAmount { get; set; }
        public Commodity Commodity { get; set; }
        public string CreditReferenceNo { get; set; }
        public string CreditReferenceNo2 { get; set; }
        public string CreditReferenceNo3 { get; set; }
        public string CurrencyCode { get; set; }
        public double DeclaredValue { get; set; }
        public string DeliveryTimeSlot { get; set; }
        public List<DimensionNew> Dimensions { get; set; }
        public string FavouringName { get; set; }
        public string ForwardAWBNo { get; set; }
        public string ForwardLogisticCompName { get; set; }
        public string InsurancePaidBy { get; set; }
        public string InvoiceNo { get; set; }
        public string IsChequeDD { get; set; }
        public bool IsDedicatedDeliveryNetwork { get; set; }
        public bool IsForcePickup { get; set; }
        public bool IsPartialPickup { get; set; }
        public bool IsReversePickup { get; set; }
        public int ItemCount { get; set; }
        public string OTPBasedDelivery { get; set; }
        public string OTPCode { get; set; }
        public string Officecutofftime { get; set; }
        public bool PDFOutputNotRequired { get; set; }
        public string PackType { get; set; }
        public string ParcelShopCode { get; set; }
        public string PayableAt { get; set; }
        public DateTime PickupDate { get; set; }
        public string PickupMode { get; set; }
        public string PickupTime { get; set; }
        public string PickupType { get; set; }
        public string PieceCount { get; set; }
        public string PreferredPickupTimeSlot { get; set; }
        public string ProductCode { get; set; }
        public string ProductFeature { get; set; }
        public int ProductType { get; set; }
        public bool RegisterPickup { get; set; }
        public string SpecialInstruction { get; set; }
        public string SubProductCode { get; set; }
        public int TotalCashPaytoCustomer { get; set; }
        public List<Itemdtl> itemdtl { get; set; }
        public int noOfDCGiven { get; set; }
    }


    public class ShipperNew
    {
        public string CustomerAddress1 { get; set; }
        public string CustomerAddress2 { get; set; }
        public string CustomerAddress3 { get; set; }
        public string CustomerAddressinfo { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerEmailID { get; set; }
        public string CustomerGSTNumber { get; set; }
        public string CustomerLatitude { get; set; }
        public string CustomerLongitude { get; set; }
        public string CustomerMaskedContactNumber { get; set; }
        public string CustomerMobile { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPincode { get; set; }
        public string CustomerTelephone { get; set; }
        public bool IsToPayCustomer { get; set; }
        public string OriginArea { get; set; }
        public string Sender { get; set; }
        public string VendorCode { get; set; }
    }


    public class GenerateWayBillResult
    {
        public string AWBNo { get; set; }
        public byte[] AWBPrintContent { get; set; }
        public int AvailableAmountForBooking { get; set; }
        public int AvailableBalance { get; set; }
        public string CCRCRDREF { get; set; }
        public string ClusterCode { get; set; }
        public string DestinationArea { get; set; }
        public string DestinationLocation { get; set; }
        public object InvoicePrintContent { get; set; }
        public bool IsError { get; set; }
        public bool IsErrorInPU { get; set; }
        public DateTime ShipmentPickupDate { get; set; }
        public List<Status> Status { get; set; }
        public string TokenNumber { get; set; }
        public int TransactionAmount { get; set; }
    }

    public class WayBillResponse
    {
        public GenerateWayBillResult GenerateWayBillResult { get; set; }
    }

    public class Status
    {
        public string StatusCode { get; set; }
        public string StatusInformation { get; set; }
    }


}