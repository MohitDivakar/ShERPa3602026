<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="FollowupCount.aspx.cs" Inherits="ShERPa360net.UTILITY.FollowupCount" %>

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
        .col-md-25{
            width:20%;
            float:left;
            border-radius:15px;
          
        }
        .col-md-25 p {
                color: #faa61a;
    font-weight: bold;
        }
         .col-md-25 a
        {
               background-color: #184f90;
    border-radius: 6%;
    box-shadow: 0 1px 3px rgb(0 0 0 / 12%), 0 1px 2px rgb(0 0 0 / 24%);
    transition: all 0.3s cubic-bezier(.25,.8,.25,1);
        }

         @media only screen and (max-width: 600px) {
 .col-md-25
                 {
                     width:100%
                 }
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
                        <fieldset class="scheduler-border shadow" runat="server" id="Fieldset1">
                            <legend class="scheduler-border">Today/OverAll</legend>
                           
                            <div class="col-md-25" style="margin-top: 10px !important;" title="Under Approvel">
                                <asp:LinkButton title="Under Approvel" ID="lnktdlisted" runat="server" CssClass="tile ">
                                    <asp:Label ID="lbtodayunderapproval" runat="server" Text="0"></asp:Label>/<asp:Label ID="Lbtotalunderapproval" runat="server" Text="0"></asp:Label>
                                    <p>TESTED</p>
                                </asp:LinkButton>
                            </div>
                               
                            <div class="col-md-25" style="margin-top: 10px !important;" title="Pick-up Pending">
                                <asp:LinkButton ID="lnktdtested" title="Pick-up Pending" runat="server" CssClass="tile">
                                    <asp:Label ID="lbtodaypickuppending" runat="server" Text="0"></asp:Label>/<asp:Label ID="Lbtotalpickuppending" runat="server" Text="0"></asp:Label>
                                    <p>RESERVED</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-25" style="margin-top: 10px !important;" title="Delivery-Store Pending">
                                <asp:LinkButton ID="lnktdpurchase" title="Delivery-Store Pending" runat="server" CssClass="tile ">
                                    <asp:Label ID="lbtodaydeliStorepending" runat="server" Text="0"></asp:Label>/<asp:Label ID="lbtotaldeliStorepending" runat="server" Text="0"></asp:Label>
                                    <p>ORDERRECEIVED</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-25" style="margin-top: 10px !important;" title="Handover-BDO Pending">
                                <asp:LinkButton ID="lnktdorderreceived" title="Handover-BDO Pending" runat="server" CssClass="tile ">
                                    <asp:Label ID="lbtodayHandoverBdopending" runat="server" Text="0"></asp:Label>/<asp:Label ID="lbtotalHandoverBdopending" runat="server" Text="0"></asp:Label>
                                    <p>RETURNREQUESTGENERATED</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-25" style="margin-top: 10px !important;" title="Handover-Vendor Pending">
                                <asp:LinkButton ID="lnktdreturn" title="Handover-Vendor Pending" runat="server" CssClass="tile ">
                                    <asp:Label ID="lbtodayHandoverVendorpending" runat="server" Text="0"></asp:Label>/<asp:Label ID="lbtotalHandoverVendorpending" runat="server" Text="0"></asp:Label>
                                    <p>RETURNHANDOVERTOBDO</p>
                                </asp:LinkButton>
                            </div>
                        </fieldset>


                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmMobexSellerFollowupCount" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />
</asp:Content>
