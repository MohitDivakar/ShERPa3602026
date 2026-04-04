<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmTCRPDF.aspx.cs" Inherits="ShERPa360net.frmTCRPDF" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rpt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style>
        body:nth-of-type(1) img[src*="Blank.gif"] {
            display: none;
        }
    </style>

</head>
<body>



    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rpt:ReportViewer ID="ReportViewer1" runat="server" SizeToReportContent="true" Visible="false" ShowPageNavigationControls="false" ShowRefreshButton="false" ShowBackButton="false" ShowZoomControl="false" ShowFindControls="false"></rpt:ReportViewer>
        </div>
    </form>
</body>
</html>
