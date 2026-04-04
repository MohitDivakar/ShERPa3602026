<%@ Page Title="View Rate Card" Language="C#" MasterPageFile="~/UTILITY/UtilityModule.Master" AutoEventWireup="true" CodeBehind="frmViewCromaRateCard.aspx.cs" Inherits="ShERPa360net.UTILITY.frmViewCromaRateCard" EnableEventValidation="true" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>View Rate Card</title>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <%--<link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />--%>

    <script>
        $(document).ready(function () {
            BindMakeAssociateModel();
        });

        /*function BindMakeAssociateModel() {*/

        function BindMakeAssociateModel() {
            /*debugger;*/
            //$('#ContentPlaceHolder1_grvData').DataTable().destroy();
            //var table = $('#ContentPlaceHolder1_grvData').DataTable();
            //if (table) {
            //    table.destroy();
            //}

            $("#ContentPlaceHolder1_grvData").DataTable({
                paging: false,
                dom: 'Bfrtip',
                destroy: true,
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

        function BindMakeAssociateModel1() {
            /*debugger;*/
            /*$('#ContentPlaceHolder1_grvData').DataTable().destroy();*/
            //var table = $('#ContentPlaceHolder1_grvData').DataTable();
            //if (table) {
            //    table.destroy();
            //}

            $('#ContentPlaceHolder1_grvData').DataTable({
                paging: false,
                dom: 'Bfrtip',
                destroy: true,
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

        function BindMakeAssociateModel12() {
            //debugger;
            /*$('#ContentPlaceHolder1_grvData').DataTable().destroy();*/
            //var table = $('#ContentPlaceHolder1_grvData').DataTable();
            //if (table) {
            //    table.destroy();
            //}

            $('#ContentPlaceHolder1_grvData').DataTable({
                paging: false,
                dom: 'Bfrtip',
                destroy: true,
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

    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            /*debugger;*/
            document.getElementById("busy-holder1").style.display = "";
            document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";
        }

        function ShowLoading2() {
            /*debugger;*/
            //var txtVendorCode = $("#ContentPlaceHolder1_txtVendorCode").val();
            var ddlVendor = $("#ContentPlaceHolder1_ddlVendor").val();
            var txtVendorName = $("#ContentPlaceHolder1_txtVendorName").val();
            //var txtVendorEmail = $("#ContentPlaceHolder1_txtVendorEmail").val();
            var txtMobileNo = $("#ContentPlaceHolder1_txtMobileNo").val();
            var txtTotalQty = $("#ContentPlaceHolder1_txtTotalQty").val();
            var txtTotalAmt = $("#ContentPlaceHolder1_txtTotalAmt").val();
            var txtFinalAmount = $("#ContentPlaceHolder1_txtFinalAmount").val();
            var ddlPlant = $("#ContentPlaceHolder1_ddlPlant").val();
            var ddlLocation = $("#ContentPlaceHolder1_ddlLocation").val();
            if (ddlVendor != "" && txtVendorName != "" && txtMobileNo != "" && txtTotalQty != "" && txtTotalAmt != "" && txtFinalAmount != "" && ddlPlant != "" && ddlLocation != "") {
                document.getElementById("busy-holder2").style.display = "";
                document.getElementById("ContentPlaceHolder1_btnCreateDoc").style.display = "none";
            }

        }

    </script>

    <style type="text/css">
        .chclass {
        }

            .chclass label {
                position: relative;
                top: -2px;
                left: -5px;
            }

            .chclass input[type="checkbox"] {
                margin: 10px 10px 0px;
            }

            .chclass label {
                margin: 10px 10px 0px;
            }
        /*  .chclass input {
                position: absolute;
                top: 10px;
                left: 10px;
            }*/

        .rowGreen {
            background-color: green !important;
            /*background: #00FF00 !important;*/
        }
    </style>


    <script type="text/javascript">
        function scrollToItem(rowIndex) {
            var grid = document.getElementById('<%= grvData.ClientID %>');
            var row = grid.rows[rowIndex];
            if (row) {
                row.scrollIntoView();
            }
        }
    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <rpt:ReportViewer ID="ReportViewer1" runat="server" SizeToReportContent="true" Visible="false"></rpt:ReportViewer>

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;View Rate Card</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">

                                    <div class="col-md-3">
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Job ID : </label>
                                                <div class="col-md-7 col-xs-12">
                                                    <asp:TextBox ID="txtJobID" runat="server" CssClass="form-control" placeholder="Job ID"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Serial No. : </label>
                                                <div class="col-md-7 col-xs-12">
                                                    <asp:TextBox ID="txtSerialNo" runat="server" CssClass="form-control" placeholder="Serial No."></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Article Code : </label>
                                                <div class="col-md-7 col-xs-12">
                                                    <asp:TextBox ID="txtItemCode" runat="server" CssClass="form-control" placeholder="Article Code"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Show All : </label>
                                                <div class="col-md-7 col-xs-12">
                                                    <asp:CheckBox ID="chkAll" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-12">

                                    <div class="col-md-3">
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Brand : </label>
                                                <div class="col-md-7 col-xs-12">
                                                    <asp:DropDownList ID="ddlBrand" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Category : </label>
                                                <div class="col-md-7 col-xs-12">
                                                    <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Size : </label>
                                                <div class="col-md-7 col-xs-12">
                                                    <asp:DropDownList ID="ddlSize" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Grade : </label>
                                                <div class="col-md-7 col-xs-12">
                                                    <asp:DropDownList ID="ddlGrade" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>





                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="row">

                                    <div class="col-md-9">
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Item Desc. : </label>
                                                <div class="col-md-7 col-xs-12">
                                                    <asp:TextBox ID="txtItemDesc" runat="server" CssClass="form-control" OnTextChanged="txtItemDesc_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <cc:AutoCompleteExtender ServiceMethod="GetItemCode" MinimumPrefixLength="1" CompletionInterval="10"
                                                        EnableCaching="false" CompletionSetCount="1" TargetControlID="txtItemDesc" ID="Auto1" runat="server"
                                                        FirstRowSelected="false" ShowOnlyCurrentWordInCompletionListItem="true">
                                                    </cc:AutoCompleteExtender>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label"></label>
                                                <div class="col-md-7 col-xs-12">
                                                    <asp:LinkButton runat="server" ID="lnkSerch" CssClass="btn btn-success" Text="Search Rate" OnClick="lnkSerch_Click" ValidationGroup="SaveAll"><span tooltip="Search" flow="down"><i class="fa fa-search"></i></span></asp:LinkButton>
                                                    <%--<asp:LinkButton runat="server" ID="lnkrefresh" CssClass="btn btn-success" Text="Refresh" PostBackUrl="~/UTILITY/frmViewCromaRateCard.aspx"><span tooltip="Search" flow="down"><i class="fa fa-refresh"></i></span></asp:LinkButton>--%>
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


    <asp:UpdatePanel ID="updateMaster" runat="server">
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="txtVendorCode" EventName="TextChanged" />--%>
            <asp:AsyncPostBackTrigger ControlID="ddlVendor" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtVendorName" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtDiscountper" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtDiscountRs" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="imgSaveAll" EventName="Click" />
            <%--<asp:PostBackTrigger ControlID="txtDiscountRs" />--%>
        </Triggers>
        <ContentTemplate>



            <div class="page-content-wrap">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="row" style="margin-top: 20px !important;">

                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">
                                                        Qty :
                                                        <asp:Label ID="lblTotalQty" runat="server" CssClass="form-control" Text="0"></asp:Label>
                                                    </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        Amt :
                                                        <asp:Label ID="lblTotalAmt" runat="server" CssClass="form-control" Text="0"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--  </div>
                                    <div class="col-md-12">--%>

                                        <div class="col-md-3">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Price : </label>
                                                    <asp:RadioButtonList ID="rblPrice" runat="server" RepeatDirection="Horizontal" CssClass="chclass" RepeatLayout="Table">
                                                        <asp:ListItem Value="1" Selected="True" Text="Partner Price"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Customer Price"></asp:ListItem>
                                                        <asp:ListItem Value="3" Text="MRP"></asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <asp:LinkButton ID="lnkCrQuot" runat="server" CssClass="btn btn-success pull-left" OnClick="lnkCrQuot_Click" Text="Quotation" ValidationGroup="SaveAll" TabIndex="15" Style="margin-top: 10px !important;" OnClientClick="BindMakeAssociateModel1()"><i class="fa fa-list"></i>   Select for Quotation</asp:LinkButton>
                                            <asp:LinkButton ID="imgSaveAll" OnClientClick="ShowLoading()" runat="server" CssClass="btn btn-success pull-left" OnClick="imgSaveAll_Click" Text="Create Quotation" ValidationGroup="SaveAll" TabIndex="15" Style="margin-top: 10px !important;" Visible="false"><i class="fa fa-save"></i>   Create Quotation</asp:LinkButton>
                                            <div id="busy-holder1" style="display: none" class="clearfix inline pull-left">
                                                <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                                <label>Please wait...</label>
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

                                    <div class="col-md-12">
                                        <div class="box-body divhorizontal" style="overflow-x: scroll; overflow-y: scroll; max-height: 500px !important;">

                                            <asp:GridView ID="grvData" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                CellSpacing="0" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%">
                                                <EmptyDataTemplate>
                                                    No Record Found!
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Select">
                                                        <%--<HeaderTemplate>

                                                            <asp:CheckBox ID="chkSelectAll" runat="server" OnCheckedChanged="chkSelectAll_CheckedChanged" AutoPostBack="true" Enabled="false" />
                                                        </HeaderTemplate>--%>
                                                        <ItemTemplate>
                                                            <asp:UpdatePanel ID="upd1" runat="server">
                                                                <Triggers>
                                                                    <%--<asp:AsyncPostBackTrigger ControlID="chkSelect" EventName="CheckedChanged" />--%>
                                                                    <asp:PostBackTrigger ControlID="chkSelect" />
                                                                </Triggers>
                                                                <ContentTemplate>
                                                                    <%--<asp:CheckBox ID="chkSelect" runat="server" OnCheckedChanged="chkSelect_CheckedChanged" AutoPostBack="true" Enabled="false" />--%>
                                                                    <asp:LinkButton runat="server" ID="chkSelect" CssClass="btn btn-success pull-left" Text="Select" OnClick="chkSelect_CheckedChanged" Visible="true" Enabled="false">Select</asp:LinkButton>
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:BoundField DataField="CATEGORY" HeaderText="Category" />--%>
                                                    <asp:TemplateField HeaderText="Size">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSIZE" runat="server" Text='<%# Eval("SIZE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Category">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCATEGORY" runat="server" Text='<%# Eval("CATEGORY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:BoundField DataField="BRAND" HeaderText="Brand" />--%>
                                                    <asp:TemplateField HeaderText="Brand">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBRAND" runat="server" Text='<%# Eval("BRAND") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:BoundField DataField="ITEMCODE" HeaderText="Article Code" />--%>
                                                    <asp:TemplateField HeaderText="Article Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblITEMCODE" runat="server" Text='<%# Eval("ITEMCODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>




                                                    <%--<asp:BoundField DataField="IETMDESC" HeaderText="Item Descr." />--%>
                                                    <asp:TemplateField HeaderText="Item Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIETMDESC" runat="server" Text='<%# Eval("IETMDESC") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Grade">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGrade" runat="server" Text='<%# Eval("GRADE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:BoundField DataField="JOBID" HeaderText="Job ID" />--%>
                                                    <asp:TemplateField HeaderText="Job ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJOBID" runat="server" Text='<%# Eval("JOBID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Job ID">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkJobid" runat="server" Text='<%# Eval("JOBID") %>' OnClick="lnkJobid_Click"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:BoundField DataField="SERIALNO" HeaderText="Serial No." />--%>
                                                    <asp:TemplateField HeaderText="Serial No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSERIALNO" runat="server" Text='<%# Eval("SERIALNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="MRP">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMRP" runat="server" Text='<%# Eval("MRP") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Online Price">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblONLINEPRICE" runat="server" Text='<%# Eval("ONLINEPRICE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:BoundField DataField="DEALERPRICE" HeaderText="Dealer Price" />--%>
                                                    <asp:TemplateField HeaderText="Partner Price">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDEALERPRICE" runat="server" Text='<%# Eval("DEALERPRICE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Purchase Price" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPURCHASEPRICE" runat="server" Text='<%# Eval("PURCHASEPRICE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:BoundField DataField="CUSTOMERPRCE" HeaderText="Sale Price" />--%>
                                                    <asp:TemplateField HeaderText="Customer Price">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCUSTOMERPRCE" runat="server" Text='<%# Eval("CUSTOMERPRCE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Price Differ (%)" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDIFFER" runat="server" Text='<%# Eval("DIFFER") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:BoundField DataField="SALEDONE" HeaderText="Sold Status" />--%>
                                                    <asp:TemplateField HeaderText="Sold Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSALEDONE" runat="server" Text='<%# Eval("SALEDONE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Job Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJOBSTATDESC" runat="server" Text='<%# Eval("JOBSTATDESC") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Location">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLOCATION" runat="server" Text='<%# Eval("LOCATION") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:BoundField DataField="USERNAME" HeaderText="Create By" />--%>
                                                    <asp:TemplateField HeaderText="Create By">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUSERNAME" runat="server" Text='<%# Eval("USERNAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:BoundField DataField="CREATEDATE" HeaderText="Create Date" />--%>
                                                    <asp:TemplateField HeaderText="Create Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCREATEDATE" runat="server" Text='<%# Eval("CREATEDATE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Tax Rate" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRATE" runat="server" Text='<%# Eval("RATE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblITEMID" runat="server" Text='<%# Eval("ITEMID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item Grp ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblITEMGRP" runat="server" Text='<%# Eval("ITEMGRP") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="UOM" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Base Amount" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBASEAMOUNT" runat="server" Text='<%# Eval("BASEAMOUNT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Cond. ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCONDID" runat="server" Text='<%# Eval("CONDID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Cond. Type" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCONDTYPE" runat="server" Text='<%# Eval("CONDTYPE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="URL" Visible="false">
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="lblURL" runat="server" Text='<%# Eval("URL") %>'></asp:Label>--%>
                                                            <asp:HyperLink ID="lblURL" runat="server" Target="_blank" Text="Open Product" NavigateUrl='<%# Eval("URL") %>'></asp:HyperLink>
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
            <br />
            <div class="page-content-wrap">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="row" style="margin-top: 20px !important;">
                                    <div class="col-md-12">
                                        <div class="box-body divhorizontal" style="overflow-x: scroll; overflow-y: scroll; max-height: 500px !important;">

                                            <asp:GridView ID="grvTempData" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                CellSpacing="0" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%">
                                                <EmptyDataTemplate>
                                                    No Record Found!
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <%--<asp:TemplateField HeaderText="Select">
                                                        <HeaderTemplate>

                                                            <asp:CheckBox ID="chkSelectAll" runat="server" OnCheckedChanged="chkSelectAll_CheckedChanged" AutoPostBack="true" Enabled="false" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:UpdatePanel ID="upd1" runat="server">
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="chkSelect" EventName="CheckedChanged" />
                                                                </Triggers>
                                                                <ContentTemplate>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" OnCheckedChanged="chkSelect_CheckedChanged" AutoPostBack="true" Enabled="false" />
                                                                </ContentTemplate>
                                                            </asp:UpdatePanel>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <%--<asp:BoundField DataField="CATEGORY" HeaderText="Category" />--%>
                                                    <asp:TemplateField HeaderText="Size">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSIZE" runat="server" Text='<%# Eval("SIZE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Category">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCATEGORY" runat="server" Text='<%# Eval("CATEGORY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:BoundField DataField="BRAND" HeaderText="Brand" />--%>
                                                    <asp:TemplateField HeaderText="Brand">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBRAND" runat="server" Text='<%# Eval("BRAND") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:BoundField DataField="ITEMCODE" HeaderText="Article Code" />--%>
                                                    <asp:TemplateField HeaderText="Article Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblITEMCODE" runat="server" Text='<%# Eval("ITEMCODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                    <%--<asp:BoundField DataField="IETMDESC" HeaderText="Item Descr." />--%>
                                                    <asp:TemplateField HeaderText="Item Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIETMDESC" runat="server" Text='<%# Eval("IETMDESC") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Grade">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGrade" runat="server" Text='<%# Eval("GRADE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:BoundField DataField="JOBID" HeaderText="Job ID" />--%>
                                                    <asp:TemplateField HeaderText="Job ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJOBID" runat="server" Text='<%# Eval("JOBID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:BoundField DataField="SERIALNO" HeaderText="Serial No." />--%>
                                                    <asp:TemplateField HeaderText="Serial No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSERIALNO" runat="server" Text='<%# Eval("SERIALNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="MRP">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMRP" runat="server" Text='<%# Eval("MRP") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Online Price">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblONLINEPRICE" runat="server" Text='<%# Eval("ONLINEPRICE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:BoundField DataField="DEALERPRICE" HeaderText="Dealer Price" />--%>
                                                    <asp:TemplateField HeaderText="Partner Price">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDEALERPRICE" runat="server" Text='<%# Eval("DEALERPRICE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Purchase Price" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPURCHASEPRICE" runat="server" Text='<%# Eval("PURCHASEPRICE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:BoundField DataField="CUSTOMERPRCE" HeaderText="Sale Price" />--%>
                                                    <asp:TemplateField HeaderText="Customer Price">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCUSTOMERPRCE" runat="server" Text='<%# Eval("CUSTOMERPRCE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Price Differ (%)" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDIFFER" runat="server" Text='<%# Eval("DIFFER") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:BoundField DataField="SALEDONE" HeaderText="Sold Status" />--%>
                                                    <asp:TemplateField HeaderText="Sold Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSALEDONE" runat="server" Text='<%# Eval("SALEDONE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Job Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJOBSTATDESC" runat="server" Text='<%# Eval("JOBSTATDESC") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Location">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLOCATION" runat="server" Text='<%# Eval("LOCATION") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:BoundField DataField="USERNAME" HeaderText="Create By" />--%>
                                                    <asp:TemplateField HeaderText="Create By">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUSERNAME" runat="server" Text='<%# Eval("USERNAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:BoundField DataField="CREATEDATE" HeaderText="Create Date" />--%>
                                                    <asp:TemplateField HeaderText="Create Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCREATEDATE" runat="server" Text='<%# Eval("CREATEDATE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Tax Rate" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRATE" runat="server" Text='<%# Eval("RATE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblITEMID" runat="server" Text='<%# Eval("ITEMID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item Grp ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblITEMGRP" runat="server" Text='<%# Eval("ITEMGRP") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="UOM" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Base Amount" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBASEAMOUNT" runat="server" Text='<%# Eval("BASEAMOUNT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Cond. ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCONDID" runat="server" Text='<%# Eval("CONDID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Cond. Type" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCONDTYPE" runat="server" Text='<%# Eval("CONDTYPE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkTempDelete" runat="server" Text="Remove" OnClick="lnkTempDelete_Click"></asp:LinkButton>
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
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" style="color: #337ab7"><strong>Quotation</strong></h4>
                        </div>
                        <div class="modal-body">
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Vendor Code : </label>
                                                <br />
                                                <%--<asp:TextBox ID="txtVendorCode" runat="server" CssClass="form-control" OnTextChanged="txtVendorCode_TextChanged" MaxLength="10" AutoPostBack="true"></asp:TextBox>--%>
                                                <asp:DropDownList ID="ddlVendor" runat="server" CssClass="form-control ddlVendor" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged" AutoPostBack="true" Width="192"></asp:DropDownList>
                                                <%--<asp:RequiredFieldValidator ID="rfvVendorCode" runat="server" ControlToValidate="txtVendorCode" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Vendor Code" ValidationGroup="Check">Enter Vendor Code</asp:RequiredFieldValidator>--%>
                                                <asp:RequiredFieldValidator ID="rfvVendorCode" runat="server" ControlToValidate="ddlVendor" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Vendor Code" ValidationGroup="Check" InitialValue="0">Enter Vendor Code</asp:RequiredFieldValidator>
                                                <asp:Label ID="lblVendorError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Vendor Name : </label>
                                                <asp:TextBox ID="txtVendorName" runat="server" CssClass="form-control" Enabled="true" OnTextChanged="txtVendorName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvVrndorName" runat="server" ControlToValidate="txtVendorName" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Vendor Name" ValidationGroup="Check" Enabled="false">Enter Vendor Name</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Vendor Mobile : </label>
                                                <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" Enabled="true" MaxLength="10"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvMobileNo" runat="server" ControlToValidate="txtMobileNo" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Vendor Mobile No." ValidationGroup="Check" Enabled="true">Enter Vendor Mobile No.</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revMobileNo" runat="server" ControlToValidate="txtMobileNo" Display="Dynamic" ForeColor="Red" ValidationGroup="Check"
                                                    ErrorMessage="Invalid Mobile No." ValidationExpression="[0-9]{10}$">Invalid Mobile No.</asp:RegularExpressionValidator>
                                            </div>
                                        </div>

                                        <%-- <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Vendor Email : </label>
                                                <asp:TextBox ID="txtVendorEmail" runat="server" CssClass="form-control" Enabled="true"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtVendorEmail" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Vendor Email" ValidationGroup="Check" Enabled="true">Enter Vendor Email</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revManagerEmail" runat="server" ControlToValidate="txtVendorEmail" Display="Dynamic" ForeColor="Red" ValidationGroup="Check"
                                                    ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid Email</asp:RegularExpressionValidator>
                                            </div>
                                        </div>--%>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Total Qty. : </label>
                                                <asp:TextBox ID="txtTotalQty" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvQty" runat="server" ControlToValidate="txtTotalQty" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Total Qty" ValidationGroup="Check">Enter Total Qty</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Total Amt. : </label>
                                                <asp:TextBox ID="txtTotalAmt" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ControlToValidate="txtTotalAmt" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Total Amount" ValidationGroup="Check">Enter Total Amount</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Discount (%) : </label>
                                                <asp:TextBox ID="txtDiscountper" runat="server" CssClass="form-control" Enabled="true" OnTextChanged="txtDiscountper_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvDicountper" runat="server" ControlToValidate="txtDiscountper" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Discount in percentage or in Rupees" ValidationGroup="Check">Enter Discount in percentage or in Rupees</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Discount (Rs.) : </label>
                                                <asp:TextBox ID="txtDiscountRs" runat="server" CssClass="form-control" Enabled="true" OnTextChanged="txtDiscountRs_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvDicountRupees" runat="server" ControlToValidate="txtDiscountRs" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Discount in percentage or in Rupees" ValidationGroup="Check">Enter Discount in percentage or in Rupees</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Final Amount : </label>
                                                <asp:TextBox ID="txtFinalAmount" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvFinalAmount" runat="server" ControlToValidate="txtFinalAmount" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Final Amount" ValidationGroup="Check">Enter Final Amount</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Plant : </label>
                                                <asp:DropDownList ID="ddlPlant" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvPlant" runat="server" ControlToValidate="ddlPlant" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Select Plant" ValidationGroup="Check" InitialValue="0">Select Plant</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Location : </label>
                                                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ControlToValidate="ddlLocation" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Select Location" ValidationGroup="Check" InitialValue="0">Select Location</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Payment Terms : </label>
                                                <asp:DropDownList ID="ddlPaymentTerms" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvPaymentTerms" runat="server" ControlToValidate="ddlPaymentTerms" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Select Payment Terms" ValidationGroup="Check" InitialValue="0">Select Payment Terms</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3"></div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-3"></div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>&nbsp;</label>
                                                <asp:Button ID="btnCreateDoc" runat="server" OnClientClick="ShowLoading2()" CssClass="btn btn-success" Text="Save Quotation" OnClick="btnCreateDoc_Click" ValidationGroup="Check" />
                                                <div id="busy-holder2" style="display: none" class="clearfix inline pull-left">
                                                    <img id="img2" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                                    <label>Please wait...</label>
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

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmRptCromaRateCard" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmReportUtility" runat="server" />

</asp:Content>
