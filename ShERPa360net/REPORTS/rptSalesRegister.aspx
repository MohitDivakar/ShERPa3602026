<%@ Page Title="Sales Register" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptSalesRegister.aspx.cs" Inherits="ShERPa360net.REPORTS.rptSalesRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Sales Register</title>



    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />


    <script>
        $(document).ready(function () {
            BindMakeAssociateModel();
        });
        function BindMakeAssociateModel() {

            $("#ContentPlaceHolder1_gvAllList").DataTable({
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

            $("#ContentPlaceHolder1_gvSummary").DataTable({
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

            $("#ContentPlaceHolder1_gvPurDetail").DataTable({
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

            $("#ContentPlaceHolder1_gvACFormat").DataTable({
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
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Sales  </strong>Register</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Segment : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlSegment" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Dist. Chnl. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlDistChnl" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Doc. Type : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlDocType" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">SI No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtSINo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class="col-md-12" style="padding-top: 10px !important;">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Date From : </label>
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
                                            <label class="col-md-4 control-label">SO No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtSONo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Job Id : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtJobId" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class="col-md-12" style="padding-top: 10px !important;">


                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Plant Code : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlPLant" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPLant_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Location Code : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">IMEI No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtIMEINo" runat="server" CssClass="form-control" MaxLength="30"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Ref. No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtRefNo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>


                                </div>

                                <div class="col-md-12" style="padding-top: 10px !important;">


                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Show Summary : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <asp:CheckBox ID="chkShowSummary" runat="server" Checked="false" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">With Pur. Detail : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <asp:CheckBox ID="chkPurDet" runat="server" Checked="false" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Exclude Returns : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <asp:CheckBox ID="chkExcludeReturn" runat="server" Checked="false" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Accounting Format : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <asp:CheckBox ID="chkAcFormat" runat="server" Checked="false" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>




                                </div>

                                <div class="col-md-12" style="padding-top: 10px !important;">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Sales Chnl. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlSalesChnl" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Device Type : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlDeviceType" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Cust. Name : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtCustName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label"></label>
                                            <div class="col-md-7 col-xs-12">
                                                <%--<asp:LinkButton runat="server" ID="lnkNewVend" CssClass="btn btn-success pull-left" Text="New Vend" PostBackUrl="~/CRM/frmVendorMaster.aspx"><i class="fa fa-file-text"></i></asp:LinkButton>--%>
                                                <asp:LinkButton runat="server" ID="lnkSearh" CssClass="btn btn-success pull-left" Text="Search PO" OnClick="lnkSearh_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                                <%--<asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export PO Register" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>--%>


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
    </div>



    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Sales Register</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <fieldset class="scheduler-border">

                                            <div class="col-md-12" id="divAll" runat="server" visible="false">
                                                <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                    <asp:GridView ID="gvAllList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                        CellSpacing="0" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="true">
                                                        <EmptyDataTemplate>
                                                            No Record Found!
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:BoundField DataField="SEGMENT" HeaderText="Segment" />
                                                            <asp:BoundField DataField="PLANTDESCR" HeaderText="Plant" />
                                                            <asp:BoundField DataField="LOCDESCR" HeaderText="Location" />
                                                            <asp:BoundField DataField="TRANTYPE" HeaderText="Doc. Type" />
                                                            <asp:BoundField DataField="SINO" HeaderText="SI No." />
                                                            <asp:BoundField DataField="SIDT" HeaderText="SI Dt." />
                                                            <asp:BoundField DataField="JOBID" HeaderText="Job Id" />
                                                            <asp:BoundField DataField="SONO" HeaderText="SO No." />

                                                            <asp:BoundField DataField="SODT" HeaderText="SO Dt." />
                                                            <asp:BoundField DataField="DOCNO" HeaderText="DC No." />
                                                            <asp:BoundField DataField="DODATE" HeaderText="DC Dt." />
                                                            <asp:BoundField DataField="BILLTOPARTYNM" HeaderText="Party Name" />
                                                            <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                            <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
                                                            <asp:BoundField DataField="ITEMSUBGROUP" HeaderText="Device Type" />
                                                            <asp:BoundField DataField="QTY" HeaderText="Qty" />
                                                            <asp:BoundField DataField="RATE" HeaderText="Rate" />
                                                            <asp:BoundField DataField="CAMOUNT" HeaderText="Item Value" />

                                                            <asp:BoundField DataField="DISCAMT" HeaderText="Disc. Amt." />
                                                            <asp:BoundField DataField="TAXAMT" HeaderText="Tax Amt." />

                                                            <asp:BoundField DataField="PAYMODEDESC" HeaderText="Pay Mode" />
                                                            <asp:BoundField DataField="NETSOAMT" HeaderText="Net SO Amt." />
                                                            <asp:BoundField DataField="PREPAIDAMT" HeaderText="Prepaid Amt." />
                                                            <asp:BoundField DataField="REMAINAMT" HeaderText="Payable Amt." />
                                                            <asp:BoundField DataField="UOMDESC" HeaderText="Unit" />
                                                            <asp:BoundField DataField="ITEMTEXT" HeaderText="Item Remark" />
                                                            <asp:BoundField DataField="REMARK" HeaderText="Remark" />
                                                            <asp:BoundField DataField="CUSTPARTDESC2" HeaderText="IMEI No." />
                                                            <asp:BoundField DataField="SALESCHANNEL" HeaderText="Sales Chnl" />
                                                            <asp:BoundField DataField="RETAILCUSTNAME" HeaderText="Retial Cust." />

                                                            <asp:BoundField DataField="CUSTADD1" HeaderText="Cust. Add1" />
                                                            <asp:BoundField DataField="CUSTADD2" HeaderText="Cust. Add2" />
                                                            <asp:BoundField DataField="CUSTADD3" HeaderText="Cust. Add3" />
                                                            <asp:BoundField DataField="CITY" HeaderText="City" />
                                                            <asp:BoundField DataField="PINCODE" HeaderText="Cust. Pin code" />
                                                            <asp:BoundField DataField="CUSTMOBILENO" HeaderText="Cust. Mobile No." />

                                                            <asp:BoundField DataField="REFSINO" HeaderText="Ref. SI No." />
                                                            <asp:BoundField DataField="REFNO" HeaderText="Ref. No." />
                                                            <asp:BoundField DataField="COMMAGENT" HeaderText="Comm. Agent" />
                                                            <asp:BoundField DataField="SALESSCHEME" HeaderText="Sales Scheme" />
                                                            <asp:BoundField DataField="DISPATCHDT" HeaderText="Dispatch Dt." />
                                                            <asp:BoundField DataField="DELIDATE" HeaderText="Deli. Dt." />
                                                            <asp:BoundField DataField="FROMSTATE" HeaderText="From State" />
                                                            <asp:BoundField DataField="TOSTATE" HeaderText="To State" />

                                                            <asp:BoundField DataField="ORDERNO" HeaderText="Order ID" />
                                                            <asp:BoundField DataField="GSTNO" HeaderText="GST No." />
                                                            <asp:BoundField DataField="FTRANNAME" HeaderText="Courier" />
                                                            <asp:BoundField DataField="FWAYBILLNO" HeaderText="Way Bill No." />
                                                            <asp:BoundField DataField="CURRLOCATION" HeaderText="Current Location" />
                                                            <%--<asp:BoundField DataField="PARTCONS" HeaderText="Part Cost" />--%>


                                                            <%-- <asp:BoundField DataField="OLDITEMCODE" HeaderText="Old Item Code" />
                                                            <asp:BoundField DataField="OLDITEMID" HeaderText="Old Item ID" />
                                                            <asp:BoundField DataField="OLDITEMDESC" HeaderText="Old Item Desc." />
                                                            <asp:BoundField DataField="NEWJOBID" HeaderText="New Job Id" />
                                                            <asp:BoundField DataField="RETURNTYPE" HeaderText="Return Type" />
                                                            <asp:BoundField DataField="RETURNREASON" HeaderText="Return Reason" />--%>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>


                                            <div class="col-md-12" id="divSummary" runat="server" visible="false">
                                                <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                    <asp:GridView ID="gvSummary" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                        CellSpacing="0" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="true">
                                                        <EmptyDataTemplate>
                                                            No Record Found!
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:BoundField DataField="SEGMENT" HeaderText="Segment" />
                                                            <asp:BoundField DataField="TRANTYPE" HeaderText="SI Type" />
                                                            <asp:BoundField DataField="SINO" HeaderText="SI No." />
                                                            <asp:BoundField DataField="SIDT" HeaderText="SI Dt." />
                                                            <asp:BoundField DataField="JOBID" HeaderText="Job No." />
                                                            <asp:BoundField DataField="SONO" HeaderText="SO No." />
                                                            <asp:BoundField DataField="REFNO" HeaderText="Ref. No." />
                                                            <asp:BoundField DataField="BILLTOPARTYNM" HeaderText="Party Name" />
                                                            <asp:BoundField DataField="ITEMAMT" HeaderText="Item Amt." />
                                                            <asp:BoundField DataField="TAXAMT" HeaderText="Tax Amt." />
                                                            <asp:BoundField DataField="OTHERCHARGE" HeaderText="Other Charges" />
                                                            <asp:BoundField DataField="TOTDISC" HeaderText="Disc. Amt." />
                                                            <asp:BoundField DataField="TOTAMT" HeaderText="Total Amt." />
                                                            <asp:BoundField DataField="PAYMODEDESC" HeaderText="Pay Mode" />
                                                            <asp:BoundField DataField="PREPAIDAMT" HeaderText="Prepaid Amt." />
                                                            <asp:BoundField DataField="REMAINAMT" HeaderText="Payable Amt." />
                                                            <asp:BoundField DataField="REMARK" HeaderText="Remark" />
                                                            <asp:BoundField DataField="CUSTNAME" HeaderText="Cust. Name" />
                                                            <asp:BoundField DataField="CITY" HeaderText="City" />
                                                            <asp:BoundField DataField="SALESFROM" HeaderText="Sales From" />
                                                            <asp:BoundField DataField="COMMAGENT" HeaderText="Comm. Agent" />
                                                            <asp:BoundField DataField="SALESSCHEME" HeaderText="Sales Scheme" />
                                                            <asp:BoundField DataField="REFSINO" HeaderText="Ref. SI No." />
                                                            <asp:BoundField DataField="DISPATCHDT" HeaderText="Dispatch Dt." />
                                                            <asp:BoundField DataField="DELIDATE" HeaderText="Deli. Dt." />
                                                            <asp:BoundField DataField="FROMSTATE" HeaderText="From State" />
                                                            <asp:BoundField DataField="TOSTATE" HeaderText="To State" />

                                                            <asp:BoundField DataField="RETAILCUSTNAME" HeaderText="Retail Customer" />
                                                            <asp:BoundField DataField="CUSTADD1" HeaderText="Cust. Add1" />
                                                            <asp:BoundField DataField="CUSTADD2" HeaderText="Cust. Add2" />
                                                            <asp:BoundField DataField="CUSTADD3" HeaderText="Cust. Add3" />
                                                            <asp:BoundField DataField="PINCODE" HeaderText="Cust. Pin Code" />
                                                            <asp:BoundField DataField="CUSTMOBILENO" HeaderText="Cust. Mobile No." />
                                                            <asp:BoundField DataField="ORDERNO" HeaderText="Order ID" />
                                                            <asp:BoundField DataField="GSTNO" HeaderText="GST No." />

                                                            <%-- <asp:BoundField DataField="OLDITEMCODE" HeaderText="Old Item Code" />
                                                            <asp:BoundField DataField="OLDITEMID" HeaderText="Old Item ID" />
                                                            <asp:BoundField DataField="OLDITEMDESC" HeaderText="Old Item Desc." />
                                                            <asp:BoundField DataField="NEWJOBID" HeaderText="New Job Id" />
                                                            <asp:BoundField DataField="RETURNTYPE" HeaderText="Return Type" />
                                                            <asp:BoundField DataField="RETURNREASON" HeaderText="Return Reason" />--%>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>


                                            <div class="col-md-12" id="divPurchase" runat="server" visible="false">
                                                <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                    <asp:GridView ID="gvPurDetail" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                        CellSpacing="0" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="true">
                                                        <EmptyDataTemplate>
                                                            No Record Found!
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:BoundField DataField="SEGMENT" HeaderText="Segment" />
                                                            <asp:BoundField DataField="PLANTDESCR" HeaderText="Plant" />
                                                            <asp:BoundField DataField="LOCDESCR" HeaderText="Location" />
                                                            <asp:BoundField DataField="TRANTYPE" HeaderText="SI Type" />
                                                            <asp:BoundField DataField="SINO" HeaderText="SI No." />
                                                            <asp:BoundField DataField="SIDT" HeaderText="SI Dt." />
                                                            <asp:BoundField DataField="JOBID" HeaderText="Job No." />
                                                            <asp:BoundField DataField="SONO" HeaderText="SO No." />
                                                            <asp:BoundField DataField="SODT" HeaderText="SO Dt." />
                                                            <asp:BoundField DataField="DOCNO" HeaderText="DC No." />
                                                            <asp:BoundField DataField="DODATE" HeaderText="DC Date" />
                                                            <asp:BoundField DataField="BILLTOPARTYNM" HeaderText="Part Name" />
                                                            <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                            <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
                                                            <asp:BoundField DataField="ITEMSUBGROUP" HeaderText="Device Type" />
                                                            <asp:BoundField DataField="QTY" HeaderText="SI Qty" />
                                                            <asp:BoundField DataField="RATE" HeaderText="Rate" />
                                                            <asp:BoundField DataField="CAMOUNT" HeaderText="Item Value" />
                                                            <asp:BoundField DataField="TAXAMT" HeaderText="Tax Amt." />
                                                            <asp:BoundField DataField="DISCAMT" HeaderText="Disc. Amt." />
                                                            <asp:BoundField DataField="PAYMODEDESC" HeaderText="Pay Mode" />
                                                            <asp:BoundField DataField="NETSOAMT" HeaderText="Net SO Amt." />
                                                            <asp:BoundField DataField="PREPAIDAMT" HeaderText="Prepaid Amt." />
                                                            <asp:BoundField DataField="REMAINAMT" HeaderText="Payable Amt." />
                                                            <asp:BoundField DataField="ITEMTEXT" HeaderText="Item Remarks" />
                                                            <asp:BoundField DataField="CUSTPARTDESC2" HeaderText="IMEI No." />
                                                            <asp:BoundField DataField="SALESCHANNEL" HeaderText="Sales Chnl." />
                                                            <asp:BoundField DataField="RETAILCUSTNAME" HeaderText="Retail Customer" />
                                                            <asp:BoundField DataField="CUSTADD1" HeaderText="Cust. Add1" />
                                                            <asp:BoundField DataField="CUSTADD2" HeaderText="Cust. Add2" />
                                                            <asp:BoundField DataField="CUSTADD3" HeaderText="Cust. Add3" />
                                                            <asp:BoundField DataField="CITY" HeaderText="City" />
                                                            <asp:BoundField DataField="PINCODE" HeaderText="Cust. Pin Code" />
                                                            <asp:BoundField DataField="CUSTMOBILENO" HeaderText="Cust. Mobile No." />
                                                            <asp:BoundField DataField="COMMAGENT" HeaderText="Comm. Agent" />
                                                            <asp:BoundField DataField="DISPATCHDT" HeaderText="Dispatch Dt." />
                                                            <asp:BoundField DataField="DELIDATE" HeaderText="Deli. Dt." />
                                                            <asp:BoundField DataField="FROMSTATE" HeaderText="From State" />
                                                            <asp:BoundField DataField="TOSTATE" HeaderText="To State" />
                                                            <asp:BoundField DataField="VENDORNAME" HeaderText="Pur. Vendor Name" />
                                                            <asp:BoundField DataField="ITEMVALUE" HeaderText="Pur. Item Value" />
                                                            <asp:BoundField DataField="POTAXVALUE" HeaderText="Pur. Tax Value" />
                                                            <asp:BoundField DataField="PURCHASEPRICE" HeaderText="Purchase Amt." />
                                                            <asp:BoundField DataField="LISTINGID" HeaderText="Listing ID" />
                                                            <asp:BoundField DataField="LISTINGTYPE" HeaderText="Listing Type" />
                                                            <asp:BoundField DataField="ISKRO" HeaderText="IS KRO" />
                                                            <asp:BoundField DataField="CREATEDATE" HeaderText="Job Dt." />
                                                            <asp:BoundField DataField="ORDERNO" HeaderText="Order ID" />
                                                            <asp:BoundField DataField="GSTNO" HeaderText="GST No." />
                                                            <asp:BoundField DataField="FTRANNAME" HeaderText="Courier" />
                                                            <asp:BoundField DataField="FWAYBILLNO" HeaderText="Way Bill No." />
                                                            <asp:BoundField DataField="CURRLOCATION" HeaderText="Current Location" />
                                                            <asp:BoundField DataField="PARTCONS" HeaderText="Part Cost" />

                                                            <%--  <asp:BoundField DataField="REFNO" HeaderText="Ref. No." />
                                                            <asp:BoundField DataField="OLDITEMCODE" HeaderText="Old Item Code" />
                                                            <asp:BoundField DataField="OLDITEMID" HeaderText="Old Item ID" />
                                                            <asp:BoundField DataField="OLDITEMDESC" HeaderText="Old Item Desc." />
                                                            <asp:BoundField DataField="NEWJOBID" HeaderText="New Job Id" />
                                                            <asp:BoundField DataField="RETURNTYPE" HeaderText="Return Type" />
                                                            <asp:BoundField DataField="RETURNREASON" HeaderText="Return Reason" />--%>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divACFormat" runat="server" visible="false">
                                                <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                    <asp:GridView ID="gvACFormat" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                        CellSpacing="0" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="true">
                                                        <EmptyDataTemplate>
                                                            No Record Found!
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <%--<asp:BoundField DataField="SEGMENT" HeaderText="Segment" />--%>
                                                            <asp:BoundField DataField="PLANTDESCR" HeaderText="Plant" />
                                                            <%--<asp:BoundField DataField="LOCDESCR" HeaderText="Location" />--%>
                                                            <asp:BoundField DataField="TRANTYPE" HeaderText="SI Type" />
                                                            <asp:BoundField DataField="SIDT" HeaderText="SI Dt." />
                                                            <asp:BoundField DataField="SINO" HeaderText="SI No." />
                                                            <asp:BoundField DataField="SONO" HeaderText="SO No." />
                                                            <asp:BoundField DataField="JOBID" HeaderText="Job No." />
                                                            <asp:BoundField DataField="ITEMGROUP" HeaderText="Item Group" />
                                                            <asp:BoundField DataField="BRANDNAME" HeaderText="Brand Name" />
                                                            <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
                                                            <asp:BoundField DataField="QTY" HeaderText="SI Qty" />
                                                            <asp:BoundField DataField="RATE" HeaderText="Rate" />
                                                            <asp:BoundField DataField="DISCAMT" HeaderText="Disc. Amt." />
                                                            <asp:BoundField DataField="TAXAMT" HeaderText="Tax Amt." />
                                                            <asp:BoundField DataField="AMOUNT" HeaderText="Amount" />
                                                            <asp:BoundField DataField="IMEI" HeaderText="IMEI No." />
                                                            <asp:BoundField DataField="SALESCHANNEL" HeaderText="Sales Chnl." />
                                                            <asp:BoundField DataField="RETAILCUSTNAME" HeaderText="Retail Customer" />
                                                            <asp:BoundField DataField="GSTNO" HeaderText="GST No." />
                                                            <asp:BoundField DataField="PINCODE" HeaderText="Cust. Pin Code" />
                                                            <asp:BoundField DataField="FROMSTATE" HeaderText="From State" />
                                                            <asp:BoundField DataField="TOSTATE" HeaderText="To State" />
                                                            <asp:BoundField DataField="REFNO" HeaderText="Ref. No." />
                                                            <asp:BoundField DataField="VENDORNAME" HeaderText="Pur. Vendor Name" />
                                                            <asp:BoundField DataField="PURTAXABLEAMOUNT" HeaderText="Pur. Taxable Amount" />
                                                            <asp:BoundField DataField="PURGSTAMOUNT" HeaderText="Pur. GST Amount" />
                                                            <asp:BoundField DataField="PURGROSSAMOUNT" HeaderText="Pur. Gross Amount" />


                                                            <%--<asp:BoundField DataField="BILLTOPARTYNM" HeaderText="Part Name" />
                                                            <asp:BoundField DataField="ITEMCODE" HeaderText="Item COde" />
                                                            <asp:BoundField DataField="PAYMODEDESC" HeaderText="Pay Mode" />
                                                            <asp:BoundField DataField="NETSOAMT" HeaderText="Net SO Amt." />
                                                            <asp:BoundField DataField="PREPAIDAMT" HeaderText="Prepaid Amt." />
                                                            <asp:BoundField DataField="REMAINAMT" HeaderText="Payable Amt." />
                                                            <asp:BoundField DataField="ITEMTEXT" HeaderText="Item Remarks" />
                                                            <asp:BoundField DataField="CUSTADD1" HeaderText="Cust. Add1" />
                                                            <asp:BoundField DataField="CUSTADD2" HeaderText="Cust. Add2" />
                                                            <asp:BoundField DataField="CUSTADD3" HeaderText="Cust. Add3" />
                                                            <asp:BoundField DataField="CITY" HeaderText="City" />
                                                            <asp:BoundField DataField="CUSTMOBILENO" HeaderText="Cust Mobile No." />
                                                            <asp:BoundField DataField="COMMAGENT" HeaderText="Comm. Agent" />
                                                            <asp:BoundField DataField="DISPATCHDT" HeaderText="Dispatch Dt." />
                                                            <asp:BoundField DataField="DELIDATE" HeaderText="Deli. Dt." />
                                                            <asp:BoundField DataField="ITEMVALUE" HeaderText="Pur. Item Value" />
                                                            <asp:BoundField DataField="POTAXVALUE" HeaderText="Pur. Tax Value" />
                                                            <asp:BoundField DataField="PURCHASEPRICE" HeaderText="Purchase Amt." />
                                                            <asp:BoundField DataField="LISTINGID" HeaderText="Listing ID" />
                                                            <asp:BoundField DataField="LISTINGTYPE" HeaderText="Listing Type" />
                                                            <asp:BoundField DataField="ISKRO" HeaderText="IS KRO" />
                                                            <asp:BoundField DataField="CREATEDATE" HeaderText="Job Dt." />
                                                            <asp:BoundField DataField="FTRANNAME" HeaderText="Courier" />
                                                            <asp:BoundField DataField="FWAYBILLNO" HeaderText="Way Bill No." />
                                                            <asp:BoundField DataField="CURRLOCATION" HeaderText="Current Location" />--%>

                                                            <%--  <asp:BoundField DataField="REFNO" HeaderText="Ref. No." />
                                                            <asp:BoundField DataField="OLDITEMCODE" HeaderText="Old Item Code" />
                                                            <asp:BoundField DataField="OLDITEMID" HeaderText="Old Item ID" />
                                                            <asp:BoundField DataField="OLDITEMDESC" HeaderText="Old Item Desc." />
                                                            <asp:BoundField DataField="NEWJOBID" HeaderText="New Job Id" />
                                                            <asp:BoundField DataField="RETURNTYPE" HeaderText="Return Type" />
                                                            <asp:BoundField DataField="RETURNREASON" HeaderText="Return Reason" />--%>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </fieldset>
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

    <input type="hidden" id="menutabid" value="tsmRptSDSIReg" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptSD" runat="server" />

</asp:Content>
