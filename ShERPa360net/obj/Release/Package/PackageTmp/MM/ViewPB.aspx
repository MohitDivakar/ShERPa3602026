<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="ViewPB.aspx.cs" Inherits="ShERPa360net.MM.ViewPB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="page-content-wrap">

        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>Bill</h3>
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

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">PB No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtPBNO" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">PO No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtPONO" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>

                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkNewPB" CssClass="btn btn-success pull-left" Text="New PB" PostBackUrl="~/MM/CreatePB.aspx"><span tooltip="Add New" flow="down"><i class="fa fa-plus"  aria-hidden="true"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkSerchPO" CssClass="btn btn-success pull-left" Text="Search PB" OnClick="lnkSerchPB_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export PB" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
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
                                                <asp:BoundField DataField="PBTYPE" HeaderText="PB Type" />
                                                <asp:BoundField DataField="PBNO" HeaderText="PB No." />
                                                <asp:BoundField DataField="PBDTD" HeaderText="PB Date" DataFormatString="{0:dd-MM-yyyy}" />
                                                <asp:BoundField DataField="BILLNO" HeaderText="Bill No" />
                                                <asp:BoundField DataField="BILLDTD" HeaderText="Bill Date" DataFormatString="{0:dd-MM-yyyy}" />
                                                <asp:BoundField DataField="PONO" HeaderText="PO No" />
                                                <asp:BoundField DataField="VENDCODE" HeaderText="Vendor Code" />
                                                <asp:BoundField DataField="VENDNAME" HeaderText="Vendor Name" />
                                                <asp:BoundField DataField="REMARK" HeaderText="Remark" />
                                                <asp:TemplateField HeaderText="Action" Visible="true">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="bntView" Text="Details" OnClick="bntView_Click"></asp:LinkButton>
                                                        |
                                                        <asp:LinkButton runat="server" ID="btnPrint" Text="PDF" OnClick="btnPrint_Click"></asp:LinkButton>
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
        <div class="modal-dialog modal-lg top-0">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>PB</strong> Details</h4>
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
                                    <label>PB No. :</label>
                                    <asp:Label ID="lblPBNo" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>PB Date :</label>
                                    <asp:Label ID="lblPBDate" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Bill No :</label>
                                    <asp:Label ID="lblBillNo" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Bill Date :</label>
                                    <asp:Label ID="lblBillDate" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                             <div class="col-md-4">
                                <div class="form-group">
                                    <label>Vendor :</label>
                                    <asp:Label ID="lblPBVendor" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>


                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Payment Terms :</label>
                                    <asp:Label ID="lblPBPayTerms" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Payment Terms Desc. :</label>
                                    <asp:Label ID="lblPBPayTermsDesc" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Remark :</label>
                                    <asp:Label ID="lblRemark" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>


                           

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Material Amount :</label>
                                    <asp:Label ID="lblMaterialAmt" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Tax Amount :</label>
                                    <asp:Label ID="lblTaxAmt" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Discount Amount :</label>
                                    <asp:Label ID="lblDiscountAmt" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Other Charges :</label>
                                    <asp:Label ID="lblOtherChg" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                             <div class="col-md-4">
                                <div class="form-group">
                                    <label>Net PB Amount :</label>
                                    <asp:Label ID="lblNetPBAmt" runat="server" CssClass="form-control"></asp:Label>
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
                                                                        <asp:BoundField DataField="SRNO" HeaderText="Sr. No." />
                                                                        <asp:BoundField DataField="POSRNO" HeaderText="Po Sr No." />
                                                                        <asp:BoundField DataField="PONO" HeaderText="Po No." />
                                                                        <asp:BoundField DataField="MIRNO" HeaderText="GRN No." />
                                                                        <asp:BoundField DataField="MIRSRNO" HeaderText="GRN SR No." />
                                                                        <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                                        <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
                                                                        <asp:BoundField DataField="ITEMGRP" HeaderText="Item Group" />
                                                                        <asp:BoundField DataField="UOMDESC" HeaderText="UOM" />
                                                                        <asp:BoundField DataField="PBQTY" HeaderText="Qty" />
                                                                        <asp:BoundField DataField="BRATE" HeaderText="Base Rate" />
                                                                        <asp:BoundField DataField="RATE" HeaderText="Rate" />
                                                                        <asp:BoundField DataField="DISCAMT" HeaderText="Amount" />
                                                                        <asp:BoundField DataField="CAMOUNT" HeaderText="Amount" />
                                                                        <asp:BoundField DataField="GLCD" HeaderText="GL Code" />
                                                                        <asp:BoundField DataField="CSTCENTCD" HeaderText="Cost Center" />
                                                                        <asp:BoundField DataField="PLANTCD" HeaderText="Plant Code" />
                                                                        <asp:BoundField DataField="LOCCD" HeaderText="Location Code" />
                                                                        <asp:BoundField DataField="PRFCNT" HeaderText="Profit Center" />
                                                                        <asp:BoundField DataField="ASSETCD" HeaderText="Asset Code" />
                                                                        <asp:BoundField DataField="TRNUM" HeaderText="Tracking No." />
                                                                        <asp:BoundField DataField="ITEMTEXT" HeaderText="Item Text" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>

                                                        </div>

                                                        <div class="tab-pane" id="tab-second">
                                                            <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                                <asp:GridView ID="grvTaxation" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                                                    CssClass="table table-hover table-striped table-bordered nowrap">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="CONDORDER" HeaderText="Sr. No." />
                                                                        <asp:BoundField DataField="PBSRNO" HeaderText="PB Sr. No." />
                                                                        <asp:BoundField DataField="CONDTYPE" HeaderText="Cond. Type" />
                                                                        <asp:BoundField DataField="RATE" HeaderText="Rate" />
                                                                        <asp:BoundField DataField="BASEAMT" HeaderText="Base Amt." />
                                                                        <asp:BoundField DataField="TAXAMT" HeaderText="Tax Amt." />
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
                                                                        <asp:BoundField DataField="SRNO" HeaderText="Sr. No." />
                                                                        <asp:BoundField DataField="CHGTYPE" HeaderText="Charge Type" />
                                                                        <asp:BoundField DataField="CHGAMT" HeaderText="Charge Amount" />
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
