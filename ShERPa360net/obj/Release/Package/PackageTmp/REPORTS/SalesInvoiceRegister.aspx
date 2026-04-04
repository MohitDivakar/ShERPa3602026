<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="SalesInvoiceRegister.aspx.cs" Inherits="ShERPa360net.REPORTS.SalesInvoiceRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }

        .pd-top {
            padding-top: 15px;
        }

        .pd-bottom {
            padding-bottom: 10px;
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Sales  </strong>Invoice Register</h3>
                        </div>


                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12 pd-top pd-bottom">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Segment : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlsegment" runat="server" CssClass="form-control " TabIndex="5"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-3">
                                        <div class="form-group">

                                            <label class="col-md-4 control-label">Dist. Chnl : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddldistchnl" runat="server" CssClass="form-control " TabIndex="5"></asp:DropDownList>

                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">

                                            <label class="col-md-4 control-label">Doc Type : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddldoctype" runat="server" CssClass="form-control " TabIndex="5"></asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">

                                            <label class="col-md-4 control-label">SINO : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtsino" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                </div>


                                <div class="col-md-12 pd-bottom">
                                    <div class="col-md-3">
                                        <div class="form-group">


                                            <label class="col-md-4 control-label">So No: </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtsono" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">

                                            <label class="col-md-4 control-label">Job Id : </label>
                                            <div class="col-md-8 col-xs-12">

                                                <asp:TextBox ID="txtjobid" runat="server" CssClass="form-control"></asp:TextBox>

                                            </div>
                                        </div>




                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">

                                            <label class="col-md-4 control-label">Plant Code : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlplantncode" runat="server" CssClass="form-control " TabIndex="5" OnSelectedIndexChanged="ddlplantncode_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                                            </div>

                                        </div>
                                    </div>


                                    <div class="col-md-3">
                                        <div class="form-group">

                                            <label class="col-md-4 control-label">Location Code : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddllocationcode" runat="server" CssClass="form-control " TabIndex="5"></asp:DropDownList>

                                            </div>

                                        </div>
                                    </div>



                                </div>

                                <div class="col-md-12 pd-bottom">

                                    <div class="col-md-3">
                                        <div class="form-group">

                                            <label class="col-md-4 control-label">IMEI No : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtimeino" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">

                                            <label class="col-md-4 control-label">Ref. No : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtrefno" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">From Date: </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">To Date: </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txttodate" runat="server" CssClass="form-control datepicker"></asp:TextBox>
                                                </div>

                                            </div>
                                        </div>
                                    </div>


                                </div>

                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">

                                            <label class="col-md-4 control-label">Sales Chnl : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlsaleschnl" runat="server" CssClass="form-control " TabIndex="5"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">

                                            <asp:LinkButton runat="server" ID="lnkSearhMR" CssClass="btn btn-success pull-left" Text="Search MR" OnClick="lnkSearhMR_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export MR" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <%--button--%>
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
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                        <asp:GridView ID="gvList1" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="SEGMENT" HeaderText="SEGMENT" />
                                                <asp:BoundField DataField="PLANTDESCR" HeaderText="PLANT DESCR" />
                                                <asp:BoundField DataField="TRANTYPE" HeaderText="TRAN TYPE" />
                                                <asp:BoundField DataField="GSTNO" HeaderText="GST NO" />
                                                <asp:BoundField DataField="SINO" HeaderText="SINO" />
                                                <asp:BoundField DataField="SISRNO" HeaderText="SI Sr. No." />
                                                <asp:BoundField DataField="SIDT" HeaderText="SIDT" />
                                                <asp:BoundField DataField="SONO" HeaderText="SONO" />
                                                <asp:BoundField DataField="SOSRNO" HeaderText="SO Sr. No." />
                                                <asp:BoundField DataField="JOBID" HeaderText="JOBID" />
                                                <asp:BoundField DataField="MAKE" HeaderText="BRAND NAME" />
                                                <asp:BoundField DataField="MODEL" HeaderText="MODEL DETAIL" />
                                                <asp:BoundField DataField="IMEI Number" HeaderText="IMEI NUMBER" />
                                                <asp:BoundField DataField="ITEMSUBGROUP" HeaderText="ITEMSUBGROUP" />
                                                <asp:BoundField DataField="ITEMCODE" HeaderText="ITEM CODE" />
                                                <asp:BoundField DataField="ITEMDESC" HeaderText="ITEM DESC" />
                                                <asp:BoundField DataField="QTY" HeaderText="QTY" />
                                                <asp:BoundField DataField="RATE" HeaderText="RATE" />
                                                <asp:BoundField DataField="DISCAMT" HeaderText="DISCAMT" />
                                                <asp:BoundField DataField="Net Invoice Amount" HeaderText="NET INVOICE AMOUNT" />
                                                <asp:BoundField DataField="MarginAmt" HeaderText="MARGIN AMT" />
                                                <asp:BoundField DataField="CGST" HeaderText="CGST" />
                                                <asp:BoundField DataField="SGST" HeaderText="SGST" />
                                                <asp:BoundField DataField="IGST" HeaderText="IGST" />
                                                <asp:BoundField DataField="SALESCHANNEL" HeaderText="SALES CHANNEL" />
                                                <asp:BoundField DataField="RETAILCUSTNAME" HeaderText="RETAIL CUSTNAME" />
                                                <asp:BoundField DataField="TOSTATE" HeaderText="TO STATE" />
                                                <asp:BoundField DataField="REFSINO" HeaderText="REFSINO" />
                                                <asp:BoundField DataField="Ref Order No." HeaderText="REF ORDER NO." />
                                                <asp:BoundField DataField="COMMAGENT" HeaderText="COMMAGENT" />
                                                <asp:BoundField DataField="ORDERNO" HeaderText="ORDER NO" />
                                                <asp:BoundField DataField="FTRANNAME" HeaderText="FTRANNAME" />
                                                <asp:BoundField DataField="FWAYBILLNO" HeaderText="FWAYBILLNO" />
                                                <asp:BoundField DataField="payment Mode" HeaderText="PAYMENT MODE" />
                                                <asp:BoundField DataField="PREPAIDAMT" HeaderText="PREPAIDAMT" />
                                                <asp:BoundField DataField="REMAINAMT" HeaderText="REMAINAMT" />
                                                <asp:BoundField DataField="PURCHASEVALUE" HeaderText="PURCHASE VALUE" />
                                                <asp:BoundField DataField="Purchase Vendor Code" HeaderText="PURCHASE VENDOR CODE" />
                                                <asp:BoundField DataField="Purchase Date" HeaderText="PURCHASE DATE" />
                                                <asp:BoundField DataField="Aging Date" HeaderText="AGING DATE" />
                                                <asp:BoundField DataField="PONO" HeaderText="PO NUMBER" />
                                                <asp:BoundField DataField="POSRNO" HeaderText="PO Sr. No." />
                                                <asp:BoundField DataField="PB Number" HeaderText="PB NUMBER" />
                                                <asp:BoundField DataField="PB Sr. Number" HeaderText="PB Sr. No." />
                                                <asp:BoundField DataField="Net Invoice Amount" HeaderText="Sales Rate" />
                                                <asp:BoundField DataField="PURCHASEVALUE" HeaderText="Purchase Value" />
                                                <asp:BoundField DataField="NetMargin" HeaderText="Net Margin" />
                                                <asp:BoundField DataField="RMC" HeaderText="RMC%" />
                                                <asp:BoundField DataField="ProcureType" HeaderText="Procure Type" />
                                                <asp:BoundField DataField="ProcureTypeforRMC" HeaderText="Procure Type wih CONSIGNMENT" />
                                                <asp:BoundField DataField="LISTINGPRICE" HeaderText="Listing Price" />
                                                <asp:BoundField DataField="LOCKAMOUNTPRICE" HeaderText="Final Lock Amount" />
                                                <asp:BoundField DataField="LISTINGID" HeaderText="Listing ID" />
                                                <asp:BoundField DataField="DEVIATION" HeaderText="Deviation" />
                                                <asp:BoundField DataField="DEVIATIONREASON" HeaderText="Deviation Reason" />
                                                <asp:BoundField DataField="LISTINGTYPE" HeaderText="Listing Type" />
                                                <asp:BoundField DataField="LISTVENDORISKRO" HeaderText="Listing Dealer Is KRO" />
                                                <asp:BoundField DataField="POLOCKAMT" HeaderText="PO Lock Amt." />
                                                <asp:BoundField DataField="SOLOCKAMT" HeaderText="SO Lock Amt." />

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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmSalesInvoiceRegister" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptSD" runat="server" />
</asp:Content>
