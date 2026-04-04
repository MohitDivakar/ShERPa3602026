<%@ Page Title="JobSheet Report" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptJobsheetData.aspx.cs" Inherits="ShERPa360net.REPORTS.rptJobsheetData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>JobSheet Report</title>

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
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="page-content-wrap">

        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View Jobsheet Report </strong>
                                <asp:Label ID="lblHeading" runat="server"></asp:Label></h3>
                        </div>

                        <div class="panel-body">

                            <div class="row">

                                <div class="col-md-12">

                                    <div class="col-md-2" id="divfromdate" runat="server" visible="false">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Date From : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2" id="divtodate" runat="server" visible="false">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">To : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker" MaxLength="10" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Segment : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlSegment" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Plant : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlPLant" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Status : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2" id="divSearch" runat="server" visible="false">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSearh" CssClass="btn btn-success pull-left" OnClick="lnkSearh_Click" Enabled="false"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span>   </asp:LinkButton>
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Jobsheet Report Details</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <fieldset class="scheduler-border">

                                            <div class="col-md-12" id="divName" runat="server" visible="true">
                                                <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                    <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                        CellSpacing="0" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%">
                                                        <EmptyDataTemplate>
                                                            <div style="text-align: center; color: red; font-size: 18px;">
                                                                List is empty !
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:BoundField DataField="SEGMENT" HeaderText="Segment" />
                                                            <asp:BoundField DataField="PLANTCD" HeaderText="Plant" />
                                                            <asp:BoundField DataField="JOBDT" HeaderText="Job Dt." />
                                                            <asp:BoundField DataField="ITEMDESC" HeaderText="Product" />
                                                            <asp:BoundField DataField="JOBID" HeaderText="Job Id" />
                                                            <%--<asp:BoundField DataField="ENDCUST" HeaderText="Cust. Name" />--%>
                                                            <asp:BoundField DataField="PRODMAKE" HeaderText="Make" />
                                                            <asp:BoundField DataField="PRODMODEL" HeaderText="Model" />
                                                            <asp:BoundField DataField="IMEINO" HeaderText="IMEI No. 1" />
                                                            <asp:BoundField DataField="IMEINO2" HeaderText="IMEI No. 2" />
                                                            <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                            <asp:BoundField DataField="JOBSTATDESC" HeaderText="Job Status" />
                                                            <asp:BoundField DataField="AGEING" HeaderText="Ageing" />
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

    <input type="hidden" id="menutabid" value="tsmRptJobsheetCount" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptCRM" runat="server" />

</asp:Content>
