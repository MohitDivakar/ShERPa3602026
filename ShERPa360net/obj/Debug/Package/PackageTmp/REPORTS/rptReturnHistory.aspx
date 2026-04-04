<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptReturnHistory.aspx.cs" Inherits="ShERPa360net.REPORTS.rptReturnHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Return History</title>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />


    <script>
        $(document).ready(function () {
            BindGrid();
        });
        function BindGrid() {

            $("#ContentPlaceHolder1_gvList").DataTable({
                dom: 'Bfrtip',
                "pageLength": 15,
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


                <div class="panel panel-default">

                    <div class="panel-heading">
                        <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Return History Data </strong></h3>

                        <asp:LinkButton runat="server" ID="lnkDetails" CssClass="btn btn-success pull-right" Text="Details" PostBackUrl="~/REPORTS/rptReturnHistorySummary.aspx"><i class="fa fa-list"></i>&nbsp; Summary</asp:LinkButton>
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
                                                <asp:BoundField DataField="JOBID" HeaderText="Job Id" />
                                                <asp:BoundField DataField="JOBDT" HeaderText="Job Dt." />
                                                <asp:BoundField DataField="PRVJOB" HeaderText="First Job Id" />
                                                <asp:BoundField DataField="PRVJOBDT" HeaderText="First Job Dt." />
                                                <asp:BoundField DataField="IMEINO" HeaderText="IMEI No." />
                                                <asp:BoundField DataField="PRODMAKE" HeaderText="Make" />
                                                <asp:BoundField DataField="PRODMODEL" HeaderText="Model" />
                                                <asp:BoundField DataField="PITEMDESC" HeaderText="Item Desc." />
                                                <asp:BoundField DataField="PRODGRADE" HeaderText="Prod. Grade" />
                                                <asp:BoundField DataField="PURGRADE" HeaderText="Purchase Grade" />
                                                <asp:BoundField DataField="AGINGFROMFIRSTJOB" HeaderText="Ageing (From <br/>First Job)" HtmlEncode="false" />
                                                <asp:BoundField DataField="AGINGFROMLASTJOB" HeaderText="Ageing (From <br/>Last Job)" HtmlEncode="false" />
                                                <asp:BoundField DataField="BUCKET" HeaderText="Bucket" />
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
