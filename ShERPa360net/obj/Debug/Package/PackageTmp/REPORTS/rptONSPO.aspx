<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptONSPO.aspx.cs" Inherits="ShERPa360net.REPORTS.rptONSPO" EnableEventValidation="false" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>ONS PO Register</title>


    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />
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

        .YELLOW odd {
            background-color: yellow !important;
        }

        .YELLOW even {
            background-color: yellow !important;
        }

        .RED odd {
            background-color: red !important;
        }

        .RED even {
            background-color: red !important;
        }

        .YELLOW {
            background-color: greenyellow !important;
        }

        .RED {
            background-color: indianred !important;
        }
        .header {
            position: sticky !important;
            top: -14px !important;
            /*background-color: #3D2A70 !important;
            color: white !important;*/
            background-color: #ffffff;
        }
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
            debugger
            if ($("#ContentPlaceHolder1_gvAllList tr").length > 2) {
                $("#ContentPlaceHolder1_gvAllList").DataTable({
                    dom: 'Bfrtip',
                    paging: false,
                    sorting: false,
                    "iTotalDisplayRecords": 25,
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Jangad Stock </strong>Report</h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <fieldset class="scheduler-border">

                                            <div class="col-md-12" id="divAll" runat="server" visible="true">
                                                <div class="box-body divhorizontal" style="overflow-x: scroll; overflow-y: scroll;height:500px;">
                                                    <asp:GridView ID="gvAllList" runat="server" CssClass="table table-hover  table-bordered nowrap"
                                                        CellSpacing="0" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="true" OnRowDataBound="gvAllList_RowDataBound">
                                                        <EmptyDataTemplate>
                                                            No Record Found!
                                                        </EmptyDataTemplate>
                                                        <HeaderStyle CssClass="header" />
                                                        <Columns>
                                                            <asp:BoundField DataField="DIFF" HeaderText="Day Diff." />
                                                            <asp:BoundField DataField="PONO" HeaderText="PO No." />
                                                            <asp:BoundField DataField="SRNO" HeaderText="Sr. No." />
                                                            <asp:BoundField DataField="TRNUM" HeaderText="Job ID" />
                                                            <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                            <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
                                                            <%--<asp:BoundField DataField="POQTY" HeaderText="PO Type" />
                                                            <asp:BoundField DataField="RATE" HeaderText="PO No." />
                                                            <asp:BoundField DataField="CAMOUNT" HeaderText="PO Dt." />
                                                            <asp:BoundField DataField="NETPOAMT" HeaderText="Party Name" />--%>
                                                            <%--<asp:BoundField DataField="VENDCODE" HeaderText="Item Code" />
                                                            <asp:BoundField DataField="VENDNAME" HeaderText="Item Desc." />--%>
                                                            <asp:BoundField DataField="VENDORNAME" HeaderText="Vendor" />
                                                            <asp:BoundField DataField="PMTTERMSDESC" HeaderText="Pay Terms" />
                                                            <asp:BoundField DataField="PLANTCODE" HeaderText="Plant" />
                                                            <asp:BoundField DataField="LOCATIONCODE" HeaderText="Location" />
                                                            <asp:BoundField DataField="ACTUALPLANT" HeaderText="Current Plant" />
                                                            <asp:BoundField DataField="ACTUALLOCATION" HeaderText="Current Location" />
                                                            <asp:BoundField DataField="USERNAME" HeaderText="Create By" />
                                                            <asp:BoundField DataField="CREATEDATE" HeaderText="Create Date" />
                                                            <asp:BoundField DataField="BDO" HeaderText="BDO" />
                                                            <asp:TemplateField HeaderText="COLOR" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblColor" runat="server" Text='<%# Eval("COLOR") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:BoundField DataField="POQTY" HeaderText="PO Qty." />
                                                            <asp:BoundField DataField="RATE" HeaderText="Rate" />
                                                            <asp:BoundField DataField="MATVAL" HeaderText="Material Value" />
                                                            <asp:BoundField DataField="PRNO" HeaderText="PR No." />
                                                            <asp:BoundField DataField="TRNUM" HeaderText="Job Id" />
                                                            <asp:BoundField DataField="SEGMENTDESC" HeaderText="Segment" />
                                                            <asp:BoundField DataField="COSTCENTER" HeaderText="Cost Center" />
                                                            <asp:BoundField DataField="HSVALUE" HeaderText="HS Value" />
                                                            <asp:BoundField DataField="REFNO" HeaderText="Ref. No." />
                                                            <asp:BoundField DataField="REMARK" HeaderText="Remark" />
                                                            <asp:BoundField DataField="IMEINO" HeaderText="IMEI No." />
                                                            <asp:BoundField DataField="DEALERNAME" HeaderText="Dealer Name" />
                                                            <asp:BoundField DataField="BDO" HeaderText="BDO" />
                                                            <asp:BoundField DataField="ASM" HeaderText="ASM" />--%>
                                                        </Columns>
                                                    </asp:GridView>


                                                </div>
                                            </div>

                                            <%--<div class="col-md-12" id="divSummary" runat="server" visible="false">
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
                                                            <asp:BoundField DataField="ITEMAMT" HeaderText="Item Amt." />
                                                            <asp:BoundField DataField="TAXAMT" HeaderText="Tax Amt." />
                                                            <asp:BoundField DataField="OTHERCHARGE" HeaderText="Other Charges" />
                                                            <asp:BoundField DataField="TOTDISC" HeaderText="Discount Amt." />
                                                            <asp:BoundField DataField="NETPOAMT" HeaderText="Total Amt" />
                                                            <asp:BoundField DataField="DEALERNAME" HeaderText="Dealer Name" />
                                                            <asp:BoundField DataField="BDO" HeaderText="BDO" />
                                                            <asp:BoundField DataField="ASM" HeaderText="ASM" />
                                                        </Columns>
                                                    </asp:GridView>


                                                </div>
                                            </div>--%>
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

</asp:Content>
