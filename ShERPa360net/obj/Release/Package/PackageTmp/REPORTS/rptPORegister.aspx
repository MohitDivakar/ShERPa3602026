<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptPORegister.aspx.cs" Inherits="ShERPa360net.REPORTS.rptPORegister" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>PO Register</title>

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
            //    $("#ContentPlaceHolder1_gvList tr ").filter(function () {
            //        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            //    });
            //});
        });
        function BindMakeAssociateModel() {
            debugger
            if ($("#ContentPlaceHolder1_gvAllList tr").length > 2) {
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
            }

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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Purchase  </strong>Register</h3>
                        </div>

                        <div class="panel-body">

                            <div class="row">

                                <div class="col-md-12">

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
                                    <%-- </div>


                                <div class="col-md-12" style="padding-top: 10px !important;">--%>

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




                                </div>

                                <div class="col-md-12" style="padding-top: 10px !important;">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">TR No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtTRNo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">PO No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtPONo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">PR No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtPRNo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Ref. No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtRefNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                </div>

                                <div class="col-md-12" style="padding-top: 10px !important;">



                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Vend. Code : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtVendCode" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Material Code : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtItemCode" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Only Summary : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:CheckBox ID="chkSummary" runat="server" Checked="false" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12" style="padding-top: 10px !important;">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">IMEI No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtIMEINo" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Item Group : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlItemGroup" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <%--<asp:LinkButton runat="server" ID="lnkNewVend" CssClass="btn btn-success pull-left" Text="New Vend" PostBackUrl="~/CRM/frmVendorMaster.aspx"><i class="fa fa-file-text"></i></asp:LinkButton>--%>
                                            <asp:LinkButton runat="server" ID="lnkSearh" CssClass="btn btn-success pull-left" Text="Search PO" OnClick="lnkSearh_Click">
                                            <span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                            <%--<asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export PO Register" OnClick="lnkExport_Click">
                                            <span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>--%>
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
                <div class="form-horizontal">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;PO Register</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <fieldset class="scheduler-border">

                                            <div class="col-md-12" id="divAll" runat="server" visible="true">
                                                <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                    <asp:GridView ID="gvAllList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                        CellSpacing="0" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="true">
                                                        <EmptyDataTemplate>
                                                            No Record Found!
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:BoundField DataField="PLANTCD" HeaderText="Plant" />
                                                            <asp:BoundField DataField="LOCCD" HeaderText="Location" />
                                                            <asp:BoundField DataField="PLANTDESCR" HeaderText="Plant Desc." Visible="false" />
                                                            <asp:BoundField DataField="LOCDESCR" HeaderText="Loc. Desc." Visible="false" />
                                                            <asp:BoundField DataField="POTYPE" HeaderText="PO Type" />
                                                            <asp:BoundField DataField="PONO" HeaderText="PO No." />
                                                            <asp:BoundField DataField="PODT" HeaderText="PO Dt." />
                                                            <asp:BoundField DataField="VENDCODE" HeaderText="Party Code" />
                                                            <asp:BoundField DataField="VENDNAME" HeaderText="Party Name" />
                                                            <asp:BoundField DataField="DEALERID" HeaderText="Dealer ID" />
                                                            <asp:BoundField DataField="DEALERNAME" HeaderText="Dealer Name" />
                                                            <asp:BoundField DataField="SRNO" HeaderText="PO Sr. No." />
                                                            <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                            <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
                                                            <asp:BoundField DataField="MODEL" HeaderText="Model" />
                                                            <asp:BoundField DataField="MAKE" HeaderText="Make" />
                                                            <asp:BoundField DataField="ITEMGRP" HeaderText="Item Group" />
                                                            <asp:BoundField DataField="ITEMSUBGRPDESC" HeaderText="Sub Group" />
                                                            <asp:BoundField DataField="HSNGRPDESC" HeaderText="HSN Item Group" />
                                                            <asp:BoundField DataField="UOMDESC" HeaderText="Unit" />
                                                            <asp:BoundField DataField="POQTY" HeaderText="PO Qty." />
                                                            <asp:BoundField DataField="RATE" HeaderText="Rate" />
                                                            <asp:BoundField DataField="MATVAL" HeaderText="Material Value" />
                                                            <asp:BoundField DataField="TAXAMT" HeaderText="Tax Amt." />
                                                            <asp:BoundField DataField="TOTALAMT" HeaderText="Total Amt." />
                                                            <asp:BoundField DataField="PMTTERMS" HeaderText="Pay Terms" />
                                                            <asp:BoundField DataField="PMTTERMSDESC" HeaderText="Pay Terms Desc." />
                                                            <asp:BoundField DataField="PRNO" HeaderText="PR No." />
                                                            <asp:BoundField DataField="TRNUM" HeaderText="TR No." />
                                                            <asp:BoundField DataField="SEGMENTDESC" HeaderText="Segment" />
                                                            <asp:BoundField DataField="COSTCENTER" HeaderText="Cost Center" />
                                                            <asp:BoundField DataField="HSVALUE" HeaderText="HS Value" />
                                                            <asp:BoundField DataField="REFNO" HeaderText="Ref. No." />
                                                            <asp:BoundField DataField="REMARK" HeaderText="Remark" />
                                                            <asp:BoundField DataField="IMEINO" HeaderText="IMEI No." />
                                                            <asp:BoundField DataField="CREATEBY" HeaderText="Create By" />
                                                            <asp:BoundField DataField="CREATEDATE" HeaderText="Create Date" />
                                                            <asp:BoundField DataField="CITY" HeaderText="Vendor City" />
                                                            <asp:BoundField DataField="FROMSTATE" HeaderText="From State" />
                                                            <asp:BoundField DataField="TOSTATE" HeaderText="To State" />
                                                            <asp:BoundField DataField="LISTINGID" HeaderText="Listing ID" />
                                                            <asp:BoundField DataField="LISTINGTYPE" HeaderText="Listing Type" />
                                                            <asp:BoundField DataField="ISKRO" HeaderText="Is KRO" />
                                                            <asp:BoundField DataField="AMAZON" HeaderText="ASIN" />
                                                            <asp:BoundField DataField="BASICPURRATE" HeaderText="Basic Pur. Rate" />
                                                            <asp:BoundField DataField="FinalApproveListingAmount" HeaderText="Final Approval Amt." />
                                                            <asp:BoundField DataField="ITEMTEXT" HeaderText="Item Remarks" />

                                                            <%--<asp:BoundField DataField="DEALERNAME" HeaderText="Dealer Name" />
                                                            <asp:BoundField DataField="BDO" HeaderText="BDO" />
                                                            <asp:BoundField DataField="ASM" HeaderText="ASM" />--%>
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
                                                            <asp:BoundField DataField="PLANTCD" HeaderText="Plant" />
                                                            <asp:BoundField DataField="LOCCD" HeaderText="Location" />
                                                            <asp:BoundField DataField="POTYPE" HeaderText="PO Type" />
                                                            <asp:BoundField DataField="PONO" HeaderText="PO No." />
                                                            <asp:BoundField DataField="PODT" HeaderText="PO Dt." />
                                                            <asp:BoundField DataField="VENDCODE" HeaderText="Party Code" />
                                                            <asp:BoundField DataField="VENDNAME" HeaderText="Party Name" />
                                                            <asp:BoundField DataField="DEALERID" HeaderText="Dealer ID" />
                                                            <asp:BoundField DataField="DEALERNAME" HeaderText="Dealer Name" />
                                                            <%--<asp:BoundField DataField="ITEMRATE" HeaderText="Item Rate" />--%>
                                                            <asp:BoundField DataField="ITEMAMT" HeaderText="Item Amt." />
                                                            <asp:BoundField DataField="TAXAMT" HeaderText="Tax Amt." />
                                                            <asp:BoundField DataField="OTHERCHARGE" HeaderText="Other Charges" />
                                                            <asp:BoundField DataField="OTHTAXAMT" HeaderText="Othr. Chg. Tax" />
                                                            <asp:BoundField DataField="TOTDISC" HeaderText="Discount Amt." />
                                                            <asp:BoundField DataField="NETPOAMT" HeaderText="Total Amt" />
                                                            <asp:BoundField DataField="PMTTERMS" HeaderText="Pay Terms" />
                                                            <asp:BoundField DataField="PMTTERMSDESC" HeaderText="Pay Terms Desc." />
                                                            <asp:BoundField DataField="CREATEBY" HeaderText="Create By" />
                                                            <asp:BoundField DataField="CREATEDATE" HeaderText="Create Date" />
                                                            <asp:BoundField DataField="CITY" HeaderText="Vendor City" />
                                                            <asp:BoundField DataField="FROMSTATE" HeaderText="From State" />
                                                            <asp:BoundField DataField="TOSTATE" HeaderText="To State" />
                                                            <asp:BoundField DataField="LISTINGID" HeaderText="Listing ID" />
                                                            <asp:BoundField DataField="LISTINGTYPE" HeaderText="Listing Type" />
                                                            <asp:BoundField DataField="ISKRO" HeaderText="Is KRO" />
                                                            <asp:BoundField DataField="AMAZON" HeaderText="ASIN" />
                                                            <asp:BoundField DataField="BASICPURRATE" HeaderText="Basic Pur. Rate" />
                                                            <asp:BoundField DataField="FinalApproveListingAmount" HeaderText="Final Approval Amt." />


                                                            <%--<asp:BoundField DataField="DEALERNAME" HeaderText="Dealer Name" />
                                                            <asp:BoundField DataField="BDO" HeaderText="BDO" />
                                                            <asp:BoundField DataField="ASM" HeaderText="ASM" />--%>
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

    <input type="hidden" id="menutabid" value="tsmRptMMPOReg" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptMM" runat="server" />
    >
</asp:Content>
