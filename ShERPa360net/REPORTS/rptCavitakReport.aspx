<%@ Page Title="Cavitak Report" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptCavitakReport.aspx.cs" Inherits="ShERPa360net.REPORTS.rptCavitakReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Cavitak Report</title>

    <style>
        .notification {
            /*background-color: #555;*/
            color: white;
            text-decoration: none;
            padding: 15px 26px;
            position: relative;
            display: inline-block;
            border-radius: 2px;
        }

            .notification:hover {
                background: red;
            }

            .notification .badge {
                position: absolute;
                top: 10px;
                right: 10px;
                padding: 0px 5px;
                border-radius: 50%;
                background-color: red;
                color: white;
            }

        /* .card-grid {
            width: 100%;
        }

            .card-grid tr {
                display: inline-block;
            }

        .card {
            width: 220px;
            margin: 10px;
            padding: 15px;
            border-radius: 10px;
            background: #f8f9fa;
            box-shadow: 0 2px 6px rgba(0,0,0,0.15);
            text-align: center;
        }

            .card h3 {
                margin-bottom: 10px;
                color: #2c3e50;
            }*/

        .card {
            width: 150px; /* smaller width */
            height: 95px; /* fixed small height */
            margin: 6px;
            padding: 8px;
            background: #E04B4A;
            border-radius: 8px;
            color: white;
            font-size: 13px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.15);
            display: flex;
            flex-direction: column;
            justify-content: center;
            text-align: center;
        }

            .card h3 {
                font-size: 14px;
                margin: 0 0 6px 0;
                font-weight: 600;
            }

            .card p {
                margin: 2px 0;
                font-size: 14px;
            }

        .card-grid tr {
            display: inline-block;
        }


        .card-grid td {
            padding: 0;
            border: none;
        }

        .card:hover {
            transform: scale(1.05);
            transition: 0.2s ease-in-out;
            cursor: pointer;
        }

        .card span {
            font-size: 18px;
            font-weight: bold;
        }
    </style>



    <script>
        $(document).ready(function () {
            BindMakeAssociateModel();
        });
        function BindMakeAssociateModel() {


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

            $("#ContentPlaceHolder1_grvTodayInwardSummary").DataTable({
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

            $("#ContentPlaceHolder1_grvOverallInwardSummary").DataTable({
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

            $("#ContentPlaceHolder1_grdPopDetails").DataTable({
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
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">

        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View Report</strong></h3>
                        </div>


                        <div class="panel-body">

                            <div class="row">

                                <div class="col-md-12 pull-right">

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-2 control-label"></label>
                                            <div class="col-md-10 col-xs-12 pull-right">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker pull-right" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-2 control-label pull-left">To</label>
                                            <div class="col-md-10 col-xs-12 pull-right">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker pull-right" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <asp:LinkButton runat="server" ID="lnkSearch" CssClass="btn btn-success pull-left" Text="Search" OnClick="lnkSearch_Click">
                                        <span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--Dashboard View Start--%>

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="row" style="margin-top: 10px !important;">
                        <div class="col-md-12">
                            <div class="col-md-6">
                                <fieldset class="scheduler-border shadow" runat="server" id="Fieldset1">
                                    <legend class="scheduler-border">Today's History</legend>

                                    <div class="col-md-3" style="margin-top: 10px !important;">
                                        <asp:LinkButton ID="lnkTodayInward" runat="server" CssClass="tile tile-warning" OnClick="lnkTodayInward_Click">
                                            <asp:Label ID="lblTodayInward" runat="server" Text="0"></asp:Label>
                                            <p>
                                                Today's
                                                <br />
                                                Inward
                                            </p>
                                        </asp:LinkButton>
                                    </div>

                                    <div class="col-md-3" style="margin-top: 10px !important;">
                                        <asp:LinkButton ID="lnkTodayDispatch" runat="server" CssClass="tile tile-danger" OnClick="lnkTodayDispatch_Click">
                                            <asp:Label ID="lblTodayDispatch" runat="server" Text="0"></asp:Label>
                                            <p>
                                                Today's
                                                <br />
                                                Dispatched
                                            </p>
                                        </asp:LinkButton>
                                    </div>

                                    <div class="col-md-3" style="margin-top: 10px !important;">
                                        <asp:LinkButton ID="lnkTodayDelivered" runat="server" CssClass="tile tile-info" BackColor="#3E8768" BorderColor="#3E8768" OnClick="lnkTodayDelivered_Click">
                                            <asp:Label ID="lblTodayDelivered" runat="server" Text="0"></asp:Label>
                                            <p>
                                                Today's
                                                <br />
                                                Delivered
                                            </p>
                                        </asp:LinkButton>
                                    </div>

                                </fieldset>
                            </div>
                            <div class="col-md-6">
                                <fieldset class="scheduler-border shadow" runat="server" id="Fieldset2">

                                    <legend class="scheduler-border">Over All History</legend>

                                    <div class="col-md-3" style="margin-top: 10px !important;">
                                        <asp:LinkButton ID="lnkTotalInward" runat="server" CssClass="tile tile-warning" OnClick="lnkTotalInward_Click">
                                            <asp:Label ID="lblTotalInward" runat="server" Text="0"></asp:Label>
                                            <p>
                                                Total
                                                <br />
                                                Inward
                                            </p>
                                        </asp:LinkButton>
                                    </div>

                                    <div class="col-md-3" style="margin-top: 10px !important;">
                                        <asp:LinkButton ID="lnkTotalDispatched" runat="server" CssClass="tile tile-danger" OnClick="lnkTotalDispatched_Click">
                                            <asp:Label ID="lblTotalDispatched" runat="server" Text="0"></asp:Label>
                                            <p>
                                                Total
                                                <br />
                                                Dispatched
                                            </p>
                                        </asp:LinkButton>
                                    </div>

                                    <div class="col-md-3" style="margin-top: 10px !important;">
                                        <asp:LinkButton ID="lnkTotalDelivered" runat="server" CssClass="tile tile-info" BackColor="#3E8768" BorderColor="#3E8768" OnClick="lnkTotalDelivered_Click">
                                            <asp:Label ID="lblTotalDelivered" runat="server" Text="0" ForeColor="White"></asp:Label>
                                            <p style="color: white;">
                                                Total
                                                <br />
                                                Delivered
                                            </p>
                                        </asp:LinkButton>
                                    </div>

                                    <div class="col-md-3" style="margin-top: 10px !important;">
                                        <asp:LinkButton ID="lnkTotalPending" runat="server" CssClass="tile tile-default" BackColor="Red" BorderColor="Red" OnClick="lnkTotalPending_Click">
                                            <asp:Label ID="lblTotalPending" runat="server" Text="0" Style="color: white;"></asp:Label>
                                            <p style="color: white;">
                                                Total
                                                <br />
                                                Pending
                                            </p>
                                        </asp:LinkButton>
                                    </div>

                                </fieldset>
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
                    <div class="panel-heading">
                        <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Today's Production Status </strong>Count</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="box">
                                    <asp:GridView ID="gvCardsToday" runat="server"
                                        AutoGenerateColumns="False"
                                        CssClass="card-grid"
                                        ShowHeader="False"
                                        GridLines="None">

                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <%--<div class="card">
                                                        <h2><strong style="color: white;"><%# Eval("CNT") %></strong></h2>
                                                        <asp:Label ID="lblStageID" runat="server" Text='<%# Eval("STAGEID") %>' Visible="false"></asp:Label>
                                                        <h3>
                                                            <strong style="color: white;"><%# Eval("STAGEDESC") %></strong>
                                                        </h3>
                                                    </div>--%>

                                                    <asp:LinkButton ID="lnkCard" runat="server" CssClass='card <%# Eval("CNT").ToString().Replace("/", "").Replace(" ", "") %>' OnClick="lnkCard_Click">
                                                        <h2><strong style="color: white;"><%# Eval("CNT") %></strong></h2>
                                                        <p><strong><%# Eval("STAGEDESC") %></strong></p>
                                                        <asp:Label ID="lblNewStageID" runat="server" Text='<%# Eval("STAGEID") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblNewStageName" runat="server" Text='<%# Eval("STAGEDESC") %>' Visible="false"></asp:Label>
                                                    </asp:LinkButton>

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

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Over All Production Status </strong>Count</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="box">
                                    <asp:GridView ID="gvCards" runat="server"
                                        AutoGenerateColumns="False"
                                        CssClass="card-grid"
                                        ShowHeader="False"
                                        GridLines="None">

                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <%--<div class="card">
                                                        <h2><strong style="color: white;"><%# Eval("CNT") %></strong></h2>
                                                        <p><strong>Value 1:</strong> <%# Eval("STAGEID") %></p>
                                                        <h3>
                                                            <strong style="color: white;"><%# Eval("STATUSDESC") %></strong>
                                                        </h3>
                                                    </div>--%>

                                                    <asp:LinkButton ID="lnkCardAll" runat="server" CssClass='card <%# Eval("CNT").ToString().Replace("/", "").Replace(" ", "") %>' OnClick="lnkCardAll_Click">
                                                        <h2><strong style="color: white;"><%# Eval("CNT") %></strong></h2>
                                                        <p><strong><%# Eval("STATUSDESC") %></strong></p>
                                                        <asp:Label ID="lblNewStageID" runat="server" Text='<%# Eval("STAGEID") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="lblNewStageName" runat="server" Text='<%# Eval("STATUSDESC") %>' Visible="false"></asp:Label>
                                                    </asp:LinkButton>

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

    <%--Dashboard View End--%>



    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Today's Inward  </strong>Data</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                        <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Inventory">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkInv" Text="Inventory" OnClick="lnkInv_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="QC">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkQC" Text="QC" OnClick="lnkQC_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Job Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJobDate" runat="server" Text='<%# Eval("Job Date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Job Date" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}" />--%>

                                                <asp:TemplateField HeaderText="Item Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemType" runat="server" Text='<%# Eval("Item Type") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Item Type" HeaderText="Item Type" />--%>

                                                <asp:TemplateField HeaderText="Job ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJobID" runat="server" Text='<%# Eval("Job ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Job ID" HeaderText="Job ID" />--%>

                                                <asp:TemplateField HeaderText="Brand">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBrand" runat="server" Text='<%# Eval("Brand") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Brand" HeaderText="Brand" />--%>

                                                <asp:TemplateField HeaderText="Model">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblModel" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Model" HeaderText="Model" />--%>

                                                <asp:TemplateField HeaderText="Job Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJobStatus" runat="server" Text='<%# Eval("Job Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Job Status" HeaderText="Job Status" />--%>

                                                <asp:TemplateField HeaderText="Production Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductionStatus" runat="server" Text='<%# Eval("Production Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Production Status" HeaderText="Production Status" />--%>

                                                <asp:TemplateField HeaderText="Serial No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSerialNo" runat="server" Text='<%# Eval("Serial No") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Serial No. 1" HeaderText="Serial No. 1" />--%>

                                                <asp:TemplateField HeaderText="Item Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("Item Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Item Code" HeaderText="Item Code" />--%>

                                                <asp:TemplateField HeaderText="Item Desc.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("Item Description") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Item Description" HeaderText="Item Desc." />--%>
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

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="panel-heading">
                                        <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Today's Inward  </strong>Summary (Make/Model wise)</h3>
                                    </div>
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; overflow-y: scroll; max-height: 500px !important;">
                                        <asp:GridView ID="grvTodayInwardSummary" runat="server" CssClass="table table-hover table-striped table-bordered nowrap" ShowFooter="false"
                                            CellSpacing="0" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <%--<asp:BoundField DataField="Brand" HeaderText="Brand" />
                                                        <asp:BoundField DataField="Model" HeaderText="Model" />
                                                        <asp:BoundField DataField="Count" HeaderText="Count" />--%>

                                                <asp:TemplateField HeaderText="Brand">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBrand" runat="server" Text='<%# Bind("Brand") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Model">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblModel" runat="server" Text='<%# Bind("Model") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Count">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCount" runat="server" Text='<%# Bind("Count") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <%--<FooterTemplate>
                                                                <asp:Label ID="lblTotalCount" runat="server"></asp:Label>
                                                            </FooterTemplate>--%>
                                                </asp:TemplateField>


                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="panel-heading">
                                        <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Over All Inward  </strong>Summary (Make/Model wise)</h3>
                                    </div>
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; overflow-y: scroll; max-height: 500px !important;">

                                        <asp:GridView ID="grvOverallInwardSummary" runat="server" CssClass="table table-hover table-striped table-bordered nowrap" ShowFooter="false"
                                            CellSpacing="0" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <%--<asp:BoundField DataField="Brand" HeaderText="Brand" />
                                                        <asp:BoundField DataField="Model" HeaderText="Model" />
                                                        <asp:BoundField DataField="Count" HeaderText="Count" />--%>

                                                <asp:TemplateField HeaderText="Brand">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBrand" runat="server" Text='<%# Bind("Brand") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Model">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblModel" runat="server" Text='<%# Bind("Model") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Count">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCount" runat="server" Text='<%# Bind("Count") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <%--<FooterTemplate>
                                                                <asp:Label ID="lblTotalCount" runat="server"></asp:Label>
                                                            </FooterTemplate>--%>
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
    </div>

    <div id="InventoryModal" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Received Inventory</h4>
                </div>
                <div class="modal-body">
                    <asp:Panel ID="Panel5" runat="server" Height="300px" ScrollBars="Vertical">
                        <asp:GridView ID="grInvRcvd" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                            CellSpacing="0" AutoGenerateColumns="False" Width="100%">
                            <EmptyDataTemplate>
                                <div style="text-align: center; color: red; font-size: 18px;">
                                    List is empty !
                                </div>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />
                                <asp:BoundField DataField="JOBID" HeaderText="Job ID" />
                                <asp:BoundField DataField="INVID" HeaderText="Inventory ID" Visible="false" />
                                <asp:BoundField DataField="INVNAME" HeaderText="Inventory Name" />
                                <asp:BoundField DataField="RCVD" HeaderText="Received?" />
                                <asp:BoundField DataField="QTY" HeaderText="Qty Req." />
                                <asp:BoundField DataField="INVQTY" HeaderText="Qty Rcvd" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>

    <div id="QCParaModal" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">QC Parameter</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Job ID :</label>
                                        <asp:Label ID="lblPopJobid" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Brand :</label>
                                        <asp:Label ID="lblPopMake" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Model :</label>
                                        <asp:Label ID="lblPopModel" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12" style="margin-top: 10px !important;">
                                <asp:Panel ID="Panel1" runat="server" Height="300px" ScrollBars="Vertical">
                                    <asp:GridView ID="grvQCPara" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                        CellSpacing="0" AutoGenerateColumns="False" Width="100%">
                                        <EmptyDataTemplate>
                                            <div style="text-align: center; color: red; font-size: 18px;">
                                                List is empty !
                                            </div>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="JC NO" HeaderText="JC NO" Visible="false" />
                                            <asp:BoundField DataField="STAGE ID" HeaderText="Stage ID" Visible="false" />
                                            <asp:BoundField DataField="STAGE" HeaderText="Stage" />
                                            <asp:BoundField DataField="QC PARA ID" HeaderText="QC Para ID" Visible="false" />
                                            <asp:BoundField DataField="JC DTL ID" HeaderText="JC Dtl ID" Visible="false" />
                                            <asp:BoundField DataField="QC PARAMETER" HeaderText="QC Parameter" />
                                            <asp:BoundField DataField="IS SELECTED" HeaderText="Checked?" />
                                            <asp:BoundField DataField="RESULT" HeaderText="Result" />
                                        </Columns>
                                    </asp:GridView>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>

    <div id="DashBoardDetails" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">
                        <asp:Label ID="popHeading" runat="server"></asp:Label></h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                        <asp:GridView ID="grdPopDetails" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <%--<asp:TemplateField HeaderText="Inventory">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkInv" Text="Inventory" OnClick="lnkInv_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <%--<asp:TemplateField HeaderText="QC">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkQC" Text="QC" OnClick="lnkQC_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="Job Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJobDate" runat="server" Text='<%# Eval("Job Date") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Job Date" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}" />--%>

                                                <asp:TemplateField HeaderText="Item Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemType" runat="server" Text='<%# Eval("Item Type") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Item Type" HeaderText="Item Type" />--%>

                                                <asp:TemplateField HeaderText="Job ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJobID" runat="server" Text='<%# Eval("Job ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Job ID" HeaderText="Job ID" />--%>

                                                <asp:TemplateField HeaderText="Brand">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBrand" runat="server" Text='<%# Eval("Brand") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Brand" HeaderText="Brand" />--%>

                                                <asp:TemplateField HeaderText="Model">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblModel" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Model" HeaderText="Model" />--%>

                                                <asp:TemplateField HeaderText="Job Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJobStatus" runat="server" Text='<%# Eval("Job Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Job Status" HeaderText="Job Status" />--%>

                                                <asp:TemplateField HeaderText="Production Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductionStatus" runat="server" Text='<%# Eval("Production Status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Production Status" HeaderText="Production Status" />--%>

                                                <asp:TemplateField HeaderText="Serial No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSerialNo" runat="server" Text='<%# Eval("Serial No") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Serial No. 1" HeaderText="Serial No. 1" />--%>

                                                <asp:TemplateField HeaderText="Item Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("Item Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Item Code" HeaderText="Item Code" />--%>

                                                <asp:TemplateField HeaderText="Item Desc.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemDescription" runat="server" Text='<%# Eval("Item Description") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="Item Description" HeaderText="Item Desc." />--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmQtekCavitakReport" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmCavitak" />

</asp:Content>
