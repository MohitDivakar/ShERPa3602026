<%@ Page Title="PO Approval Report" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="rptPOApporval.aspx.cs" Inherits="ShERPa360net.MM.rptPOApporval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>PO Approval Report</title>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
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
            $("#ContentPlaceHolder1_grvData").DataTable({
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;View PO Approval Report</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">From Date : </label>
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
                                            <label class="col-md-4 control-label">Vendor : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList TabIndex="2" ID="ddlVendor" ClientIDMode="Static" runat="server" CssClass="form-control ddlVendor"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    </div>

                                    <div class="col-md-12">

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">PO No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtPONO" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Plant : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlPlant" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label"></label>
                                                <div class="col-md-8 col-xs-12">
                                                    <asp:LinkButton runat="server" ID="lnkSerch" CssClass="btn btn-success" Text="Search Invoice" OnClick="lnkSerch_Click" ValidationGroup="SaveAll"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
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
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row" style="margin-top: 20px !important;">
                            <div class="col-md-12">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                        <asp:GridView ID="grvData" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="PONO" HeaderText="PO No." />
                                                <asp:BoundField DataField="VENDOR" HeaderText="Vendor" />
                                                <asp:BoundField DataField="DEPTNAME" HeaderText="Department" />
                                                <asp:BoundField DataField="PLANT" HeaderText="Plant" />
                                                <asp:BoundField DataField="POCREATEBY" HeaderText="Create By" />
                                                <asp:BoundField DataField="CREATEDATE" HeaderText="Create Date" />
                                                <asp:BoundField DataField="APRVBY1" HeaderText="Aprrover 1" />
                                                <asp:BoundField DataField="APPROVEDBY1" HeaderText="Aprroved By 1" />
                                                <asp:BoundField DataField="STATUS1" HeaderText="1st Aprv. Status" />
                                                <asp:BoundField DataField="REASON1" HeaderText="1st Aprv Reason" />
                                                <asp:BoundField DataField="APRVDATE1" HeaderText="1st Aprv Dt." />
                                                <asp:BoundField DataField="APRVBY2" HeaderText="Approver 2" />
                                                <asp:BoundField DataField="APPROVEDBY2" HeaderText="Aprroved By 2" />
                                                <asp:BoundField DataField="STATUS2" HeaderText="2nd Aprv. Status" />
                                                <asp:BoundField DataField="REASON2" HeaderText="2nd Aprv Reason" />
                                                <asp:BoundField DataField="APRVDATE2" HeaderText="2nd Aprv Dt." />
                                                <asp:BoundField DataField="APRVBY3" HeaderText="Approver 3" />
                                                <asp:BoundField DataField="APPROVEDBY3" HeaderText="Aprroved By 3" />
                                                <asp:BoundField DataField="STATUS3" HeaderText="3rd Aprv. Status" />
                                                <asp:BoundField DataField="REASON3" HeaderText="3rd Aprv Reason" />
                                                <asp:BoundField DataField="APRVDATE3" HeaderText="3rd Aprv Dt." />
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

    <input type="hidden" id="menutabid" value="tsmTranMMPO" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />

</asp:Content>
