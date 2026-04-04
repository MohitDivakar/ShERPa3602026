<%@ Page Title="Pending Commission Invoice" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptPendingSCM.aspx.cs" Inherits="ShERPa360net.REPORTS.rptPendingSCM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Pending Commission Invoice</title>

    <style>
        .icon1 {
            text-align: center;
            vertical-align: middle;
            height: 116px;
            width: 116px;
            border-radius: 50%;
            color: #999999;
            margin: 0 auto 20px;
            border: 4px solid #fe970a;
            transition: all 0.2s;
            -webkit-transition: all 0.2s;
        }

        .imgcircle {
            height: 65px !important;
            width: 65px !important;
            margin-top: 20px !important;
        }

        .shadow {
            -moz-box-shadow: 3px 3px 5px 6px #fe970a !important;
            -webkit-box-shadow: 0 0 5px #cccccc87 !important;
            border: 5px solid #cccccc87 !important;
            box-shadow: 0 0 5px #cccccc87 !important;
            background: #fff;
        }
    </style>

    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }

        body .table thead th {
            position: sticky !important;
            top: -1px !important;
            /*background-color: #3D2A70 !important;
            color: white !important;*/
        }

        .header {
            position: sticky !important;
            top: -14px !important;
            /*background-color: #3D2A70 !important;
            color: white !important;*/
            background-color: #ffffff;
        }
    </style>

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
    </style>

    <script>
        $(document).ready(function () {
            BindMakeAssociateModel();
        });
        function BindMakeAssociateModel() {
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

            if ($("#ContentPlaceHolder1_gvListReturn tr").length > 2) {
                $("#ContentPlaceHolder1_gvListReturn").DataTable({
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Pending Franchise Commission Invoice  </strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <%--<label class="col-md-3 control-label">Date : </label>--%>
                                        <div class="col-md-9 col-xs-12">
                                            <div class="input-group">
                                                <asp:RadioButtonList ID="rblSIType" runat="server" OnSelectedIndexChanged="rblSIType_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal" RepeatLayout="Table" CssClass="chclass">
                                                    <asp:ListItem Selected="True" Text="Franchise Commission Invoice" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Franchise Commission Invoice Return (Credit Note)" Value="2"></asp:ListItem>
                                                </asp:RadioButtonList>
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


    <div runat="server" id="divSCM" visible="false">
        <div class="page-content-wrap">

            <div class="row">
                <div class="col-md-12">

                    <div class="form-horizontal">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Pending Franchise Commission Invoice  </strong></h3>
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
                                        <div class="box-body divhorizontal" style="overflow-x: scroll; overflow-y: scroll; max-height: 500px !important;">
                                            <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                                <EmptyDataTemplate>
                                                    No Record Found!
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:BoundField HeaderText="Purchase Type" DataField="PURTYPE" />
                                                    <asp:BoundField HeaderText="PO Dt." DataField="PODT" />
                                                    <asp:BoundField HeaderText="Plant" DataField="PLANT" />
                                                    <asp:BoundField HeaderText="Vendor Code" DataField="VENDCODE" />
                                                    <asp:BoundField HeaderText="Vendor Name" DataField="VENDNAME" />
                                                    <asp:BoundField HeaderText="PO No." DataField="PONO" />
                                                    <asp:BoundField HeaderText="SR NO." DataField="SRNO" />
                                                    <asp:BoundField HeaderText="Item Code" DataField="ITEMCODE" />
                                                    <asp:BoundField HeaderText="Item Desc." DataField="ITEMDESC" />
                                                    <asp:BoundField HeaderText="Item Group" DataField="ITEMGRP" />
                                                    <asp:BoundField HeaderText="PO Qty" DataField="POQTY" />
                                                    <asp:BoundField HeaderText="Rate" DataField="RATE" />
                                                    <asp:BoundField HeaderText="Total" DataField="CAMOUNT" />
                                                    <asp:BoundField HeaderText="Job ID" DataField="TRNUM" />
                                                    <asp:BoundField HeaderText="IMEI No." DataField="IMEINO" />
                                                    <asp:BoundField HeaderText="Create By" DataField="USERNAME" />
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
    </div>

    <div runat="server" id="divSCR" visible="false">

        <div class="page-content-wrap">

            <div class="row">
                <div class="col-md-12">

                    <div class="form-horizontal">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Pending Franchise Commission Invoice Return (Credit Note)  </strong></h3>
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
                                        <div class="box-body divhorizontal" style="overflow-x: scroll; overflow-y: scroll; max-height: 500px !important;">
                                            <asp:GridView ID="gvListReturn" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                                <EmptyDataTemplate>
                                                    No Record Found!
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:BoundField HeaderText="Purchase Type" DataField="PURTYPE" />
                                                    <asp:BoundField HeaderText="PO Dt." DataField="PODT" />
                                                    <asp:BoundField HeaderText="Plant" DataField="PLANT" />
                                                    <asp:BoundField HeaderText="Vendor Code" DataField="VENDCODE" />
                                                    <asp:BoundField HeaderText="Vendor Name" DataField="VENDNAME" />
                                                    <asp:BoundField HeaderText="PO No." DataField="PONO" />
                                                    <asp:BoundField HeaderText="SR NO." DataField="SRNO" />
                                                    <asp:BoundField HeaderText="Item Code" DataField="ITEMCODE" />
                                                    <asp:BoundField HeaderText="Item Desc." DataField="ITEMDESC" />
                                                    <asp:BoundField HeaderText="Item Group" DataField="ITEMGRP" />
                                                    <asp:BoundField HeaderText="PO Qty" DataField="POQTY" />
                                                    <asp:BoundField HeaderText="Rate" DataField="RATE" />
                                                    <asp:BoundField HeaderText="Total" DataField="CAMOUNT" />
                                                    <asp:BoundField HeaderText="Job ID" DataField="TRNUM" />
                                                    <asp:BoundField HeaderText="IMEI No." DataField="IMEINO" />
                                                    <asp:BoundField HeaderText="Create By" DataField="USERNAME" />
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
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmRptSDSIReg" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptSD" runat="server" />

</asp:Content>
