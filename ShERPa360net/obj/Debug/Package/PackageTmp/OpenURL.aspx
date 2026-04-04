<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OpenURL.aspx.cs" Inherits="ShERPa360net.OpenURL" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            URL No. From : <asp:TextBox ID="txtFromUrlNo" runat="server"></asp:TextBox>
           To : <asp:TextBox ID="txtToUrlNo" runat="server"></asp:TextBox>
            <asp:Button runat="server" ID="btnOpen" OnClick="btnOpen_Click" Text="Open URL" />
        </div>
    </form>
</body>
</html>
