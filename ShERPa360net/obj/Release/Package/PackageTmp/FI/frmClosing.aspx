<%@ Page Title="" Language="C#" MasterPageFile="~/FI/MasterFI.Master" AutoEventWireup="true" CodeBehind="frmClosing.aspx.cs" Inherits="ShERPa360net.FI.frmClosing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Vendor Closing Ledger</title>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Vendor Closing Ledger</strong></h3>
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
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10" Enabled="false"></asp:TextBox>
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
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker" MaxLength="10" Enabled="false"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvTodate" runat="server" ControlToValidate="txtToDate" ForeColor="Red" Display="Dynamic"
                                                        ErrorMessage="Enter From Date" ValidationGroup="LedgerVal">Enter To Date</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Tally Group : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlTallyGroup" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ID="rfvVendor" runat="server" ControlToValidate="ddlVendor" ForeColor="Red" Display="Dynamic"
                                                        InitialValue="0" ErrorMessage="Select Vendor" ValidationGroup="LedgerVal">Select Vendor</asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSerch" CssClass="btn btn-success pull-left" Text="Search" OnClick="lnkSerch_Click" ValidationGroup="LedgerVal"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export PO" OnClick="lnkExport_Click" Enabled="false"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
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
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Ledger </strong></h3>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 10px !important;">
                                        <div class="box">
                                            <div class="box-body divhorizontal" style="overflow-x: scroll; overflow-y: scroll !important; max-height: 500px !important;">
                                                <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                    CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" ShowFooter="true">
                                                    <Columns>
                                                        <asp:BoundField DataField="VENDCODE" HeaderText="Party Code" />
                                                        <asp:BoundField DataField="NAME1" HeaderText="Party Name" />
                                                        <asp:BoundField DataField="CR" HeaderText="CR" ItemStyle-HorizontalAlign="Right" />
                                                        <asp:BoundField DataField="DR" HeaderText="DR" ItemStyle-HorizontalAlign="Right" />
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
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranFIBP" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranFI" runat="server" />

</asp:Content>
