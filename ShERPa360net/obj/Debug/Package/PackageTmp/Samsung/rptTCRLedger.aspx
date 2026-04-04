<%@ Page Title="TCR Ledger" Language="C#" MasterPageFile="~/Samsung/Samsung.Master" AutoEventWireup="true" CodeBehind="rptTCRLedger.aspx.cs" Inherits="ShERPa360net.Samsung.rptTCRLedger" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>TCR Ledger</title>

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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View TCR Report</strong></h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">From : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate" ForeColor="Red" Display="Dynamic"
                                                        ErrorMessage="Enter From Date" ValidationGroup="LedgerVal">Enter From Date</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">To : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvTodate" runat="server" ControlToValidate="txtToDate" ForeColor="Red" Display="Dynamic"
                                                        ErrorMessage="Enter From Date" ValidationGroup="LedgerVal">Enter To Date</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSerch" CssClass="btn btn-success pull-left" Text="Search" OnClick="lnkSerch_Click" ValidationGroup="LedgerVal"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                            <%--<asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export PO" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>--%>
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


                            <section class="content">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="nav-tabs-custom">
                                            <ul class="nav nav-pills">
                                                <li class="active"><a data-toggle="pill" href="#tab_1">Center</a></li>
                                                <li><a data-toggle="pill" href="#tab_2">Account</a></li>
                                            </ul>
                                            <br />
                                            <div class="tab-content">
                                                <div class="tab-pane active" id="tab_1">
                                                    <asp:GridView ID="grvDataCenter" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                        CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" ShowFooter="true">
                                                        <Columns>
                                                            <asp:BoundField DataField="TCR Date" HeaderText="TCR Dt." />
                                                            <asp:BoundField DataField="Complaint No." HeaderText="Complaint No." />
                                                            <asp:BoundField DataField="Location" HeaderText="Location" />
                                                            <asp:BoundField DataField="Payment Mode" HeaderText="Pay Mode" />
                                                            <asp:BoundField DataField="TCR No." HeaderText="TCR No." />
                                                            <asp:BoundField DataField="Total" HeaderText="Total Amt." />
                                                            <asp:BoundField DataField="Received at center" HeaderText="Amt. Received at center" ItemStyle-HorizontalAlign="Right" />
                                                            <asp:BoundField DataField="To be received at Center" HeaderText="Amt. To be received at Center" ItemStyle-HorizontalAlign="Right" />
                                                            <asp:BoundField DataField="Received at Account" HeaderText="Amt. Received at Account" ItemStyle-HorizontalAlign="Right" />
                                                            <asp:BoundField DataField="To be received at Account" HeaderText="Amt. To be received at Account" ItemStyle-HorizontalAlign="Right" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>

                                                <div class="tab-pane" id="tab_2">
                                                    <asp:GridView ID="grvDataAccount" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                        CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" ShowFooter="true">
                                                        <Columns>
                                                            <asp:BoundField DataField="TCR Date" HeaderText="TCR Dt." />
                                                            <asp:BoundField DataField="Complaint No." HeaderText="Complaint No." />
                                                            <asp:BoundField DataField="Location" HeaderText="Location" />
                                                            <asp:BoundField DataField="Payment Mode" HeaderText="Pay Mode" />
                                                            <asp:BoundField DataField="TCR No." HeaderText="TCR No." />
                                                            <asp:BoundField DataField="Total" HeaderText="Total Amt." />
                                                            <asp:BoundField DataField="Received at Account" HeaderText="Amt. Received at Account" ItemStyle-HorizontalAlign="Right" />
                                                            <asp:BoundField DataField="To be received at Account" HeaderText="Amt. To be received at Account" ItemStyle-HorizontalAlign="Right" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </section>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmRptTCRReport" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptSamsung" runat="server" />

</asp:Content>
