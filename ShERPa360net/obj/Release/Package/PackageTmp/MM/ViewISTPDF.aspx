<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewISTPDF.aspx.cs" Inherits="ShERPa360net.MM.ViewISTPDF" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rpt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ShERPa 360°</title>

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />




    <link rel="icon" href="../img/favicon.png" sizes="32x32" type="image/x-icon" />
    <link rel="stylesheet" type="text/css" id="theme" href="../css/theme-default.css" />
    <link rel="stylesheet" type="text/css" href="../css/custom.css" />

    <style>
        body:nth-of-type(1) img[src*="Blank.gif"] {
            display: none;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="page-content-wrap">

            <div class="row">
                <div class="col-md-12">
                    <div class="form-horizontal">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <rpt:ReportViewer ID="ReportViewer1" runat="server" SizeToReportContent="true" Visible="false"></rpt:ReportViewer>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <input type="hidden" id="menutabid" value="tsmRptDocISSUE" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptDocs" runat="server" />
</body>
</html>
