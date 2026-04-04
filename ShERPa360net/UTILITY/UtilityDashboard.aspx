<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="UtilityDashboard.aspx.cs" Inherits="ShERPa360net.UTILITY.UtilityDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>DashBoard</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />
    <style>
        .icon1 {
            text-align: center;
            vertical-align: middle;
            height: 116px;
            width: 116px;
            border-radius: 50%;
            color: #999999;
            margin: 0 auto 20px;
            border: 4px solid #fe970a;
            transition: all 0.2s;
            -webkit-transition: all 0.2s;
        }

        .imgcircle {
            height: 65px !important;
            width: 65px !important;
            margin-top: 20px !important;
        }

        .shadow {
            -moz-box-shadow: 3px 3px 5px 6px #fe970a !important;
            -webkit-box-shadow: 0 0 5px #cccccc87 !important;
            border: 5px solid #cccccc87 !important;
            box-shadow: 0 0 5px #cccccc87 !important;
            background: #fff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; DashBoard  </strong></h3>
                    </div>
                    <div class="row" style="margin-top: 10px !important;">
                        <%--<fieldset class="scheduler-border shadow" runat="server" id="Fieldset1">
                            <legend class="scheduler-border">Today</legend>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnktdlisted" runat="server" CssClass="tile tile-info" BackColor="#3E8768" BorderColor="#3E8768">
                                    <asp:Label ID="lbtdlisted" runat="server" Text="0"></asp:Label>
                                    <p>Total Listed</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnktdtested" runat="server" CssClass="tile tile-success">
                                    <asp:Label ID="lbltdtested" runat="server" Text="0"></asp:Label>
                                    <p>Total Tested</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnktdpurchase" runat="server" CssClass="tile tile-primary">
                                    <asp:Label ID="lbltdpurchase" runat="server" Text="0"></asp:Label>
                                    <p>Total Purchase</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnktdorderreceived" runat="server" CssClass="tile tile-warning">
                                    <asp:Label ID="lbtdorderreceived" runat="server" Text="0"></asp:Label>
                                    <p>Total Order Received</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnktdreturn" runat="server" CssClass="tile tile-danger">
                                    <asp:Label ID="lbtdreturn" runat="server" Text="0"></asp:Label>
                                    <p>Total Return</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnktdrejected" runat="server" CssClass="tile tile-default" BackColor="Red" BorderColor="Red">
                                    <asp:Label ID="lbtdrejected" runat="server" Text="0" ForeColor="White"></asp:Label>
                                    <p style="color: white;">Total Rejected</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnktdapproved" runat="server" CssClass="tile tile-info" BackColor="#3E8768" BorderColor="#3E8768">
                                    <asp:Label ID="lbtdapproved" runat="server" Text="0"></asp:Label>
                                    <p>Total Approved</p>
                                </asp:LinkButton>
                            </div>
                        </fieldset>

                        <fieldset class="scheduler-border shadow" runat="server" id="Fieldset2">
                            <legend class="scheduler-border">OverAll</legend>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkovlisted" runat="server" CssClass="tile tile-info" BackColor="#3E8768" BorderColor="#3E8768">
                                    <asp:Label ID="lbovlisted" runat="server" Text="0"></asp:Label>
                                    <p>Total Listed</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkovtested" runat="server" CssClass="tile tile-success">
                                    <asp:Label ID="lbovtested" runat="server" Text="0"></asp:Label>
                                    <p>Total Tested</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkovpurchase" runat="server" CssClass="tile tile-primary">
                                    <asp:Label ID="lbovpurchase" runat="server" Text="0"></asp:Label>
                                    <p>Total Purchase</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkovorderreceived" runat="server" CssClass="tile tile-warning">
                                    <asp:Label ID="lbovorderreceived" runat="server" Text="0"></asp:Label>
                                    <p>Total Order Received</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkovreturn" runat="server" CssClass="tile tile-danger">
                                    <asp:Label ID="lbovreturn" runat="server" Text="0"></asp:Label>
                                    <p>Total Return</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkovrejected" runat="server" CssClass="tile tile-default" BackColor="Red" BorderColor="Red">
                                    <asp:Label ID="lbovrejected" runat="server" Text="0" ForeColor="White"></asp:Label>
                                    <p style="color: white;">Total Rejected</p>
                                </asp:LinkButton>
                            </div>
                        </fieldset>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmUtility" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmUtility" runat="server" />
</asp:Content>
