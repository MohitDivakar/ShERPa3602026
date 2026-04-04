<%@ Page Title="" Language="C#" MasterPageFile="~/FI/MasterFI.Master" AutoEventWireup="true" CodeBehind="frmDealerOutStanding.aspx.cs" Inherits="ShERPa360net.FI.frmDealerOutStanding" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Dealer OutStanding</title>
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>Jangad OutStanding</h3>
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
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Vendor : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control ddlDealer"></asp:DropDownList>    
                                                    <asp:RequiredFieldValidator ID="rfvVendor" runat="server" ControlToValidate="ddlDealer" ForeColor="Red" Display="Dynamic"
                                                        InitialValue="0" ErrorMessage="Select Dealer" ValidationGroup="LedgerVal">Select Dealer</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSerch" CssClass="btn btn-success pull-left" Text="Search" OnClick="lnkSerch_Click" ValidationGroup="LedgerVal"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export PO" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Jangad OutStanding </strong></h3>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 10px !important;">
                                        <div class="box">
                                            <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                                <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                    CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" ShowFooter="true">
                                                    <Columns>
                                                        <asp:BoundField DataField="TOTALGIVENSTOCKAMOUNT" HeaderText="TOTAL GIVEN STOCK AMOUNT" />
                                                        <asp:BoundField DataField="TOTALGIVENSTOCKQTY" HeaderText="TOTAL GIVEN STOCK QTY" />
                                                        <asp:BoundField DataField="TOTALSALEAMOUNT" HeaderText="TOTAL SALE AMOUNT" />
                                                        <asp:BoundField DataField="TOTALSALEQTY" HeaderText="TOTAL SALE QTY" />      
                                                        <asp:BoundField DataField="TOTALPAIDAMOUNT" HeaderText="TOTAL PAID AMOUNT" />
                                                        <asp:BoundField DataField="TOTALPAIDQTY" HeaderText="TOTAL PAID QTY" />      
                                                        <asp:BoundField DataField="TOTALRETURNAMOUNT" HeaderText="TOTAL RETURN AMOUNT" />
                                                        <asp:BoundField DataField="TOTALRETURNQTY" HeaderText="TOTAL RETURN QTY" />      
                                                        <asp:BoundField DataField="TOTALEXISTSTOCKAMOUNT" HeaderText="TOTAL EXIST STOCK AMOUNT" />
                                                        <asp:BoundField DataField="TOTALEXISTSTOCKQTY" HeaderText="TOTAL EXIST STOCK QTY" />      
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
