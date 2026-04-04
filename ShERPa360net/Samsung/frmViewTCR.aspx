<%@ Page Title="View TCR" Language="C#" MasterPageFile="~/Samsung/Samsung.Master" AutoEventWireup="true" CodeBehind="frmViewTCR.aspx.cs" Inherits="ShERPa360net.Samsung.frmViewTCR" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>View TCR</title>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />


    <script>
        $(document).ready(function () {
            BindMakeAssociateModel();
            //$("#myInput").on("keyup", function () {
            //    var value = $(this).val().toLowerCase();
            //    $("#ContentPlaceHolder1_grvData tr ").filter(function () {
            //        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            //    });
            //});
        });
        function BindMakeAssociateModel() {
            //debugger
            //if ($("#ContentPlaceHolder1_grvData tr").length > 2) {
            $("#ContentPlaceHolder1_grvData").DataTable({
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
            //}
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

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <rpt:ReportViewer ID="ReportViewer1" runat="server" SizeToReportContent="true" Visible="false"></rpt:ReportViewer>

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;View TCR</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">From Date : </label>
                                                <div class="col-md-8 col-xs-12">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
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
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Complaint No. : </label>
                                                <div class="col-md-8 col-xs-12">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtComplaintNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Mobile No. : </label>
                                                <div class="col-md-8 col-xs-12">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Receipt No. : </label>
                                                <div class="col-md-8 col-xs-12">
                                                    <div class="input-group">
                                                        <asp:TextBox ID="txtReceipt" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Payment Mode : </label>
                                                <div class="col-md-8 col-xs-12">
                                                    <div class="input-group">
                                                        <asp:DropDownList ID="ddlPayMode" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>



                                        <div class="col-md-6">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label"></label>
                                                    <div class="col-md-8 col-xs-12">
                                                        <asp:LinkButton runat="server" ID="lnkSerch" CssClass="btn btn-success" Text="Search Invoice" OnClick="lnkSerch_Click" ValidationGroup="SaveAll"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
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
        </div>
    </div>

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row" style="margin-top: 20px !important;">
                            <asp:LinkButton runat="server" ID="lnkDownloadImages" CssClass="btn btn-success" Text="Search Invoice" OnClick="lnkDownloadImages_Click" ValidationGroup="SaveAll"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> Dwonload All Images</span></asp:LinkButton>
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


                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="btnDetails" Text="Details" OnClick="btnDetails_Click"></asp:LinkButton>
                                                        <%--| 
                                                        <asp:LinkButton runat="server" ID="btnSend" Text="Send TCR" OnClick="btnSend_Click"></asp:LinkButton>--%>
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
                                        <label>Service Order No :</label>
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



                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranViewTCR" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranSAMSUNG" runat="server" />

</asp:Content>
