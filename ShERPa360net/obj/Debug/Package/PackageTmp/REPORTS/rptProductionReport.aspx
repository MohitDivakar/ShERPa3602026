<%@ Page Title="Production Report" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptProductionReport.aspx.cs" Inherits="ShERPa360net.REPORTS.rptProductionReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Production Report</title>

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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Production Report</strong></h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Segment : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlSegment" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

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

                                </div>
                                <div class="col-md-12" style="margin-top: 10px;">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">QC Result : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtJobID" runat="server" CssClass="form-control" placeholder="Job ID"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Filter with QC Date : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:CheckBox ID="chkQCDate" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSerch" CssClass="btn btn-success pull-left" Text="Search" OnClick="lnkSerch_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                            <%--<asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>--%>
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
                    <div class="panel-heading">
                        <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Today's Inward  </strong>Data</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                        <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Stage Details">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkJobStat" Text="Stage Details" OnClick="lnkJobStat_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="QC Result">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="lnkQC" Text="QC Result" OnClick="lnkQC_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="JOB DATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJOBDATE" runat="server" Text='<%# Eval("JOB DATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="JOB ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJOBID" runat="server" Text='<%# Eval("JOB ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="BRAND">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBRAND" runat="server" Text='<%# Eval("BRAND") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="MODEL">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMODEL" runat="server" Text='<%# Eval("MODEL") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="JOB STATUS">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJOBSTATUS" runat="server" Text='<%# Eval("JOB STATUS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="PRODUCTION STATUS">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJPRODUCTIONSTATUS" runat="server" Text='<%# Eval("PRODUCTION STATUS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="JOB CREATED BY">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJOBCREATEDBY" runat="server" Text='<%# Eval("JOB CREATED BY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

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

    <div id="StageDetails" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Stage Update Details</h4>
                </div>
                <div class="modal-body">
                    <asp:Panel ID="Panel5" runat="server" Height="300px" ScrollBars="Vertical">
                        <asp:GridView ID="gvStageDetails" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                            CellSpacing="0" AutoGenerateColumns="False" Width="100%">
                            <EmptyDataTemplate>
                                <div style="text-align: center; color: red; font-size: 18px;">
                                    List is empty !
                                </div>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="JOB ID" HeaderText="Job ID" />
                                <asp:BoundField DataField="STAGE DESC" HeaderText="Stage" />
                                <asp:BoundField DataField="STAGE UPDATE DATE" HeaderText="Stage Update Date" />
                                <asp:BoundField DataField="JOB DONE BY" HeaderText="Job Done By" />

                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>

    <div id="QCDetails" class="modal fade" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">QC Details</h4>
                </div>
                <div class="modal-body">
                    <asp:Panel ID="Panel1" runat="server" Height="300px" ScrollBars="Vertical">
                        <asp:GridView ID="gvQCDetails" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                            CellSpacing="0" AutoGenerateColumns="False" Width="100%">
                            <EmptyDataTemplate>
                                <div style="text-align: center; color: red; font-size: 18px;">
                                    List is empty !
                                </div>
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:BoundField DataField="STAGE" HeaderText="Stage" />
                                <asp:BoundField DataField="QC PARAMETER" HeaderText="QC Parameter" />
                                <asp:BoundField DataField="RESULT" HeaderText="Result" />
                                <asp:BoundField DataField="QC DATE" HeaderText="QC Date" />
                                <asp:BoundField DataField="QC DONE BY" HeaderText="QC Done By" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>

        </div>
    </div>



</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="rptProductionReport" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptMM" runat="server" />

</asp:Content>
