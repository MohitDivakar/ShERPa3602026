<%@ Page Title="Samsung TCR Report" Language="C#" MasterPageFile="~/Samsung/Samsung.Master" AutoEventWireup="true" CodeBehind="SamsungDashboard.aspx.cs" Inherits="ShERPa360net.Samsung.SamsungDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Samsung TCR Report</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />
    <style>
        .icon1 {
            text-align: center;
            vertical-align: middle;
            height: 116px;
            width: 116px;
            border-radius: 50%;
            color: #999999;
            margin: 0 auto 20px;
            border: 4px solid #fe970a;
            transition: all 0.2s;
            -webkit-transition: all 0.2s;
        }

        .imgcircle {
            height: 65px !important;
            width: 65px !important;
            margin-top: 20px !important;
        }

        .shadow {
            -moz-box-shadow: 3px 3px 5px 6px #fe970a !important;
            -webkit-box-shadow: 0 0 5px #cccccc87 !important;
            border: 5px solid #cccccc87 !important;
            box-shadow: 0 0 5px #cccccc87 !important;
            background: #fff;
        }
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
            if ($("#ContentPlaceHolder1_gvAllList tr").length > 2) {
                $("#ContentPlaceHolder1_gvAllList").DataTable({
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

            if ($("#ContentPlaceHolder1_gvAmtData tr").length > 2) {
                $("#ContentPlaceHolder1_gvAmtData").DataTable({
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
    <section class="page-title">
        <h1><span class="fa fa-th"></span>&nbsp;     Dashboard</h1>
    </section>


    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; DashBoard  </strong></h3>
                    </div>
                    <div class="row" style="margin-top: 10px !important;">
                        <fieldset class="scheduler-border shadow" runat="server" id="Fieldset1">
                            <div class="col-md-4" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkTotalSO" runat="server" CssClass="tile tile-info" BackColor="#3E8768" BorderColor="#3E8768" Enabled="false">
                                    <asp:Label ID="lblTotalSO" runat="server" Text="0"></asp:Label>
                                    <p>Total Service Order</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-4" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkTotalTCR" runat="server" CssClass="tile tile-warning" Enabled="false">
                                    <asp:Label ID="lblTotalTCR" runat="server" Text="0"></asp:Label>
                                    <p>Total TCR</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-4" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkPendingSO" runat="server" CssClass="tile tile-primary" BackColor="Red" BorderColor="Red" Enabled="false">
                                    <asp:Label ID="lblPendingSO" runat="server" Text="0"></asp:Label>
                                    <p>Pending Service Order</p>
                                </asp:LinkButton>
                            </div>
                        </fieldset>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Location wise Service Order - TCR Data</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <fieldset class="scheduler-border">

                                            <div class="col-md-12" id="divAll" runat="server" visible="true">
                                                <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                    <asp:GridView ID="gvAllList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                        CellSpacing="0" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="true">
                                                        <EmptyDataTemplate>
                                                            No Record Found!
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:BoundField DataField="LOCATION" HeaderText="Location" />
                                                            <asp:BoundField DataField="SOCNT" HeaderText="Total Service Order" />
                                                            <asp:BoundField DataField="TCRCNT" HeaderText="Total TCR Created" />
                                                            <asp:BoundField DataField="PENDINGCNT" HeaderText="Pending Service Order" />
                                                        </Columns>
                                                    </asp:GridView>


                                                </div>
                                            </div>


                                        </fieldset>
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
                <div class="form-horizontal">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Location wise Amount Received Data</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <fieldset class="scheduler-border">

                                            <div class="col-md-12" id="div1" runat="server" visible="true">
                                                <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                    <asp:GridView ID="gvAmtData" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                        CellSpacing="0" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="true">
                                                        <EmptyDataTemplate>
                                                            No Record Found!
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:BoundField DataField="LOCATION" HeaderText="Location" />
                                                            <asp:BoundField DataField="SOCNT" HeaderText="Total Service Order" />
                                                            <asp:BoundField DataField="TCRCNT" HeaderText="Total TCR Created" />
                                                            <asp:BoundField DataField="PENDINGCNT" HeaderText="Pending Service Order" />
                                                            <asp:BoundField DataField="TOTAL" HeaderText="Total TCR Amt." />

                                                            <%--<asp:BoundField DataField="AMTRCVDATCENTER" HeaderText="TCR Rcvd at Center" />--%>
                                                            <asp:BoundField DataField="TOTALRCVD" HeaderText="TCR Amt. Rcvd at Center" />
                                                            <%--<asp:BoundField DataField="PENDINGRCVDATCENTER" HeaderText="Pending TCR at Center" />--%>
                                                            <asp:BoundField DataField="PENDINGRCVDATCM" HeaderText="Pending TCR Amt. at Center" />

                                                            <%--<asp:BoundField DataField="AMTRCVDATACCOUNT" HeaderText="TCR Rcvd at Account" />--%>
                                                            <asp:BoundField DataField="TOTALRCVDAC" HeaderText="TCR Amt. Rcvd at Account" />
                                                            <%--<asp:BoundField DataField="PENDINGRCVDATACCOUNT" HeaderText="Pending TCR at Account" />--%>
                                                            <asp:BoundField DataField="PENDINGRCVDATAC" HeaderText="Pending TCR Amt. at Account" />
                                                        </Columns>
                                                    </asp:GridView>


                                                </div>
                                            </div>


                                        </fieldset>
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
    <input type="hidden" id="menutabid" value="tsmRptSamsung" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmReports" runat="server" />
</asp:Content>
