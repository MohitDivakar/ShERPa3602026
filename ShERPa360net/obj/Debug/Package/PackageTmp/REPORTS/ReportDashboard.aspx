<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="ReportDashboard.aspx.cs" Inherits="ShERPa360net.REPORTS.ReportDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="page-title">
        <h1><span class="fa fa-th"></span>&nbsp;     Dashboard</h1>
    </section>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmRptMM" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmReports" runat="server" />
</asp:Content>
