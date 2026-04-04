<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptMaterialMovement.aspx.cs" Inherits="ShERPa360net.REPORTS.rptMaterialMovement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Material Movment</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />
    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }

        .btnAqua {
            width: 60px;
            background-color: #faa61a;
            color: white;
        }
    </style>

    <script>
        $(document).ready(function () {
            BindMakeAssociateModel();
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

            $("#ContentPlaceHolder1_gvWOList").DataTable({
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Matrial Movement  </strong></h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label pull-left" style="padding-top: 7px;">Plant Code</label>
                                            <div class="col-md-9 col-xs-10">
                                                <asp:DropDownList ID="ddlPlancode" OnSelectedIndexChanged="ddlPlancode_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label pull-left" style="padding-top: 7px;">Location Code </label>
                                            <div class="col-md-9 col-xs-10">
                                                <asp:DropDownList ID="ddlLocation" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" runat="server" AutoPostBack="true" CssClass="form-control "></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label pull-left" style="padding-top: 7px;">Segment</label>
                                            <div class="col-md-9 col-xs-10">
                                                <asp:DropDownList ID="ddlSegment" OnSelectedIndexChanged="ddlPlancode_SelectedIndexChanged" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label pull-left" style="padding-top: 7px;">IMEI NO.</label>
                                            <div class="col-md-9 col-xs-10 pull-right">
                                                <asp:TextBox ID="txtImeino" runat="server" CssClass="form-control  pull-right" MaxLength="20"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label pull-left" style="padding-top: 7px;">Track No.</label>
                                                <div class="col-md-9 col-xs-10 pull-right">
                                                    <asp:TextBox ID="textTrackno" runat="server" CssClass="form-control  pull-right" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label  pull-left" style="padding-top: 7px;">Movment Type</label>
                                                <div class="col-md-9 col-xs-10">
                                                    <asp:TextBox ID="textMovmentType" runat="server" CssClass="form-control pull-right" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label  pull-left" style="padding-top: 7px;">Matirial Code</label>
                                                <div class="col-md-9 col-xs-10">
                                                    <asp:TextBox ID="textMatirialCode" runat="server" CssClass="form-control pull-left" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label pull-left" style="padding-top: 7px;">From</label>
                                                <div class="col-md-9 col-xs-8 pull-left">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker pull-left"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label pull-left" style="padding-top: 7px;">To</label>
                                                <div class="col-md-9 col-xs-8 pull-right">
                                                    <div class="input-group">
                                                        <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker pull-right"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="col-md-3 control-label pull-left" style="padding-top: 7px;">With Value</label>
                                                <div class="col-md-9 col-xs-8 pull-right">
                                                    <div class="input-group">
                                                        <asp:CheckBox ID="chkvalue" runat="server" CssClass="form-control pull-right" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:LinkButton runat="server" ID="lnkSearch" CssClass="btn btn-success pull-" OnClick="lnkSearch_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
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


                                    <div id="witValue" runat="server" visible="false">
                                        <div class="box-body divhorizontal" style="margin-top: 10px; overflow: scroll;">
                                            <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                                <EmptyDataTemplate>
                                                    No Record Found!
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:BoundField HeaderText="Doc. Dt" DataField="DOCDATE" />
                                                    <asp:BoundField HeaderText="Doc. No" DataField="DOCNO" />
                                                    <asp:BoundField HeaderText="Doc" DataField="DOCTYPE" />
                                                    <asp:BoundField HeaderText="Doc. Type" DataField="DOCTYPED" />
                                                    <asp:BoundField HeaderText="Ref No." DataField="REFNO" />
                                                    <asp:BoundField HeaderText="PO No." DataField="PONO" />
                                                    <asp:BoundField HeaderText="Segment" DataField="SEGMENT" />
                                                    <asp:BoundField HeaderText="Track No." DataField="TRACKNO" />
                                                    <asp:BoundField HeaderText="Item Code" DataField="ITEMCODE" />
                                                    <asp:BoundField HeaderText="Item Description" DataField="ITEMDESC" />
                                                    <asp:BoundField HeaderText="Model" DataField="MODEL" />
                                                    <asp:BoundField HeaderText="Make" DataField="MAKE" />
                                                    <asp:BoundField HeaderText="Sub Group" DataField="ITEMSUBGRPDESC" />
                                                    <asp:BoundField HeaderText="Group" DataField="HSNGRPDESC" />
                                                    <asp:BoundField HeaderText="Plant" DataField="PLANTCD" />
                                                    <asp:BoundField HeaderText="Loc." DataField="LOCCD" />
                                                    <asp:BoundField HeaderText="UNIT" DataField="UNIT" />
                                                    <asp:BoundField HeaderText="Qty" DataField="QTY" />
                                                    <asp:BoundField HeaderText="VALUE" DataField="MATVALUE" />
                                                    <asp:BoundField HeaderText="VENDNAME" DataField="VENDNAME" />
                                                    <asp:BoundField HeaderText="Entered By" DataField="ENTEREDBY" />
                                                    <asp:BoundField HeaderText="IMEI NO." DataField="IMEINO" />
                                                </Columns>
                                            </asp:GridView>

                                        </div>
                                    </div>


                                    <div id="woValue" runat="server" visible="false">
                                        <div class="box-body divhorizontal" style="margin-top: 10px; overflow: scroll;">
                                            <asp:GridView ID="gvWOList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                                <EmptyDataTemplate>
                                                    No Record Found!
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:BoundField HeaderText="Doc. Dt" DataField="DOCDATE" />
                                                    <asp:BoundField HeaderText="Doc. No" DataField="DOCNO" />
                                                    <asp:BoundField HeaderText="Doc" DataField="DOCTYPE" />
                                                    <asp:BoundField HeaderText="Doc. Type" DataField="DOCTYPED" />
                                                    <asp:BoundField HeaderText="Ref No." DataField="REFNO" />
                                                    <asp:BoundField HeaderText="PO No." DataField="PONO" />
                                                    <asp:BoundField HeaderText="Segment" DataField="SEGMENT" />
                                                    <asp:BoundField HeaderText="Track No." DataField="TRACKNO" />
                                                    <asp:BoundField HeaderText="Item Code" DataField="ITEMCODE" />
                                                    <asp:BoundField HeaderText="Item Description" DataField="ITEMDESC" />
                                                    <asp:BoundField HeaderText="Model" DataField="MODEL" />
                                                    <asp:BoundField HeaderText="Make" DataField="MAKE" />
                                                    <asp:BoundField HeaderText="Sub Group" DataField="ITEMSUBGRPDESC" />
                                                    <asp:BoundField HeaderText="Group" DataField="HSNGRPDESC" />
                                                    <asp:BoundField HeaderText="Plant" DataField="PLANTCD" />
                                                    <asp:BoundField HeaderText="Loc." DataField="LOCCD" />
                                                    <asp:BoundField HeaderText="UNIT" DataField="UNIT" />
                                                    <asp:BoundField HeaderText="Qty" DataField="QTY" />
                                                    <asp:BoundField HeaderText="VENDNAME" DataField="VENDNAME" />
                                                    <asp:BoundField HeaderText="Entered By" DataField="ENTEREDBY" />
                                                    <asp:BoundField HeaderText="IMEI NO." DataField="IMEINO" />
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
    <input type="hidden" id="menutibid" value="tsmRptCRMCallLogs" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptCRM" />
</asp:Content>


