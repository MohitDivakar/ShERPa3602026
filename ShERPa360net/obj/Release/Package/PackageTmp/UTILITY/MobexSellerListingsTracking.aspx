<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="MobexSellerListingsTracking.aspx.cs" Inherits="ShERPa360net.UTILITY.MobexSellerListingsTracking" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        body .table thead th {
            position: sticky;
            top: 0px;
        }
    </style>
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

        .col-md-25 {
            width: 20%;
            float: left;
            border-radius: 15px;
        }

            .col-md-25 p {
                color: #faa61a;
                font-weight: bold;
            }

            .col-md-25 a {
                background-color: #184f90;
                border-radius: 6%;
                box-shadow: 0 1px 3px rgb(0 0 0 / 12%), 0 1px 2px rgb(0 0 0 / 24%);
                transition: all 0.3s cubic-bezier(.25,.8,.25,1);
            }

        @media only screen and (max-width: 600px) {
            .col-md-25 {
                width: 100%
            }
        }
    </style>
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong runat="server" id="Strong1"><span class="fa fa-file"></span>&nbsp;Listing Status Tracking </strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row" style="margin-top: 10px !important;">
                                        <fieldset class="scheduler-border shadow" runat="server" id="Fieldset1">
                                            <legend class="scheduler-border">Today/From 01-01-2023 To Upto Date </legend>

                                            <div class="col-md-25" style="margin-top: 10px !important;" title="Under Approvel">
                                                <asp:LinkButton title="Under Approvel" OnClick="lnkTested_Click" ID="lnkTested" runat="server" CssClass="tile ">
                                                    <asp:Label ID="lbtodayunderapproval" runat="server" Text="0"></asp:Label>/<asp:Label ID="Lbtotalunderapproval" runat="server" Text="0"></asp:Label>
                                                    <p>TESTED</p>
                                                </asp:LinkButton>
                                            </div>

                                            <div class="col-md-25" style="margin-top: 10px !important;" title="Pick-up Pending">
                                                <asp:LinkButton ID="lnkReserved" OnClick="lnkReserved_Click" title="Pick-up Pending" runat="server" CssClass="tile">
                                                    <asp:Label ID="lbtodaypickuppending" runat="server" Text="0"></asp:Label>/<asp:Label ID="Lbtotalpickuppending" runat="server" Text="0"></asp:Label>
                                                    <p>RESERVED</p>
                                                </asp:LinkButton>
                                            </div>

                                            <div class="col-md-25" style="margin-top: 10px !important;" title="Delivery-Store Pending">
                                                <asp:LinkButton ID="lnkOrderReceived" OnClick="lnkOrderReceived_Click" title="Delivery-Store Pending" runat="server" CssClass="tile ">
                                                    <asp:Label ID="lbtodaydeliStorepending" runat="server" Text="0"></asp:Label>/<asp:Label ID="lbtotaldeliStorepending" runat="server" Text="0"></asp:Label>
                                                    <p>ORDERRECEIVED</p>
                                                </asp:LinkButton>
                                            </div>

                                            <div class="col-md-25" style="margin-top: 10px !important;" title="Handover-BDO Pending">
                                                <asp:LinkButton ID="lnkReturnRequestGenerate" OnClick="lnkReturnRequestGenerate_Click" title="Handover-BDO Pending" runat="server" CssClass="tile ">
                                                    <asp:Label ID="lbtodayHandoverBdopending" runat="server" Text="0"></asp:Label>/<asp:Label ID="lbtotalHandoverBdopending" runat="server" Text="0"></asp:Label>
                                                    <p>RETURNREQUESTGENERATED</p>
                                                </asp:LinkButton>
                                            </div>

                                            <div class="col-md-25" style="margin-top: 10px !important;" title="Handover-Vendor Pending">
                                                <asp:LinkButton ID="lnkReturnHandoverVendor" OnClick="lnkReturnHandoverVendor_Click" title="Handover-Vendor Pending" runat="server" CssClass="tile ">
                                                    <asp:Label ID="lbtodayHandoverVendorpending" runat="server" Text="0"></asp:Label>/<asp:Label ID="lbtotalHandoverVendorpending" runat="server" Text="0"></asp:Label>
                                                    <p>RETURNHANDOVERTOBDO</p>
                                                </asp:LinkButton>
                                            </div>
                                        </fieldset>


                                    </div>

                                    <div class="row" runat="server" visible="false">
                                        <fieldset class="scheduler-border">
                                            <legend class="scheduler-border">Filter Detail</legend>
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">From Date</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                            </div>
                                                            <asp:RequiredFieldValidator ID="rfvfromdate" Style="color: red!important;" runat="server" ControlToValidate="txtFromDate" ValidationGroup="SearchDetail"
                                                                ErrorMessage="Please Enter From Date To Search">Please Enter From Date To Search</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">To Date</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                            </div>
                                                            <asp:RequiredFieldValidator ID="rfvToDate" Style="color: red!important;" runat="server" ControlToValidate="txtToDate" ValidationGroup="SearchDetail"
                                                                ErrorMessage="Please Enter To Date To Search">Please Enter To Date To Search</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
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
    <input type="hidden" id="menutabCheckerid" value="tsmMobexSellerIntervalListedUnlistedReport" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />
</asp:Content>
