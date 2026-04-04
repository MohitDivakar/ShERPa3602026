<%@ Page Title="View Booking Data" Language="C#" MasterPageFile="~/UTILITY/UtilityModule.Master" AutoEventWireup="true" CodeBehind="frmViewBooking.aspx.cs" Inherits="ShERPa360net.UTILITY.frmViewBooking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>View Booking Data</title>

    <script>
        $(document).ready(function () {
            BindMakeAssociateModel();
        });
        function BindMakeAssociateModel() {


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
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  Booking</strong>Data</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="box">
                                    <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                        CellSpacing="0" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%" OnRowDataBound="gvList_RowDataBound">
                                        <EmptyDataTemplate>
                                            No Record Found!
                                        </EmptyDataTemplate>
                                        <Columns>

                                            <asp:TemplateField HeaderText="ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Croma Lot No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCROMALOTNO" runat="server" Text='<%# Eval("CROMALOTNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="QTEK Lot No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQTEKLOTNO" runat="server" Text='<%# Eval("QTEKLOTNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Booking Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBOOKIGTYPE" runat="server" Text='<%# Eval("BOOKIGTYPE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sale Price">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSALESPRICE" runat="server" Text='<%# Eval("SALESPRICE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Booking Amt.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBOOKINGAMT" runat="server" Text='<%# Eval("BOOKINGAMT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Booking By">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBOOKINGDONEBY" runat="server" Text='<%# Eval("BOOKINGDONEBY") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Visit Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVISITDATE" runat="server" Text='<%# Eval("VISITDATE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Booking Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBOOKINGDATE" runat="server" Text='<%# Eval("BOOKINGDATE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="UTR No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUTRNO" runat="server" Text='<%# Eval("UTRNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Booking Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSTATUS" runat="server" Text='<%# Eval("STATUS") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action" Visible="True">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="btnAccept" Text="Accept" OnClick="btnAccept_Click" CssClass="btn btn-success"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action" Visible="True">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="btnCancel" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-success"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action" Visible="True">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="btnReturn" Text="Refund" OnClick="btnReturn_Click" CssClass="btn btn-success"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Action" Visible="True">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="btnPurchase" Text="Purchase" OnClick="btnPurchase_Click" CssClass="btn btn-success" Enabled="false" Visible="false"></asp:LinkButton>
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


    <div class="modal fade" id="modal-Varified" data-backdrop="static">
        <div class="modal-dialog modal-lg" style="width: 1250px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Lot Booking</strong> Details</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="box">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Croma Lot No. :</label>
                                            <asp:Label ID="lblCromaVarLotNo" runat="server" CssClass="form-control"></asp:Label>
                                            <asp:Label ID="lblVarID" runat="server" CssClass="form-control" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Qtek Lot No. :</label>
                                            <asp:Label ID="lblCromaVarQtekNo" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Sales Price :</label>
                                            <asp:Label ID="lblVarSalesPrice" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Booking Type :</label>
                                            <asp:Label ID="lblVarBookingType" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Booking Amt. :</label>
                                        <asp:Label ID="lblVarBookingAmt" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Remarks :</label>
                                        <asp:TextBox ID="txtVarifiedRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtVarifiedRemarks" runat="server" ControlToValidate="txtVarifiedRemarks" ValidationGroup="SaveVar" Style="color: red;"
                                            Display="Dynamic" ErrorMessage="Please Enter Remarks">Please Enter Remarks</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Amount :</label>
                                        <asp:TextBox ID="txtvarifiedAmount" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtvarifiedAmount" runat="server" ControlToValidate="txtvarifiedAmount" ValidationGroup="SaveVar" Style="color: red;"
                                            Display="Dynamic" ErrorMessage="Please Enter Amount">Please Enter Amount</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnSaveVarified" runat="server" CssClass="btn btn-success pull-right" OnClick="btnSaveVarified_Click" Text="Submit" ValidationGroup="SaveVar" />
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="modal-Cancelled" data-backdrop="static">
        <div class="modal-dialog modal-lg" style="width: 1250px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Lot Booking</strong> Details</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="box">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Croma Lot No. :</label>
                                            <asp:Label ID="lblCancelCromaLotNo" runat="server" CssClass="form-control"></asp:Label>
                                            <asp:Label ID="lblCancelID" runat="server" CssClass="form-control" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Qtek Lot No. :</label>
                                            <asp:Label ID="lblCancelQtekLotNo" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Sales Price :</label>
                                            <asp:Label ID="lblCancelSalesPrice" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Booking Type :</label>
                                            <asp:Label ID="lblCancelBookingType" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Booking Amt. :</label>
                                        <asp:Label ID="lblCancelBookingAmt" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Cancel Reason :</label>
                                        <asp:TextBox ID="txtCancelReacon" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtCancelReacon" runat="server" ControlToValidate="txtCancelReacon" ValidationGroup="SaveCancel" Style="color: red;"
                                            Display="Dynamic" ErrorMessage="Please Enter Cancel Reason">Please Enter Cancel Reason</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-success pull-right" OnClick="btnCancel_Click1" Text="Cancel" ValidationGroup="SaveCancel" />
                                    </div>
                                </div>

                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-Returned" data-backdrop="static">
        <div class="modal-dialog modal-lg" style="width: 1250px !important;">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Lot Booking</strong> Details</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="box">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Croma Lot No. :</label>
                                            <asp:Label ID="lblReturnCromaLotNo" runat="server" CssClass="form-control"></asp:Label>
                                            <asp:Label ID="lblReturnID" runat="server" CssClass="form-control" Visible="false"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Qtek Lot No. :</label>
                                            <asp:Label ID="lblReturnQtekLotNo" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Sales Price :</label>
                                            <asp:Label ID="lblReturnSalesPrice" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label>Booking Type :</label>
                                            <asp:Label ID="lblReturnBookingType" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Booking Amt. :</label>
                                        <asp:Label ID="lblReturnBookingAmt" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Return Reason/Remarks :</label>
                                        <asp:TextBox ID="txtReturnRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtReturnRemarks" runat="server" ControlToValidate="txtReturnRemarks" ValidationGroup="SaveReturn" Style="color: red;"
                                            Display="Dynamic" ErrorMessage="Please Enter Return Reason">Please Enter Return Reason</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Return UTR No. :</label>
                                        <asp:TextBox ID="txtReturnUTRNo" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvtxtReturnUTRNo" runat="server" ControlToValidate="txtReturnUTRNo" ValidationGroup="SaveReturn" Style="color: red;"
                                            Display="Dynamic" ErrorMessage="Please Enter Return UTR No.">Please Enter Return UTR No.</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>&nbsp;</label>
                                        <asp:Button ID="btnReturn" runat="server" CssClass="btn btn-success pull-right" OnClick="btnReturn_Click1" Text="Return" ValidationGroup="SaveReturn" />
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

    <input type="hidden" id="menutabid" value="tsmCromaLotUpload" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmUtility" runat="server" />

</asp:Content>
