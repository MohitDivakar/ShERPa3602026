<%@ Page Title="" Language="C#" MasterPageFile="~/FI/MasterFI.Master" AutoEventWireup="true" CodeBehind="frmLedger.aspx.cs" Inherits="ShERPa360net.FI.frmLedger" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Vendor Ledger</title>

    <%--    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>--%>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />

    <link rel="stylesheet" href="https://cdn.datatables.net/2.1.8/css/dataTables.dataTables.css" />
    <script src="https://cdn.datatables.net/2.1.8/js/dataTables.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>Vendor Ledger</h3>
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
                                                    <asp:DropDownList ID="ddlVendor" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvVendor" runat="server" ControlToValidate="ddlVendor" ForeColor="Red" Display="Dynamic"
                                                        InitialValue="0" ErrorMessage="Select Vendor" ValidationGroup="LedgerVal">Select Vendor</asp:RequiredFieldValidator>
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Ledger </strong></h3>
                            <%--<asp:LinkButton ID="imgDownload" runat="server" CssClass="btn btn-success pull-right" Text="Download" TabIndex="2" OnClick="lnkDownLoadAll_Click">
<span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>--%>
                            <%--<asp:LinkButton ID="imgSave" runat="server" CssClass="btn btn-success pull-right" Text="Save" TabIndex="1"><i class="fa fa-save"></i></asp:LinkButton>--%>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 10px !important;">
                                        <div class="box">
                                            <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                                <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                    CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" ShowFooter="true">
                                                    <Columns>
                                                        <asp:BoundField DataField="VENDCODE" HeaderText="Party Code" />
                                                        <asp:BoundField DataField="DOCNO" HeaderText="Doc No." />
                                                        <asp:BoundField DataField="DOCDT" HeaderText="Doc Dt." />
                                                        <asp:BoundField DataField="TITLE" HeaderText="Title" ItemStyle-HorizontalAlign="Right" />
                                                        <asp:BoundField DataField="CR" HeaderText="CR" ItemStyle-HorizontalAlign="Right" />
                                                        <asp:BoundField DataField="DR" HeaderText="DR" ItemStyle-HorizontalAlign="Right" />
                                                        <%--<asp:BoundField DataField="CRDR" HeaderText="CR / DR" />
                                                        <asp:BoundField DataField="PDOCNO" HeaderText="Doc No." />
                                                        <asp:BoundField DataField="PDOCTYPE" HeaderText="Doc Type" />--%>
                                                    </Columns>
                                                </asp:GridView>
                                                <br />
                                                <asp:Label ID="lblHeading" runat="server" CssClass="form-control" Text="Closing Balance : " Visible="false"></asp:Label>
                                                <asp:Label ID="lblClosing" runat="server" CssClass="form-control" Visible="false"></asp:Label>
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
