<%@ Page Title="OCR Scan Report" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptExtendedWarranty.aspx.cs" Inherits="ShERPa360net.REPORTS.rptExtendedWarranty" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Extended Warranty Report</title>
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Extended Warranty </strong>Report</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12" style="padding-top: 10px !important;">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Status : </label>
                                            <div class="col-md-8 col-xs-12">
                                                    <asp:DropDownList runat="server" id="ddlStatus" CssClass="form-control">
                                                        <asp:ListItem Value="PENDING" Text="PENDING" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="COMPLETED" Text="COMPLETED"></asp:ListItem>
                                                        <asp:ListItem Value="ALL" Text="ALL"></asp:ListItem>
                                                    </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>



                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label"></label>
                                            <div class="col-md-7 col-xs-12">
                                                <asp:LinkButton runat="server" ID="lnkSearh" CssClass="btn btn-success pull-left" Text="Search PO" OnClick="lnkSearh_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i></span></asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export PO OCR Report" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i></span></asp:LinkButton>
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
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Extended Warranty Report</strong></h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <fieldset class="scheduler-border">
                                            <div class="col-md-12" runat="server">
                                                <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                    <asp:GridView ID="gvAllList" ClientIDMode="Static" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                        CellSpacing="0" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="true">
                                                        <emptydatatemplate>
                                                            No Record Found!
                                                        </emptydatatemplate>
                                                        <columns>
                                                            <asp:BoundField DataField="ID" HeaderText="ID" />
                                                            <asp:BoundField DataField="MOBILE" HeaderText="MOBILE" />
                                                            <asp:BoundField DataField="MOBILEVERFIEDDATETIME" HeaderText="MOBILEVERFIEDDATETIME" />
                                                            <asp:BoundField DataField="IMEINO" HeaderText="IMEINO" />
                                                            <asp:BoundField DataField="ORDERNO" HeaderText="ORDERNO" />
                                                            <asp:BoundField DataField="SONO" HeaderText="SONO" />
                                                            <asp:BoundField DataField="STATUS" HeaderText="STATUS" />
                                                            <asp:BoundField DataField="IMEINOCONFIRMDATETIME" HeaderText="IMEINOCONFIRMDATETIME" />
                                                            <asp:BoundField DataField="CALLREMARK" HeaderText="Call Remark" />
                                                            <asp:BoundField DataField="REVIEWLINK" HeaderText="Review Link" />
                                                        </columns>
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
    <input type="hidden" id="menutabid" value="tsmOCRReport" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptSD" runat="server" />
</asp:Content>
