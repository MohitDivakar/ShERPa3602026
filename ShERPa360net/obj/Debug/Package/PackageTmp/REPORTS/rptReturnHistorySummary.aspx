<%@ Page Title="Return History Summary" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptReturnHistorySummary.aspx.cs" Inherits="ShERPa360net.REPORTS.rptReturnHistorySummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Return History Summary</title>

    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />--%>


    <%--<script>
        $(document).ready(function () {
            BindGrid();
        });
        function BindGrid() {

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
    </script>--%>

   

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">

        <div class="row">
            <div class="col-md-12">


                <div class="panel panel-default">

                    <div class="panel-heading">
                        <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Return History Data Summary</strong></h3>

                        <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-right" Text="Export" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
                        <asp:LinkButton runat="server" ID="lnkDetails" CssClass="btn btn-success pull-right" Text="Details" PostBackUrl="~/REPORTS/rptReturnHistory.aspx"><span tooltip="Detail" flow="down"><i class="fa fa-list"></i> </span></asp:LinkButton>

                    </div>
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
                                                <%--<asp:BoundField DataField="DOCTYPE" HeaderText="Doc Type" />--%>
                                                <asp:BoundField DataField="GRADE" HeaderText="Job Id" />
                                                <asp:BoundField DataField="<= 15" HeaderText="<= 15" />
                                                <asp:BoundField DataField="<= 30" HeaderText="<= 30" />
                                                <asp:BoundField DataField="WITH IN 1 YEAR" HeaderText="WITH IN 1 YEAR" />
                                                <asp:BoundField DataField="OLDER THEN 1 YEAR" HeaderText="OLDER THEN 1 YEAR" />
                                                <asp:BoundField DataField="GRAND TOTAL" HeaderText="GRAND TOTAL" />
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

    <input type="hidden" id="menutabid" value="tsmPhyDocVarData" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptMM" runat="server" />

</asp:Content>
