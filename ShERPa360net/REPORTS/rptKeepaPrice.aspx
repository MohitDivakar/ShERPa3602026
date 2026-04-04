<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptKeepaPrice.aspx.cs" Inherits="ShERPa360net.REPORTS.rptKeepaPrice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>KEEPA PRICE REPORT</title>

    <script>
        $(document).ready(function () {
            BindGrid();
        });
        function BindGrid() {

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
    </script>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Price  </strong>From KEEPA</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Item Code : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtItemCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label"></label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:LinkButton runat="server" ID="lnkSerch" CssClass="btn btn-success " Text="Search" OnClick="lnkSerch_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success" Text="Export " OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
                                                </div>
                                            </div>
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
                            <div class="col-md-12" style="margin-top: 10px !important;">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                        <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <HeaderStyle CssClass="header" />
                                            <Columns>
                                                <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                <asp:BoundField DataField="SKU" HeaderText="OSM Sku" />
                                                <asp:BoundField DataField="AMAZON" HeaderText="ASIN" />
                                                <asp:BoundField DataField="FLIPKART" HeaderText="Flipkart Code" />
                                                <asp:BoundField DataField="WEBSITE" HeaderText="Website Code" />
                                                <asp:BoundField DataField="SCWEBSITE" HeaderText="Stock Clearance" />
                                                <asp:BoundField DataField="PRICE" HeaderText="Current List Price" />
                                                <asp:BoundField DataField="NEWPRICE" HeaderText="Current Buy Box Price" />
                                                <asp:BoundField DataField="NEWCURRENT" HeaderText="New Current Price" />
                                                <asp:BoundField DataField="AVGPRICE30DAYS" HeaderText="AVG (30 Day)" />
                                                <asp:BoundField DataField="AVGPRICE90DAYS" HeaderText="AVG (60 Day)" />
                                                <asp:BoundField DataField="NEWLOWEST" HeaderText="New Lowest Price" />
                                                <asp:BoundField DataField="NEWHIGHEST" HeaderText="New Highest Price" />
                                                <asp:BoundField DataField="URL" HeaderText="Amazon URL" />
                                                <asp:BoundField DataField="CREATEDATE" HeaderText="Keepa Create Dt." />
                                                <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
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

    <input type="hidden" id="menutabid" value="tsmKeepaPrice" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptSD" runat="server" />

</asp:Content>
