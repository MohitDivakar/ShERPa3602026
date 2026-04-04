<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="frmCalculatedAllPlatFormAvgAmt.aspx.cs" Inherits="ShERPa360net.UTILITY.frmCalculatedAllPlatFormAvgAmt" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Calculated All PlatForm Upload Avg Amount</title>
    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }

        body .table thead th {
            position: sticky !important;
            top: -1px !important;
            /*background-color: #3D2A70 !important;
            color: white !important;*/
        }

        .header {
            position: sticky !important;
            top: -1px !important;
            /*background-color: #3D2A70 !important;
            color: white !important;*/
        }
    </style>

    <style type="text/css">
        .margin-top {
            margin-top: 25px;
        }

        .new {
            height: 100px;
            width: 100px;
        }

        .col-md-12 .margin-bottom img {
            margin: 20px;
        }

        .red {
            background: none;
            color: red;
            border: none;
        }

        .zoom:hover {
            margin-top: -50px !important;
            -ms-transform: scale(15); /* IE 9 */
            -webkit-transform: scale(15); /* Safari 3-8 */
            transform: scale(15);
            transform-origin: -1px;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">

        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Calculated All PlatForm   </strong>Upload Avg Amount</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <%--<div class="col-md-4">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Itemcode : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtItemcode" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">SKU : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtSKU" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>

                                    <div class="col-md-4">
                                    </div>

                                    <div class="col-md-4">
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSerchCalcWebAmt" CssClass="btn btn-success pull-right" Text="Search Mapping" OnClick="lnkSerchCalcWebAmt_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-right" Text="Export Mapping" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
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
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Listed Product</strong> List</h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <fieldset class="scheduler-border">
                                            <div class="col-md-12" id="divName" runat="server" visible="true">
                                                <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                    <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                        CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                                        <EmptyDataTemplate>
                                                            <div style="text-align: center; color: red; font-size: 18px;">
                                                                No Record Found !
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>' CssClass="hidden"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Item Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblITEMCODE" runat="server" Text='<%# Eval("ITEMCODE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Item Desc.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblITEMDESC" runat="server" Text='<%# Eval("ITEMDESC") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="SKU">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSKU" runat="server" Text='<%# Eval("SKU") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Flipkart Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFLIPKART" runat="server" Text='<%# Eval("FLIPKART") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Amazon Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAMAZON" runat="server" Text='<%# Eval("AMAZON") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Website Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWEBSITE" runat="server" Text='<%# Eval("WEBSITE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                 <%--                                           <asp:TemplateField HeaderText="Listing ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblListingID" runat="server" Text='<%# Eval("LISTINGID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>

                                                         <%--   <asp:TemplateField HeaderText="ISAPPROVEDFK">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblISAPPROVEDFK" runat="server" Text='<%# Eval("ISAPPROVEDFK") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="ISAPPROVEDAMZ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblISAPPROVEDAMZ" runat="server" Text='<%# Eval("ISAPPROVEDAMZ") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="ISAPPROVEDWEB">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblISAPPROVEDWEB" runat="server" Text='<%# Eval("ISAPPROVEDWEB") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>

                                                            <asp:TemplateField HeaderText="FK Avg Amt">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAverageFKAmt" runat="server" Text='<%# Eval("AVERAGEFLIPKARTAMT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="FK Total Qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblFKTotalQty" runat="server" Text='<%# Eval("TOTALFLIPKARTQTY") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="AMZ Avg Amt">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAverageAMZAmt" runat="server" Text='<%# Eval("AVERAGEAMAZONAMT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="AMZ Total Qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAMZTotalQty" runat="server" Text='<%# Eval("TOTALAMAZONQTY") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Web Avg Amt">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAverageWebAmt" runat="server" Text='<%# Eval("AVERAGEWEBAMT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Web Total Qty">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWebTotalQty" runat="server" Text='<%# Eval("TOTALWEBQTY") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
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
    <input type="hidden" id="menutabid" value="tsmTranMobexSellerCalWebAvgAmt" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmMstMM" />
</asp:Content>
