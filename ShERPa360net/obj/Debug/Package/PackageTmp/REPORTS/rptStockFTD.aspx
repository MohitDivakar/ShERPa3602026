<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptStockFTD.aspx.cs" Inherits="ShERPa360net.REPORTS.rptStockFTD" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Stock FTD</title>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />


    <script>
        $(document).ready(function () {
            BindGrid();
        });
        function BindGrid() {

            $("#ContentPlaceHolder1_gvList").DataTable({
                dom: 'Bfrtip',
                "pageLength": 15,
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

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Daily Stock Summary</strong></h3>
                        <%--<asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-right" Text="Export" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
                        <asp:LinkButton runat="server" ID="lnkDetails" CssClass="btn btn-success pull-right" Text="Details" PostBackUrl="~/REPORTS/rptReturnHistory.aspx"><span tooltip="Detail" flow="down"><i class="fa fa-list"></i> </span></asp:LinkButton>--%>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="CMPID" HeaderText="CMP ID" Visible="false" />
                                                <asp:BoundField DataField="PLANTCD" HeaderText="Plant Code" Visible="false" />
                                                <asp:BoundField DataField="DESCR" HeaderText="Plant" />
                                                <asp:BoundField DataField="PHY DOC VAR" HeaderText="Phy. Doc. <br/>Var." HtmlEncode="false" />
                                                <asp:BoundField DataField="ON FLOOR STOCK" HeaderText="On Floor <br/>Stock" HtmlEncode="false" />
                                                <asp:BoundField DataField="TOTAL PHY STOCK" HeaderText="Total Phy. <br/>Stock" HtmlEncode="false" />
                                                <asp:BoundField DataField="PURCHASE FTD" HeaderText="Purchase <br/>FTD" HtmlEncode="false" />
                                                <asp:BoundField DataField="WEBSITE RETURN" HeaderText="Return Received <br/>From Website Cust." HtmlEncode="false" />
                                                <asp:BoundField DataField="AMAZON RETURN" HeaderText="Return Received  <br/>From Amazon Cust." HtmlEncode="false" />
                                                <asp:BoundField DataField="OTHER RETURN" HeaderText="Return Received  <br/>From Other Cust." HtmlEncode="false" />
                                                <asp:BoundField DataField="STO INWARD" HeaderText="Stock Received  <br/>From Other Center" HtmlEncode="false" />
                                                <asp:BoundField DataField="TOTAL INWARD" HeaderText="Total <br/>Inward" HtmlEncode="false" />
                                                <asp:BoundField DataField="DISPATCH FTD" HeaderText="Dispatch <br/>FTD" HtmlEncode="false" />
                                                <asp:BoundField DataField="RETURN TO VENDOR" HeaderText="Handset Return <br/>to Vendor" HtmlEncode="false" />
                                                <asp:BoundField DataField="AT ASC" HeaderText="Handset given to <br/>ASC/Other work" HtmlEncode="false" />
                                                <asp:BoundField DataField="DUMMY RCVD" HeaderText="Dummy Handset <br/>Received" HtmlEncode="false" />
                                                <asp:BoundField DataField="STO OUTWARD" HeaderText="Stock Transfer to <br/>Other Center" HtmlEncode="false" />
                                                <asp:BoundField DataField="TOTAL OUTWARD" HeaderText="Total <br/>Outward" HtmlEncode="false" />
                                                <asp:BoundField DataField="PHY STOCK DERIVED" HeaderText="Physical Closing <br/>Stock (Derrived)" HtmlEncode="false" />
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

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmRptMMFTD" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptMM" runat="server" />

</asp:Content>
