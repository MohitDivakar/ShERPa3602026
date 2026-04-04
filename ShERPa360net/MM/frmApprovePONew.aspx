<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="frmApprovePONew.aspx.cs" Inherits="ShERPa360net.MM.frmApprovePONew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />

    <style>
        .newRW {
            text-align-last: right !important;
        }

        .newStock {
            vertical-align: middle !important;
            border: none !important;
            border-style: none !important;
            border-width: 0px !important;
        }

        /* body .table tbody tr th {
            position: sticky;
            top: -1px;
        }*/
    </style>

    <script>
        $(document).ready(function () {
            BindMakeAssociateModel();
            //$("#myInput").on("keyup", function () {
            //    var value = $(this).val().toLowerCase();
            //    $("#ContentPlaceHolder1_gvList tr ").filter(function () {
            //        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            //    });
            //});
        });
        function BindMakeAssociateModel() {
            //debugger
            if ($("#ContentPlaceHolder1_gvList tr").length > 2) {
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

            if ($("#ContentPlaceHolder1_gvListSecond tr").length > 2) {
                $("#ContentPlaceHolder1_gvListSecond").DataTable({
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

            if ($("#ContentPlaceHolder1_gvListThird tr").length > 2) {
                $("#ContentPlaceHolder1_gvListThird").DataTable({
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

            if ($("#ContentPlaceHolder1_gvListFourth tr").length > 2) {
                $("#ContentPlaceHolder1_gvListFourth").DataTable({
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Approve  </strong>Purchase Order</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">PO Date : </label>
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
                                            <label class="col-md-3 control-label">PO No. : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtPONO" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSearhPO" CssClass="btn btn-success pull-left" Text="Search PO" OnClick="lnkSearhPO_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export PO" OnClick="lnkExport_Click"><i class="fa fa-file"></i></asp:LinkButton>
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
                                <div class="box">
                                    <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                        CellSpacing="0" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%">
                                        <EmptyDataTemplate>
                                            No Record Found!
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="POTYPE" HeaderText="PO Type" />
                                            <asp:BoundField DataField="PONO" HeaderText="PO No." />
                                            <asp:BoundField DataField="PODTD" HeaderText="PO Date" />
                                            <asp:BoundField DataField="NETPOAMT" HeaderText="PO Amt" />
                                            <asp:BoundField DataField="PLANTCD" HeaderText="Plant Code" />
                                            <asp:BoundField DataField="DEPTNAME" HeaderText="Department" />
                                            <asp:BoundField DataField="REMARK" HeaderText="Remark" />
                                            <asp:BoundField DataField="LISTDESC" HeaderText="Status" />
                                            <asp:BoundField DataField="ENTEREDBY" HeaderText="Entered By" />
                                            <asp:BoundField DataField="UPDATEBY" HeaderText="Updated By" />
                                            <asp:BoundField DataField="SEQ" HeaderText="SEQ" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                            <asp:TemplateField HeaderText="Action" Visible="True">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="btnDetails" Text="Details" OnClick="btnDetails_Click"></asp:LinkButton>
                                                    | 
                                                        <asp:LinkButton runat="server" ID="btnApprove" Text="Approve" OnClick="btnApprove_Click"></asp:LinkButton>
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

    <div class="modal fade" id="modal-detail" data-backdrop="static">
        <div class="modal-dialog modal-lg" style="width: 1350px! important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>PO</strong> Details</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">

                            <div class="col-md-12">
                                <div class="form-group" style="text-align-last: center;">
                                    <asp:Label ID="lblDoctype" runat="server" CssClass="pull-left" Style="font-size: 15px;"></asp:Label>
                                    <asp:HiddenField ID="hfDeptID" runat="server" />
                                    <asp:HiddenField ID="hfSeq" runat="server" />
                                    <asp:HiddenField ID="hfPlant" runat="server" />
                                    <asp:HiddenField ID="hfPODate" runat="server" />
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Invoice To :</label>
                                    <asp:TextBox ID="txtInvoiceTo" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine" Style="height: 120px; width: 410px;"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Delivery To :</label>
                                    <asp:TextBox ID="txtDeliveryTo" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine" Style="height: 120px; width: 410px;"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Supplier :</label>
                                    <asp:TextBox ID="txtSupplier" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine" Style="height: 120px; width: 410px;"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>PO Details :</label>
                                    <asp:TextBox ID="txtPODetail" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine" Style="height: 120px; width: 410px;"></asp:TextBox>
                                    <asp:Label ID="lblPONo" runat="server" CssClass="form-control" Visible="false"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-2" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Material Amount :</label>
                                    <asp:Label ID="lblMaterialAmt" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-2" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Discount Amount :</label>
                                    <asp:Label ID="lblDiscountAmt" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-2" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Other Charges :</label>
                                    <asp:Label ID="lblOtherChg" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-2" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Tax Amount :</label>
                                    <asp:Label ID="lblTaxAmt" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-2" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Total Amount :</label>
                                    <asp:Label ID="lblPOTotalAmt" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="box">

                                    <div class="page-content-wrap">


                                        <div class="row">
                                            <div class="col-md-12">

                                                <div class="panel panel-default tabs">
                                                    <ul class="nav nav-tabs" role="tablist">
                                                        <li class="active"><a href="#tab-first" role="tab" data-toggle="tab">Material</a></li>
                                                        <li><a href="#tab-second" role="tab" data-toggle="tab">Taxation </a></li>
                                                        <li><a href="#tab-third" role="tab" data-toggle="tab">Other Charges </a></li>
                                                    </ul>
                                                    <div class="panel-body tab-content">

                                                        <div class="tab-pane active" id="tab-first">

                                                            <div class="box-body divhorizontal" style="overflow-x: scroll; overflow-y: scroll; height: 300px;">
                                                                <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                                                    CssClass="table table-hover table-striped table-bordered nowrap" ShowHeader="true" GridLines="None">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Sr. No." ItemStyle-CssClass="newStock" HeaderStyle-CssClass="newStock">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPOID" runat="server" Text='<%# Eval("POID") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Curr. Srock" ItemStyle-CssClass="newStock" HeaderStyle-CssClass="newStock">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblStock" runat="server" Text='<%# Eval("STOCK") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Last PO Dtl." ItemStyle-CssClass="newStock" HeaderStyle-CssClass="newStock">
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="lblOldPONo" runat="server" Text='<%# Eval("OLDPONO") %>' Font-Size="12px" Style="letter-spacing: 0px; font-size: 12px; letter-spacing: 0px; border: none; background: none; margin-left: -6px" OnClick="lblOldPONo_Click"></asp:Button>
                                                                                <br />
                                                                                <asp:Label ID="lblOLDPOCREATEDDATE" runat="server" Text='<%# Eval("OLDPOCREATEDDATE") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                                                <br />
                                                                                <asp:Label ID="lblOLDRATE" runat="server" Text='<%# Eval("OLDRATE") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Item Detail" ItemStyle-CssClass="newStock" HeaderStyle-CssClass="newStock">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblITEMCODE" runat="server" Text='<%# Eval("ITEMCODE") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                                                -
                                                                                                        <asp:Label ID="lblITEMDESC" runat="server" Text='<%# Eval("ITEMDESC") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label><br />
                                                                                <asp:Label ID="lblITEMPLANTCD" runat="server" Text='<%# Eval("ITEMPLANTCD") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                                                / 
                                                                                                        <asp:Label ID="lblITEMLOCCD" runat="server" Text='<%# Eval("ITEMLOCCD") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label><br />
                                                                                <asp:Label ID="lblITEMREMARKS" runat="server" Text='<%# Eval("ITEMREMARKS") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label><br />
                                                                                Cost Center :
                                                                                <asp:Label ID="lblCostCenter" runat="server" Text='<%# Eval("COSTCENTER") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>

                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Amount" ItemStyle-CssClass="newRW newStock" HeaderStyle-CssClass="newStock">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblQtyText" runat="server" Text="(Qty)" CssClass="pull-left" Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                                                <asp:Label ID="lblPOQTY" runat="server" Text='<%# Eval("POQTY") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                                                <br />
                                                                                <asp:Label ID="lblRateText" runat="server" Text="(Rate)" CssClass="pull-left" Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                                                <asp:Label ID="lblITEMBRATE" runat="server" Text='<%# Eval("ITEMBRATE") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                                                <br />
                                                                                (<asp:Label ID="lblGSTRATE" runat="server" Text='<%# Eval("GSTRATE") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                                                %)
                                                                                <asp:Label ID="lblTaxText" runat="server" Text="(Tax)" CssClass="pull-left" Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                                                <asp:Label ID="lblTAXAMT" runat="server" Text='<%# Eval("TAXAMT") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                                                <br />
                                                                                <asp:Label ID="lblAmtText" runat="server" Text="(Amt.)" CssClass="pull-left" Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                                                <asp:Label ID="lblITEMAMOUNT" runat="server" Text='<%# Eval("ITEMAMOUNT") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="MR/PR Details" ItemStyle-CssClass="newStock" HeaderStyle-CssClass="newStock">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblPRNO" runat="server" Text='<%# Eval("PRNO") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                                                <br />
                                                                                <asp:Label ID="lblPRAPRV" runat="server" Text='<%# Eval("PRAPRV") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                                                <br />
                                                                                <asp:Label ID="lblMRNO" runat="server" Text='<%# Eval("MRNO") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                                                <br />
                                                                                <asp:Label ID="lblMRAPRV" runat="server" Text='<%# Eval("MRAPRV") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>

                                                        </div>

                                                        <div class="tab-pane" id="tab-second">
                                                            <div class="box-body divhorizontal" style="overflow-x: scroll; overflow-y: scroll; height: 300px;">
                                                                <asp:GridView ID="grvTaxation" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                                                    CssClass="table table-hover table-striped table-bordered nowrap">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="TAXSRNO" HeaderText="Sr. No." />
                                                                        <asp:BoundField DataField="POSRNO" HeaderText="PO Sr. No." />
                                                                        <asp:BoundField DataField="CONDTYPE" HeaderText="Cond. Type" />
                                                                        <asp:BoundField DataField="TAXRATE" HeaderText="Rate" />
                                                                        <asp:BoundField DataField="TAXBASEAMOUNT" HeaderText="Base Amt." />
                                                                        <asp:BoundField DataField="TAXAMOUNT" HeaderText="Tax Amt." />
                                                                        <asp:BoundField DataField="OPERATOR" HeaderText="Operator" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>

                                                        <div class="tab-pane" id="tab-third">
                                                            <div class="box-body divhorizontal" style="overflow-x: scroll; overflow-y: scroll; height: 300px;">
                                                                <asp:GridView ID="grvOtherChg" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                                                    CssClass="table table-hover table-striped table-bordered nowrap">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="CHRGSRNO" HeaderText="Sr. No." />
                                                                        <asp:BoundField DataField="CHRGTYPE" HeaderText="Charge Type" />
                                                                        <asp:BoundField DataField="CHRGAMOUNT" HeaderText="Charge Amount" />
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

                            <div class="col-md-5" style="margin-top: 5px;">
                                <div class="form-group">
                                    <asp:Label ID="lblAPRV1" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-3" style="margin-top: 5px;">
                                <div class="form-group">
                                    <asp:TextBox ID="txtApRejDetReason" runat="server" CssClass="form-control" placeholder="Approve / Reject Reason"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtApRejDetReason" ValidationGroup="ValDetRej">Enter Reject Reason</asp:RequiredFieldValidator>
                                    <br />
                                    <asp:LinkButton runat="server" ID="lnkReject" CssClass="btn btn-success pull-left" Text="Reject PR" OnClick="lnkReject_Click" Visible="false" ValidationGroup="ValDetRej"><i class="fa fa-times-circle"></i> Reject</asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="lnkApprove" CssClass="btn btn-success pull-left" Text="Approve PR" OnClick="lnkApprove_Click" Visible="false"><i class="fa fa-check-circle"></i> Approve</asp:LinkButton>

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
            <h4 class="modal-title" style="color: #faa61a;"><strong>PO</strong> Approval</h4>
            <div class="mb-middle">
                <div class="mb-title" style="color: #faa61a"><span class="fa fa-check"></span>Approve <strong>PO</strong> ?</div>
                <div class="mb-content">
                    <h4 style="color: #ffffff;">PO No. : <strong>
                        <asp:Label runat="server" ID="lblPopupPONO"></asp:Label></strong></h4>
                    <asp:HiddenField ID="hfPopPOId" runat="server" />
                    <asp:HiddenField ID="hfPopDeptcd" runat="server" />
                    <asp:HiddenField ID="hfPODT" runat="server" />
                    <asp:HiddenField ID="hfPopSeq" runat="server" />
                    <asp:HiddenField ID="hfPopPlant" runat="server" />


                    <h4 style="color: #ffffff;">Are you sure you want to Approve this PO?</h4>
                </div>

                <div class="mb-footer">
                    <div class="pull-right">
                        <asp:TextBox ID="txtAPREJReason" runat="server" CssClass="form-control" placeholder="Approve / Reject Reason"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvVal" runat="server" ControlToValidate="txtAPREJReason" ValidationGroup="ValRej">Enter Reject Reason</asp:RequiredFieldValidator>
                        <br />
                        <asp:LinkButton runat="server" ID="lnkPopReject" CssClass="btn btn-success pull-left" Text="Reject PR" OnClick="lnkPopReject_Click" ValidationGroup="ValRej"><i class="fa fa-times-circle"></i> Reject</asp:LinkButton>
                        <asp:LinkButton runat="server" ID="lnkPopApprove" CssClass="btn btn-success pull-left" Text="Approve PR" OnClick="lnkPopApprove_Click"><i class="fa fa-check-circle"></i> Approve</asp:LinkButton>
                        <asp:Label runat="server" ID="lblRightsMessage" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-oldpo" data-backdrop="static">
        <div class="modal-dialog modal-lg" style="width: 1100px! important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Old PO</strong> Details</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group" style="text-align-last: center;">
                                    <asp:GridView ID="gvOldPOData" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                        CssClass="table table-hover table-striped table-bordered nowrap">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr. No." ItemStyle-CssClass="newStock" HeaderStyle-CssClass="newStock">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOldSRNO" runat="server" Text='<%# Eval("SRNO") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="PO Dtl." ItemStyle-CssClass="newStock" HeaderStyle-CssClass="newStock">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblOldPONO" runat="server" Text='<%# Eval("PONO") %>' Font-Size="12px" Style="letter-spacing: 0px; font-size: 12px; letter-spacing: 0px; border: none; background: none; margin-left: -6px"></asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblOldPODT" runat="server" Text='<%# Eval("PODT") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item Detail" ItemStyle-CssClass="newStock" HeaderStyle-CssClass="newStock">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOldITEMCODE" runat="server" Text='<%# Eval("ITEMCODE") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                    -
                                                    <asp:Label ID="lblOldITEMDESC" runat="server" Text='<%# Eval("ITEMDESC") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label><br />
                                                    <asp:Label ID="lblOldPLANT" runat="server" Text='<%# Eval("PLANT") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                    / 
                                                    <asp:Label ID="lblOldLOCATION" runat="server" Text='<%# Eval("LOCATION") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label><br />
                                                    <asp:Label ID="lblOldREMARKS" runat="server" Text='<%# Eval("REMARKS") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Amount" ItemStyle-CssClass="newRW newStock" HeaderStyle-CssClass="newStock">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOldQtyText" runat="server" Text="(Qty)" CssClass="pull-left" Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                    <asp:Label ID="lblOldPOQTY" runat="server" Text='<%# Eval("POQTY") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblOldRateText" runat="server" Text="(Rate)" CssClass="pull-left" Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                    <asp:Label ID="lblOldBASICRATE" runat="server" Text='<%# Eval("BASICRATE") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblOldTotalAmtText" runat="server" Text="(Total Amt. w/o Tax)" CssClass="pull-left" Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                    <asp:Label ID="lblOldTOTALAMRWOTAX" runat="server" Text='<%# Eval("TOTALAMRWOTAX") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblOldTaxText" runat="server" Text="(Tax)" CssClass="pull-left" Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                    (<asp:Label ID="lblOldGSTRATE" runat="server" Text='<%# Eval("GSTRATE") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                    %)
                                                    <asp:Label ID="lblOldTAXAMT" runat="server" Text='<%# Eval("TAXAMT") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblOldTotalTaxText" runat="server" Text="(Total Tax)" CssClass="pull-left" Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                    <asp:Label ID="lblOldTOTALTAXAMT" runat="server" Text='<%# Eval("TOTALTAXAMT") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblOldTotalwithTaxText" runat="server" Text="(Total Amt. with Tax)" CssClass="pull-left" Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                    <asp:Label ID="lbloldTOTALAMTWITHTAX" runat="server" Text='<%# Eval("TOTALAMTWITHTAX") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Vendor Details" ItemStyle-CssClass="newStock" HeaderStyle-CssClass="newStock">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOldVENDCODE" runat="server" Text='<%# Eval("VENDCODE") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblOldVENDNAME" runat="server" Text='<%# Eval("VENDNAME") %>' Font-Size="12px" Style="letter-spacing: 0px;"> </asp:Label>
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

    <input type="hidden" id="menutabid" value="tsmTranMMPO" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />

</asp:Content>
