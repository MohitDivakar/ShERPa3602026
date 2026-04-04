<%@ Page Title="Croma Voucher Payment" Language="C#" MasterPageFile="~/UTILITY/UtilityModule.Master" AutoEventWireup="true" CodeBehind="frmVoucherPmt.aspx.cs" Inherits="ShERPa360net.UTILITY.frmVoucherPmt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Croma Voucher Payment</title>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />

    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            document.getElementById("busy-holder1").style.display = "";
            document.getElementById("ContentPlaceHolder1_lnkApprove").style.display = "none";
        }
        function ShowLoadingReverse() {
            document.getElementById("busy-holder1").style.display = "none";
            document.getElementById("ContentPlaceHolder1_lnkApprove").style.display = "";
        }
    </script>

    <script>
        $(document).ready(function () {
            BindMakeAssociateModel();
        });
        function BindMakeAssociateModel() {

            $("#ContentPlaceHolder1_gvList").DataTable({
                dom: 'Bfrtip',
                paging: false,
                sorting: false,
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Croma Voucher </strong>Payment</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Date From : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">To : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Store Name : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlStore" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSearh" CssClass="btn btn-success pull-left" Text="Search" OnClick="lnkSearh_Click"><i class="fa fa-search"></i></asp:LinkButton>
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
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <asp:LinkButton ID="lnkPaymentEntry" OnClientClick="ShowLoading()" runat="server" CssClass="btn btn-success pull-left" OnClick="lnkPaymentEntry_Click" Text="  Payment Entry" Style="margin-top: 10px !important;"><i class="fa fa-save"></i>   Payment Entry</asp:LinkButton>
                                    <div id="busy-holder1" style="display: none" class="clearfix inline pull-left">
                                        <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                        <label>Please wait...</label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12" style="margin-top: 20px !important;">
                                <div class="box">
                                    <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                        CellSpacing="0" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%">
                                        <EmptyDataTemplate>
                                            No Record Found!
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelectAll" runat="server" OnCheckedChanged="chkSelectAll_CheckedChanged" AutoPostBack="true" Enabled="true" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="upd1" runat="server">
                                                        <Triggers>
                                                            <asp:PostBackTrigger ControlID="chkSelect" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Vchr. No" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVCHRNO" runat="server" Text='<%# Eval("VCHRNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Vchr. Dt." Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPICKUPDATE" runat="server" Text='<%# Eval("PICKUPDATE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Vchr. Amt." Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTOTALAMT" runat="server" Text='<%# Eval("TOTALAMT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total Qty." Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTOTALQTY" runat="server" Text='<%# Eval("TOTALQTY") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Vchr. Create By" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPICKUPBY" runat="server" Text='<%# Eval("PICKUPBY") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Entry By" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUSERNAME" runat="server" Text='<%# Eval("USERNAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Store Name" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSTORENAME" runat="server" Text='<%# Eval("STORENAME") %>'></asp:Label>
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

    <div class="modal fade" id="modal-detail" data-backdrop="static" style="margin-top: 200px;">
        <center>
            <div class="modal-dialog modal-lg">
                <div class="modal-content" style="height: 120px !important; width: 75% !important;">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" style="color: #337ab7"><strong>Payment Details Entry</strong></h4>
                    </div>
                    <div class="modal-body">
                        <div class="box-body">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtTXNNO" runat="server" CssClass="form-control" placeholder="Transaction Number"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvTXNNO" runat="server" ControlToValidate="txtTXNNO" Display="Dynamic" ForeColor="Red"
                                            ErrorMessage="Enter Transaction Number" ValidationGroup="Check">Enter Transaction Number</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <div class="input-group">
                                            <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                            <asp:TextBox ID="txtTXNDT" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                        </div>
                                        <asp:RequiredFieldValidator ID="rfvTxnDt" runat="server" ControlToValidate="txtTXNDT" Display="Dynamic" ForeColor="Red"
                                            ErrorMessage="Enter Transaction Date" ValidationGroup="Check">Enter Transaction Date</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="btn btn-success pull-left" OnClick="lnkSubmit_Click" Text="Approve" ValidationGroup="Check"><i class="fa fa-check-circle"></i>   Submit</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </center>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmCromaPmt" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmUtility" runat="server" />

</asp:Content>
