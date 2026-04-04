<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptSafetyReport.aspx.cs" Inherits="ShERPa360net.REPORTS.rptSafetyReport" EnableEventValidation="false" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <rpt:ReportViewer ID="ReportViewer1" runat="server" SizeToReportContent="true" Visible="false"></rpt:ReportViewer>

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>Safety Report</h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">

                                <div class="col-md-12">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">From Date : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                        ErrorMessage="Enter From Date" ValidationGroup="valSearch" Display="Dynamic">Enter From Date</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">To Date : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtTodate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtTodate"
                                                        ErrorMessage="Enter To Date" ValidationGroup="valSearch" Display="Dynamic">Enter To Date</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-6 control-label">Location : </label>
                                            <div class="col-md-6 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlPlantCode" runat="server" CssClass="form-control" Width="150" OnSelectedIndexChanged="ddlPlantCode_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSummary" CssClass="btn btn-success pull-right" Text="Report Summary" PostBackUrl="~/REPORTS/rptSafetyReportSummary.aspx"></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-right" Text="Export Report" OnClick="lnkExport_Click">

<span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkSearhSR" CssClass="btn btn-success pull-right" Text="Search Report" OnClick="lnkSearhSR_Click" ValidationGroup="valSearch"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>

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
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                        <asp:GridView ID="grvSafetyReport" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="true">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="ID" />
                                                <asp:BoundField DataField="REPORTDATE" HeaderText="Report Date" />
                                                <asp:BoundField DataField="LOCATION" HeaderText="Location" />
                                                <asp:BoundField DataField="INSPECTBY" HeaderText="Inspected By" />
                                                <asp:BoundField DataField="ENTEREDBY" HeaderText="Entered By" />
                                                <asp:BoundField DataField="CREATEDATE" HeaderText="Entered Date" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="lnkDownload_Click"></asp:LinkButton>
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


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmRptSysSafetyReport" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptSys" runat="server" />

</asp:Content>
