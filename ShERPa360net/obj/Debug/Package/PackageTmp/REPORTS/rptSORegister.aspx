<%@ Page Title="Sales Order Register" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptSORegister.aspx.cs" Inherits="ShERPa360net.REPORTS.rptSORegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Sales Order Register</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Sales Order </strong>Register</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
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

                                <div class="col-md-12">
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

                                </div>

                                <div class="col-md-12">
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
                                            <label class="col-md-4 control-label">ItemCode : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtItemCode" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSerch" CssClass="btn btn-success pull-left" Text="Search" OnClick="lnkSerch_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
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
                                        <asp:GridView ID="gvList" ClientIDMode="Static" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="SOTYPE" HeaderText="SO Type" />
                                                <asp:BoundField DataField="SONO" HeaderText="SO No" />
                                                <asp:BoundField DataField="SOSRNO" HeaderText="SO Sr. No" />
                                                <asp:BoundField DataField="SODT" HeaderText="SO Date" />
                                                <asp:BoundField DataField="CUSTNAME" HeaderText="Party Name" />
                                                <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc" />
                                                <asp:BoundField DataField="SOQTY" HeaderText="SO Qty" />
                                                <asp:BoundField DataField="Rate" HeaderText="Rate" />
                                                <asp:BoundField DataField="CAMOUNT" HeaderText="Item Value" />
                                                <asp:BoundField DataField="UOMDESC" HeaderText="Unit" />
                                                <asp:BoundField DataField="Segment" HeaderText="Segment" />
                                                <asp:BoundField DataField="DISTCHNL" HeaderText="Dist. Channel" />
                                                <asp:BoundField DataField="PLANTDESCR" HeaderText="Plant" />
                                                <asp:BoundField DataField="LOCDESCR" HeaderText="Location" />
                                                <asp:BoundField DataField="CUSTPARTDESC2" HeaderText="IMEI No." />
                                                <asp:BoundField DataField="JOBID" HeaderText="Job Id" />
                                                <asp:BoundField DataField="REFNO" HeaderText="Ref. No." />
                                                <asp:BoundField DataField="RETAILCUSTNAME" HeaderText="Retail Customer Name" />
                                                <asp:BoundField DataField="SALESCHANNEL" HeaderText="Sales Channel" />
                                                <asp:BoundField DataField="CommAgent" HeaderText="Comm. Agent" />
                                                <asp:BoundField DataField="SALESSCHEME" HeaderText="Sales Scheme" />
                                                <asp:BoundField DataField="OldItem" HeaderText="Original Product" />
                                                <asp:BoundField DataField="ChangeReason" HeaderText="Reason for change" />
                                                <asp:BoundField DataField="DELIDT" HeaderText="Delivery Date" />
                                                <asp:BoundField DataField="ENTRYBY" HeaderText="Entry By" />
                                                <asp:BoundField DataField="ENTRYDATE" HeaderText="Entry Date" />
                                                <asp:BoundField DataField="FROMSTATE" HeaderText="From State" />
                                                <asp:BoundField DataField="TOSTATE" HeaderText="To State" />
                                                <asp:BoundField DataField="ITEMTEXT" HeaderText="Item Remarks" />
                                                <asp:BoundField DataField="REMARK" HeaderText="SO Remarks" />
                                                <asp:BoundField DataField="OLDITEMCODE" HeaderText="Original Item Code" />
                                                <asp:BoundField DataField="SPECITEMCODE" HeaderText="Spec. Item Code" />
                                                <asp:BoundField DataField="FTRANNAME" HeaderText="Courier" />
                                                <asp:BoundField DataField="FWAYBILLNO" HeaderText="Way Bill No." />
                                                <asp:BoundField DataField="PONO" HeaderText="PO Number" />
                                                <asp:BoundField DataField="POSRNO" HeaderText="PO Sr. Number" />
                                                <asp:BoundField DataField="LOCKAMT" HeaderText="Lock Amt." />
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
    <input type="hidden" id="menutabid" value="tsmRptSDSOReg" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptSD" runat="server" />
</asp:Content>
