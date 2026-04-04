<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="AprvPO.aspx.cs" Inherits="ShERPa360net.MM.AprvPO" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />


     

    <script>
        $(document).ready(function () {
            //BindMakeAssociateModel();
            //$("#myInput").on("keyup", function () {
            //    var value = $(this).val().toLowerCase();
            //    $("#ContentPlaceHolder1_gvList tr ").filter(function () {
            //        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            //    });
            //});
        });
        function BindMakeAssociateModel() {
            debugger
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

                                    <%--<div class="col-md-3">
                                        <div class="form-group">



                                            <label class="col-md-3 control-label">PR No. : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtPrno" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>

                                                </div>
                                            </div>



                                        </div>
                                    </div>--%>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSearhPR" CssClass="btn btn-success pull-left" Text="Search PO" OnClick="lnkSearhPR_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkExport" OnClick="lnkExport_Click" CssClass="btn btn-success pull-left" Text="Export Po"><i class="fa fa-file"></i></asp:LinkButton>
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
                                    <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Action" Visible="True">
                                                    <ItemTemplate>
                                                        <%--<asp:LinkButton runat="server" ID="btnDetails" Text="Details" OnClick="btnDetails_Click"></asp:LinkButton>
                                                        | --%>
                                                        <asp:LinkButton runat="server" ID="btnApprove" Text="Approve" OnClick="btnApprove_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PLANTCD" HeaderText="Plant" />
                                                <asp:BoundField DataField="LOCATIONDETAIL" HeaderText="Location" />
                                                <asp:BoundField DataField="LISTINGID" HeaderText="Listing ID" />
                                                <asp:BoundField DataField="BRATE" HeaderText="PO Rate" />
                                                <asp:BoundField DataField="POTYPE" HeaderText="PO Type" />
                                                <asp:BoundField DataField="PONO" HeaderText="PO No." />
                                                <asp:BoundField DataField="PRNO" HeaderText="PR No." />
                                                <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
                                                <asp:BoundField DataField="NEW90" HeaderText="New Rate(90%)" />
                                                <asp:BoundField DataField="VENDORNAME" HeaderText="Vendor Name" />
                                                <asp:BoundField DataField="DEVREASON" HeaderText="Reject Reason" />
                                                <asp:BoundField DataField="CREATEBY" HeaderText="Create By" />
                                                <asp:BoundField DataField="CREATEDATE" HeaderText="Create Dt." />

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
                    <h4 class="modal-title" style="color: #337ab7"><strong>PO</strong> Details</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Doc Type :</label>
                                    <asp:Label ID="lblDoctype" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>PR No. :</label>
                                    <asp:Label ID="lblPONo" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>PR Date :</label>
                                    <asp:Label ID="lblPODate" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>


                            <div class="col-md-4" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Payment Terms :</label>
                                    <asp:Label ID="lblPOPayTerms" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-8" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Payment Terms Desc. :</label>
                                    <asp:Label ID="lblPOPayTermsDesc" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>


                            <div class="col-md-6" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Vendor :</label>
                                    <asp:Label ID="lblPOVendor" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-6" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Transporter :</label>
                                    <asp:Label ID="lblPOTransporter" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-8" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Remark :</label>
                                    <asp:Label ID="lblRemark" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-4" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Net PO Amount :</label>
                                    <asp:Label ID="lblNetPOAmt" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-3" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Material Amount :</label>
                                    <asp:Label ID="lblMaterialAmt" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Tax Amount :</label>
                                    <asp:Label ID="lblTaxAmt" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Discount Amount :</label>
                                    <asp:Label ID="lblDiscountAmt" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Other Charges :</label>
                                    <asp:Label ID="lblOtherChg" runat="server" CssClass="form-control"></asp:Label>
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

                                                            <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                                <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                                                    CssClass="table table-hover table-striped table-bordered nowrap">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="ID" HeaderText="Sr. No." />
                                                                        <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                                        <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
                                                                        <asp:BoundField DataField="ITEMGROUP" HeaderText="Item Group" />
                                                                        <asp:BoundField DataField="ITEMUOM" HeaderText="UOM" />
                                                                        <asp:BoundField DataField="POQTY" HeaderText="Qty" />
                                                                        <asp:BoundField DataField="ITEMRATE" HeaderText="Rate" />
                                                                        <asp:BoundField DataField="ITEMAMOUNT" HeaderText="Amount" />
                                                                        <asp:BoundField DataField="DELIVERYDATE" HeaderText="Delivery Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                                        <asp:BoundField DataField="GLCODE" HeaderText="GL Code" />
                                                                        <asp:BoundField DataField="COSTCENTER" HeaderText="Cost Center" />
                                                                        <asp:BoundField DataField="ITEMPLANTCD" HeaderText="Plant Code" />
                                                                        <asp:BoundField DataField="ITEMLOCCD" HeaderText="Location Code" />
                                                                        <asp:BoundField DataField="PROFITCENTER" HeaderText="Profit Center" />
                                                                        <asp:BoundField DataField="ASSETCODE" HeaderText="Asset Code" />
                                                                        <asp:BoundField DataField="TRACKNO" HeaderText="Tracking No." />
                                                                        <asp:BoundField DataField="ITEMREMARKS" HeaderText="Item Text" />
                                                                        <asp:BoundField DataField="PRNO" HeaderText="PR No." />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>

                                                        </div>

                                                        <div class="tab-pane" id="tab-second">
                                                            <div class="box-body divhorizontal" style="overflow-x: scroll">
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
                                                            <div class="box-body divhorizontal" style="overflow-x: scroll">
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
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtApRejDetReason" ValidationGroup="ValDetRej">Enter Reason</asp:RequiredFieldValidator>
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
                    <h4 style="color: #ffffff;">Are you sure you want to Approve this PO?</h4>
                </div>


                <div class="mb-footer">
                    <div class="pull-right">
                        <asp:TextBox ID="txtAPREJReason" runat="server" CssClass="form-control" placeholder="Approve / Reject Reason"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvVal" runat="server" ControlToValidate="txtAPREJReason" ValidationGroup="ValRej">Enter Reject Reason</asp:RequiredFieldValidator>
                        <br />
                        <asp:LinkButton runat="server" ID="lnkPopReject" CssClass="btn btn-success pull-left" Text="Reject PR" OnClick="lnkPopReject_Click" ValidationGroup="ValRej"><i class="fa fa-times-circle"></i> Reject</asp:LinkButton>
                        <asp:LinkButton runat="server" ID="lnkPopApprove" CssClass="btn btn-success pull-left" Text="Approve PR" ValidationGroup="ValRej" OnClick="lnkPopApprove_Click"><i class="fa fa-check-circle"></i> Approve</asp:LinkButton>
                        <asp:Label runat="server" ID="lblRightsMessage" Visible="false"></asp:Label>
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
