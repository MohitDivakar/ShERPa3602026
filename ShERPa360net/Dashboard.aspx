<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dashboard.aspx.cs" Inherits="Dashboard" %>
<%@ Register Assembly="System.Web.DataVisualization" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<!DOCTYPE html>
<html>
<head>
    <title>Sales Dashboard</title>
    <style>
        .card { display:inline-block; width:200px; padding:15px; background:#f0f0f0; margin:10px; border-radius:8px; text-align:center; box-shadow:0 0 5px #aaa;}
    </style>
</head>
<body>
    <h2>Sales Dashboard</h2>

    <div class="card">
        <h3>Total Sales</h3>
        <asp:Label ID="lblTotalSales" runat="server" Font-Size="Large" ForeColor="Green"></asp:Label>
    </div>

    <div>
        <asp:Chart ID="ChartCategorySales" runat="server" Width="600px" Height="300px">
            <Series>
                <asp:Series Name="CategorySales" ChartType="Column"></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea1"></asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </div>

    <div>
        <asp:Chart ID="ChartMonthlySales" runat="server" Width="600px" Height="300px">
            <Series>
                <asp:Series Name="MonthlySales" ChartType="Line"></asp:Series>
            </Series>
            <ChartAreas>
                <asp:ChartArea Name="ChartArea2"></asp:ChartArea>
            </ChartAreas>
        </asp:Chart>
    </div>

    <asp:GridView ID="gvSales" runat="server" AutoGenerateColumns="true" Width="100%" />
</body>
</html>
