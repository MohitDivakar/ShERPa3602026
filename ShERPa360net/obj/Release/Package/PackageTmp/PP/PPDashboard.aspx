<%@ Page Title="" Language="C#" MasterPageFile="~/PP/MasterPP.Master" AutoEventWireup="true" CodeBehind="PPDashboard.aspx.cs" Inherits="ShERPa360net.PP.PPDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
     <section class="page-title">
        <h1><span class="fa fa-th"></span>&nbsp;     Dashboard</h1>
    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <input type="hidden" id="menutabid" value="tsmTranPP" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranPP" runat="server" />
</asp:Content>
