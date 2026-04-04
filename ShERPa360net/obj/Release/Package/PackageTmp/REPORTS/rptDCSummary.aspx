<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptDCSummary.aspx.cs" Inherits="ShERPa360net.REPORTS.rptDCSummary" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        body:nth-of-type(1) img[src*="Blank.gif"] {
            display: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">

        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; DC </strong>Summary Report</h3>
                        </div>


                        <div class="panel-body">

                            <div class="row">

                                <div class="col-md-12">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">DC No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtDCNo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvDCNo" runat="server" ControlToValidate="txtDCNo" ForeColor="Red"
                                                    ErrorMessage="Enter DC No." ValidationGroup="valRpt" Display="Dynamic">Enter DC No.</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 13px !important;">Status : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal" CssClass="radio-inline" RepeatLayout="Flow">
                                                    <asp:ListItem Value="0" Text="OK" Selected="True" class="radio-inline"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="RWR" class="radio-inline"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="BER" class="radio-inline"></asp:ListItem>
                                                </asp:RadioButtonList>
                                                <%--<asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="rblStatus" InitialValue="0" ForeColor="Red"
                                                    ErrorMessage="Select Status" ValidationGroup="valRpt" Display="Dynamic">Select Status</asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Amount : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" Text="1300" ReadOnly="true"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ControlToValidate="txtAmount" ForeColor="Red"
                                                    ErrorMessage="Enter Amount" ValidationGroup="valRpt" Display="Dynamic">Enter Amount</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label" style="padding-top: 13px !important;">Shipped : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:RadioButtonList ID="rblShipped" runat="server" RepeatDirection="Horizontal" CssClass="radio-inline" RepeatLayout="Flow">
                                                    <asp:ListItem Value="0" Text="By Road" class="radio-inline" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="By Air" class="radio-inline"></asp:ListItem>
                                                    <%--<asp:ListItem Value="2" Text="BER"></asp:ListItem>--%>
                                                </asp:RadioButtonList>
                                                <%--<asp:RequiredFieldValidator ID="rfvShipped" runat="server" ControlToValidate="rblShipped" InitialValue="0" ForeColor="Red"
                                                    ErrorMessage="Select Status" ValidationGroup="valRpt" Display="Dynamic">Select Status</asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                    </div>


                                </div>

                                <div class="col-md-12" style="padding-top: 10px;">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">HSN : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtHSNNo" runat="server" CssClass="form-control" Text="8517" ReadOnly="true"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvHSN" runat="server" ControlToValidate="txtHSNNo" ForeColor="Red"
                                                    ErrorMessage="Enter HSN" ValidationGroup="valRpt" Display="Dynamic">Enter HSN</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">No. of Box : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtNoOfBOX" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvBoxNo" runat="server" ControlToValidate="txtNoOfBOX" ForeColor="Red"
                                                    ErrorMessage="Enter Total No. of Box" ValidationGroup="valRpt" Display="Dynamic">Enter Total No. of Box</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkGenerate" CssClass="btn btn-success pull-left" Text="Search Report" OnClick="lnkGenerate_Click" ValidationGroup="valRpt"><i class="fa fa-file"></i></asp:LinkButton>
                                            <%--<asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export Report" OnClick="lnkExport_Click"><i class="fa fa-file-download"></i></asp:LinkButton>--%>
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
                                    <rpt:ReportViewer ID="ReportViewer1" runat="server" SizeToReportContent="true" Visible="false"></rpt:ReportViewer>
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


    <input type="hidden" id="menutabid" value="tsmRptDocFDC" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptDocs" runat="server" />

</asp:Content>
