<%@ Page Title="Amount Received at Account" Language="C#" MasterPageFile="~/Samsung/Samsung.Master" AutoEventWireup="true" CodeBehind="frmTCRRCVDAC.aspx.cs" Inherits="ShERPa360net.Samsung.frmTCRRCVDAC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Amount Received at Account</title>

    <script>
        $(document).ready(function () {
            BindMakeAssociateModel();
        });
        function BindMakeAssociateModel() {
            if ($("#ContentPlaceHolder1_grvData tr").length > 2) {
                $("#ContentPlaceHolder1_grvData").DataTable({
                    paging: false,
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
                //$('#ContentPlaceHolder1_gvAllList_info').hide();
                //$('#ContentPlaceHolder1_gvAllList_paginate').hide();
                //$('#ContentPlaceHolder1_gvAllList_length').hide();
            }
        }
    </script>

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

    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            debugger;
            document.getElementById("busy-holder1").style.display = "";
            document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;TCR List</strong></h3>
                    </div>
                    <div class="panel-body">
                        <div class="row" style="margin-top: 20px !important;">
                            <asp:LinkButton runat="server" ID="lnkDownloadImages" CssClass="btn btn-success pull-left" Text="Search Invoice" OnClick="lnkDownloadImages_Click" ValidationGroup="SaveAll" Style="margin-top: 10px !important;"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> Dwonload All Images</span></asp:LinkButton>

                            <asp:LinkButton ID="imgSaveAll" OnClientClick="ShowLoading()" runat="server" CssClass="btn btn-success pull-left" OnClick="imgSaveAll_Click" Text="Receive Selected" ValidationGroup="SaveAll" TabIndex="15" Style="margin-top: 10px !important;"><i class="fa fa-save"></i>  Receive Selected</asp:LinkButton>
                            <div id="busy-holder1" style="display: none" class="clearfix inline pull-left">
                                <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                <label>Please wait...</label>
                            </div>

                            <div class="col-md-12">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                        <asp:GridView ID="grvData" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <%--  <asp:BoundField DataField="RCPTNO" HeaderText="Rcpt No." />
                                                <asp:BoundField DataField="COMPLAINTNO" HeaderText="Complnt No." />
                                                <asp:BoundField DataField="CUSTNAME" HeaderText="Cust. Name" />
                                                <asp:BoundField DataField="MOBILENO" HeaderText="Mobile No." />
                                                <asp:BoundField DataField="MODELNO" HeaderText="Model" />
                                                <asp:BoundField DataField="SERIALNO" HeaderText="Serial No." />
                                                <asp:BoundField DataField="TOTAL" HeaderText="Total Cost" />
                                                <asp:BoundField DataField="PAYMODE" HeaderText="Paymode" />
                                                <asp:BoundField DataField="ENTRYBY" HeaderText="Entry By" />
                                                <asp:BoundField DataField="CREATEDATE" HeaderText="Entry Date" />--%>

                                                <asp:TemplateField HeaderText="Select">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSelectAll" runat="server" OnCheckedChanged="chkSelectAll_CheckedChanged" AutoPostBack="true" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Rcpt No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRCPTNO" runat="server" Text='<%#Eval("RCPTNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Service Order No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCOMPLAINTNO" runat="server" Text='<%#Eval("COMPLAINTNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust. Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUSTNAME" runat="server" Text='<%#Eval("CUSTNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Mobile No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMOBILENO" runat="server" Text='<%#Eval("MOBILENO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Model">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMODELNO" runat="server" Text='<%#Eval("MODELNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Serial No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSERIALNO" runat="server" Text='<%#Eval("SERIALNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Total Cost">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTOTAL" runat="server" Text='<%#Eval("TOTAL") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Paymode">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPAYMODE" runat="server" Text='<%#Eval("PAYMODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Entry By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblENTRYBY" runat="server" Text='<%#Eval("ENTRYBY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Entry Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCREATEDATE" runat="server" Text='<%#Eval("CREATEDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Rcvd at Centre By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRCVDBYCMNAME" runat="server" Text='<%#Eval("RCVDBYCMNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Rcvd at Centre Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="RCVDBYCMDATE" runat="server" Text='<%#Eval("RCVDBYCMDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="btnDetails" Text="Details" OnClick="btnDetails_Click"></asp:LinkButton>
                                                        | 
                                                        <asp:LinkButton runat="server" ID="btnApprove" Text="Receive TCR Amount" OnClick="btnApprove_Click"></asp:LinkButton>
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
    </div>

    <div class="modal fade" id="modal-detail" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Vendor</strong> Details</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Receipt No :</label>
                                        <asp:Label ID="lblPopRcptNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Complaint No :</label>
                                        <asp:Label ID="lblPopComplaintNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Cust. Name :</label>
                                        <asp:Label ID="lblPopCustName" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Mobile No. :</label>
                                        <asp:Label ID="lblPopMobileNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Address :</label>
                                        <asp:TextBox ID="lblPopAddress" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Other Contact No. :</label>
                                        <asp:Label ID="lblPopContactno" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Model :</label>
                                        <asp:Label ID="lblPopModelNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Serial No. :</label>
                                        <asp:Label ID="lblPopSerialNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Total Cost :</label>
                                        <asp:Label ID="lblPopTotalCost" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Paymode :</label>
                                        <asp:Label ID="lblPopPaymode" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Transaction ID :</label>
                                        <asp:Label ID="lblPopTransactionID" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Entry By :</label>
                                        <asp:Label ID="lblPopEntryBy" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Entry Date :</label>
                                        <asp:Label ID="lblPopEntryDate" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                            </div>


                            <div class="col-md-12" style="margin-top: 10px !important;">
                                <div class="col-md-6">
                                    <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                        CssClass="table table-hover table-striped table-bordered nowrap" OnRowDataBound="gvDetail_RowDataBound">
                                        <EmptyDataTemplate>
                                            No Record Found!
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Receipt No" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGVRcptNo" runat="server" Text='<%# Eval("RCPTNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Service Order No" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGVComplaintNo" runat="server" Text='<%# Eval("COMPLAINTNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Image" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblImageData" runat="server" Text='<%# Eval("IMAGE") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Image Type">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgImage" runat="server" Height="50" Width="50" CssClass="zoom" AlternateText='<%# Eval("EXTENSION") %>' Visible="true" />
                                                    <asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkDownload_Click" Text="Download" Visible="true"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%--<asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblImageID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Extension" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblImageExtension" runat="server" Text='<%# Eval("EXTENSION") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>



                            </div>

                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <div class="form-group">
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:LinkButton runat="server" ID="lnkReceive" CssClass="btn btn-success pull-left" Text="Receive TCR Amount" OnClick="lnkReceive_Click" Visible="true"><i class="fa fa-check-circle"></i> Receive Amt</asp:LinkButton>
                                    </div>
                                </div>

                            </div>

                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>


    <div class="message-box animated fadeIn" data-sound="alert" id="mb-aprv">

        <div class="mb-container">

            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: #ffffff;">
                <span aria-hidden="true">&times;</span></button>
            <h4 class="modal-title" style="color: #faa61a;"><strong>TCR Amount</strong> Receive</h4>
            <div class="mb-middle">
                <div class="mb-title" style="color: #faa61a"><span class="fa fa-check"></span>Received Amount for this <strong>TCR</strong> ?</div>
                <div class="mb-content">
                    <h4 style="color: #ffffff;">TCR No. : <strong>
                        <asp:Label runat="server" ID="lblTCRNO"></asp:Label></strong></h4>
                    <asp:HiddenField ID="hfComplaintNo" runat="server" />
                    <h4 style="color: #ffffff;">Are you sure you have received amount for this TCR?</h4>
                </div>


                <div class="mb-footer">
                    <div class="pull-right">
                        <asp:LinkButton runat="server" ID="lnkPopReceived" CssClass="btn btn-success pull-left" Text="Receive TCR Amount" OnClick="lnkPopReceived_Click"><i class="fa fa-check-circle"></i> Receive Amt</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranRcvdByAC" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranSAMSUNG" runat="server" />

</asp:Content>
