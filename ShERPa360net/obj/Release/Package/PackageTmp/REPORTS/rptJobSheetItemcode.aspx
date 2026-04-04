<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptJobSheetItemcode.aspx.cs" Inherits="ShERPa360net.REPORTS.rptJobSheetItemcode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>JobSheet With ItemCode</title>
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

            $("#ContentPlaceHolder1_GridView1").DataTable({
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; <b>JobSheet</b></strong><b> with Item Code</b></h3>
                            <div class="col-md-12 pull-right" style="margin-top: 10px;">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label pull-left">Plant : </label>
                                        <div class="col-md-8 col-xs-10 pull-right">
                                            <asp:DropDownList ID="ddlPlant" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label pull-left">Status : </label>
                                        <div class="col-md-8 col-xs-10 pull-right">
                                            <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label pull-left" style="padding-top: 5px;">Job. Id : </label>
                                        <div class="col-md-8 col-xs-10 pull-Left">
                                            <asp:TextBox ID="txtJobId" runat="server" CssClass="form-control  pull-right" MaxLength="15"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label pull-left">IMEI No. : </label>
                                        <div class="col-md-8 col-xs-10 pull-right">
                                            <asp:TextBox ID="txtImeiNo" runat="server" CssClass="form-control  pull-right" MaxLength="20"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12 pull-right" style="margin-top: 10px;">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label   pull-left" style="padding-top: 10px;">From : </label>
                                        <div class="col-md-8 col-xs- pull-right">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker pull-right" MaxLength="10"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-4 control-label pull-left" style="padding-top: 10px;">To : </label>
                                        <div class="col-md-8 col-xs-10 pull-right">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker pull-right" MaxLength="12"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <asp:LinkButton runat="server" ID="lnkDownLoadAll" CssClass="btn btn-success pull-right" Text="Download All" OnClick="lnkDownLoadAll_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="lnkSearch" CssClass="btn btn-success pull-right"  OnClick="lnkSearch_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div id="divAllLead" class="box" runat="server" visible="true">
                                <div class="col-md-12" style="margin-top: 10px !important;">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll;">

                                        <div class="row" style="margin-top: 10px !important;">
                                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                                <EmptyDataTemplate>
                                                    No Record Found!
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:BoundField HeaderText="Plant" DataField="PLANTCD" />
                                                    <asp:BoundField HeaderText="Job. Id" DataField="JOBID" />
                                                    <asp:BoundField HeaderText="PO No." DataField="PONO" />
                                                    <asp:BoundField HeaderText="Make" DataField="PRODMAKE" />
                                                    <asp:BoundField HeaderText="Model" DataField="MODELDESC" />
                                                    <asp:BoundField HeaderText="Color" DataField="COLOR" />
                                                    <asp:BoundField HeaderText="RAM" DataField="RAMSIZE" />
                                                    <asp:BoundField HeaderText="ROM" DataField="ROMSIZE" />
                                                    <asp:BoundField HeaderText="IMEI No." DataField="IMEINO" />
                                                    <asp:BoundField HeaderText="Purchase Date" DataField="PurchaseDate" />
                                                    <asp:BoundField HeaderText="Create Date" DataField="CREATEDATE" />
                                                    <asp:BoundField HeaderText="Product Grade" DataField="PRODGRADE" />
                                                    <asp:BoundField HeaderText="Status " DataField="JOBSTATDESC" />
                                                    <asp:BoundField HeaderText="Reason" DataField="FAILREASON" />
                                                    <asp:BoundField HeaderText="Item Code" DataField="ITEMCODE" />
                                                    <asp:BoundField HeaderText="Item Descrption" DataField="ITEMDESC" />
                                                    <asp:BoundField HeaderText="Qc Result" DataField="QCRESULT" />
                                                    <asp:BoundField HeaderText="Esti. Status" DataField="ESTIMATESTATUS" />
                                                    <asp:BoundField HeaderText="Hold Reason" DataField="ONHOLDREASON" />

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

    <input type="hidden" id="menutibid" value="tsmRptCRMJSItem" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptCRM" />

</asp:Content>
