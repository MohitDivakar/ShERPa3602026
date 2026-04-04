$(document).ready(function () {
    AddRemoveMenuactiveclass();
});


function AddRemoveMenuactiveclass() {

    var url = window.location.pathname;
    $(".nav-links li ul li").each(function () {
        var parentmenuId = this.parentElement.id;
        $(("#" + parentmenuId)).addClass("actives");
        (url.indexOf("HomePage") > 0)
        {

            $("#home").addClass("actives");
            $("#dashboard").removeClass("actives");

        }
        (url.indexOf("CustomerRltionShipMngDashBoard") > 0)
        {

            $("#dashboard").addClass("actives");
            $("#crm").removeClass("actives");
            $("#vendor").removeClass("actives");
            $("#report").removeClass("actives");
        }
        (url.indexOf("MMDashboard") > 0)
        {

            $("#dashboard").addClass("actives");

            $("#List").removeClass("actives");
            $("#dashboard").removeClass("actives");
            $("#Report").removeClass("actives");
            $("#PODepart").removeClass("actives");
            $("#RTV").removeClass("actives");
        }


        (url.indexOf("UtilityDashboard") > 0)


        {

            $("#dashboard").addClass("actives");
            $("#bdo").removeClass("actives");
            $("#store").removeClass("actives");
            $("#backoffice").removeClass("actives");
            $("#report").removeClass("actives");
            $("#other").removeClass("actives");
            $("#utility").removeClass("actives");


        }
        (url.indexOf("SDDashBoard") > 0)


        {

            $("#dashboard").addClass("actives");
            $("#sd").removeClass("actives");
        }
        (url.indexOf("FIDashBoard") > 0)
        {

            $("#dashboard").addClass("actives");
            $("#account").removeClass("actives");



        }

        (url.indexOf("UtilityModuleDashboard") > 0)
        {

            $("#dashboard").addClass("actives");
            $("#rrptlist").removeClass("actives");



        }
        (url.indexOf("ReportDashboard") > 0)
        {

            $("#dashboard").addClass("actives");
            $("#reportlist").removeClass("actives");
            $("#rreport").removeClass("actives");
            $("#price").removeClass("actives");


        }
        if (url.indexOf("LeadGeneration") > 0 || url.indexOf("frmLeadHistory") > 0 ||
            url.indexOf("CallLog") > 0 ||
            url.indexOf("ReAssignCall") > 0 ||
            url.indexOf("SendWAMessage") > 0 ||
            url.indexOf("frmCheckWarrantyaspx") > 0 ||
            url.indexOf("frmProdPrice") > 0

        ) {
            $("#vendor").removeClass("actives");
            $("#report").removeClass("actives");
            $("#crm").addClass("actives");
            $("#dashboard").removeClass("actives");
        }
        else if (url.indexOf("frmViewDealer") > 0 ||
            url.indexOf("frmViewVendorMaster") > 0 ||
            url.indexOf("frmVendorAprv") > 0 ||
            url.indexOf("frmDealer") > 0 ||
            url.indexOf("frmVendorMaster") > 0

        ) {

            $("#vendor").addClass("actives");
            $("#crm").removeClass("actives");
            $("#report").removeClass("actives");
            $("#dashboard").removeClass("actives");
        }
        else if (url.indexOf("rptCRMReports") > 0) {

            $("#crm").removeClass("actives");
            $("#vendor").removeClass("actives");
            $("#report").addClass("actives");
            $("#dashboard").removeClass("actives");
        }
        else if (url.indexOf("ViewMR") > 0 || url.indexOf("AprvMR") > 0 ||
            url.indexOf("ApprovedMR") > 0 ||
            url.indexOf("ViewMaterialInwardList") > 0 ||
            url.indexOf("ViewMaterialIssue") > 0 ||
            url.indexOf("CreateMR") > 0 ||
            url.indexOf("CreateIST") > 0 ||
            url.indexOf("CreateMaterialIssue") > 0 ||
            url.indexOf("CreateMaterialConsumption") > 0 ||
            url.indexOf("CreateSTODC") > 0 ||
            url.indexOf("CreateSTODC") > 0 ||
            url.indexOf("CreateSTOIN") > 0 ||
            url.indexOf("CreateMaterialReturn") > 0 ||
            url.indexOf("CreateMR") > 0 ||
            url.indexOf("ViewPR") > 0 ||
            url.indexOf("AprvPR") > 0 ||
            url.indexOf("CreateMR") > 0 ||
            url.indexOf("ViewPO") > 0 ||
            url.indexOf("AprvPO") > 0 ||
            url.indexOf("CreateMaterialAdjust") > 0 ||
            url.indexOf("frmApprovePO") > 0 ||

            url.indexOf("ViewMaterialIssue") > 0 ||
            url.indexOf("CreateMaterialInspection") > 0 ||
            url.indexOf("ViewPB") > 0 ||
            url.indexOf("MaterialInwardFromPo") > 0 ||
            url.indexOf("CreatePR") > 0 ||
            url.indexOf("CreatePO") > 0 ||
            url.indexOf("CreateMaterialSplit") > 0 ||
            url.indexOf("CreatePB") > 0 ||
            url.indexOf("frmTRNPending") > 0
        ) {
            $("#List").addClass("actives");
            $("#dashboard").removeClass("actives");
            $("#Report").removeClass("actives");
            $("#PODepart").removeClass("actives");
            $("#RTV").removeClass("actives");
        }
        else if (url.indexOf("frmRTVData") > 0) {
            $("#RTV").addClass("actives");
            $("#dashboard").removeClass("actives");
            $("#List").removeClass("actives");
            $("#Report").removeClass("actives");
            $("#PODepart").removeClass("actives");
        }
        else if (url.indexOf("rptPOApporval") > 0) {
            $("#RTV").removeClass("actives");
            $("#dashboard").removeClass("actives");
            $("#List").removeClass("actives");
            $("#Report").addClass("actives");
            $("#PODepart").removeClass("actives");
        }
        else if (url.indexOf("frmCreatePOBulk") > 0) {
            debugger;
            $("#PODepart").addClass("actives");
            $("#RTV").removeClass("actives");
            $("#dashboard").removeClass("actives");
            $("#List").removeClass("actives");
            $("#Report").removeClass("actives");
        }
        else if (url.indexOf("VendorVisitEntry") > 0 || url.indexOf("ProductUnListedEntry") > 0 ||
            url.indexOf("ProductReturnBDOToVendorDetail") > 0 ||
            url.indexOf("ProductOtherDeviceEntry") > 0 ||
            url.indexOf("BikerProductStatuswiseDetail") > 0 ||


            url.indexOf("ProductBlanccoQcApproveDetail") > 0 ||

            url.indexOf("ProductEntry") > 0

        ) {
            $("#bdo").addClass("actives");
            $("#store").removeClass("actives");
            $("#backoffice").removeClass("actives");
            $("#report").removeClass("actives");
            $("#other").removeClass("actives");

            $("#dashboard").removeClass("actives");
            $("#utility").removeClass("actives");
        }
        else if (url.indexOf("ProductInwardDetail") > 0 ||
            url.indexOf("ProductReturnDetail") > 0 ||
            url.indexOf("PoandPRFromInward") > 0 ||
            url.indexOf("JangadKROListing") > 0 ||
            url.indexOf("JangadKROReturnListingReport") > 0
        ) {
            $("#store").addClass("actives");
            $("#backoffice").removeClass("actives");
            $("#report").removeClass("actives");
            $("#other").removeClass("actives");

            $("#dashboard").removeClass("actives");
            $("#utility").removeClass("actives");
        }
        else if (url.indexOf("PriceApproveDetail") > 0 ||
            url.indexOf("ReadyForListingDetail") > 0 ||
            url.indexOf("frmBDOAssign") > 0 ||
            url.indexOf("frmASMAssign") > 0 ||
            url.indexOf("PartnerApprovedQtyListingDetail") > 0 ||

            url.indexOf("frmViewProdSpec") > 0 ||
            url.indexOf("ReservedExcelUpload") > 0 ||
            url.indexOf("ProductQcApproveDetail") > 0 ||
            url.indexOf("frmViewMappedItem") > 0 ||

            url.indexOf("frmItemMapping") > 0 ||
            url.indexOf("frmUnMappedItemMapping") > 0 ||
            //03-02-2023 swetal
            url.indexOf("AddNewList") > 0 ||
            url.indexOf("CreateList") > 0 ||
            //06-02-2023 swetal
            url.indexOf("AddNewModel") > 0 ||
            url.indexOf("CreateModelList") > 0 ||


            /*  url.indexOf("frmViewDealer") > 0 ||*/
            url.indexOf("ProductReportDetail") > 0 ||
            url.indexOf("frmCalculatedWebsiteAvgAmt") > 0 ||
            url.indexOf("frmCalculatedAllPlatFormAvgAmt") > 0
            ||
            url.indexOf("frmAddProdSpec") > 0
            ||
            url.indexOf("frmPendingPayment") > 0 ||
            url.indexOf("FollowupCount") > 0 ||
            url.indexOf("frmListingAssign") > 0 ||
            url.indexOf("MobexSellerListingsTracking") > 0 ||
            url.indexOf("MobileBrand") > 0 ||
            url.indexOf("DelearToKRO") > 0 ||
            url.indexOf("frmListableDataUpload") > 0 ||
            url.indexOf("MosecUserDailyActivity") > 0
        ) {


            $("#backoffice").addClass("actives");
            $("#bdo").removeClass("actives");
            $("#store").removeClass("actives");
            $("#report").removeClass("actives");
            $("#other").removeClass("actives");
            $("#dashboard").removeClass("actives");
            $("#utility").removeClass("actives");
        }


        else if (url.indexOf("frmUpdateComplianceReport") > 0 || url.indexOf("ProductStatuswiseDetail") > 0 ||
            url.indexOf("frmItemPriceMaster") > 0 ||
            url.indexOf("frmPopup") > 0 ||
            url.indexOf("frmPartPrice") > 0 ||
            url.indexOf("frmUpdateSafetyReport") > 0 ||
            url.indexOf("frmLabelPrint") > 0 ||
            url.indexOf("ProductExportinExcelDetail") > 0 ||
            url.indexOf("ProductASMStatuswiseDetail") > 0 ||
            url.indexOf("ProductEveryStatusDateTimeDetail") > 0 ||
            url.indexOf("MakeModelSuggestPriceHistoryReport") > 0 ||
            url.indexOf("MakeModelSuggestPriceHistoryReportItemCode") > 0 ||
            url.indexOf("ProductIntervalListedUnlistedDetail") > 0 ||
            url.indexOf("ProductDateWiseListedDetail") > 0 ||
            url.indexOf("ListingTransactionDetail") > 0

            ||
            url.indexOf("VendorListedActivityDetail") > 0
            ||
            url.indexOf("MakeModelPOAvrageAmount") > 0 ||
            url.indexOf("RejectToAcceptEx") > 0 ||
            url.indexOf("RelistDetail") > 0 ||
            url.indexOf("AutoUnlistedDetail") > 0 ||
            url.indexOf("LockPriceHistory") > 0
        ) {
            $("#report").addClass("actives");
            $("#bdo").removeClass("actives");
            $("#store").removeClass("actives");
            $("#backoffice").removeClass("actives");
            $("#rrptlist").addClass("actives");

            $("#other").removeClass("actives");

            $("#dashboard").removeClass("actives");
            $("#utility").removeClass("actives");
        }

        else if (url.indexOf("frmItemPriceMaster") > 0 ||
            url.indexOf("frmPopup") > 0 ||

            url.indexOf("frmPartPrice") > 0 ||
            url.indexOf("frmUpdateSafetyReport") > 0 ||
            url.indexOf("frmLabelPrint") > 0


        ) {
            $("#other").addClass("actives");
            $("#dashboard").removeClass("actives");
            $("#bdo").removeClass("actives");
            $("#store").removeClass("actives");
            $("#backoffice").removeClass("actives");
            $("#report").removeClass("actives");
            $("#utility").removeClass("actives");

        }
        else if (url.indexOf("frmUploadMobexListing") > 0) {
            $("#other").removeClass("actives");
            $("#dashboard").removeClass("actives");
            $("#bdo").removeClass("actives");
            $("#store").removeClass("actives");
            $("#backoffice").removeClass("actives");
            $("#report").removeClass("actives");
            $("#utility").addClass("actives");
        }
        else if (url.indexOf("frmViewSO") > 0 ||
            url.indexOf("frmViewSODeviation") > 0 ||
            url.indexOf("frmSODeviationAprv") > 0 ||
            url.indexOf("frmSOSIUpdate") > 0 ||
            url.indexOf("frmSOItemAssign") > 0 ||
            url.indexOf("frmSO") > 0 ||
            url.indexOf("BulkSoCreation") > 0 ||
            url.indexOf("BulkWebsiteSoCreation") > 0 ||
            url.indexOf("BulkCreatedSoNameAddUpdate") > 0 ||
            url.indexOf("frmCustomerList") > 0 ||
            url.indexOf("frmCustomerAdd") > 0
        ) {


            $("#sd").addClass("actives");

            $("#dashboard").removeClass("actives");


        }

        else if (url.indexOf("frmBankPayment") > 0 ||
            url.indexOf("frmDealerOutStanding") > 0 ||
            url.indexOf("frmFIViewVendorMaster") > 0 ||
            url.indexOf("frmExportPaymentDetails") > 0 ||
            url.indexOf("frmActualBankPayment") > 0 ||
            url.indexOf("frmPBReceive") > 0 ||
            url.indexOf("frmAPRVREJPB") > 0 ||
            url.indexOf("frmFIVendMaster") > 0 ||
            url.indexOf("frmLedger") > 0 ||
            url.indexOf("frmCMSFormat") > 0 ||
            url.indexOf("frmBankDetail") > 0 ||
            url.indexOf("frmClosing") > 0 ||
            url.indexOf("rptIMEILedger") > 0
        ) {
            $("#account").addClass("actives");
            $("#dashboard").removeClass("actives");
        }

        else if (url.indexOf("frmUpdateComplianceReport") > 0 ||
            url.indexOf("frmItemPriceMaster") > 0 ||
            url.indexOf("frmPopup") > 0 ||
            url.indexOf("frmPartPrice") > 0 ||
            url.indexOf("frmUpdateSafetyReport") > 0 ||
            url.indexOf("frmLabelPrint") > 0
        ) {


            $("#dashboard").removeClass("actives");
            $("#rrptlist").addClass("actives");
            $("#rrutility").removeClass("actives");
        }
        else if (url.indexOf("frmUploadRateCard") > 0
        ) {
            $("#dashboard").removeClass("actives");
            $("#rrptlist").removeClass("actives");
            $("#rrutility").addClass("actives");
        }
        else if (url.indexOf("frmViewCromaRateCard") > 0
        ) {
            $("#dashboard").removeClass("actives");
            $("#rrptlist").removeClass("actives");
            $("#rrutility").addClass("actives");
        }
        else if

            (url.indexOf("rptOpenMR") > 0 ||
            url.indexOf("rptMRStatus") > 0 ||
            url.indexOf("MRToPBDetail") > 0 ||
            url.indexOf("rptGRNInvoice") > 0 ||
            url.indexOf("rptSafetyReport") > 0 ||
            url.indexOf("rptSOAging") > 0 ||
            url.indexOf("rptComplianceReport") > 0 ||
            url.indexOf("rptDCSummary") > 0 ||
            url.indexOf("rptBikerVisitReport") > 0 ||
            url.indexOf("rptVendorStatus") > 0 ||
            url.indexOf("rptPORegister") > 0 ||
            url.indexOf("rptOCRScanReport") > 0 ||
            url.indexOf("rptONSPO") > 0
            ||
            url.indexOf("rptApprovedPO") > 0
            ||
            url.indexOf("rptMRReport") > 0 ||
            url.indexOf("rptBikerRejectionReport") > 0 ||
            url.indexOf("rptPBRegister") > 0 ||
            url.indexOf("rptBikerVisitFeedBackReport") > 0 ||

            url.indexOf("rptSMTree") > 0 ||
            url.indexOf("AginBucket") > 0 ||
            url.indexOf("frmMSLReport") > 0 ||
            url.indexOf("rptMaterialMovement") > 0 ||
            url.indexOf("rptJobSheetItemcode") > 0


        ) {
            $("#Account").removeClass("actives");

            $("#dashboard").removeClass("actives");
            $("#reportlist").addClass("actives");
            $("#rreport").removeClass("actives");
            $("#price").removeClass("actives");
            $("#FBA").removeClass("actives");
            $("#Sales").removeClass("actives");



        }
        else if (url.indexOf("rptCRM") > 0 ||

            url.indexOf("rptAllCallData") > 0 ||
            url.indexOf("rptCRMGrab") > 0 ||
            url.indexOf("WAMessageReport") > 0 ||
            url.indexOf("rptPhyDocVar") > 0 ||
            url.indexOf("rptReturnHistorySummary") > 0 ||
            url.indexOf("rptReturnHistory") > 0 ||
            url.indexOf("BDOMonthlyPerformaceReport") > 0 ||
            url.indexOf("rptStockFTD") > 0 ||
            url.indexOf("rptClaimData") > 0 ||
            url.indexOf("rptListableData") > 0 ||
            url.indexOf("rptSOData") > 0 ||
            url.indexOf("rptPurchaseVsSale") > 0 ||
            url.indexOf("rptClaimPLData") > 0 ||
            url.indexOf("rptLogistic") > 0 ||
            url.indexOf("rptBrandwiselisting") > 0 ||
            url.indexOf("rptGradewiselisting") > 0 ||
            url.indexOf("SalesInvoiceRegister") > 0 ||
            url.indexOf("SkuUpload") > 0 ||
            url.indexOf("rptSORegister") > 0 ||
            url.indexOf("rptExtendedWarranty") > 0
        ) {
            $("#Account").removeClass("actives");
            $("#dashboard").removeClass("actives");
            $("#reportlist").removeClass("actives");
            $("#rreport").addClass("actives");
            $("#price").removeClass("actives");
            $("#FBA").removeClass("actives");
            $("#Sales").removeClass("actives");
        }
        else if (url.indexOf("rptKeepaPrice") > 0 ||
            url.indexOf("rptSalesRegister") > 0 ||

            url.indexOf("rptVendorLedgerReport") > 0


        ) {
            $("#dashboard").removeClass("actives");
            $("#reportlist").removeClass("actives");
            $("#rreport").removeClass("actives");
            $("#FBA").removeClass("actives");
            $("#Account").removeClass("actives");
            $("#Sales").removeClass("actives");
            $("#price").addClass("actives");
        }
        else if (url.indexOf("rptFBAReport") > 0
        ) {
            $("#dashboard").removeClass("actives");
            $("#reportlist").removeClass("actives");
            $("#rreport").removeClass("actives");
            $("#price").removeClass("actives");
            $("#Account").removeClass("actives");
            $("#Sales").removeClass("actives");
            $("#FBA").addClass("actives");
        }
        else if (url.indexOf("rptTRNJobAgeing") > 0
        ) {
            $("#dashboard").removeClass("actives");
            $("#reportlist").removeClass("actives");
            $("#rreport").removeClass("actives");
            $("#price").removeClass("actives");
            $("#FBA").removeClass("actives");
            $("#Sales").removeClass("actives");
            $("#Account").addClass("actives");
        }
        else if (url.indexOf("rptSalesDeviation") > 0
        ) {
            $("#dashboard").removeClass("actives");
            $("#reportlist").removeClass("actives");
            $("#rreport").removeClass("actives");
            $("#price").removeClass("actives");
            $("#FBA").removeClass("actives");
            $("#Account").removeClass("actives");
            $("#Sales").addClass("actives");
        }

        else {
            $("#crm").removeClass("actives");
            $("#vendor").removeClass("actives");
            $("#report").removeClass("actives");
            $("#List").removeClass("actives");
            $("#other").removeClass("actives");
            $("#bdo").removeClass("actives");
            $("#store").removeClass("actives");
            $("#backoffice").removeClass("actives");

            $("#fi").removeClass("actives");
            $("#reportlist").removeClass("actives");
            $("#rreport").removeClass("actives");
            $("#price").removeClass("actives");
            $("#ropit").removeClass("actives");
            $("#sd").removeClass("actives");
        }
    })
}