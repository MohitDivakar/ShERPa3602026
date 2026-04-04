<%@ Page Title="" Language="C#" MasterPageFile="~/Logistic/MasterLogistic.Master" AutoEventWireup="true" CodeBehind="LogisticDashBoard.aspx.cs" Inherits="ShERPa360net.Logistic.LogisticDashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="page-title">
        <h1><span class="fa fa-th"></span>&nbsp;     Dashboard</h1>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmLogisticSys" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmLogisticRequest" runat="server" />

</asp:Content>
