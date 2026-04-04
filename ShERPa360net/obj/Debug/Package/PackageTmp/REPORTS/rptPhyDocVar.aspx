<%@ Page Title="Phy. Doc. Var." Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptPhyDocVar.aspx.cs" Inherits="ShERPa360net.REPORTS.rptPhyDocVar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Phy. Doc. Var. Data</title>

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
            if ($("#ContentPlaceHolder1_gvAllGrab tr").length > 2) {
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Phy. Doc. Var. </strong>Data</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">PO Date : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
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
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Status : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12" style="margin-top: 10px;">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">QC Result : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlQCResult" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="ALL" Text="ALL" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="PASS" Text="PASS"></asp:ListItem>
                                                        <asp:ListItem Value="FAIL" Text="FAIL"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Plant Code : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlPlant" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSerch" CssClass="btn btn-success pull-left" Text="Search" OnClick="lnkSerch_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
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
                                                <asp:BoundField DataField="PLANTCD" HeaderText="Plant" />
                                                <asp:BoundField DataField="JOBID" HeaderText="Job Id" />
                                                <asp:BoundField DataField="PurchaseDate" HeaderText="Date" />
                                                <asp:BoundField DataField="PRODMAKE" HeaderText="Make" />
                                                <asp:BoundField DataField="MODELDESC" HeaderText="Model" />
                                                <asp:BoundField DataField="COLOR" HeaderText="Color" />
                                                <asp:BoundField DataField="RAMSIZE" HeaderText="RAM" />
                                                <asp:BoundField DataField="ROMSIZE" HeaderText="ROM" />
                                                <asp:BoundField DataField="PRODGRADE" HeaderText="Grade" />
                                                <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                <asp:BoundField DataField="ASIN" HeaderText="ASIN" />
                                                <asp:BoundField DataField="IMEINO" HeaderText="IMEI No." />
                                                <asp:BoundField DataField="PURCHASE PRICE" HeaderText="Purchase Rate" />
                                                <asp:BoundField DataField="REPAIR COST" HeaderText="Repair Cost" />
                                                <asp:BoundField DataField="ESTIMATESTATUS" HeaderText="Estimate Status" />
                                                <%--<asp:BoundField DataField="TOTALCOST" HeaderText="Total Cost" />--%>
                                                <asp:TemplateField HeaderText="Total Cost">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblToalCost" runat="server" Text='<%# Convert.ToDecimal(Eval("PURCHASE PRICE")) +Convert.ToDecimal(Eval("REPAIR COST")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="JOBSTATDESC" HeaderText="Job Status" />
                                                <asp:BoundField DataField="CREATEDATE" HeaderText="Create Date" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="QCBY" HeaderText="QC By" />
                                                <asp:BoundField DataField="QCRESULT" HeaderText="QC Result" />
                                                <asp:BoundField DataField="ONHOLDREASON" HeaderText="Hold Reason" />
                                                <asp:BoundField DataField="FAILREASON" HeaderText="Fail Reason" />
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
