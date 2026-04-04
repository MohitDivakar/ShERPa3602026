$(document).ready(function () {
    var pageURL = $(location).attr("href").toString();
    $("#fuSerialNo").change(function () {
        $("#txtIMEINo").val("");
        $("#btnUploadScan").click();
        //alert('changed!');
    });

    onlynumber();

    if (pageURL.indexOf("/ProductEntry.aspx") > 0 || pageURL.indexOf("/BikerProductStatuswiseDetail.aspx") > 0 || pageURL.indexOf("/frmAddProdSpec.aspx") > 0 || pageURL.indexOf("/frmViewProdSpec.aspx") > 0
        || pageURL.indexOf("/ProductASMStatuswiseDetail.aspx") > 0) {
        $(".ddlMake").select2();
        $(".ddlModel").select2();
        $(".ddlRom").select2();
        $(".ddlRam").select2();
        $(".ddlColor").select2();
        if (pageURL.indexOf("/ProductEntry.aspx") > 0) {
            $(".ddlVendor").select2();
            $("#fuImage").addClass("file-simple");
        }
        if (pageURL.indexOf("/BikerProductStatuswiseDetail.aspx") > 0) {
            //$(".ContentPlaceHolder1_ddlVendor").select2();
            $(".ddlVendor").select2();
            $(".ddlModel").select2();
            $("#ContentPlaceHolder1_gvProduct").DataTable({
                dom: 'Bfrtip',
                buttons: [
                    {
                        extend: 'collection',
                        text: 'Export',
                        buttons: [
                            'copy',
                            'excel',
                            'csv',
                            'pdf',
                            'print'
                        ]
                    }
                ]
            });
        }



        if (pageURL.indexOf("/ProductASMStatuswiseDetail.aspx") > 0) {
            $(".ddlModel").select2();
            $(".ddlVendor").select2();
            $("#ContentPlaceHolder1_gvProduct").DataTable({
                dom: 'Bfrtip',

                buttons: [
                    {
                        extend: 'collection',
                        text: 'Export',
                        buttons: [
                            'copy',
                            'excel',
                            'csv',
                            'pdf',
                            'print'
                        ]
                    }
                ]
            });
        }
        if (pageURL.indexOf("/frmViewProdSpec.aspx") > 0) {
            $(".ddlItemType").select2();
            $(".ddlItemGroup").select2();
            $(".ddlItemSubGroup").select2();

        }

        if (pageURL.indexOf("/frmAddProdSpec.aspx") > 0) {
            $(".ddlItemType").select2();
            $(".ddlItemGroup").select2();
            $(".ddlItemSubGroup").select2();
            //$("$ddlMake").addEventListener("blur", GetEachProdSpecPrimaryDetail());
        }
    }

    else if (pageURL.indexOf("/frmMSLReport.aspx") > 0) {
        $("#ContentPlaceHolder1_gvMslReport").DataTable({
            dom: 'Bfrtip',
            "pageLength": 50,
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/ProductUnListedEntry.aspx") > 0 || pageURL.indexOf("/ProductQcApproveDetail.aspx") > 0
        || pageURL.indexOf("/PriceApproveDetail.aspx") > 0 || pageURL.indexOf("/ReadyForListingDetail.aspx") > 0
        || pageURL.indexOf("/BikerProductStatuswiseDetail.aspx") > 0
        || pageURL.indexOf("/VendorVisitEntry.aspx") > 0
        || pageURL.indexOf("/ProductASMStatuswiseDetail.aspx") > 0
        || pageURL.indexOf("/ProductInwardDetail.aspx") > 0
        || pageURL.indexOf("/rptPOApporval.aspx") > 0
    ) {
        $(".ddlVendor").select2();
    }
    else if (pageURL.indexOf("/frmMSLReport.aspx") > 0) {
        $("#ContentPlaceHolder1_gvMslReport").DataTable({
            dom: 'Bfrtip',
            "pageLength": 50,
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/ProductIntervalListedUnlistedDetail.aspx") > 0
    ) {
        $("#txtFromTime").timepicker({
            showInputs: false,
            showMeridian: false
            //minuteStep: 15,
        })
        $("#txtToTime").timepicker({
            showInputs: false,
            showMeridian: false
            //minuteStep: 15,
        })
    }

    else if (pageURL.indexOf("/ProductStatuswiseDetail.aspx") > 0) {
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/rptBikerVisitReport.aspx") > 0) {
        $("#ContentPlaceHolder1_gvList").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/rptBikerRejectionReport.aspx") > 0) {
        $("#ddlCity").select2();
        $("#ContentPlaceHolder1_gvList").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/rptBikerVisitFeedBackReport.aspx") > 0) {
        $("#ddlCity").select2();
    }

    else if (pageURL.indexOf("/rptSOAging.aspx") > 0) {
        $("#ContentPlaceHolder1_gvList").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/frmBikerVendorReg.aspx") > 0) {
        $("#ContentPlaceHolder1_ddlDealer").select2();
    }
    else if (pageURL.indexOf("/frmDealer.aspx") > 0) {
        $("#ContentPlaceHolder1_ddlArea").select2();
    }
    else if (pageURL.indexOf("/ProductInwardDetail.aspx") > 0) {
        $(".ddlVendor").select2();
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }
    else if (pageURL.indexOf("/ProductBlanccoQcApproveDetail.aspx") > 0) {
        $(".ddlVendor").select2();
        if ($("#ContentPlaceHolder1_gvProduct tr").length > 2) {
            $("#ContentPlaceHolder1_gvProduct").DataTable({
                dom: 'Bfrtip',
                buttons: [
                    {
                        extend: 'collection',
                        text: 'Export',
                        buttons: [
                            'copy',
                            'excel',
                            'csv',
                            'pdf',
                            'print'
                        ]
                    }
                ]
            });
        }
    }

    else if ((pageURL.indexOf("/frmViewVendorMaster.aspx") > 0) || (pageURL.indexOf("/frmFIViewVendorMaster.aspx") > 0)) {
        $("#ContentPlaceHolder1_gvList").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/frmBDOAssign.aspx") > 0) {
        $("#ContentPlaceHolder1_ddlBDO").select2();
    }

    else if (pageURL.indexOf("/frmVendorMaster.aspx") > 0) {
        $("#ContentPlaceHolder1_ddlDealer").select2();
    }

    else if (pageURL.indexOf("/ProductReturnDetail.aspx") > 0) {
        $("#ContentPlaceHolder1_ddlVendor").select2();
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/ProductReportDetail.aspx") > 0) {
        $(".ddlMake").select2();
        $(".ddlModel").select2();
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/CRM/frmProdPrice.aspx") > 0) {

        $(".ddlMake").select2();
        $(".ddlModel").select2();
        $(".ddlROM").select2();
        $(".ddlRAM").select2();
        $(".ddlColor").select2();
        $(".ddlGrade").select2();
    }
    else if (pageURL.indexOf("UTILITY/frmItemMapping.aspx") > 0) {

        $(".ddlMake").select2();
        $(".ddlModel").select2();
        $(".ddlROM").select2();
        $(".ddlRAM").select2();
        $(".ddlColor").select2();
        $(".ddlGrade").select2();
    }
    else if (pageURL.indexOf("UTILITY/frmItemPriceMaster.aspx") > 0) {

        $(".ddlMake").select2();
        $(".ddlModel").select2();
        $(".ddlRom").select2();
        $(".ddlRam").select2();
        $(".ddlColor").select2();
        $(".ddlGrade").select2();
    }
    else if (pageURL.indexOf("UTILITY/frmCalculatedWebsiteAvgAmt.aspx") > 0) {
        $(".ddlMake").select2();
        $("#ContentPlaceHolder1_gvList").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("UTILITY/frmCalculatedAllPlatFormAvgAmt.aspx") > 0) {
        $("#ContentPlaceHolder1_gvList").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("UTILITY/ProductEveryStatusDateTimeDetail.aspx") > 0) {
        $("#gvProduct").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("UTILITY/frmUnMappedItemMapping.aspx") > 0) {
        $("#ContentPlaceHolder1_gvList").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("MakeModelSuggestPriceHistoryReport.aspx") > 0) {
        $(".ddlMake").select2();
        $(".ddlModel").select2();
    }

    else if (pageURL.indexOf("MakeModelSuggestPriceHistoryReportItemCode.aspx") > 0) {
        $(".ddlMake").select2();
        $(".ddlModel").select2();
    }
    else if (pageURL.indexOf("ProductReturnBDOToVendorDetail.aspx") > 0) {
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
        $(".ddlVendor").select2();
    }
    else if (pageURL.indexOf("rptVendorLedgerReport.aspx") > 0) {
        $(".ddlVendor").select2();
    }
    else if (pageURL.indexOf("PoandPRFromInward.aspx") > 0) {
        $(".ddlVendor").select2();
    }

    else if (pageURL.indexOf("ProductOtherDeviceEntry.aspx") > 0) {
        $(".ddlMake").select2();
        $(".ddlMake").select2();
        $(".ddlMake").select2();
        $(".ddlModel").select2();
        $(".ddlRom").select2();
        $(".ddlRam").select2();
        $(".ddlColor").select2();
        $(".ddlVendor").select2();
        $(".ddlItemGroup").select2();
        $(".ddlItemSubGroup").select2();
    }

    else if (pageURL.indexOf("BulkSoCreation.aspx") > 0) {
        $("#gvbulksoProduct").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("BulkCreatedSoNameAddUpdate.aspx") > 0) {
        $("#gvbulksoAddressupdate").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }



    else if (pageURL.indexOf("MakeModelPOAvrageAmount.aspx") > 0) {
        $(".ddlMake").select2();
        $(".ddlMake").select2();
        $("#gvMakeModelList").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }
    else if (pageURL.indexOf("MakeModelPOAvrageAmount.aspx") > 0) {
        $(".ddlMake").select2();
        $(".ddlMake").select2();
        $("#gvMakeModelList").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }
    else if (pageURL.indexOf("frmBuyBackCromaPickup.aspx") > 0) {
        $("#ContentPlaceHolder1_ddlStore").select2();
    }
    else if (pageURL.indexOf("frmLedger.aspx") > 0) {
        debugger
        $("#ContentPlaceHolder1_ddlVendor").select2();
        //$("#ContentPlaceHolder1_gvList").DataTable({
        //    dom: 'Bfrtip',
        //    buttons: [
        //        {
        //            extend: 'collection',
        //            text: 'Export',
        //            buttons: [
        //                'copy',
        //                'excel',
        //                'csv',
        //                'pdf',
        //                'print'
        //            ]
        //        }
        //    ]
        //});
    }
    else if (pageURL.indexOf("rptPOApporval.aspx") > 0) {
        debugger
        $("#ContentPlaceHolder1_ddlVendor").select2();
        //$("#ContentPlaceHolder1_gvList").DataTable({
        //    dom: 'Bfrtip',
        //    buttons: [
        //        {
        //            extend: 'collection',
        //            text: 'Export',
        //            buttons: [
        //                'copy',
        //                'excel',
        //                'csv',
        //                'pdf',
        //                'print'
        //            ]
        //        }
        //    ]
        //});
    }

    else if (pageURL.indexOf("ProductExportinExcelDetail.aspx") > 0) {
        $("#ddlCity").select2();
    }
    else if (pageURL.indexOf("frmPendingPayment.aspx") > 0) {
        $("#ContentPlaceHolder1_ddlVendor").select2();
    }

    else if (pageURL.indexOf("/UTILITY/RejectToAcceptEx.aspx") > 0) {
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/REPORTS/rptPurchaseVsSale.aspx") > 0) {
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/REPORTS/rptLogistic.aspx") > 0) {
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            "ordering": false,
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/UTILITY/AddNewList.aspx") > 0) {

        $("#ContentPlaceHolder1_ddlList").select2();
        $("#ContentPlaceHolder1_gvList1").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }
    else if (pageURL.indexOf("CreateList.aspx") > 0) {
        $("#ContentPlaceHolder1_ddlisttype").select2();
        $("#ContentPlaceHolder1_ddlList").select2();
    }

    //06-02-2023 swetal start
    else if (pageURL.indexOf("/UTILITY/AddNewModel.aspx") > 0) {
        $("#ContentPlaceHolder1_gvList1").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("BulkWebsiteSoCreation.aspx") > 0) {
        $("#gvbulksoProduct").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }
    //end

    //17-02-2023 SWETAL
    else if (pageURL.indexOf("/REPORTS/SalesInvoiceRegister.aspx") > 0) {
        $("#ContentPlaceHolder1_gvList1").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }
    //END

    //21-02-2023 SWETAL  
    else if (pageURL.indexOf("/UTILITY/MobileBrand.aspx") > 0) {
        $("#ContentPlaceHolder1_gvList1").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }
    //END

    //23-02-2023 swetal
    else if (pageURL.indexOf("/REPORTS/SkuUpload.aspx") > 0) {

        $("#gvasindetail").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }
    //end

    else if (pageURL.indexOf("/frmCustomerList.aspx") > 0) {
        $("#ContentPlaceHolder1_gvList").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
            , "ordering": false
        });
    }
    else if (pageURL.indexOf("/JangadKROListing.aspx") > 0) {
        $(".ddlVendor").select2();
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            "ordering": false,
            "paging": false,
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }


            ]
        });
    }
    else if (pageURL.indexOf("/JangadKROReturnListingReport.aspx") > 0) {
        $(".ddlVendor").select2();
    }
    else if (pageURL.indexOf("/DelearToKRO.aspx") > 0) {
        $(".ddlVendor").select2();
    }
    else if (pageURL.indexOf("/UTILITY/RelistDetail.aspx") > 0) {
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/UTILITY/AutoUnlistedDetail.aspx") > 0) {
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/REPORTS/rptOCRScanReport.aspx") > 0) {
        $("#ContentPlaceHolder1_gvAllList").DataTable({
            dom: 'Bfrtip',
            "ordering": false,
            "pageLength": 50,
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }
    else if (pageURL.indexOf("/REPORTS/rptSORegister.aspx") > 0) {
        $("#gvList").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/rptIMEILedger.aspx") > 0) {
        $(".ddlVendor").select2();
    }
    else if (pageURL.indexOf("/frmDealerOutStanding.aspx") > 0) {
        $(".ddlDealer").select2();
    }
    else if (pageURL.indexOf("/PickedUpProductReturn.aspx") > 0) {
        $(".ddlVendor").select2();
    }
    else if (pageURL.indexOf("/frmViewCromaRateCard.aspx") > 0) {
        $(".ddlVendor").select2();
    }
    else if (pageURL.indexOf("/frmDealerSearchHistory.aspx") > 0) {
        $(".ddlVendor").select2();
    }
    else if (pageURL.indexOf("/REPORTS/rptExtendedWarranty.aspx") > 0) {
        $("#gvAllList").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/UTILITY/ListingTransactionDetail.aspx") > 0) {
        $(".ddlVendor").select2();
        $("#gvProduct").DataTable({
            dom: 'Bfrtip',
            "ordering": false,
            "pageLength": 50,
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/UTILITY/frmUploadItemMapping.aspx") > 0) {
        $(".ddlVendor").select2();
        $("#gvDetail").DataTable({
            dom: 'Bfrtip',
            "ordering": false,
            "pageLength": 50,
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/UTILITY/MosecUserDailyActivity.aspx") > 0) {

        $("#gvMosecuseractivity").DataTable({
            dom: 'Bfrtip',
            "ordering": true,
            "pageLength": 10,
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });

        $("#gvMosecUserTotalListing").DataTable({
            dom: 'Bfrtip',
            "ordering": true,
            "pageLength": 10,
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/UTILITY/ReturnableDCDetail.aspx") > 0) {
        $("#gvReturnableDC").DataTable({
            dom: 'Bfrtip',
            "ordering": true,
            "pageLength": 10,
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/UTILITY/PartnerApprovedQtyListingDetail.aspx") > 0) {
        $(".ddlVendor").select2();
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            "ordering": true,
            "pageLength": 10,
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/UTILITY/LockPriceHistory.aspx") > 0) {
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            "ordering": false,
            "paging": false,
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(ApplyAutoSuggestfunctionality);
});

function ApplyAutoSuggestfunctionality() {

    $("#fuSerialNo").change(function () {
        $("#txtIMEINo").val("");
        $("#btnUploadScan").click();
    });

    var pageURL = $(location).attr("href").toString();
    if (pageURL.indexOf("/ProductEntry.aspx") > 0 || pageURL.indexOf("/BikerProductStatuswiseDetail.aspx") > 0 || pageURL.indexOf("/frmAddProdSpec.aspx") > 0 || pageURL.indexOf("/frmViewProdSpec.aspx") > 0) {
        $(".ddlMake").select2();
        $(".ddlModel").select2();
        $(".ddlRom").select2();
        $(".ddlRam").select2();
        $(".ddlColor").select2();
        if (pageURL.indexOf("/ProductEntry.aspx") > 0) {
            $(".ddlVendor").select2();
            InitiateFileCtrl();
        }

        if (pageURL.indexOf("/BikerProductStatuswiseDetail.aspx") > 0) {
            $(".ddlModel").select2();
            $(".ddlVendor").select2();
            $("#ContentPlaceHolder1_gvProduct").DataTable({
                dom: 'Bfrtip',
                buttons: [
                    {
                        extend: 'collection',
                        text: 'Export',
                        buttons: [
                            'copy',
                            'excel',
                            'csv',
                            'pdf',
                            'print'
                        ]
                    }
                ]
            });
        }

        if (pageURL.indexOf("/ProductASMStatuswiseDetail.aspx") > 0) {
            $(".ddlModel").select2();
            $("#ContentPlaceHolder1_gvProduct").DataTable({
                dom: 'Bfrtip',
                buttons: [
                    {
                        extend: 'collection',
                        text: 'Export',
                        buttons: [
                            'copy',
                            'excel',
                            'csv',
                            'pdf',
                            'print'
                        ]
                    }
                ]
            });
        }

        if (pageURL.indexOf("/frmViewProdSpec.aspx") > 0) {
            $(".ddlItemType").select2();
            $(".ddlItemGroup").select2();
            $(".ddlItemSubGroup").select2();
        }

        if (pageURL.indexOf("/frmAddProdSpec.aspx") > 0) {
            $(".ddlItemType").select2();
            $(".ddlItemGroup").select2();
            $(".ddlItemSubGroup").select2();
            //$("$ddlMake").addEventListener("blur", GetEachProdSpecPrimaryDetail());
        }
    }
    else if (pageURL.indexOf("UTILITY/frmItemPriceMaster.aspx") > 0) {

        $(".ddlMake").select2();
        $(".ddlModel").select2();
        $(".ddlRom").select2();
        $(".ddlRam").select2();
        $(".ddlColor").select2();
        $(".ddlGrade").select2();
    }
    else if (pageURL.indexOf("/ProductUnListedEntry.aspx") > 0 || pageURL.indexOf("/ProductQcApproveDetail.aspx") > 0
        || pageURL.indexOf("/PriceApproveDetail.aspx") > 0 || pageURL.indexOf("/ReadyForListingDetail.aspx") > 0
        || pageURL.indexOf("/VendorVisitEntry.aspx") > 0
        || pageURL.indexOf("/ProductASMStatuswiseDetail.aspx")
        || pageURL.indexOf("/BikerProductStatuswiseDetail.aspx") > 0
        || pageURL.indexOf("/rptPOApporval.aspx") > 0
    ) {
        $(".ddlVendor").select2();
    }

    else if (pageURL.indexOf("/frmMSLReport.aspx") > 0) {
        $("#ContentPlaceHolder1_gvMslReport").DataTable({
            dom: 'Bfrtip',
            "pageLength": 50,
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/ProductIntervalListedUnlistedDetail.aspx") > 0
    ) {
        $("#txtFromTime").timepicker({
            showInputs: false,
            showMeridian: false
            //minuteStep: 15,
        })
        $("#txtToTime").timepicker({
            showInputs: false,
            showMeridian: false
            //minuteStep: 15,
        })
    }

    else if (pageURL.indexOf("/ProductStatuswiseDetail.aspx") > 0) {
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/rptBikerVisitReport.aspx") > 0) {
        $("#ContentPlaceHolder1_gvList").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }
    else if (pageURL.indexOf("/rptBikerVisitFeedBackReport.aspx") > 0) {
        $("#ddlCity").select2();
    }
    else if (pageURL.indexOf("/rptBikerRejectionReport.aspx") > 0) {
        $("#ddlCity").select2();
    }

    else if (pageURL.indexOf("/rptSOAging.aspx") > 0) {
        $("#ContentPlaceHolder1_gvList").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/frmBikerVendorReg.aspx") > 0) {
        $("#ContentPlaceHolder1_ddlDealer").select2();
    }
    else if (pageURL.indexOf("/frmDealer.aspx") > 0) {
        $("#ContentPlaceHolder1_ddlArea").select2();
    }

    else if (pageURL.indexOf("/BikerProductStatuswiseDetail.aspx") > 0) {
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/ProductASMStatuswiseDetail.aspx") > 0) {
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/ProductInwardDetail.aspx") > 0) {
        $(".ddlVendor").select2();
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/ProductBlanccoQcApproveDetail.aspx") > 0) {
        $(".ddlVendor").select2();
        if ($("#ContentPlaceHolder1_gvProduct tr").length > 2) {
            $("#ContentPlaceHolder1_gvProduct").DataTable({
                dom: 'Bfrtip',
                buttons: [
                    {
                        extend: 'collection',
                        text: 'Export',
                        buttons: [
                            'copy',
                            'excel',
                            'csv',
                            'pdf',
                            'print'
                        ]
                    }
                ]
            });
        }
    }

    else if ((pageURL.indexOf("/frmViewVendorMaster.aspx") > 0) || (pageURL.indexOf("/frmFIViewVendorMaster.aspx") > 0)) {
        $("#ContentPlaceHolder1_gvList").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/frmVendorMaster.aspx") > 0) {
        $("#ContentPlaceHolder1_ddlDealer").select2();
    }

    else if (pageURL.indexOf("/frmBDOAssign.aspx") > 0) {
        $("#ContentPlaceHolder1_ddlBDO").select2();
    }


    else if (pageURL.indexOf("/ProductReturnDetail.aspx") > 0) {
        $("#ContentPlaceHolder1_ddlVendor").select2();
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/ProductReportDetail.aspx") > 0) {
        $(".ddlMake").select2();
        $(".ddlModel").select2();
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/CRM/frmProdPrice.aspx") > 0) {
        $(".ddlMake").select2();
        $(".ddlModel").select2();
        $(".ddlROM").select2();
        $(".ddlRAM").select2();
        $(".ddlColor").select2();
        $(".ddlGrade").select2();
    }
    else if (pageURL.indexOf("UTILITY/frmItemMapping.aspx") > 0) {
        $(".ddlMake").select2();
        $(".ddlModel").select2();
        $(".ddlROM").select2();
        $(".ddlRAM").select2();
        $(".ddlColor").select2();
        $(".ddlGrade").select2();
    }
    else if (pageURL.indexOf("UTILITY/frmItemPriceMaster.aspx") > 0) {
        $(".ddlMake").select2();
        $(".ddlModel").select2();
        $(".ddlRom").select2();
        $(".ddlRam").select2();
        $(".ddlColor").select2();
        $(".ddlGrade").select2();
    }
    else if (pageURL.indexOf("UTILITY/frmCalculatedWebsiteAvgAmt.aspx") > 0) {
        $(".ddlMake").select2();
        $("#ContentPlaceHolder1_gvList").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("UTILITY/frmCalculatedAllPlatFormAvgAmt.aspx") > 0) {
        $("#ContentPlaceHolder1_gvList").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("UTILITY/ProductEveryStatusDateTimeDetail.aspx") > 0) {
        $("#gvProduct").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("UTILITY/frmUnMappedItemMapping.aspx") > 0) {
        $("#ContentPlaceHolder1_gvList").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("MakeModelSuggestPriceHistoryReport.aspx") > 0) {
        $(".ddlMake").select2();
        $(".ddlModel").select2();
    }
    else if (pageURL.indexOf("MakeModelSuggestPriceHistoryReportItemCode.aspx") > 0) {
        $(".ddlMake").select2();
        $(".ddlModel").select2();
    }

    else if (pageURL.indexOf("ProductReturnBDOToVendorDetail.aspx") > 0) {
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
        $(".ddlVendor").select2();
    }
    else if (pageURL.indexOf("rptVendorLedgerReport.aspx") > 0) {
        $(".ddlVendor").select2();
    }
    else if (pageURL.indexOf("PoandPRFromInward.aspx") > 0) {
        $(".ddlVendor").select2();
    }
    else if (pageURL.indexOf("ProductOtherDeviceEntry.aspx") > 0) {
        $(".ddlMake").select2();
        $(".ddlModel").select2();
        $(".ddlRom").select2();
        $(".ddlRam").select2();
        $(".ddlColor").select2();
        $(".ddlVendor").select2();
        $(".ddlItemGroup").select2();
        $(".ddlItemSubGroup").select2();
    }

    else if (pageURL.indexOf("BulkSoCreation.aspx") > 0) {
        $("#gvbulksoProduct").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }
    else if (pageURL.indexOf("BulkCreatedSoNameAddUpdate.aspx") > 0) {
        $("#gvbulksoAddressupdate").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("MakeModelPOAvrageAmount.aspx") > 0) {
        $(".ddlMake").select2();
        $(".ddlModel").select2();
        $("#gvMakeModelList").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/UTILITY/RejectToAcceptEx.aspx") > 0) {
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("ProductExportinExcelDetail.aspx") > 0) {
        $("#ddlCity").select2();
    }

    else if (pageURL.indexOf("frmPendingPayment.aspx") > 0) {
        $("#ContentPlaceHolder1_ddlVendor").select2();
    }

    else if (pageURL.indexOf("/REPORTS/rptPurchaseVsSale.aspx") > 0) {
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/REPORTS/rptLogistic.aspx") > 0) {
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            "ordering": false,
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/UTILITY/AddNewList.aspx") > 0) {
        $("#ContentPlaceHolder1_ddlList").select2();
        $("#ContentPlaceHolder1_gvList1").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("CreateList.aspx") > 0) {
        $("#ContentPlaceHolder1_ddlisttype").select2();
        $("#ContentPlaceHolder1_ddlList").select2();
    }

    //06-02-2023 swetal start
    else if (pageURL.indexOf("/UTILITY/AddNewModel.aspx") > 0) {

        $("#ContentPlaceHolder1_gvList1").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }
    else if (pageURL.indexOf("BulkWebsiteSoCreation.aspx") > 0) {
        $("#gvbulksoProduct").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }
    //17-02-2023 SWETAL
    else if (pageURL.indexOf("/REPORTS/SalesInvoiceRegister.aspx") > 0) {
        $("#ContentPlaceHolder1_gvList1").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }
    //END

    //21-02-2023 SWETAL  
    else if (pageURL.indexOf("/UTILITY/MobileBrand.aspx") > 0) {
        $("#ContentPlaceHolder1_gvList1").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }
    //END

    //23-02-2023 swetal
    else if (pageURL.indexOf("/REPORTS/SkuUpload.aspx") > 0) {
        $("#gvasindetail").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }
    //end

    else if (pageURL.indexOf("/frmCustomerList.aspx") > 0) {
        $("#ContentPlaceHolder1_gvList").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
            , "ordering": false
        });
    }

    else if (pageURL.indexOf("/JangadKROListing.aspx") > 0) {
        $(".ddlVendor").select2();
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            "paging": false,
            "ordering": false,
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/DelearToKRO.aspx") > 0) {
        $(".ddlVendor").select2();
    }

    else if (pageURL.indexOf("/JangadKROReturnListingReport.aspx") > 0) {
        $(".ddlVendor").select2();
    }
    //end

    else if (pageURL.indexOf("/UTILITY/RelistDetail.aspx") > 0) {
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/UTILITY/AutoUnlistedDetail.aspx") > 0) {
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }
    else if (pageURL.indexOf("/rptIMEILedger.aspx") > 0) {
        $(".ddlVendor").select2();
        $("#ContentPlaceHolder1_gvList").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }
    else if (pageURL.indexOf("/frmDealerOutStanding.aspx") > 0) {
        $(".ddlDealer").select2();
    }
    else if (pageURL.indexOf("/PickedUpProductReturn.aspx") > 0) {
        $(".ddlVendor").select2();
    }
    else if (pageURL.indexOf("/frmViewCromaRateCard.aspx") > 0) {
        $(".ddlVendor").select2();
    }
    else if (pageURL.indexOf("/frmDealerSearchHistory.aspx") > 0) {
        $(".ddlVendor").select2();
    }
    else if (pageURL.indexOf("/JangadKROListing.aspx") > 0) {
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            "paging": false,
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }


            ]
        });
    }

    else if (pageURL.indexOf("/REPORTS/rptOCRScanReport.aspx") > 0) {
        $("#ContentPlaceHolder1_gvAllList").DataTable({
            dom: 'Bfrtip',
            "ordering": false,
            "pageLength": 50,
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/REPORTS/rptSORegister.aspx") > 0) {
        $("#gvList").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/REPORTS/rptExtendedWarranty.aspx") > 0) {
        $("#gvAllList").DataTable({
            dom: 'Bfrtip',
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/UTILITY/ListingTransactionDetail.aspx") > 0) {
        $(".ddlVendor").select2();
        $("#gvProduct").DataTable({
            dom: 'Bfrtip',
            "ordering": false,
            "pageLength": 50,
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/UTILITY/frmUploadItemMapping.aspx") > 0) {
        $(".ddlVendor").select2();
        $("#gvDetail").DataTable({
            dom: 'Bfrtip',
            "ordering": false,
            "pageLength": 50,
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/UTILITY/MosecUserDailyActivity.aspx") > 0) {
        $("#gvMosecuseractivity").DataTable({
            dom: 'Bfrtip',
            "ordering": true,
            "pageLength": 10,
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });

        $("#gvMosecUserTotalListing").DataTable({
            dom: 'Bfrtip',
            "ordering": true,
            "pageLength": 10,
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/UTILITY/ReturnableDCDetail.aspx") > 0) {
        $("#gvReturnableDC").DataTable({
            dom: 'Bfrtip',
            "ordering": true,
            "pageLength": 10,
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/UTILITY/PartnerApprovedQtyListingDetail.aspx") > 0) {
        $(".ddlVendor").select2();
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            "ordering": true,
            "pageLength": 10,
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }

    else if (pageURL.indexOf("/UTILITY/LockPriceHistory.aspx") > 0) {
        $("#ContentPlaceHolder1_gvProduct").DataTable({
            dom: 'Bfrtip',
            "ordering": false,
            "paging": false,
            buttons: [
                {
                    extend: 'collection',
                    text: 'Export',
                    buttons: [
                        'copy',
                        'excel',
                        'csv',
                        'pdf',
                        'print'
                    ]
                }
            ]
        });
    }
}
function onlynumber() {

    $('.numberonly').each(function () {
        $(this).keypress(function (e) {
            var charCode = (e.which) ? e.which : event.keyCode
            if (String.fromCharCode(charCode).match(/[^0-9]/g))
                return false;
        });
    });
}

function ValidateQcResult() {
    var Isvalidate = true;
    $("#lblQcResultalert").css("display", "none");
    $("#lblReasonalert").css("display", "none");
    $("#lblGradealert").css("display", "none");

    if ($("#ddlQcResult option:selected").val() == "SELECT") {
        $("#lblQcResultalert").css("display", "block");
        Isvalidate = false;
    }

    if ($("#ddlQcResult option:selected").val() == "FAIL" && $("#txtReason").val().length == 0) {
        $("#lblReasonalert").css("display", "block");
        Isvalidate = false;
    }

    if ($("#ddlQcGrade option:selected").val() == "SELECT") {
        $("#lblGradealert").css("display", "block");
        Isvalidate = false;
    }


    return Isvalidate;
}


function ValidateBlanccoQcResult() {
    var Isvalidate = true;
    $("#lblBlanccoQcResultalert").css("display", "none");
    $("#lblBlanccoReasonalert").css("display", "none");
    $("#lblQcResultalert").css("display", "none");
    $("#lblReasonalert").css("display", "none");
    $("#lblDealerVendoralert").css("display", "none");
    $("#lblIMEINoalert").css("display", "none");
    $("#lblBlanccoGradealert").css("display", "none");
    $("#lblimeialert").css("display", "none");
    $("#lblInvoicealert").css("display", "none");

    if (($("#ddlBlanccoQcResult option:selected").val() != "NOTDONE" && $("#ddlBlanccoQcResult option:selected").val() != "NOTAVAILABLE") || $("#ddlBlanccoQcResult option:selected").val() == "SELECT") {
        if ($("#ddlBlanccoQcResult option:selected").val() == "SELECT") {
            $("#lblBlanccoQcResultalert").css("display", "block");
            Isvalidate = false;
        }

        if ($("#ddlBlanccoQcResult option:selected").val() == "FAIL" && $("#txtBlanccoReason").val().length == 0) {
            $("#lblBlanccoReasonalert").css("display", "block");
            Isvalidate = false;
        }

        if ($("#ddlQcResult option:selected").val() == "SELECT") {
            $("#lblQcResultalert").css("display", "block");
            Isvalidate = false;
        }

        if ($("#ddlQcResult option:selected").val() == "FAIL" && $("#txtReason").val().length == 0) {
            $("#lblReasonalert").css("display", "block");
            Isvalidate = false;
        }

        if ($("#txtIMEINo").val().length != 15) {
            $("#lblIMEINoalert").css("display", "block");
            Isvalidate = false;
        }


        if ($("#ddlBlanccoGrade option:selected").val() == "SELECT") {
            $("#lblBlanccoGradealert").css("display", "block");
            Isvalidate = false;
        }

        if ($("#txtPurchaseRate").val().length == 0) {
            $("#lblPurchasealert").css("display", "block");
            Isvalidate = false;
        }

        if ($("#fuImeiImage").get(0).files.length == 0) {
            $("#lblimeialert").css("display", "block");
            Isvalidate = false;
        }

        if ($("#fuinvoiceimage").get(0).files.length == 0) {
            $("#lblInvoicealert").css("display", "block");
            Isvalidate = false;
        }

        if ($("#fuImeiImage").get(0).files.length > 0) {
            var isimglfile = false;
            var filename = $("#fuImeiImage").get(0).files[0].name;
            var fileextensionarray = filename.split(".");
            var fileextension = fileextensionarray[(fileextensionarray.length - 1)];
            if ((fileextension.toUpperCase()).includes("JPEG") || (fileextension.toUpperCase()).includes("JPG") || (fileextension.toUpperCase()).includes("TIFF")
                || (fileextension.toUpperCase()).includes("PNG")) {
                var isimglfile = true;
            }

            if (isimglfile == false) {
                $("#lblimeialert").text("Please Select the only JPEG,JPG,TIFF,PNG.");
                $("#lblimeialert").css("display", "block");
                Isvalidate = false;
            }
        }


        if ($("#fuinvoiceimage").get(0).files.length > 0) {
            var isimglfile = false;
            var filename = $("#fuinvoiceimage").get(0).files[0].name;
            var fileextensionarray = filename.split(".");
            var fileextension = fileextensionarray[(fileextensionarray.length - 1)];
            if ((fileextension.toUpperCase()).includes("JPEG") || (fileextension.toUpperCase()).includes("JPG") || (fileextension.toUpperCase()).includes("TIFF")
                || (fileextension.toUpperCase()).includes("PNG")) {
                var isimglfile = true;
            }

            if (isimglfile == false) {
                $("#lblInvoicealert").text("Please Select the only JPEG,JPG,TIFF,PNG.");
                $("#lblInvoicealert").css("display", "block");
                Isvalidate = false;
            }
        }
    }
    else {
        if ($("#ddlBlanccoQcResult option:selected").val() == "NOTDONE") {
            if ($("#txtIMEINo").val().length != 15) {
                $("#lblIMEINoalert").css("display", "block");
                Isvalidate = false;
            }

            if ($("#ddlBlanccoGrade option:selected").val() == "SELECT") {
                $("#lblBlanccoGradealert").css("display", "block");
                Isvalidate = false;
            }

            if ($("#txtPurchaseRate").val().length == 0) {
                $("#lblPurchasealert").css("display", "block");
                Isvalidate = false;
            }

            if ($("#fuImeiImage").get(0).files.length == 0) {
                $("#lblimeialert").css("display", "block");
                Isvalidate = false;
            }

            if ($("#fuinvoiceimage").get(0).files.length == 0) {
                $("#lblInvoicealert").css("display", "block");
                Isvalidate = false;
            }
        }
    }

    if ($("#txtIMEINo").val().length > 0) {
        if ($("#lblimei").css("display") == "block") {
            Isvalidate = false;
        }
    }

    //if ($("#ddlDealerVendor option:selected").val() == "0") {
    //    $("#lblDealerVendoralert").css("display", "block");
    //    Isvalidate = false;
    //}
    return Isvalidate;
}

function EnableDisableCtrl() {
    $("#lblBlanccoQcResultalert").css("display", "none");
    $("#lblBlanccoReasonalert").css("display", "none");
    $("#lblQcResultalert").css("display", "none");
    $("#lblReasonalert").css("display", "none");
    $("#lblDealerVendoralert").css("display", "none");
    $("#lblIMEINoalert").css("display", "none");
    $("#lblBlanccoGradealert").css("display", "none");

    if ($("#ddlBlanccoQcResult option:selected").val() == "NOTAVAILABLE") {
        $("#txtBlanccoReason").attr("disabled", "disabled");
        $("#ddlQcResult").attr("disabled", "disabled");
        $("#txtReason").attr("disabled", "disabled");
        $("#txtIMEINo").attr("disabled", "disabled");
        $("#txtPurchaseRate").attr("disabled", "disabled");
        $("#ddlBlanccoGrade").attr("disabled", "disabled");
        $("#ddlDealerVendor").attr("disabled", "disabled");

    }
    else if ($("#ddlBlanccoQcResult option:selected").val() == "NOTDONE") {
        $("#txtBlanccoReason").attr("disabled", "disabled");
        $("#ddlQcResult").attr("disabled", "disabled");
        $("#txtReason").attr("disabled", "disabled");

        $("#txtIMEINo").removeAttr('disabled');
        $("#txtPurchaseRate").removeAttr('disabled');
        $("#ddlBlanccoGrade").removeAttr('disabled');
        $("#ddlDealerVendor").removeAttr('disabled');
    }
    else {
        $("#txtBlanccoReason").removeAttr('disabled');
        $("#ddlQcResult").removeAttr('disabled');
        $("#txtReason").removeAttr('disabled');

        $("#txtIMEINo").removeAttr('disabled');
        $("#txtPurchaseRate").removeAttr('disabled');
        $("#ddlBlanccoGrade").removeAttr('disabled');
        $("#ddlDealerVendor").removeAttr('disabled');
    }
}

function IsAllowonlyNumericIMEINO() {
    if ($("#txtIMEINo").val().length > 0) {
        if (!$.isNumeric($("#txtIMEINo").val())) {
            $("#txtIMEINo").val("");
        }
    }
}


function ValidateProductInwardDetail() {
    var Isvalidate = true;
    $("#lblInwardResultalert").css("display", "none");
    $("#lblInwardReasonalert").css("display", "none");
    $("#lblInwardGradealert").css("display", "none");
    $("#lblInwardealert").css("display", "none");
    $("#lblOrderNoalert").css("display", "none");
    $("#lblSoNoalert").css("display", "none");



    //For Ram, Rom , Color
    $("#lblInvoicealert").css("display", "none");
    $("#lblBoxalert").css("display", "none");
    $("#lblChargeralert").css("display", "none");
    $("#lblorignalalert").css("display", "none");
    $("#lblchargerwaltalert").css("display", "none");
    $("#lblchargerwaltalert").css("display", "none");
    $("#lblvalidSONoalert").css("display", "none");
    //For Ram, Rom , Color
    if ($("#txtSoNo").val().length > 0) {
        if ($("#lblSONoNotFound").css("display") == "block") {
            Isvalidate = false;
        }
    }

    if ($("#hdlistingtype").val() == "12233") {
        if ($("#txtSoNo").val() == "4000043615" || $("#txtSoNo").val() == "4000043616" || $("#txtSoNo").val() == "4000033043" || $("#txtSoNo").val() == "4000039858") {
            $("#lblvalidSONoalert").css("display", "block");
            Isvalidate = false;
        }
    }

    if ($("#txtOrderNo").val().length == 0) {
        $("#lblOrderNoalert").css("display", "block");
        Isvalidate = false;
    }

    if ($("#txtSoNo").val().length == 0) {
        $("#lblSoNoalert").css("display", "block");
        Isvalidate = false;
    }

    if ($("#ddlInwardResult option:selected").val() == "SELECT") {
        $("#lblInwardResultalert").css("display", "block");
        Isvalidate = false;
    }

    if ($("#ddlInwardResult option:selected").val() == "FAIL" && $("#txtInwardReason").val().length == 0) {
        $("#lblInwardReasonalert").css("display", "block");
        Isvalidate = false;
    }

    if ($("#ddlInwardGrade option:selected").val() == "SELECT") {
        $("#lblInwardGradealert").css("display", "block");
        Isvalidate = false;
    }

    if ($(".ddlInvoice option:selected").val() == "-1") {
        $("#lblInvoicealert").css("display", "block");
        Isvalidate = false;
    }

    if ($(".ddlBox option:selected").val() == "-1") {
        $("#lblBoxalert").css("display", "block");
        Isvalidate = false;
    }

    if ($(".ddlCharger option:selected").val() == "-1") {
        $("#lblChargeralert").css("display", "block");
        Isvalidate = false;
    }

    if ($(".ddlCharger option:selected").val() != "0" && $(".ddlCharger option:selected").val() != "-1"
        && $(".ddlOrignal option:selected").val() == "-1") {
        $("#lblorignalalert").css("display", "block");
        Isvalidate = false;
    }

    if ($(".ddlCharger option:selected").val() != "0" && $(".ddlCharger option:selected").val() != "-1"
        && $("#txtChargerWalt").val().length == 0) {
        $("#lblchargerwaltalert").css("display", "block");
        Isvalidate = false;
    }
    return Isvalidate;
}



function CalculatePurchasePercentageAmt(vendorrate, ctrpurid, ctrnewrtid, ctrfkrtid, ctramzrtid, ctrwebrtid,
    ctrpurperid, ctrfkperid, ctramzperid, ctrwebperid, make, mobexgrade, vendorgrade, basicpurrate) {
    var purchasevalue = 0;
    var newratevalue = 0;
    var fkratevalue = 0;
    var amzratevalue = 0;
    var webratevalue = 0;
    var purpervalue = 0;
    var fkpervalue = 0;
    var amzpervalue = 0;
    var webpervalue = 0;
    var basicpurvalue = 0;

    basicpurvalue = parseFloat(basicpurrate);

    vendorrate = parseFloat(vendorrate);
    ctrpurid = "#" + ctrpurid;
    ctrnewrtid = "#" + ctrnewrtid;
    ctrfkrtid = "#" + ctrfkrtid;
    ctramzrtid = "#" + ctramzrtid;
    ctrwebrtid = "#" + ctrwebrtid;
    ctrpurperid = "#" + ctrpurperid;
    ctrfkperid = "#" + ctrfkperid;
    ctramzperid = "#" + ctramzperid;
    ctrwebperid = "#" + ctrwebperid;

    purchasevalue = $("" + ctrpurid + "").val().length > 0 ? parseFloat($("" + ctrpurid + "").val()) : 0;
    newratevalue = $("" + ctrnewrtid + "").val().length > 0 ? parseFloat($("" + ctrnewrtid + "").val()) : 0;

    if (purchasevalue != 0) {
        fkratevalue = (((purchasevalue)) * (1.224));
        //if (purchasevalue > basicpurvalue) {
        //}
        //else {
        //    fkratevalue = (((basicpurvalue)) * (1.224));
        //}
        fkratevalue = (Math.floor(fkratevalue * Math.pow(10, -2)) / Math.pow(10, -2));
    }

    if (purchasevalue != 0) {
        amzratevalue = (((purchasevalue + 700)) * (1.175));
        //if (purchasevalue > basicpurvalue) {
        //}
        //else {
        //    amzratevalue = (((basicpurvalue + 700)) * (1.165));
        //}
        amzratevalue = (Math.floor(amzratevalue * Math.pow(10, -2)) / Math.pow(10, -2));
    }

    if (purchasevalue != 0) {
        webratevalue = (((amzratevalue)) - (350));
        //webratevalue = (Math.floor(webratevalue * Math.pow(10, -2)) / Math.pow(10, -2));
    }

    if (purchasevalue != 0 && fkratevalue != 0) {
        fkpervalue = (((fkratevalue * 100)) / newratevalue).toFixed(0);
    }

    if (purchasevalue != 0 && amzratevalue != 0) {
        amzpervalue = (((amzratevalue * 100)) / newratevalue).toFixed(0);
    }

    if (purchasevalue != 0 && webratevalue != 0) {
        webpervalue = (((webratevalue * 100)) / newratevalue).toFixed(0);

        //if (purchasevalue >= 1 && purchasevalue <= 7000)
        //{
        //    webratevalue = purchasevalue + ((purchasevalue * 15) / 100);
        //}
        //else if (purchasevalue >= 7001 && purchasevalue <= 15000)
        //{
        //    webratevalue = purchasevalue + ((purchasevalue * 10) / 100);
        //}
        //else if (purchasevalue >= 15001 && purchasevalue <= 30000)
        //{
        //    webratevalue = purchasevalue + ((purchasevalue * 8) / 100);
        //}
        //else if (purchasevalue >= 30001 && purchasevalue <= 45000)
        //{
        //    webratevalue = purchasevalue + ((purchasevalue * 5) / 100);
        //}
        //else if (purchasevalue >= 45001 && purchasevalue <= 60000)
        //{
        //    webratevalue = purchasevalue + ((purchasevalue * 4) / 100);
        //}
        //else
        //{
        //    webratevalue = purchasevalue + ((purchasevalue * 3) / 100);
        //}
    }

    if (purchasevalue != 0 && newratevalue != 0) {
        purpervalue = (((purchasevalue * 100)) / newratevalue).toFixed(0);

        //if (purchasevalue > basicpurvalue) {
        //}
        //else {
        //    purpervalue = (((basicpurvalue * 100)) / newratevalue).toFixed(0);
        //}
    }

    $("" + ctrfkrtid + "").val("FK:" + fkratevalue.toString());
    $("" + ctramzrtid + "").val("AMZ:" + amzratevalue.toString());
    $("" + ctrwebrtid + "").val("WEB:" + webratevalue.toString());

    //|| (make.toUpperCase() == "APPLE")
    if ((fkratevalue > 45000) || (fkpervalue >= 90)) {
        //$("" + ctrfkrtid + "").css("color", "red");
        $("" + ctrfkperid + "").css("color", "red");
    }
    else {
        //$("" + ctrfkrtid + "").css("color", "black");
        $("" + ctrfkperid + "").css("color", "black");
    }

    //|| (purchasevalue > basicpurvalue) mobexgrade == "B" || vendorgrade == "B" ||
    if ((make.toUpperCase() == "APPLE") || (mobexgrade == "C")
        || (vendorgrade == "C") || (amzpervalue >= 90)) {
        //$("" + ctramzrtid + "").css("color", "red");
        $("" + ctramzperid + "").css("color", "red");
    }
    else {
        //$("" + ctramzrtid + "").css("color", "black");
        $("" + ctramzperid + "").css("color", "black");
    }

    //(mobexgrade == "B") || (vendorgrade == "B") ||
    if ((mobexgrade == "C") || (vendorgrade == "C") //|| (webpervalue >= 90
        || (purchasevalue > basicpurvalue)) {
        //$("" + ctrwebrtid + "").css("color", "red");
        $("" + ctrwebperid + "").css("color", "red");
    }
    else {
        //$("" + ctrwebrtid + "").css("color", "black");
        $("" + ctrwebperid + "").css("color", "black");
    }

    $("" + ctrpurperid + "").text("PUR:" + purpervalue.toString() + "%");
    $("" + ctrfkperid + "").text("FK:" + (fkpervalue.toString() == "Infinity" ? "0" : fkpervalue.toString()) + "%");
    $("" + ctramzperid + "").text("AMZ:" + (amzpervalue.toString() == "Infinity" ? "0" : amzpervalue.toString()) + "%");
    $("" + ctrwebperid + "").text("WEB:" + (webpervalue.toString() == "Infinity" ? "0" : webpervalue.toString()) + "%");
}

//function ValidateProductEntry() {
//    var Isvalidate = false;

//    $("#lblRamalert").css("display", "none");

//    if ($("#ddlMake option:selected").val() != "Apple" && $("#ddlRam option:selected").val() == "0") {
//        $("#lblRamalert").css("display", "block");
//        Isvalidate = false;
//    }

//    return Isvalidate;
//}


function InitiateUnlistedDataTable() {
    $("#ContentPlaceHolder1_gvProduct").DataTable({
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'collection',
                text: 'Export',
                buttons: [
                    'copy',
                    'excel',
                    'csv',
                    'pdf',
                    'print'
                ]
            }
        ]
    });
    $(".dt-buttons").css("display", "none");
}


function ValidateVendorVisit(requestype) {
    var Isvalidate = true;
    $("#lblvendoralert").css("display", "none");
    $("#lblfeedbackalert").css("display", "none");

    if (requestype == "AddStock") {

        if ($("#ddlVendor option:selected").val() == "0") {
            $("#lblvendoralert").css("display", "block");
            Isvalidate = false;
        }
    }
    else {
        if ($("#ddlVendor option:selected").val() == "0") {
            $("#lblvendoralert").css("display", "block");
            Isvalidate = false;
        }

        if ($("#txtFeedback").val().length == 0) {
            $("#lblfeedbackalert").css("display", "block");
            Isvalidate = false;
        }
    }
    return Isvalidate;
}

function UnListedLoadingFunctionality(btnid) {
    var Isconfirm = confirm('Are you sure you want to Unlist Product?');
    if (Isconfirm) {
        btnid = "#" + btnid;
        $("" + btnid + "").text("Please wait..");
        $("" + btnid + "").css("cursor", "not-allowed");
        $("" + btnid + "").css("pointer-events", "none");
    }
    return Isconfirm;
}

function LoadVendorAutoSuggestJs() {
    $(".ddlMake").select2();
    $(".ddlMake").select2();
    $(".ddlMake").select2();
    $(".ddlModel").select2();
    $(".ddlRom").select2();
    $(".ddlRam").select2();
    $(".ddlColor").select2();
    $(".ddlVendor").select2();
    $(".ddlItemGroup").select2();
    $(".ddlItemSubGroup").select2();
}


function ShowProgressBaar() {
    $("#progress").show();
}

function HideProgressBaar() {
    $("#progress").hide();
}

function IsAllowonlyNumeric() {
    if ($("#ContentPlaceHolder1_txtVendorRate").val().length > 0) {
        if ($.isNumeric($("#ContentPlaceHolder1_txtVendorRate").val())) {
            if (parseFloat($("#ContentPlaceHolder1_txtVendorRate").val()) <= 1000) {
                $("#lblVendorratealert").css("display", "block");
                $("#ContentPlaceHolder1_txtVendorRate").val("");
            }
            else {
                $("#lblVendorratealert").css("display", "none");
            }
        }
        else {
            $("#lblVendorratealert").css("display", "block");
            $("#ContentPlaceHolder1_txtVendorRate").val("");
        }
    }
    else {
        $("#lblVendorratealert").css("display", "none");
    }
}


function ValidatePanCardIDProofDetail() {
    $("#lblPancardalert").css("display", "none");
    $("#lblIdproofalert").css("display", "none");

    var IsvalidatePancardIDProof = true;
    // For Pan Card Detail
    if ($("#ContentPlaceHolder1_fuPAN").get(0).files.length > 0) {
        var filename = $("#ContentPlaceHolder1_fuPAN").get(0).files[0].name;
        var fileextensionarray = filename.split(".");
        var fileextension = fileextensionarray[(fileextensionarray.length - 1)];

        if ((fileextension.toUpperCase()) != "JPEG" && (fileextension.toUpperCase()) != "JPG" && (fileextension.toUpperCase()) != "TIFF"
            && (fileextension.toUpperCase()) != "PNG") {
            IsvalidatePancardIDProof = false;
            $("#lblPancardalert").css("display", "block");
        }
    }

    // For ID Proof Detail
    if ($("#ContentPlaceHolder1_fuIDProof").get(0).files.length > 0) {
        var filenameIDProof = $("#ContentPlaceHolder1_fuIDProof").get(0).files[0].name;
        var fileextensionIDProofarray = filenameIDProof.split(".");
        var fileextensionIDProof = fileextensionIDProofarray[(fileextensionIDProofarray.length - 1)];

        if ((fileextensionIDProof.toUpperCase()) != "JPEG" && (fileextensionIDProof.toUpperCase()) != "JPG" && (fileextensionIDProof.toUpperCase()) != "TIFF"
            && (fileextensionIDProof.toUpperCase()) != "PNG") {
            IsvalidatePancardIDProof = false;
            $("#lblIdproofalert").css("display", "block");
        }
    }
    // For ID Proof Detail

    return IsvalidatePancardIDProof;
}

function ValidateGSTCertificate() {
    $("#lblGStCeralert").css("display", "none");
    $("#lblMSMECertialert").css("display", "none");

    var IsvalidateGSTCertificate = true;

    // For GST Certificate Detail
    if ($("#ContentPlaceHolder1_fuGSTCerti").get(0).files.length > 0) {
        var filename = $("#ContentPlaceHolder1_fuGSTCerti").get(0).files[0].name;
        var fileextensionarray = filename.split(".");
        var fileextension = fileextensionarray[(fileextensionarray.length - 1)];

        if ((fileextension.toUpperCase()) != "JPEG" && (fileextension.toUpperCase()) != "JPG" && (fileextension.toUpperCase()) != "TIFF"
            && (fileextension.toUpperCase()) != "PNG") {
            IsvalidateGSTCertificate = false;
            $("#lblGStCeralert").css("display", "block");
        }
    }

    // For ID Proof Detail
    if ($("#ContentPlaceHolder1_fuMSMECerti").get(0).files.length > 0) {
        var filenameIDProof = $("#ContentPlaceHolder1_fuMSMECerti").get(0).files[0].name;
        var fileextensionIDProofarray = filenameIDProof.split(".");
        var fileextensionIDProof = fileextensionIDProofarray[(fileextensionIDProofarray.length - 1)];

        if ((fileextensionIDProof.toUpperCase()) != "JPEG" && (fileextensionIDProof.toUpperCase()) != "JPG" && (fileextensionIDProof.toUpperCase()) != "TIFF"
            && (fileextensionIDProof.toUpperCase()) != "PNG") {
            IsvalidatePancardIDProof = false;
            $("#lblMSMECertialert").css("display", "block");
        }
    }
    // For ID Proof Detail

    return IsvalidateGSTCertificate;
}

function ValidateCancelledCheque() {
    $("#lblChequealert").css("display", "none");
    var IsvalidateCancelledCertificate = true;

    // For Bank  Detail
    if ($("#ContentPlaceHolder1_fuCheque").get(0).files.length > 0) {
        var filename = $("#ContentPlaceHolder1_fuCheque").get(0).files[0].name;
        var fileextensionarray = filename.split(".");
        var fileextension = fileextensionarray[(fileextensionarray.length - 1)];

        if ((fileextension.toUpperCase()) != "JPEG" && (fileextension.toUpperCase()) != "JPG" && (fileextension.toUpperCase()) != "TIFF"
            && (fileextension.toUpperCase()) != "PNG") {
            IsvalidateCancelledCertificate = false;
            $("#lblChequealert").css("display", "block");
        }
    }

    return IsvalidateCancelledCertificate;
}


function InitiateFileCtrl() {
    $(".file-simple").fileinput({
        showUpload: false,
        showCaption: false,
        browseClass: "btn btn-danger",
        fileType: "any"
    });
}


function ValidateProductReturn() {
    var Isvalidate = true;
    $("#lblReturnReasonalert").css("display", "none");

    if ($("#txtReturnReason").val().length == 0) {
        $("#lblReturnReasonalert").css("display", "block");
        Isvalidate = false;
    }
    return Isvalidate;
}


function ValidateProductPickedupReturn() {
    var Isvalidate = true;
    $("#lblReturnReasonalert").css("display", "none");
    $("#lblalertotp").css("display", "none");

    if ($("#txtReturnReason").val().length == 0) {
        $("#lblReturnReasonalert").css("display", "block");
        Isvalidate = false;
    }

    if ($("#txtHandoverReturnBDOOTP").val().length == 0) {
        $("#lblalertotp").css("display", "block");
        Isvalidate = false;
    }


    return Isvalidate;
}

function ValidateProductHandoverToBDO() {
    var Isvalidate = true;
    $("#lblHandoverOTP").css("display", "none");
    if ($("#txtHandoverBDOOTP").val().length == 0) {
        $("#lblHandoverOTP").css("display", "block");
        Isvalidate = false;
    }
    return Isvalidate;
}


function ValidateReservedExcelUpload() {
    $("#lblfilealert").css("display", "none");
    var IsvalidateReservedExcelUpload = true;

    // For Bank  Detail
    if ($("#flReserUpload").get(0).files.length == 0) {
        IsvalidateReservedExcelUpload = false;
        $("#lblfilealert").css("display", "block");
    }

    return IsvalidateReservedExcelUpload;
}

function UploadScanImageEvent() {
    $("#btnUploadScan").click();
}

function SetTarget() {
    document.forms[0].target = "_blank";
}


function GetEachProdSpecPrimaryDetail() {
    debugger;
    var Isvalidate = true;
    if (($("#ddlMake option:selected").val() != "0" && $("#ContentPlaceHolder1_ddlModel option:selected").val() != "0" && $("#ddlRam option:selected").val() != "0") && $("#ContentPlaceHolder1_ddlRom option:selected").val() != "0") {
        if ($("#ddlMake option:selected").val() != "0" &&
            $("#ContentPlaceHolder1_ddlModel option:selected").val() != "0" &&
            $("#ddlRam option:selected").val() != "0" &&
            $("#ContentPlaceHolder1_ddlRom option:selected").val() != "0"
        ) {
            var obj = {};
            obj.brand = $("#ddlMake option:selected").text();
            obj.model = $("#ContentPlaceHolder1_ddlModel option:selected").text();
            obj.ram = $("#ddlRam option:selected").text();
            obj.rom = $("#ContentPlaceHolder1_ddlRom option:selected").text();
            debugger;
            jQuery.ajax({
                url: 'frmAddProdSpec.aspx/GetEachProdSpecPrimaryDetail',
                type: "POST",
                dataType: "json",
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                success: function (d) {
                    var objeachSpecPrimary = JSON.parse(d.d);
                    LoadEachProdSpecPrimaryDetail(objeachSpecPrimary, "Load");
                },
                error: function (response) {
                    var objeachSpecPrimary = JSON.parse(response.responseJSON);
                    LoadEachProdSpecPrimaryDetail(objeachSpecPrimary, "Load");
                }
            });
        }
        else {
            LoadEachProdSpecPrimaryDetail("", "Reset");
        }
    }
    else {
        debugger;
        if (
            $("#ddlMake option:selected").val() != "0" &&
            $("#ContentPlaceHolder1_ddlModel option:selected").val() != "0" &&
            $("#ddlRam option:selected").val() == "0" &&
            $("#ContentPlaceHolder1_ddlRom option:selected").val() == "0"
        ) {
            var obj = {};
            obj.brand = $("#ddlMake option:selected").text();
            obj.model = $("#ContentPlaceHolder1_ddlModel option:selected").text();
            jQuery.ajax({
                url: 'frmAddProdSpec.aspx/GetEachProdLaunchDetail',
                type: "POST",
                dataType: "json",
                data: JSON.stringify(obj),
                contentType: "application/json; charset=utf-8",
                success: function (d) {
                    var objeachSpecPrimary = JSON.parse(d.d);
                    LoadEachProdLaunchDetail(objeachSpecPrimary, "Load");
                },
                error: function (response) {
                    var objeachSpecPrimary = JSON.parse(response.responseJSON);
                    LoadEachProdLaunchDetail(objeachSpecPrimary, "Load");
                }
            });
        }
        else {
            if (
                $("#ddlMake option:selected").val() == "0" &&
                $("#ContentPlaceHolder1_ddlModel option:selected").val() == "0"
            ) {
                LoadEachProdLaunchDetail("", "Reset");
            }
        }
    }
    return false;
}

function LoadEachProdSpecPrimaryDetail(objeachSpecPrimary, action) {
    debugger;
    if (action == "Load") {
        $("#ContentPlaceHolder1_txtNewRate").val(objeachSpecPrimary.NEWRATE);
        $("#ContentPlaceHolder1_txtBasiPurRate").val(objeachSpecPrimary.BASICPURRATE);
        //$("#ContentPlaceHolder1_txtLaunchYear").val(objeachSpecPrimary.LAUNCHYEAR);
        $("#txtFinalAmount").val(objeachSpecPrimary.FinalApproveListingAmount);

    }
    else {
        $("#ContentPlaceHolder1_txtNewRate").val("");
        $("#ContentPlaceHolder1_txtBasiPurRate").val("");


        //  if (
        //    $("#ContentPlaceHolder1_txtLaunchYear").val("").length > 0 &&
        //    $("#ddlRam option:selected").val() == "0" &&
        //    $("#ContentPlaceHolder1_ddlRom option:selected").val() == "0"
        //) {
        //    $("#ContentPlaceHolder1_txtLaunchYear").val($("#ContentPlaceHolder1_txtLaunchYear").val());
        //}
        //else {
        //    $("#ContentPlaceHolder1_txtLaunchYear").val("");
        //}

        $("#txtFinalAmount").val("");
    }
}


function GetEachProdSpecalreadyExist() {
    if ($("#ddlMake option:selected").val() != "0" &&
        $("#ContentPlaceHolder1_ddlModel option:selected").val() != "0" &&
        $("#ddlRam option:selected").val() != "0" &&
        $("#ContentPlaceHolder1_ddlRom option:selected").val() != "0" &&
        $("#ContentPlaceHolder1_ddlColor option:selected").val() != "0" &&
        $("#ContentPlaceHolder1_btnAdd").val() == "Add"
    ) {
        var obj = {};
        obj.brand = $("#ddlMake option:selected").text();
        obj.model = $("#ContentPlaceHolder1_ddlModel option:selected").text();
        obj.ram = $("#ddlRam option:selected").text();
        obj.rom = $("#ContentPlaceHolder1_ddlRom option:selected").text();
        obj.color = $("#ContentPlaceHolder1_ddlColor option:selected").text();

        jQuery.ajax({
            url: 'frmAddProdSpec.aspx/GetEachProdSpecalreadyExist',
            type: "POST",
            dataType: "json",
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            success: function (d) {
                var objeachSpecPrimary = JSON.parse(d.d);
                if (objeachSpecPrimary.BRAND_ID == 0) {
                    $("#lblalreadyexist").css("display", "none");
                    $("#btnAddconfirm").click();
                    return true;
                }
                else {
                    $("#lblalreadyexist").css("display", "block");
                    return false;
                }
            },
            error: function (response) {
                var objeachSpecPrimary = JSON.parse(response.responseJSON);
                if (objeachSpecPrimary.BRAND_ID == 0) {
                    $("#lblalreadyexist").css("display", "block");
                    return false;
                }
                else {
                    $("#lblalreadyexist").css("display", "none");
                    return true;
                }
            }
        });
        return false;
    }
    else {
        $("#btnAddconfirm").click();
        return false;
    }

}

function ValidateReservedOrderNoRequired(ctrordernoid, ctralerlableid) {
    debugger
    var Isconfirm = confirm('Are you sure you want to Reserve this Model ?')
    if (Isconfirm) {
        var Isvalidate = true;
        ctrordernoid = "#" + ctrordernoid;
        ctralerlableid = "#" + ctralerlableid;
        if ($("" + ctrordernoid + "").val().length == 0) {
            $("" + ctralerlableid + "").css("display", "block");
            Isvalidate = false;
        }
        return Isvalidate;
    }
    else {
        return false;
    }

}

function SONOFound() {

    if ($("#txtSoNo").val() != ""
    ) {
        var obj = {};
        obj.SONo = $("#txtSoNo").val();
        jQuery.ajax({
            url: 'ProductInwardDetail.aspx/CheckSOnumber',
            type: "POST",
            dataType: "json",
            data: JSON.stringify(obj),
            //data: {
            //    "SONo": $("#txtSoNo").val()
            //},
            contentType: "application/json; charset=utf-8",
            success: function (d) {
                debugger;
                if (d.d == false) {
                    $("#lblSONoNotFound").css("display", "block");
                    Isvalidate = false;
                }
                else {
                    $("#lblSONoNotFound").css("display", "none");
                }
                return d;
            },
            error: function (response) {
                return true;
            }
        });
    }
    else {
        return false;
    }
    return false;
}

//10-02-2023
function ValidatePanNo() {
    debugger
    var Isvalidate = true;
    var panno = $("#ContentPlaceHolder1_txtPAN").val();
    if (panno.length > 0) {
        var obj = {};
        obj.panno = panno;
        jQuery.ajax({
            url: 'frmVendorMaster.aspx/GetPanno',
            type: "POST",
            dataType: "json",
            data: JSON.stringify(obj),
            aysync: false,
            contentType: "application/json; charset=utf-8",
            success: function (d) {
                debugger
                var objresponse = JSON.parse(d.d);
                if (objresponse == "true") {
                    Isvalidate = false;
                    //alert("Already exist");
                    $("#lblpanno").css("display", "block");
                    return false;
                }
                else {
                    $("#lblpanno").css("display", "none");
                }
            },
            error: function (response) {
                debugger
                var objresponse = JSON.parse(response.responseJSON);
                if (objresponse == "true") {
                    Isvalidate = false;

                }
            }
        });
    }
    return Isvalidate;
}

function ValidateAdharNo() {
    var aadharno = $("#ContentPlaceHolder1_txtAadharNo").val();
    if (aadharno.length > 0) {
        var obj = {};
        obj.aadharno = aadharno;
        jQuery.ajax({
            url: 'frmVendorMaster.aspx/GetAdharNo',
            type: "POST",
            dataType: "json",

            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            success: function (d) {
                debugger
                var objresponse = JSON.parse(d.d);
                if (objresponse == "true") {
                    Isvalidate = false;
                    //alert("Already exist");
                    $("#lbladharno").css("display", "block");
                    return false;
                }
                else {
                    $("#lbladharno").css("display", "none");
                }
            },
            error: function (response) {
                debugger
                var objresponse = JSON.parse(response.responseJSON);
                if (objresponse == "true") {
                    Isvalidate = false;
                    //alert("Already exist");
                }
            }
        });
    }
}

function ValidateGSTNO() {
    debugger
    if ($("#ContentPlaceHolder1_txtGST").val() != ""
    ) {
        var obj = {};
        obj.GSTNO = $("#ContentPlaceHolder1_txtGST").val();
        jQuery.ajax({
            url: 'frmVendorMaster.aspx/GetGSTNo',
            type: "POST",
            dataType: "json",
            data: JSON.stringify(obj),
            //data: {
            //    "SONo": $("#txtSoNo").val()
            //},
            contentType: "application/json; charset=utf-8",
            success: function (d) {
                debugger;
                var result = (d.d.replace("\"", "")).replace("\"", "")
                if (result == 'true') {
                    $("#ContentPlaceHolder1_lblgst").css("display", "block");
                    Isvalidate = false;
                }
                else {
                    $("#ContentPlaceHolder1_lblgst").css("display", "none");
                }
                return d;
            },
            error: function (response) {
                debugger
                return true;
            }
        });
    }
    else {
        return false;
    }
    return false;
}

//14-02-2023 SWETAL START
function ValidateVendorName() {
    debugger
    if ($("#ContentPlaceHolder1_txtSuggestedName").val() != ""
    ) {
        var obj = {};
        obj.NAME1 = $("#ContentPlaceHolder1_txtSuggestedName").val();
        jQuery.ajax({
            url: 'frmVendorMaster.aspx/GetVendorName',
            type: "POST",
            dataType: "json",
            data: JSON.stringify(obj),
            //data: {
            //    "SONo": $("#txtSoNo").val()
            //},
            contentType: "application/json; charset=utf-8",
            success: function (d) {
                debugger;
                var result = (d.d.replace("\"", "")).replace("\"", "")
                if (result == 'true') {
                    $("#lblvendorname").css("display", "block");
                    Isvalidate = false;
                }
                else {
                    $("#lblvendorname").css("display", "none");
                }
                return d;
            },
            error: function (response) {
                debugger
                return true;
            }
        });
    }
    else {
        return false;
    }
    return false;
}

//14-02-2023 
function ValidateVendorNAME() {
    //debugger
    var Isvalidate = true;
    if ($("#ContentPlaceHolder1_txtSuggestedName").val().length > 0) {
        if ($("#lblvendorname").css("display") == "block") {
            Isvalidate = false;
        }
    }

    var Isvalidate = true;
    if ($("#ContentPlaceHolder1_txtContactPerson").val().length > 0) {
        if ($("#lblpaymentto").css("display") == "block") {
            Isvalidate = false;
        }
    }
    return Isvalidate;

}

//14-02-2023 swetal start
function VALIDATEPAYMENTNO() {
    debugger
    if ($("#ContentPlaceHolder1_txtContactPerson").val() != ""
    ) {
        var obj = {};
        obj.SHORTNM = $("#ContentPlaceHolder1_txtContactPerson").val();
        jQuery.ajax({
            url: 'frmVendorMaster.aspx/GETPAYMENTNO',
            type: "POST",
            dataType: "json",
            data: JSON.stringify(obj),
            //data: {
            //    "SONo": $("#txtSoNo").val()
            //},
            contentType: "application/json; charset=utf-8",
            success: function (d) {
                debugger;
                var result = (d.d.replace("\"", "")).replace("\"", "")
                if (result == 'true') {
                    $("#lblpaymentto").css("display", "block");
                    Isvalidate = false;
                }
                else {
                    $("#lblpaymentto").css("display", "none");
                }
                return d;
            },
            error: function (response) {
                debugger
                return true;
            }
        });
    }
    else {
        return false;
    }
    return false;
}


//end

//13-02-2023 swetal
function ValidatePANNO() {

    var Isvalidate = true;

    if ($("#ContentPlaceHolder1_txtGST").val().length > 0) {
        if ($("#ContentPlaceHolder1_lblgst").css("display") == "block") {
            Isvalidate = false;
        }

    }
    return Isvalidate;
}

//13-02-2023 swetal
function ValidatePanAdhar() {
    debugger
    var Isvalidate = true;

    if ($("#ContentPlaceHolder1_txtPAN").val().length > 0) {
        if ($("#lblpanno").css("display") == "block") {
            Isvalidate = false;
        }
    }

    if ($("#ContentPlaceHolder1_txtAadharNo").val().length > 0) {
        if ($("#lbladharno").css("display") == "block") {
            Isvalidate = false;
        }
    }
    return Isvalidate;
}

//16-02-20023 SWETAL START
function VALIDATECONTACTNO(ctrcontactnoid, ctralerlableid) {
    debugger
    var Isvalidate = true;
    ctrcontactnoid = "#" + ctrcontactnoid;
    ctralerlableid = "#" + ctralerlableid;
    if ($("" + ctrcontactnoid + "").val().length == 0) {
        $("" + ctralerlableid + "").css("display", "block");
        Isvalidate = false;
    }
    return Isvalidate;
}
//END
function isValidatForm() {
    var Isvalidate = true;
    debugger;

    if ($("#ContentPlaceHolder1_txtAdharNo").val().length > 0) {
        if ($("#lbladharno").css("display") == "block") {
            Isvalidate = false;
        }
    }
    if ($("#ContentPlaceHolder1_txtPanno").val().length > 0) {
        if ($("#lblPan").css("display") == "block") {
            Isvalidate = false;
        }
    }
    if ($("#ContentPlaceHolder1_txtGstNo").val().length > 0) {
        if ($("#lblGSTNo").css("display") == "block") {
            Isvalidate = false;
        }
    }
    if ($("#ContentPlaceHolder1_txtName2").val().length > 0) {
        if ($("#lblName2").css("display") == "block") {
            Isvalidate = false;
        }
    }
    return Isvalidate;
}

function CustomerValidateAdharNo() {
    var aadharno = $("#ContentPlaceHolder1_txtAdharNo").val();
    if (aadharno.length > 0) {
        var obj = {};
        obj.aadharno = aadharno;
        jQuery.ajax({
            url: 'frmCustomerAdd.aspx/GetAdharNo',
            type: "POST",
            dataType: "json",

            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            success: function (d) {
                debugger
                var objresponse = JSON.parse(d.d);
                if (objresponse == "true") {
                    Isvalidate = false;
                    //alert("Already exist");
                    $("#lbladharno").css("display", "block");
                    return false;
                }
                else {
                    $("#lbladharno").css("display", "none");
                }
            },
            error: function (response) {
                debugger
                var objresponse = JSON.parse(response.responseJSON);
                if (objresponse == "true") {
                    Isvalidate = false;
                    //alert("Already exist");
                }
            }
        });
    }

}

function CustomerValidatePanno() {
    var panno = $("#ContentPlaceHolder1_txtPanno").val();
    if (panno.length > 0) {
        var obj = {};
        obj.panno = panno;
        jQuery.ajax({
            url: 'frmCustomerAdd.aspx/GetPanno',
            type: "POST",
            dataType: "json",

            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            success: function (d) {
                debugger
                var objresponse = JSON.parse(d.d);
                if (objresponse == "true") {
                    Isvalidate = false;
                    //alert("Already exist");
                    $("#lblPan").css("display", "block");
                    return false;
                }
                else {
                    $("#lblPan").css("display", "none");
                }
            },
            error: function (response) {
                debugger
                var objresponse = JSON.parse(response.responseJSON);
                if (objresponse == "true") {
                    Isvalidate = false;
                    //alert("Already exist");
                }
            }
        });
    }
}

function CustomerValidateGST() {
    var gstno = $("#ContentPlaceHolder1_txtGstNo").val();
    if (gstno.length > 0) {
        var obj = {};
        obj.gstno = gstno;
        jQuery.ajax({
            url: 'frmCustomerAdd.aspx/GetGST',
            type: "POST",
            dataType: "json",

            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            success: function (d) {
                debugger
                var objresponse = JSON.parse(d.d);
                if (objresponse == "true") {
                    Isvalidate = false;
                    //alert("Already exist");
                    $("#lblGSTNo").css("display", "block");
                    return false;
                }
                else {
                    $("#lblGSTNo").css("display", "none");
                }
            },
            error: function (response) {
                debugger
                var objresponse = JSON.parse(response.responseJSON);
                if (objresponse == "true") {
                    Isvalidate = false;
                    //alert("Already exist");
                }
            }
        });
    }
}

function CustomerValidateName() {
    //var name2 = $("#ContentPlaceHolder1_txtName2").val();
    var name = $("#ContentPlaceHolder1_txtName").val();
    if (name.length > 0) {
        var obj = {};
        obj.name1 = name;
        //obj.name2 = name2;
        jQuery.ajax({
            url: 'frmCustomerAdd.aspx/GetName',
            type: "POST",
            dataType: "json",

            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            success: function (d) {
                debugger
                var objresponse = JSON.parse(d.d);
                if (objresponse == "true") {
                    Isvalidate = false;
                    //alert("Already exist");
                    $("#lblName2").css("display", "block");
                    return false;
                }
                else {
                    $("#lblName2").css("display", "none");
                }
            },
            error: function (response) {
                debugger
                var objresponse = JSON.parse(response.responseJSON);
                if (objresponse == "true") {
                    Isvalidate = false;
                    //alert("Already exist");
                }
            }
        });
    }
}

function GetEachProdLaunchDetail() {
    if ($("#ddlMake option:selected").val() != "0" &&
        $("#ContentPlaceHolder1_ddlModel option:selected").val() != "0"
    ) {
        var obj = {};
        obj.brand = $("#ddlMake option:selected").text();
        obj.model = $("#ContentPlaceHolder1_ddlModel option:selected").text();
        jQuery.ajax({
            url: 'frmAddProdSpec.aspx/GetEachProdLaunchDetail',
            type: "POST",
            dataType: "json",
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            success: function (d) {
                var objeachSpecPrimary = JSON.parse(d.d);
                LoadEachProdLaunchDetail(objeachSpecPrimary, "Load");
            },
            error: function (response) {
                var objeachSpecPrimary = JSON.parse(response.responseJSON);
                LoadEachProdLaunchDetail(objeachSpecPrimary, "Load");
            }
        });
    }
    else {
        LoadEachProdLaunchDetail("", "Reset");
    }
    return false;
}

function LoadEachProdLaunchDetail(objeachSpecPrimary, action) {
    if (action == "Load") {
        $("#ContentPlaceHolder1_txtLaunchYear").val(objeachSpecPrimary.LAUNCHYEAR);
    }
    else {
        $("#ContentPlaceHolder1_txtLaunchYear").val("");
    }
}

function ShowErrorMessageKRO() {
    var Isvalidate = true;
    $("#lblvendoralert").css("display", "none");
    $("#lblmaxday").css("display", "none");

    if ($("#ddlVendor option:selected").val() == "0") {
        $("#lblvendoralert").css("display", "block");
        Isvalidate = false;
    }

    if ($("#ddlmaxday option:selected").val() == "0") {
        $("#lblmaxday").css("display", "block");
        Isvalidate = false;
    }
    return Isvalidate;
}


function ValidateExtendays() {
    var Isvalidate = true;
    var verndorprice = 0;
    var lockamount = 0;

    verndorprice = $("#txtVendorprice").val().length > 0 ? parseFloat($("#txtVendorprice").val()) : 0;
    lockamount = $("#hdLockAmount").val().length > 0 ? parseFloat($("#hdLockAmount").val()) : 0;


    $("#lblExtenddaysalert").css("display", "none");
    $("#lblVendorpricealert").css("display", "none");
    $("#lblVendorpricealert").text("Please enter vendor price");

    if ($("#ddlmaxday option:selected").val() == "0") {
        $("#lblExtenddaysalert").css("display", "block");
        Isvalidate = false;
    }

    if ($("#txtVendorprice").val().length == 0) {
        $("#lblVendorpricealert").css("display", "block");
        Isvalidate = false;
    }

    if (verndorprice > lockamount) {
        if (confirm("Your device will be rejected due to lock amount because lock amount is:" + lockamount.toString() + " and Vendor price is:" + verndorprice.toString())) {
            Isvalidate = false;
            alert("lock amount is lower then the vendor price so you can't renewed? you should return this device.")
        }
        else {
            Isvalidate = false;
        }
    }

    return Isvalidate;
}

function ShowErrorMessageMaxDay() {
    debugger;
    var Isvalidate = true;
    $("#lblmaxday").css("display", "none");

    if ($("#ContentPlaceHolder1_chkIsKro").length == 1 && $("#ddlmaxday option:selected").val() == "0") {

        $("#lblmaxday").css("display", "block");
        Isvalidate = false;
    }
    return Isvalidate;
}

function ValidateIMEINO() {
    if ($("#ContentPlaceHolder1_txtIMEINo").val() != "" && $("#ContentPlaceHolder1_ddllistype option:selected").val() != "0"
    ) {
        var obj = {};
        obj.imeino = $("#ContentPlaceHolder1_txtIMEINo").val();
        obj.LISTINGTYPE = $("#ContentPlaceHolder1_ddllistype option:selected").val();
        jQuery.ajax({
            url: 'ProductEntry.aspx/GetIMEINo',
            type: "POST",
            dataType: "json",
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            success: function (d) {
                var result = (d.d.replace("\"", "")).replace("\"", "")
                if (result == 'true') {
                    $("#lblimei").css("display", "block");
                    Isvalidate = false;
                }
                else {
                    $("#lblimei").css("display", "none");
                }
                return d;
            },
            error: function (response) {
                return true;
            }
        });
    }
    else {
        $("#lblimei").css("display", "none");

        return false;
    }
    return false;
}

function ValidateBlanccoIMEINo() {
    if ($("#txtIMEINo").val() != "" && $("#txtIMEINo").val().length == 15 && $("#ContentPlaceHolder1_hdlisttype").val() != "0"
        && $("#ContentPlaceHolder1_hdprimarykey").val() != "0"
    ) {
        $("#lblIMEINoalert").css("display", "none");
        var obj = {};
        obj.imeino = $("#txtIMEINo").val();
        obj.LISTINGTYPE = $("#ContentPlaceHolder1_hdlisttype").val();
        obj.ID = $("#ContentPlaceHolder1_hdprimarykey").val();
        jQuery.ajax({
            url: 'ProductBlanccoQcApproveDetail.aspx/GetIMEINo',
            type: "POST",
            dataType: "json",
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            success: function (d) {
                debugger;
                var result = (d.d.replace("\"", "")).replace("\"", "")
                if (result == 'true') {
                    $("#lblimei").css("display", "block");
                    Isvalidate = false;
                }
                else {
                    $("#lblimei").css("display", "none");
                }
                return d;
            },
            error: function (response) {
                debugger
                return true;
            }
        });
    }
    else {
        $("#lblimei").css("display", "none");
        return false;
    }
    return false;
}


function SubmitBid(ID) {
    if ($(".txtbid" + ID).val() != "") {
        var obj = {};
        obj.ItemCode = ID;
        obj.BidAmount = $(".txtbid" + ID).val();
        jQuery.ajax({
            url: 'frmSOItemAssign.aspx/SubmitBid',
            type: "POST",
            dataType: "json",
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            success: function (d) {
                console.log(d);
                if (d.d == true) {
                    alert("Bid added successfully.");
                }
                else {
                    /* $("#lblSONoNotFound").css("display", "block");*/
                    //Isvalidate = false;
                }
                return d;
            },
            error: function (response) {
                return true;
            }
        });
    }
    else {
        return false;
    }
    return false;
}


function ValidateAccountNumber() {
    debugger
    if ($("#ContentPlaceHolder1_txtACNo").val() != ""
    ) {
        var obj = {};
        obj.ACCOUNTNO = $("#ContentPlaceHolder1_txtACNo").val();
        jQuery.ajax({
            url: 'frmVendorMaster.aspx/GetAccountNumber',
            type: "POST",
            dataType: "json",
            data: JSON.stringify(obj),
            //data: {
            //    "SONo": $("#txtSoNo").val()
            //},
            contentType: "application/json; charset=utf-8",
            success: function (d) {
                var result = (d.d.replace("\"", "")).replace("\"", "")
                if (result == 'true') {
                    $("#lblAccntNoExist").css("display", "block");
                    Isvalidate = false;
                }
                else {
                    $("#lblAccntNoExist").css("display", "none");
                }
                return d;
            },
            error: function (response) {
                return true;
            }
        });
    }
    else {
        return false;
    }
    return false;
}


function ValidateAccountNo() {
    //debugger
    var Isvalidate = true;
    if ($("#ContentPlaceHolder1_txtACNo").val().length > 0) {
        if ($("#lblAccntNoExist").css("display") == "block") {
            Isvalidate = false;
        }
    }
    return Isvalidate;
}


function ValidateAccountNumberFIVendorMaster() {
    if ($("#ContentPlaceHolder1_txtACNo").val() != ""
    ) {
        var obj = {};
        obj.ACCOUNTNO = $("#ContentPlaceHolder1_txtACNo").val();
        jQuery.ajax({
            url: 'frmFIVendMaster.aspx/GetAccountNumber',
            type: "POST",
            dataType: "json",
            data: JSON.stringify(obj),
            //data: {
            //    "SONo": $("#txtSoNo").val()
            //},
            contentType: "application/json; charset=utf-8",
            success: function (d) {
                var result = (d.d.replace("\"", "")).replace("\"", "")
                if (result == 'true') {
                    $("#lblAccntNoExist").css("display", "block");
                    Isvalidate = false;
                }
                else {
                    $("#lblAccntNoExist").css("display", "none");
                }
                return d;
            },
            error: function (response) {
                return true;
            }
        });
    }
    else {
        return false;
    }
    return false;
}


function ValidateGenerateInsurance() {
    var Isvalidate = true;
    $("#lblInvoicealert").css("display", "none");
    $("#lblimeialert").css("display", "none");

    if ($("#fuImeiImage").get(0).files.length == 0) {
        $("#lblimeialert").css("display", "block");
        Isvalidate = false;
    }

    if ($("#fuinvoiceimage").get(0).files.length == 0) {
        $("#lblInvoicealert").css("display", "block");
        Isvalidate = false;
    }

    if ($("#fuinvoiceimage").get(0).files.length > 0) {
        var isimglfile = false;
        var filename = $("#fuinvoiceimage").get(0).files[0].name;
        var fileextensionarray = filename.split(".");
        var fileextension = fileextensionarray[(fileextensionarray.length - 1)];
        if ((fileextension.toUpperCase()).includes("JPEG") || (fileextension.toUpperCase()).includes("JPG") || (fileextension.toUpperCase()).includes("TIFF")
            || (fileextension.toUpperCase()).includes("PNG") || (fileextension.toUpperCase()).includes("PDF")) {
            var isimglfile = true;
        }

        if (isimglfile == false) {
            $("#lblInvoicealert").text("Please Select the only JPEG,JPG,TIFF,PNG,PDF.");
            $("#lblInvoicealert").css("display", "block");
            Isvalidate = false;
        }
    }

    if ($("#fuImeiImage").get(0).files.length > 0) {
        var isimglfile = false;
        var filename = $("#fuImeiImage").get(0).files[0].name;
        var fileextensionarray = filename.split(".");
        var fileextension = fileextensionarray[(fileextensionarray.length - 1)];
        if ((fileextension.toUpperCase()).includes("JPEG") || (fileextension.toUpperCase()).includes("JPG") || (fileextension.toUpperCase()).includes("TIFF")
            || (fileextension.toUpperCase()).includes("PNG") || (fileextension.toUpperCase()).includes("PDF")
        ) {
            var isimglfile = true;
        }

        if (isimglfile == false) {
            $("#lblimeialert").text("Please Select the only JPEG,JPG,TIFF,PNG,PDF.");
            $("#lblimeialert").css("display", "block");
            Isvalidate = false;
        }
    }
    return Isvalidate;
}

function updateTimers() {
    var rows = document.querySelectorAll("[id^='timer_']");
    var now = new Date();

    rows.forEach(function (row) {
        var startTime = new Date((row.id.split('_')[2]));
        var elapsed = startTime - now;

        // Time calculations for days, hours, minutes and seconds
        var days = Math.floor(elapsed / (1000 * 60 * 60 * 24));
        var hours = Math.floor((elapsed % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        var minutes = Math.floor((elapsed % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((elapsed % (1000 * 60)) / 1000);

        row.innerText = days + "d " + hours + "h "
            + minutes + "m " + seconds + "s ";

        if (elapsed < 0) {
            row.innerText = "OVERDUE";
        }

        //var hours       = Math.floor(elapsed / 3600000);
        //var minutes     = Math.floor((elapsed % 3600000) / 60000);
        //var seconds     = Math.floor((elapsed % 60000) / 1000);

        //row.innerText = hours.toString().padStart(2, '0') + ":" +
        //    minutes.toString().padStart(2, '0') + ":" +
        //    seconds.toString().padStart(2, '0');
    });
}


function ValidateFIPanNo() {
    debugger
    var Isvalidate = true;
    var panno = $("#ContentPlaceHolder1_txtPAN").val();
    if (panno.length > 0) {
        var obj = {};
        obj.panno = panno;
        jQuery.ajax({
            url: 'frmFIVendMaster.aspx/GetPanno',
            type: "POST",
            dataType: "json",
            data: JSON.stringify(obj),
            aysync: false,
            contentType: "application/json; charset=utf-8",
            success: function (d) {
                debugger
                var objresponse = JSON.parse(d.d);
                if (objresponse == "true") {
                    Isvalidate = false;
                    $("#lblpanno").css("display", "block");
                    return false;
                }
                else {
                    $("#lblpanno").css("display", "none");
                }
            },
            error: function (response) {
                var objresponse = JSON.parse(response.responseJSON);
                if (objresponse == "true") {
                    Isvalidate = false;
                }
            }
        });
    }
    return Isvalidate;
}

function ValidateFIAdharNo() {
    var aadharno = $("#ContentPlaceHolder1_txtAadharNo").val();
    if (aadharno.length > 0) {
        var obj = {};
        obj.aadharno = aadharno;
        jQuery.ajax({
            url: 'frmFIVendMaster.aspx/GetAdharNo',
            type: "POST",
            dataType: "json",
            data: JSON.stringify(obj),
            contentType: "application/json; charset=utf-8",
            success: function (d) {
                debugger
                var objresponse = JSON.parse(d.d);
                if (objresponse == "true") {
                    Isvalidate = false;
                    $("#lbladharno").css("display", "block");
                    return false;
                }
                else {
                    $("#lbladharno").css("display", "none");
                }
            },
            error: function (response) {
                debugger
                var objresponse = JSON.parse(response.responseJSON);
                if (objresponse == "true") {
                    Isvalidate = false;
                }
            }
        });
    }
}

function ValidateFIGSTNO() {
    debugger
    if ($("#ContentPlaceHolder1_txtGST").val() != ""
    ) {
        var obj = {};
        obj.GSTNO = $("#ContentPlaceHolder1_txtGST").val();
        jQuery.ajax({
            url: 'frmFIVendMaster.aspx/GetGSTNo',
            type: "POST",
            dataType: "json",
            data: JSON.stringify(obj),
            //data: {
            //    "SONo": $("#txtSoNo").val()
            //},
            contentType: "application/json; charset=utf-8",
            success: function (d) {
                debugger;
                var result = (d.d.replace("\"", "")).replace("\"", "")
                if (result == 'true') {
                    $("#ContentPlaceHolder1_lblgst").css("display", "block");
                    Isvalidate = false;
                }
                else {
                    $("#ContentPlaceHolder1_lblgst").css("display", "none");
                }
                return d;
            },
            error: function (response) {
                debugger
                return true;
            }
        });
    }
    else {
        return false;
    }
    return false;
}

function ValidateFIPanAdharbtn() {
    debugger
    var Isvalidate = true;

    if ($("#ContentPlaceHolder1_txtPAN").val().length > 0) {
        if ($("#lblpanno").css("display") == "block") {
            Isvalidate = false;
        }
    }

    if ($("#ContentPlaceHolder1_txtAadharNo").val().length > 0) {
        if ($("#lbladharno").css("display") == "block") {
            Isvalidate = false;
        }
    }
    return Isvalidate;
}


function ValidateGSTCertificatebtn() {
    var Isvalidate = true;
    if ($("#ContentPlaceHolder1_txtGST").val().length > 0) {
        if ($("#ContentPlaceHolder1_lblgst").css("display") == "block") {
            Isvalidate = false;
        }
    }
    return Isvalidate;
}









