<%@ Page Title="View Quotation" Language="C#" MasterPageFile="~/UTILITY/UtilityModule.Master" AutoEventWireup="true" CodeBehind="ViewQuotation.aspx.cs" Inherits="ShERPa360net.UTILITY.ViewQuotation" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>View Quotation</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />

    <style>
        .newRW {
            text-align-last: right !important;
        }

        .newStock {
            vertical-align: middle !important;
            border: none !important;
            border-style: none !important;
            border-width: 0px !important;
        }

        /* body .table tbody tr th {
            position: sticky;
            top: -1px;
        }*/
    </style>

    <script>
        $(document).ready(function () {
            BindMakeAssociateModel();
            //$("#myInput").on("keyup", function () {
            //    var value = $(this).val().toLowerCase();
            //    $("#ContentPlaceHolder1_gvList tr ").filter(function () {
            //        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            //    });
            //});
        });
        function BindMakeAssociateModel() {
            //debugger
            if ($("#ContentPlaceHolder1_gvList tr").length > 2) {
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
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <rpt:ReportViewer ID="ReportViewer1" runat="server" SizeToReportContent="true" Visible="false"></rpt:ReportViewer>

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>Quotation</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Date From : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">To : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Quotation No. : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtQuotNo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSearch" CssClass="btn btn-success pull-left" Text="Search Quotation" OnClick="lnkSearch_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export Quotation" OnClick="lnkExport_Click"><i class="fa fa-file"></i></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                                <div id="mobile_View">
                                    <div class="col-md-1" style="margin-top: 5px;">&nbsp;</div>
                                    <div class="col-md-3" style="margin-top: 5px;">
                                        <div class="form-group">
                                            <div class="col-md-9 col-xs-12">
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
                        <div class="row">
                            <div class="col-md-12">
                                <div class="box">
                                    <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                        CellSpacing="0" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%">
                                        <EmptyDataTemplate>
                                            No Record Found!
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="QUOTNO" HeaderText="Quot. No." />
                                            <asp:BoundField DataField="QUOTDTD" HeaderText="Quot. Dt." />
                                            <asp:BoundField DataField="PLANTCD" HeaderText="Plant Code" />
                                            <asp:BoundField DataField="QUOTAMT" HeaderText="Quot. Amt" />
                                            <asp:BoundField DataField="APRVSTATUS" HeaderText="Aprv. Status" />
                                            <asp:BoundField DataField="USERNAME" HeaderText="Aprv. By" />
                                            <asp:BoundField DataField="ENTEREDBY" HeaderText="Entered By" />
                                            <asp:BoundField DataField="UPDATEBY" HeaderText="Updated By" />
                                            <asp:TemplateField HeaderText="Action" Visible="True">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="btnDetails" Text="Details" OnClick="btnDetails_Click"></asp:LinkButton>
                                                    | 
                                                        <asp:LinkButton runat="server" ID="btnDownload" Text="Download" OnClick="btnDownload_Click"></asp:LinkButton>
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

    <div class="modal fade" id="modal-detail" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Quotation</strong> Details</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-12">

                                    <div class="col-md-6">
                                        <div class="form-group" style="text-align-last: center;">
                                            <asp:Label ID="lblDoctype" runat="server" CssClass="pull-left" Style="font-size: 15px;"></asp:Label>
                                            <br />
                                            <%--<asp:HiddenField ID="hfDeptID" runat="server" />--%>
                                            <asp:HiddenField ID="hfSeq" runat="server" />
                                            <asp:HiddenField ID="hfPlant" runat="server" />
                                            <asp:HiddenField ID="hfPODate" runat="server" />
                                            <asp:HiddenField ID="hfMail" runat="server" />
                                            <asp:HiddenField ID="hfMobile" runat="server" />
                                        </div>
                                    </div>
                                    <%--<div class="col-md-6">
                                        <div class="form-group" style="text-align-last: center;">
                                            <asp:LinkButton runat="server" ID="lnkQuotEdit" CssClass="btn btn-success pull-right" Text="Edit Quotation" OnClick="lnkQuotEdit_Click" Visible="true"><i class="fa fa-edit"></i> Edit</asp:LinkButton>
                                        </div>
                                    </div>--%>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Quotation To :</label>
                                        <asp:TextBox ID="txtInvoiceTo" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine" Style="height: 120px; width: 410px;"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Delivery To :</label>
                                        <asp:TextBox ID="txtDeliveryTo" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine" Style="height: 120px; width: 410px;"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Supplier :</label>
                                        <asp:TextBox ID="txtSupplier" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine" Style="height: 120px; width: 410px;"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Quotation Details :</label>
                                        <asp:TextBox ID="txtPODetail" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine" Style="height: 120px; width: 410px;"></asp:TextBox>
                                        <asp:Label ID="lblPONo" runat="server" CssClass="form-control" Visible="false"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-2" style="margin-top: 5px;">
                                    <div class="form-group">
                                        <label>Sale Amount :</label>
                                        <asp:Label ID="lblMaterialAmt" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2" style="margin-top: 5px;">
                                    <div class="form-group">
                                        <label>Base Amount :</label>
                                        <asp:Label ID="lblOtherChg" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2" style="margin-top: 5px;">
                                    <div class="form-group">
                                        <label>Discount Per. (%) :</label>
                                        <asp:Label ID="lblDiscountpercentage" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2" style="margin-top: 5px;">
                                    <div class="form-group">
                                        <label>Discount Amount :</label>
                                        <asp:Label ID="lblDiscountAmt" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2" style="margin-top: 5px;">
                                    <div class="form-group">
                                        <label>Tax Amount :</label>
                                        <asp:Label ID="lblTaxAmt" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2" style="margin-top: 5px;">
                                    <div class="form-group">
                                        <label>Total Amount :</label>
                                        <asp:Label ID="lblPOTotalAmt" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="box">
                                    <div class="page-content-wrap">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="panel panel-default tabs">
                                                    <ul class="nav nav-tabs" role="tablist">
                                                        <li class="active"><a href="#tab-first" role="tab" data-toggle="tab">Material</a></li>
                                                        <li><a href="#tab-second" role="tab" data-toggle="tab">Taxation </a></li>
                                                        <%--<li><a href="#tab-third" role="tab" data-toggle="tab">Other Charges </a></li>--%>
                                                    </ul>
                                                    <div class="panel-body tab-content">
                                                        <div class="tab-pane active" id="tab-first">
                                                            <div class="box-body divhorizontal" style="overflow-x: scroll; overflow-y: scroll;">
                                                                <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                                                    CssClass="table table-hover table-striped table-bordered nowrap" ShowHeader="true" GridLines="None">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="QUOTNO" HeaderText="Quot. No." />
                                                                        <asp:BoundField DataField="QUOTID" HeaderText="Quot. Sr. No." />
                                                                        <asp:BoundField DataField="ITEMCODE" HeaderText="Article Code" />
                                                                        <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
                                                                        <asp:BoundField DataField="TRACKNO" HeaderText="Job ID" />
                                                                        <asp:BoundField DataField="IMEINO" HeaderText="Serial No." />
                                                                        <asp:BoundField DataField="ITEMGROUP" HeaderText="Item Group" />
                                                                        <asp:BoundField DataField="ITEMUOM" HeaderText="UOM" />
                                                                        <asp:BoundField DataField="POQTY" HeaderText="Qty." />
                                                                        <%--<asp:BoundField DataField="PURCHASERATE" HeaderText="Purchase Price" />--%>
                                                                        <asp:BoundField DataField="MRP" HeaderText="MRP" />
                                                                        <asp:BoundField DataField="ITEMBRATE" HeaderText="Sale Amt." />
                                                                        <asp:BoundField DataField="CAMOUNT" HeaderText="Base Amt." />
                                                                        <asp:BoundField DataField="DISCOUNTAMT" HeaderText="Disc. Amt." />
                                                                        <asp:BoundField DataField="TAXABLE" HeaderText="Taxable Amt." />
                                                                        <asp:BoundField DataField="TAXAMT" HeaderText="Tax Amt." />
                                                                        <asp:BoundField DataField="ITEMPLANTCD" HeaderText="Plant" />
                                                                        <asp:BoundField DataField="ITEMLOCCD" HeaderText="Location" />
                                                                        <asp:BoundField DataField="COSTCENTER" HeaderText="Cost Center" />
                                                                        <asp:BoundField DataField="MAKE" HeaderText="Make" />
                                                                        <asp:BoundField DataField="MODEL" HeaderText="Model" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>
                                                        <div class="tab-pane" id="tab-second">
                                                            <div class="box-body divhorizontal" style="overflow-x: scroll; overflow-y: scroll; height: 300px;">
                                                                <asp:GridView ID="grvTaxation" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                                                    CssClass="table table-hover table-striped table-bordered nowrap">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="TAXSRNO" HeaderText="Sr. No." />
                                                                        <asp:BoundField DataField="POSRNO" HeaderText="Quot. Sr. No." />
                                                                        <asp:BoundField DataField="CONDTYPE" HeaderText="Cond. Type" />
                                                                        <asp:BoundField DataField="TAXRATE" HeaderText="Rate" />
                                                                        <asp:BoundField DataField="TAXBASEAMOUNT" HeaderText="Base Amt." />
                                                                        <asp:BoundField DataField="TAXAMOUNT" HeaderText="Tax Amt." />
                                                                        <asp:BoundField DataField="OPERATOR" HeaderText="Operator" />
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
                            </div>

                            <div class="col-md-5" style="margin-top: 5px;">
                                <div class="form-group">
                                    <asp:Label ID="lblAPRV1" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>

                            <%-- <div class="col-md-3" style="margin-top: 5px;">
                                <div class="form-group">
                                    <asp:TextBox ID="txtApRejDetReason" runat="server" CssClass="form-control" placeholder="Approve / Reject Reason" MaxLength="100"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtApRejDetReason" ValidationGroup="ValDetRej">Enter Reject Reason</asp:RequiredFieldValidator>
                                    <br />
                                    <asp:LinkButton runat="server" ID="lnkReject" CssClass="btn btn-success pull-left" Text="Reject Quotation" OnClick="lnkReject_Click" Visible="false" ValidationGroup="ValDetRej"><i class="fa fa-times-circle"></i> Reject</asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="lnkApprove" CssClass="btn btn-success pull-left" Text="Approve Quotation" OnClick="lnkApprove_Click" Visible="false"><i class="fa fa-check-circle"></i> Approve</asp:LinkButton>
                                </div>
                            </div>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmViewCromaQuotation" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmUtility" runat="server" />

</asp:Content>
