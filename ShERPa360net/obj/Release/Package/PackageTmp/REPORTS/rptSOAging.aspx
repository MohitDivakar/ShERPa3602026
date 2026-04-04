<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptSOAging.aspx.cs" Inherits="ShERPa360net.REPORTS.rptSOAging" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
            top: -1px !important;
            /*background-color: #3D2A70 !important;
            color: white !important;*/
        }
    </style>



    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />


<%--    <script>
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
    </script>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>Penoding SO List</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">From : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDocDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">To : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtToDocDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">SO No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtDocNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <%--  </div>

                                <div class="col-md-12" style="margin-top: 10px !important;">--%>

                                    <%--<div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Group By : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlGroupBy" runat="server" CssClass="form-control">
                                                        <asp:ListItem Text="Age" Value="Age" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Text="Make" Value="Make"></asp:ListItem>
                                                        <asp:ListItem Text="Model" Value="Model"></asp:ListItem>
                                                        <asp:ListItem Text="Sales From" Value="PLATFORM"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>

                                    <%--<div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Ascending : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <asp:Button ID="btnGroupBy" runat="server" Text="Group" CssClass="form-control" OnClick="btnGroupBy_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>

                                    <%--<div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">PO No. : </label>
                                                    <div class="col-md-8 col-xs-12">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtPoNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>--%>



                                    <%--<div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Plant : </label>
                                                    <div class="col-md-8 col-xs-12">
                                                        <div class="input-group">
                                                            <asp:DropDownList ID="ddlPlantCode" runat="server" CssClass="form-control" Width="205" OnSelectedIndexChanged="ddlPlantCode_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>--%>


                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label"></label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:LinkButton runat="server" ID="lnkSerch" CssClass="btn btn-success " Text="Search Invoice" OnClick="lnkSerch_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success" Text="Export " OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
                                                </div>
                                            </div>
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
                <div class="panel panel-default">
                    <br />

                    <asp:Button ID="btnGreater10" runat="server" Text="> 10  - (0)" CssClass="btn btn-success" OnClick="btnGreater10_Click" />
                    <asp:Button ID="btnbtw8to10" runat="server" Text=">= 8 to <= 10 - (0)" CssClass="btn btn-success" OnClick="btnbtw8to10_Click" />
                    <asp:Button ID="btnbtw5to7" runat="server" Text=">= 5 to <= 7 - (0)" CssClass="btn btn-success" OnClick="btnbtw5to7_Click" />
                    <asp:Button ID="btnless4" runat="server" Text=" <= 4 - (0)" CssClass="btn btn-success" OnClick="btnless4_Click" />
                    <asp:Button ID="btnReset" runat="server" Text=" RESET" CssClass="btn btn-success" OnClick="btnReset_Click" />
                    <div class="panel-body">
                        <%--<div class="row">
                            <div class="col-md-12">
                                <div class="box">
                                    <input class="form-control pull-right" style="width: 200px !important;" id="myInput" type="text" placeholder="Search..">
                                </div>
                            </div>
                        </div>--%>
                        <div class="row">
                            <div class="col-md-12" style="margin-top: 10px !important;">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">


                                        <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <HeaderStyle CssClass="header" />
                                            <Columns>

                                                <asp:BoundField DataField="SONO" HeaderText="SO No." />
                                                <asp:BoundField DataField="SODT" HeaderText="SO Date" DataFormatString="{0:d}" />
                                                <asp:BoundField DataField="MAKE" HeaderText="Make" />
                                                <asp:BoundField DataField="MODEL" HeaderText="Model" />
                                                <asp:BoundField DataField="PRODGRADE" HeaderText="Grade" />
                                                <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
                                                <asp:BoundField DataField="JOBID" HeaderText="Job Id" />
                                                <asp:BoundField DataField="CUSTPARTDESC2" HeaderText="IMEI No." />
                                                <asp:BoundField DataField="PLATFORM" HeaderText="Sales From" />
                                                <asp:BoundField DataField="REFNO" HeaderText="Order/Reff. No." />
                                                <asp:BoundField DataField="AGE" HeaderText="Aging (Days)" />
                                                <asp:BoundField DataField="NETMATVALUE" HeaderText="Material Amt." />
                                                <asp:BoundField DataField="NETTAXAMT" HeaderText="TAX Amt." />
                                                <asp:BoundField DataField="DISCOUNT" HeaderText="Disc. Amt." />
                                                <asp:BoundField DataField="NETSOAMT" HeaderText="Net SO Amt." />
                                                <asp:BoundField DataField="ITEMID" HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <%--<asp:TemplateField HeaderText="Action" Visible="True">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ID="btnInv" Text="INVOICE" OnClick="btnInv_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
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
    <%--For Data Table Jquery--%>
    <script type="text/javascript" src="https://cdn.datatables.net/1.10.20/js/jquery.dataTables.min.js"></script>
    <script type="text/javascript" src="https://editor.datatables.net/extensions/Editor/js/dataTables.editor.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/select/1.3.1/js/dataTables.select.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/colreorder/1.5.2/js/dataTables.colReorder.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.6.1/js/dataTables.buttons.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/jszip/3.1.3/jszip.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/pdfmake.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.53/vfs_fonts.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.6.1/js/buttons.html5.min.js"></script>
    <script type="text/javascript" src="https://cdn.datatables.net/buttons/1.6.1/js/buttons.print.min.js"></script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranMMMIRPOINV" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />

</asp:Content>
