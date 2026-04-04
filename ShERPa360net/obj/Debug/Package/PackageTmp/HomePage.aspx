<%@ Page Title="" Language="C#" MasterPageFile="~/MasterSherpa360.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="ShERPa360net.HomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Dashboardcss/style.bundle.css" rel="stylesheet" type="text/css" />
    <link href="../Dashboardcss/Dashboard.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container-fluid pt-5">
        <div class="row">
            <div class="col-sm-4 mb-5 mb-xl-10">
                <a href="MM/MMDashboard.aspx">
                    <!--begin::List widget 2-->
                    <div class="card card-flush h-lg-100 border-ra-top ">
                        <!--begin::Header-->
                        <div class="card-header  border-top-none">
                            <!--begin::Title-->
                            <h3 class="card-title align-items-start flex-column">
                                <span class="card-label fw-bold text-dark">MM</span>
                                <span class="text-gray-400 mt-1 fw-semibold fs-6">Material Movement</span>
                            </h3>
                            <!--end::Title-->
                            <!--begin::Toolbar-->
                            <div class="card-toolbar">


                                <img src="Dashboardimg/MM.jpeg">

                                <!--end::Menu-->
                            </div>
                            <!--end::Toolbar-->
                        </div>
                        <!--end::Header-->
                        <!--begin::Body-->

                        <div class="card-body pt-5">
                            <!--begin::Item-->
                            <div class="d-flex">
                                <!--begin::Section-->
                                <div class="d-flex align-items-senter">

                                    <!--end::Svg Icon-->
                                    <!--begin::Number-->
                                    <span class="text-gray-900 fw-bolder fs-6"><i class='bx bxs-image-add icn-h'></i></span>

                                </div>
                                <div class="text-gray-700 fw-semibold fs-6 me-2"><span class="text-primary opacity-75-hover fs-6 fw-semibold"><a href="MM/ViewMR.aspx">Create MR</a></span></div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>


                            <!--begin::Separator-->
                            <div class="separator separator-dashed my-3"></div>
                            <!--end::Separator-->
                            <!--begin::Item-->
                            <div class="d-flex ">
                                <!--begin::Section-->
                                <div class="d-flex align-items-senter">
                                    <!--end::Svg Icon-->
                                    <!--begin::Number-->
                                    <span class="text-gray-900 fw-bolder fs-6"><i class='bx bx-message-square-add icn-h'></i></span>
                                </div>
                                <div class="text-gray-700 fw-semibold fs-6 me-2"><span class="text-primary opacity-75-hover fs-6 fw-semibold"><a href="MM/AprvMR.aspx">Approve MR </a></span></div>
                                <!--end::Section-->
                                <!--begin::Statistics-->
                                <!--end::Statistics-->
                            </div>
                            <!--end::Item-->
                            <!--begin::Separator-->
                            <div class="separator separator-dashed my-3"></div>
                            <!--end::Separator-->
                            <!--begin::Item-->
                            <div class="d-flex">
                                <!--begin::Section-->
                                <div class="d-flex align-items-senter">
                                    <!--end::Svg Icon-->
                                    <!--begin::Number-->
                                    <span class="text-gray-900 fw-bolder fs-6"><i class='bx bxs-user-detail icn-h'></i></span>
                                </div>
                                <div class="text-gray-700 fw-semibold fs-6 me-2"><span class="text-primary opacity-75-hover fs-6 fw-semibold"><a href="MM/ViewMaterialInwardList.aspx">Material Inward(PO)</a></span></div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>

                            <div class="separator separator-dashed my-3"></div>
                            <div class="d-flex">
                                <!--begin::Section-->

                                <div class="text-gray-700 fw-semibold fs-6 me-2">
                                    <a href="MM/MMDashboard.aspx"><i class="bx bx-dots-horizontal-rounded icn-blue"></i></a>

                                </div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>

                            <!--end::Item-->
                        </div>
                        <!--end::Body-->
                    </div>
                    <!--end::List widget 2-->
                </a>
            </div>


            <div class="col-sm-4 mb-5 mb-xl-10">
                <!--begin::List widget 2-->
                <a href="UTILITY/UtilityDashboard.aspx">
                    <div class="card card-flush h-lg-100 border-ra-top">
                        <!--begin::Header-->
                        <div class="card-header border-top-none">
                            <!--begin::Title-->
                            <h3 class="card-title align-items-start flex-column">
                                <span class="card-label fw-bold text-dark">Mobex Seller
                                </span>
                                <span class="text-gray-400 mt-1 fw-semibold fs-6">Project Mobex Seller</span>
                            </h3>
                            <!--end::Title-->
                            <!--begin::Toolbar-->
                            <div class="card-toolbar">
                                <img src="Dashboardimg/utillity.jpeg">

                                <!--end::Menu-->
                            </div>
                            <!--end::Toolbar-->
                        </div>
                        <!--end::Header-->
                        <!--begin::Body-->
                        <div class="card-body pt-5">
                            <!--begin::Item-->
                            <div class="d-flex ">
                                <!--begin::Section-->
                                <div class="d-flex align-items-senter">

                                    <!--end::Svg Icon-->
                                    <!--begin::Number-->
                                    <span class="text-gray-900 fw-bolder fs-6"><i class='bx bxs-component icn-h'></i></span>

                                </div>
                                <div class="text-gray-700 fw-semibold fs-6 me-2"><span class="text-primary opacity-75-hover fs-6 fw-semibold"><a href="UTILITY/VendorVisitEntry.aspx">Product Entry</a></span></div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>
                            <!--end::Item-->
                            <!--begin::Separator-->
                            <div class="separator separator-dashed my-3"></div>
                            <!--end::Separator-->
                            <!--begin::Item-->
                            <div class="d-flex ">
                                <!--begin::Section-->
                                <div class="d-flex align-items-senter">

                                    <!--end::Svg Icon-->
                                    <!--begin::Number-->
                                    <span class="text-gray-900 fw-bolder fs-6"><i class='bx bx-list-ul icn-h'></i></span>

                                </div>
                                <div class="text-gray-700 fw-semibold fs-6 me-2"><span class="text-primary opacity-75-hover fs-6 fw-semibold"><a href="UTILITY/ProductUnListedEntry.aspx">Product UnListed</a></span></div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>
                            <!--end::Item-->
                            <!--begin::Separator-->
                            <div class="separator separator-dashed my-3"></div>
                            <!--end::Separator-->
                            <!--begin::Item-->
                            <div class="d-flex ">
                                <!--begin::Section-->
                                <div class="d-flex align-items-senter">

                                    <!--end::Svg Icon-->
                                    <!--begin::Number-->
                                    <span class="text-gray-900 fw-bolder fs-6"><i class='bx bx-badge-check icn-h'></i></span>

                                </div>
                                <div class="text-gray-700 fw-semibold fs-6 me-2"><span class="text-primary opacity-75-hover fs-6 fw-semibold"><a href="UTILITY/ProductBlanccoQcApproveDetail.aspx">Blancco Qc Approval</a></span></div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>

                            <div class="separator separator-dashed my-3"></div>
                            <div class="d-flex">
                                <!--begin::Section-->

                                <div class="text-gray-700 fw-semibold fs-6 me-2">
                                    <a href="UTILITY/UtilityDashboard.aspx">
                                        <i class="bx bx-dots-horizontal-rounded icn-blue"></i>
                                    </a>
                                </div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>

                            <!--end::Item-->
                        </div>
                        <!--end::Body-->
                    </div>
                    <!--end::List widget 2-->
                </a>
            </div>

            <div class="col-sm-4 mb-5 mb-xl-10">
                <!--begin::List widget 2-->
                <a href="CRM/CustomerRltionShipMngDashBoard.aspx">
                    <div class="card card-flush h-lg-100 border-ra-top">
                        <!--begin::Header-->
                        <div class="card-header  border-top-none">
                            <!--begin::Title-->
                            <h3 class="card-title align-items-start flex-column">
                                <span class="card-label fw-bold text-dark">CRM
                                </span>
                                <span class="text-gray-400 mt-1 fw-semibold fs-6">Department</span>
                            </h3>
                            <!--end::Title-->
                            <!--begin::Toolbar-->
                            <div class="card-toolbar">
                                <img src="Dashboardimg/crm-icon-png-9.jpeg">

                                <!--end::Menu-->
                            </div>
                            <!--end::Toolbar-->
                        </div>
                        <!--end::Header-->
                        <!--begin::Body-->
                        <div class="card-body pt-5">
                            <!--begin::Item-->
                            <div class="d-flex ">
                                <!--begin::Section-->
                                <div class="d-flex align-items-senter">

                                    <!--end::Svg Icon-->
                                    <!--begin::Number-->
                                    <span class="text-gray-900 fw-bolder fs-6"><i class='bx bxs-group icn-h'></i></span>

                                </div>
                                <div class="text-gray-700 fw-semibold fs-6 me-2"><span class="text-primary opacity-75-hover fs-6 fw-semibold"><a href="CRM/LeadGeneration.aspx">Lead Generation </a></span></div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>
                            <!--end::Item-->
                            <!--begin::Separator-->
                            <div class="separator separator-dashed my-3"></div>
                            <!--end::Separator-->
                            <!--begin::Item-->
                            <div class="d-flex ">
                                <!--begin::Section-->
                                <div class="d-flex align-items-senter">

                                    <!--end::Svg Icon-->
                                    <!--begin::Number-->
                                    <span class="text-gray-900 fw-bolder fs-6"><i class='bx bx-history icn-h'></i></span>

                                </div>
                                <div class="text-gray-700 fw-semibold fs-6 me-2"><span class="text-primary opacity-75-hover fs-6 fw-semibold"><a href="CRM/frmLeadHistory.aspx">Lead History </a></span></div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>
                            <!--end::Item-->
                            <!--begin::Separator-->
                            <div class="separator separator-dashed my-3"></div>
                            <!--end::Separator-->
                            <!--begin::Item-->
                            <div class="d-flex ">
                                <!--begin::Section-->
                                <div class="d-flex align-items-senter">

                                    <!--end::Svg Icon-->
                                    <!--begin::Number-->
                                    <span class="text-gray-900 fw-bolder fs-6"><i class='bx bx-revision icn-h'></i></span>

                                </div>
                                <div class="text-gray-700 fw-semibold fs-6 me-2"><span class="text-primary opacity-75-hover fs-6 fw-semibold"><a href="CRM/frmViewDealer.aspx">Dealer Master  </a></span></div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>

                            <div class="separator separator-dashed my-3"></div>
                            <div class="d-flex">
                                <!--begin::Section-->

                                <div class="text-gray-700 fw-semibold fs-6 me-2">
                                    <a href="CRM/CustomerRltionShipMngDashBoard.aspx">
                                        <i class="bx bx-dots-horizontal-rounded icn-blue"></i>
                                    </a>
                                </div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>

                            <!--end::Item-->
                        </div>
                        <!--end::Body-->
                    </div>
                    <!--end::List widget 2-->
                </a>
            </div>

            <div class="col-sm-4 mb-5 mb-xl-10">
                <!--begin::List widget 2-->
                <a href="SD/SDDashBoard.aspx">
                    <div class="card card-flush h-lg-100 border-ra-top">
                        <!--begin::Header-->
                        <div class="card-header  border-top-none">
                            <!--begin::Title-->
                            <h3 class="card-title align-items-start flex-column">
                                <span class="card-label fw-bold text-dark">SD
                                </span>
                                <span class="text-gray-400 mt-1 fw-semibold fs-6">Department</span>
                            </h3>
                            <!--end::Title-->
                            <!--begin::Toolbar-->
                            <div class="card-toolbar">
                                <img src="Dashboardimg/sd1.jpeg">

                                <!--end::Menu-->
                            </div>
                            <!--end::Toolbar-->
                        </div>
                        <!--end::Header-->
                        <!--begin::Body-->
                        <div class="card-body pt-5">
                            <!--begin::Item-->
                            <div class="d-flex ">
                                <!--begin::Section-->
                                <div class="d-flex align-items-senter">

                                    <!--end::Svg Icon-->
                                    <!--begin::Number-->
                                    <span class="text-gray-900 fw-bolder fs-6"><i class='bx bx-search icn-h'></i></span>

                                </div>
                                <div class="text-gray-700 fw-semibold fs-6 me-2"><span class="text-primary opacity-75-hover fs-6 fw-semibold"><a href="UTILITY/frmNewCromaJobID.aspx">Croma Job ID Search </a></span></div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>
                            <!--end::Item-->
                            <!--begin::Separator-->
                            <div class="separator separator-dashed my-3"></div>
                            <!--end::Separator-->
                            <!--begin::Item-->
                            <div class="d-flex ">
                                <!--begin::Section-->
                                <div class="d-flex align-items-senter">

                                    <!--end::Svg Icon-->
                                    <!--begin::Number-->
                                    <span class="text-gray-900 fw-bolder fs-6"><i class='bx bx-laptop icn-h'></i></span>

                                </div>
                                <div class="text-gray-700 fw-semibold fs-6 me-2"><span class="text-primary opacity-75-hover fs-6 fw-semibold"><a href="SD/frmPOS.aspx">POS</a></span></div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>
                            <!--end::Item-->
                            <!--begin::Separator-->
                            <div class="separator separator-dashed my-3"></div>
                            <!--end::Separator-->
                            <!--begin::Item-->
                            <div class="d-flex ">
                                <!--begin::Section-->
                                <div class="d-flex align-items-senter">

                                    <!--end::Svg Icon-->
                                    <!--begin::Number-->
                                    <span class="text-gray-900 fw-bolder fs-6"><i class='bx bx-message-alt-edit  icn-h'></i></span>

                                </div>
                                <div class="text-gray-700 fw-semibold fs-6 me-2"><span class="text-primary opacity-75-hover fs-6 fw-semibold"><a href="SD/frmBulkSOUpload.aspx">Bulk SO Upload</a></span></div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>

                            <div class="separator separator-dashed my-3"></div>
                            <div class="d-flex">
                                <!--begin::Section-->

                                <div class="text-gray-700 fw-semibold fs-6 me-2">
                                    <a href="SD/SDDashBoard.aspx">
                                        <i class="bx bx-dots-horizontal-rounded icn-blue"></i>
                                    </a>
                                </div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>

                            <!--end::Item-->
                        </div>
                        <!--end::Body-->
                    </div>
                    <!--end::List widget 2-->
                </a>
            </div>

            <div class="col-sm-4 mb-5 mb-xl-10">
                <!--begin::List widget 2-->
                <a href="FI/FIDashBoard.aspx">
                    <div class="card card-flush h-lg-100 border-ra-top">
                        <!--begin::Header-->
                        <div class="card-header   border-top-none">
                            <!--begin::Title-->
                            <h3 class="card-title align-items-start flex-column">
                                <span class="card-label fw-bold text-dark">Accounting & Finance 
                                </span>
                                <span class="text-gray-400 mt-1 fw-semibold fs-6">Department</span>
                            </h3>
                            <!--end::Title-->
                            <!--begin::Toolbar-->
                            <div class="card-toolbar">
                                <img src="Dashboardimg/account.jpeg">

                                <!--end::Menu-->
                            </div>
                            <!--end::Toolbar-->
                        </div>
                        <!--end::Header-->
                        <!--begin::Body-->
                        <div class="card-body pt-5">
                            <!--begin::Item-->
                            <div class="d-flex ">
                                <!--begin::Section-->
                                <div class="d-flex align-items-senter">

                                    <!--end::Svg Icon-->
                                    <!--begin::Number-->
                                    <span class="text-gray-900 fw-bolder fs-6"><i class='bx bx-wallet-alt icn-h'></i></span>

                                </div>
                                <div class="text-gray-700 fw-semibold fs-6 me-2"><span class="text-primary opacity-75-hover fs-6 fw-semibold"><a href="FI/frmBankPayment.aspx">Bank Payment Entry </a></span></div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>
                            <!--end::Item-->
                            <!--begin::Separator-->
                            <div class="separator separator-dashed my-3"></div>
                            <!--end::Separator-->
                            <!--begin::Item-->
                            <div class="d-flex ">
                                <!--begin::Section-->
                                <div class="d-flex align-items-senter">

                                    <!--end::Svg Icon-->
                                    <!--begin::Number-->
                                    <span class="text-gray-900 fw-bolder fs-6"><i class='bx bxs-user-detail icn-h'></i></span>

                                </div>
                                <div class="text-gray-700 fw-semibold fs-6 me-2"><span class="text-primary opacity-75-hover fs-6 fw-semibold"><a href="FI/frmFIViewVendorMaster.aspx">Vendor Master </a></span></div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>
                            <!--end::Item-->


                            <div class="separator separator-dashed my-3"></div>
                            <div class="d-flex">
                                <!--begin::Section-->

                                <div class="text-gray-700 fw-semibold fs-6 me-2">
                                    <a href="FI/FIDashBoard.aspx">

                                        <i class="bx bx-dots-horizontal-rounded icn-blue"></i>
                                    </a>
                                </div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>

                            <!--end::Item-->
                        </div>
                        <!--end::Body-->
                    </div>

                    <!--end::List widget 2-->
                </a>
            </div>

            <div class="col-sm-4 mb-5 mb-xl-10">
                <!--begin::List widget 2-->
                <a href="REPORTS/ReportDashboard.aspx">
                    <div class="card card-flush h-lg-100 border-ra-top">
                        <!--begin::Header-->
                        <div class="card-header border-top-none">
                            <!--begin::Title-->
                            <h3 class="card-title align-items-start flex-column">
                                <span class="card-label fw-bold text-dark">Reports</span>
                                <span class="text-gray-400 mt-1 fw-semibold fs-6">Reporting Service</span>
                            </h3>
                            <!--end::Title-->
                            <!--begin::Toolbar-->
                            <div class="card-toolbar">
                                <img src="Dashboardimg/images.jpeg" style="height: 50px">

                                <!--end::Menu-->
                            </div>
                            <!--end::Toolbar-->
                        </div>
                        <!--end::Header-->
                        <!--begin::Body-->
                        <div class="card-body pt-5">
                            <!--begin::Item-->
                            <div class="d-flex ">
                                <!--begin::Section-->
                                <div class="d-flex align-items-senter">

                                    <!--end::Svg Icon-->
                                    <!--begin::Number-->
                                    <span class="text-gray-900 fw-bolder fs-6"><i class='bx bxs-report icn-h'></i></span>

                                </div>
                                <div class="text-gray-700 fw-semibold fs-6 me-2"><span class="text-primary opacity-75-hover fs-6 fw-semibold"><a href="REPORTS/rptCRM.aspx">Call Log Report</a></span></div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>
                            <!--end::Item-->
                            <!--begin::Separator-->
                            <div class="separator separator-dashed my-3"></div>
                            <!--end::Separator-->
                            <!--begin::Item-->
                            <div class="d-flex ">
                                <!--begin::Section-->
                                <div class="d-flex align-items-senter">

                                    <!--end::Svg Icon-->
                                    <!--begin::Number-->
                                    <span class="text-gray-900 fw-bolder fs-6"><i class='bx bxs-data icn-h'></i></span>

                                </div>
                                <div class="text-gray-700 fw-semibold fs-6 me-2"><span class="text-primary opacity-75-hover fs-6 fw-semibold"><a href="REPORTS/rptAllCallData.aspx">Call Data Report</a></span>   </div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>
                            <!--end::Item-->
                            <!--begin::Separator-->
                            <div class="separator separator-dashed my-3"></div>
                            <!--end::Separator-->
                            <!--begin::Item-->
                            <div class="d-flex ">
                                <!--begin::Section-->
                                <div class="d-flex align-items-senter">

                                    <!--end::Svg Icon-->
                                    <!--begin::Number-->
                                    <span class="text-gray-900 fw-bolder fs-6"><i class='bx bxs-report icn-h'></i></span>

                                </div>
                                <div class="text-gray-700 fw-semibold fs-6 me-2"><span class="text-primary opacity-75-hover fs-6 fw-semibold"><a href="REPORTS/rptCRMGrab.aspx">Grab Data Report</a></span></div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>

                            <div class="separator separator-dashed my-3"></div>
                            <div class="d-flex">
                                <!--begin::Section-->

                                <div class="text-gray-700 fw-semibold fs-6 me-2">
                                    <a href="REPORTS/ReportDashboard.aspx"><i class="bx bx-dots-horizontal-rounded icn-blue"></i></a>

                                </div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>

                            <!--end::Item-->
                        </div>
                        <!--end::Body-->
                    </div>
                    <!--end::List widget 2-->
                </a>
            </div>

            <div class="col-sm-4 mb-5 mb-xl-10">
                <!--begin::List widget 2-->
                <a href="UTILITY/UtilityModuleDashboard.aspx">
                    <div class="card card-flush h-lg-100 border-ra-top">
                        <!--begin::Header-->
                        <div class="card-header border-top-none">
                            <!--begin::Title-->
                            <h3 class="card-title align-items-start flex-column">
                                <span class="card-label fw-bold text-dark">Utility
                                </span>
                                <span class="text-gray-400 mt-1 fw-semibold fs-6">Project Utility</span>
                            </h3>
                            <!--end::Title-->
                            <!--begin::Toolbar-->
                            <div class="card-toolbar">
                                <img src="Dashboardimg/utillity.jpeg">

                                <!--end::Menu-->
                            </div>

                            <!--end::Toolbar-->
                        </div>
                        <!--end::Header-->
                        <!--begin::Body-->
                        <div class="card-body pt-5">
                            <!--begin::Item-->
                            <div class="d-flex ">
                                <!--begin::Section-->
                                <div class="d-flex align-items-senter">

                                    <!--end::Svg Icon-->
                                    <!--begin::Number-->
                                    <span class="text-gray-900 fw-bolder fs-6"><i class='bx bxs-component  icn-h'></i></span>

                                </div>
                                <div class="text-gray-700 fw-semibold fs-6 me-2"><span class="text-primary opacity-75-hover fs-6 fw-semibold"><a href="/UTILITY/frmItemPriceMaster.aspx">MRP Master</a></span></div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>
                            <!--end::Item-->
                            <!--begin::Separator-->
                            <div class="separator separator-dashed my-3"></div>
                            <!--end::Separator-->
                            <!--begin::Item-->
                            <div class="d-flex ">
                                <!--begin::Section-->
                                <div class="d-flex align-items-senter">

                                    <!--end::Svg Icon-->
                                    <!--begin::Number-->
                                    <span class="text-gray-900 fw-bolder fs-6"><i class='bx bx-list-ul icn-h'></i></span>

                                </div>
                                <div class="text-gray-700 fw-semibold fs-6 me-2"><span class="text-primary opacity-75-hover fs-6 fw-semibold"><a href="UTILITY/frmPopup.aspx">Lead Popup Show/Hide</a></span></div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>
                            <!--end::Item-->
                            <!--begin::Separator-->
                            <div class="separator separator-dashed my-3"></div>
                            <!--end::Separator-->
                            <!--begin::Item-->
                            <div class="d-flex ">
                                <!--begin::Section-->
                                <div class="d-flex align-items-senter">

                                    <!--end::Svg Icon-->
                                    <!--begin::Number-->
                                    <span class="text-gray-900 fw-bolder fs-6"><i class='bx bx-badge-check icn-h'></i></span>

                                </div>
                                <div class="text-gray-700 fw-semibold fs-6 me-2"><span class="text-primary opacity-75-hover fs-6 fw-semibold"><a href="UTILITY/frmPartPrice.aspx">Part Price Master</a></span></div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>

                            <div class="separator separator-dashed my-3"></div>
                            <div class="d-flex">
                                <!--begin::Section-->

                                <div class="text-gray-700 fw-semibold fs-6 me-2">
                                    <a href="UTILITY/UtilityModuleDashboard.aspx">
                                        <i class="bx bx-dots-horizontal-rounded icn-blue"></i>
                                    </a>
                                </div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>

                            <!--end::Item-->
                        </div>
                        <!--end::Body-->
                    </div>
                    <!--end::List widget 2-->
                </a>
            </div>


            <div class="col-sm-4 mb-5 mb-xl-10">
                <!--begin::List widget 2-->
                <a href="Logistic/LogisticDashboard.aspx">
                    <div class="card card-flush h-lg-100 border-ra-top">
                        <!--begin::Header-->
                        <div class="card-header border-top-none">
                            <!--begin::Title-->
                            <h3 class="card-title align-items-start flex-column">
                                <span class="card-label fw-bold text-dark">Logistic
                                </span>
                                <span class="text-gray-400 mt-1 fw-semibold fs-6">Department</span>
                            </h3>
                            <!--end::Title-->
                            <!--begin::Toolbar-->
                            <div class="card-toolbar">
                                <img src="Dashboardimg/logistic.png" height="65" width="50">

                                <!--end::Menu-->
                            </div>

                            <!--end::Toolbar-->
                        </div>
                        <!--end::Header-->
                        <!--begin::Body-->
                        <div class="card-body pt-5">
                            <!--begin::Item-->
                            <div class="d-flex ">
                                <!--begin::Section-->
                                <div class="d-flex align-items-senter">

                                    <!--end::Svg Icon-->
                                    <!--begin::Number-->
                                    <span class="text-gray-900 fw-bolder fs-6"><i class='bx bxs-component  icn-h'></i></span>

                                </div>
                                <div class="text-gray-700 fw-semibold fs-6 me-2"><span class="text-primary opacity-75-hover fs-6 fw-semibold"><a href="/Logistic/frmPickupAssign.aspx">Bluedart Assign</a></span></div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>
                            <!--end::Item-->
                            <!--begin::Separator-->
                            <div class="separator separator-dashed my-3"></div>
                            <!--end::Separator-->

                            <div class="d-flex ">
                                <!--begin::Section-->
                                <div class="d-flex align-items-senter">
                                    <!--end::Svg Icon-->
                                    <!--begin::Number-->
                                    <span class="text-gray-900 fw-bolder fs-6"><i class='bx bxs-component  icn-h'></i></span>
                                </div>
                                <div class="text-gray-700 fw-semibold fs-6 me-2"><span class="text-primary opacity-75-hover fs-6 fw-semibold"><a href="Logistic/frmPickupProduct.aspx">Reverse Pickup Assign</a></span></div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>
                            <!--end::Item-->

                            <!--begin::Separator-->
                            <div class="separator separator-dashed my-3"></div>
                            <!--end::Separator-->

                            <div class="d-flex">
                                <!--begin::Section-->

                                <div class="text-gray-700 fw-semibold fs-6 me-2">
                                    <a href="Logistic/LogisticDashboard.aspx">
                                        <i class="bx bx-dots-horizontal-rounded icn-blue"></i>
                                    </a>
                                </div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>

                            <!--end::Item-->
                        </div>
                        <!--end::Body-->
                    </div>
                    <!--end::List widget 2-->
                </a>
            </div>







            <%--Samsung TCR--%>




            <div class="col-sm-4 mb-5 mb-xl-10">
                <!--begin::List widget 2-->
                <a href="Samsung/SamsungDashboard.aspx">
                    <div class="card card-flush h-lg-100 border-ra-top">
                        <!--begin::Header-->
                        <div class="card-header border-top-none">
                            <!--begin::Title-->
                            <h3 class="card-title align-items-start flex-column">
                                <span class="card-label fw-bold text-dark">Samsung
                                </span>
                                <span class="text-gray-400 mt-1 fw-semibold fs-6">Department</span>
                            </h3>
                            <!--end::Title-->
                            <!--begin::Toolbar-->
                            <div class="card-toolbar">
                                <img src="Dashboardimg/samsung.png" height="65" width="100">

                                <!--end::Menu-->
                            </div>

                            <!--end::Toolbar-->
                        </div>
                        <!--end::Header-->
                        <!--begin::Body-->
                        <div class="card-body pt-5">
                            <!--begin::Item-->
                            <div class="d-flex ">
                                <!--begin::Section-->
                                <div class="d-flex align-items-senter">

                                    <!--end::Svg Icon-->
                                    <!--begin::Number-->
                                    <span class="text-gray-900 fw-bolder fs-6"><i class='bx bxs-component  icn-h'></i></span>

                                </div>
                                <div class="text-gray-700 fw-semibold fs-6 me-2"><span class="text-primary opacity-75-hover fs-6 fw-semibold"><a href="/Samsung/frmViewTCR.aspx">View TCR</a></span></div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>
                            <!--end::Item-->
                            <!--begin::Separator-->
                            <div class="separator separator-dashed my-3"></div>
                            <!--end::Separator-->

                            <div class="d-flex ">
                                <!--begin::Section-->
                                <div class="d-flex align-items-senter">
                                    <!--end::Svg Icon-->
                                    <!--begin::Number-->
                                    <span class="text-gray-900 fw-bolder fs-6"><i class='bx bxs-component  icn-h'></i></span>
                                </div>
                                <div class="text-gray-700 fw-semibold fs-6 me-2"><span class="text-primary opacity-75-hover fs-6 fw-semibold"><a href="Samsung/frmJobUpload.aspx">Upload Service Order</a></span></div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>
                            <!--end::Item-->

                            <!--begin::Separator-->
                            <div class="separator separator-dashed my-3"></div>
                            <!--end::Separator-->

                            <div class="d-flex ">
                                <!--begin::Section-->
                                <div class="d-flex align-items-senter">

                                    <!--end::Svg Icon-->
                                    <!--begin::Number-->
                                    <span class="text-gray-900 fw-bolder fs-6"><i class='bx bxs-component icn-h'></i></span>

                                </div>
                                <div class="text-gray-700 fw-semibold fs-6 me-2"><span class="text-primary opacity-75-hover fs-6 fw-semibold"><a href="Samsung/frmViewComplaint.aspx">View Service Order</a></span></div>
                                <!--end::Section-->
                                <!--begin::Statistics-->

                                <!--end::Statistics-->
                            </div>

                            <!--end::Item-->
                        </div>
                        <!--end::Body-->
                    </div>
                    <!--end::List widget 2-->
                </a>
            </div>


        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
</asp:Content>
