<%@ Page Title="One Window Report" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptOneWindowTracker.aspx.cs" Inherits="ShERPa360net.REPORTS.rptOneWindowTracker" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>One Window Report</title>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <%--<link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />--%>

    <script>
        $(document).ready(function () {
            BindMakeAssociateModel();
        });

        function BindMakeAssociateModel() {
            $("#ContentPlaceHolder1_grvData").DataTable({
                paging: false,
                dom: 'Bfrtip',
                destroy: true,
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

    </script>

    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            /*debugger;*/
            document.getElementById("busy-holder1").style.display = "";
            document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;One Window Tracker</strong></h3>
                        </div>

                        <div class="panel-body">

                            <div class="col-md-12">
                                <div class="row">


                                    <div class="col-md-3">
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label"></label>
                                                <div class="col-md-7 col-xs-12">
                                                    <asp:LinkButton runat="server" ID="lnkSerch" CssClass="btn btn-success" Text="Search Rate" OnClick="lnkSerch_Click" ValidationGroup="SaveAll"><span tooltip="Search" flow="down"><i class="fa fa-search"> View Report</i></span></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row" style="margin-top: 20px !important;">

                            <div class="col-md-12">
                                <div class="box-body divhorizontal" style="overflow-x: scroll; overflow-y: scroll; max-height: 500px !important;">
                                    <asp:GridView ID="grvData" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                        CellSpacing="0" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%">
                                        <EmptyDataTemplate>
                                            No Record Found!
                                        </EmptyDataTemplate>
                                        <Columns>

                                            <asp:TemplateField HeaderText="Purchase from - (Vendor Name)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVendorName" runat="server" Text='<%# Eval("Vendor Name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="RP NO / Lot No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLotNo" runat="server" Text='<%# Eval("RP NO / Lot No") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bill no">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBillNo" runat="server" Text='<%# Eval("Bill no") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bill Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBillDate" runat="server" Text='<%# Eval("Bill Date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="INWARD DATE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInwardDate" runat="server" Text='<%# Eval("INWARD DATE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="TAT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTAT" runat="server" Text='<%# Eval("TAT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Ageing Cat">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAgeingCat" runat="server" Text='<%# Eval("Ageing Cat") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Article Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblArticleNumber" runat="server" Text='<%# Eval("Article Number") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item-Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemDesc" runat="server" Text='<%# Eval("Item-Description") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Brand">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBrand" runat="server" Text='<%# Eval("Brand") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Capacity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCapacity" runat="server" Text='<%# Eval("Capacity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="SIZE (S,M,L)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSize" runat="server" Text='<%# Eval("SIZE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Category (AC, WM, REF)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCategory" runat="server" Text='<%# Eval("Category") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Serial No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSerialNo" runat="server" Text='<%# Eval("Serial No") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Job id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblJobID" runat="server" Text='<%# Eval("job id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Purchase Price">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPurchasePrice" runat="server" Text='<%# Eval("Purchase Price") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="MRP">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMRP" runat="server" Text='<%# Eval("MRP") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Partner Price">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPartnerPrice" runat="server" Text='<%# Eval("Partner Price") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Customer Price">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustomerPrice" runat="server" Text='<%# Eval("Customer Price") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Online Price">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOnlinePrice" runat="server" Text='<%# Eval("Online Price") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Online Price Updated Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOnlinePriceUpdateDate" runat="server" Text='<%# Eval("Online Price Updated Date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="INWARD GRADE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInwardGrade" runat="server" Text='<%# Eval("INWARD GRADE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Curr Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurrStatus" runat="server" Text='<%# Eval("Curr Status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="WHY IN PNA">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblWhyInPNA" runat="server" Text='<%# Eval("WHY IN PNA") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="RFD DATE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRFDDATE" runat="server" Text='<%# Eval("RFD DATE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Parts order date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPartsorderdate" runat="server" Text='<%# Eval("Parts order date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ETA">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblETA" runat="server" Text='<%# Eval("ETA") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ETA RESPONSIBLE PERSON">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblETARESPONSIBLEPERSON" runat="server" Text='<%# Eval("ETA RESPONSIBLE PERSON") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Parts rec date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPartsrecdate" runat="server" Text='<%# Eval("Parts rec date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Parts Tat">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPartsTat" runat="server" Text='<%# Eval("Parts Tat") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Outward DC">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOutwardDC" runat="server" Text='<%# Eval("Outward DC") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Outward for repair">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOutwardforrepair" runat="server" Text='<%# Eval("Outward for repair") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Inward after repair">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInwardafterrepair" runat="server" Text='<%# Eval("Inward after repair") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Repair TAT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRepairTAT" runat="server" Text='<%# Eval("Repair TAT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Repair Cost">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRepairCost" runat="server" Text='<%# Eval("Repair Cost") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Packing & misc">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPackingmisc" runat="server" Text='<%# Eval("Packing & misc") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Logistics cost">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLogisticscost" runat="server" Text='<%# Eval("Logistics cost") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total Cost">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalCost" runat="server" Text='<%# Eval("Total Cost") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Booked Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBookedDate" runat="server" Text='<%# Eval("Booked Date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Booked by">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBookedby" runat="server" Text='<%# Eval("Booked by") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="So No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSoNo" runat="server" Text='<%# Eval("So No") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="SO Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSODate" runat="server" Text='<%# Eval("SO Date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Invoice no">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoiceno" runat="server" Text='<%# Eval("Invoice no") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Invoice date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoicedate" runat="server" Text='<%# Eval("Invoice date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Invoice TAT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoiceTAT" runat="server" Text='<%# Eval("Invoice TAT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Delivered Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDeliveredStatus" runat="server" Text='<%# Eval("Delivered Status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Vehicle no">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVehicleno" runat="server" Text='<%# Eval("Vehicle no") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Type of customer">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTypeofcustomer" runat="server" Text='<%# Eval("Type of customer") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Customer name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustomername" runat="server" Text='<%# Eval("Customer name") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cust Details">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustDetails" runat="server" Text='<%# Eval("Cust Details") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Invoice Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInvoiceAmount" runat="server" Text='<%# Eval("Invoice Amount") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Margin">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMargin" runat="server" Text='<%# Eval("Margin") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Payment date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPaymentdate" runat="server" Text='<%# Eval("Payment date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="MODE OF PAYMENT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMODEOFPAYMENT" runat="server" Text='<%# Eval("MODE OF PAYMENT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Transaction detail">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTransactiondetail" runat="server" Text='<%# Eval("Transaction detail") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sales Person">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSalesPerson" runat="server" Text='<%# Eval("Sales Person") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Installation Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInstallationDate" runat="server" Text='<%# Eval("Installation Date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Customer Feedback">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustomerFeedback" runat="server" Text='<%# Eval("Customer Feedback") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cust Complaints">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCustComplaints" runat="server" Text='<%# Eval("Cust Complaints") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Marketing Ref">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMarketingRef" runat="server" Text='<%# Eval("Marketing Ref") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Booking status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBookingstatus" runat="server" Text='<%# Eval("Booking status") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="EW">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblEW" runat="server" Text='<%# Eval("EW") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Store Location">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStoreLocation" runat="server" Text='<%# Eval("Store Location") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Booked or not">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBookedornot" runat="server" Text='<%# Eval("Booked or not") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmRptSingleWindowTracker" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptMIS" runat="server" />

</asp:Content>
