<%@ Page Title="" Language="C#" MasterPageFile="~/TS/MasterTS.Master" AutoEventWireup="true" CodeBehind="TSDashboard.aspx.cs" Inherits="ShERPa360net.TS.TSDashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="page-title">
        <h1><span class="fa fa-th"></span>&nbsp;     Dashboard</h1>
        <!-- START WIDGETS -->
        <div class="row">
            <div class="col-md-3">
                <div class="widget widget-default widget-item-icon" id="">
                    <div class="widget-item-left">
                        <img src="../img/icons/Purchase.png" />
                    </div>
                    <div class="widget-data">
                        <div class="widget-int num-count" runat="server" id="dvNotification">48</div>
                        <div class="widget-title">NF</div>
                        <div class="widget-subtitle">Today Total Notification</div>
                    </div>
                </div>
            </div>

             <div class="col-md-3">
                 <div class="widget widget-default widget-item-icon" id="">
                    <div class="widget-item-left">
                        <img src="../img/icons/Purchase.png" />
                    </div>
                    <div class="widget-data">
                        <div class="widget-int num-count" runat="server" id="dvPrescanning">48</div>
                        <div class="widget-title">PS</div>
                        <div class="widget-subtitle">Today Total Prescanning</div>
                    </div>
                </div>
            </div>

             <div class="col-md-3">
                 <div class="widget widget-default widget-item-icon" id="">
                    <div class="widget-item-left">
                        <img src="../img/icons/Purchase.png" />
                    </div>
                    <div class="widget-data">
                        <div class="widget-int num-count" runat="server" id="dvRepaired">48</div>
                        <div class="widget-title">RP</div>
                        <div class="widget-subtitle">Today Total Repaired</div>
                    </div>
                </div>
            </div>

             <div class="col-md-3">
                 <div class="widget widget-default widget-item-icon" id="">
                    <div class="widget-item-left">
                        <img src="../img/icons/Purchase.png" />
                    </div>
                    <div class="widget-data">
                        <div class="widget-int num-count" runat="server" id="dvIR">48</div>
                        <div class="widget-title">IR</div>
                        <div class="widget-subtitle">Today Total IR</div>
                    </div>
                </div>
            </div>
            
        </div>
        <!-- END WIDGETS -->


        <!-- START WIDGETS -->
        <div class="row">
            <div class="col-md-3">
                <div class="widget widget-default widget-item-icon" id="">
                    <div class="widget-item-left">
                        <img src="../img/icons/Purchase.png" />
                    </div>
                    <div class="widget-data">
                        <div class="widget-int num-count" runat="server" id="dvBER">48</div>
                        <div class="widget-title">BER</div>
                        <div class="widget-subtitle">Today Total BER</div>
                    </div>
                </div>
            </div>

             <div class="col-md-3">
                 <div class="widget widget-default widget-item-icon" id="">
                    <div class="widget-item-left">
                        <img src="../img/icons/Purchase.png" />
                    </div>
                    <div class="widget-data">
                        <div class="widget-int num-count" runat="server" id="dvFailure">48</div>
                        <div class="widget-title">FL</div>
                        <div class="widget-subtitle">Today Total Failured</div>
                    </div>
                </div>
            </div>

             <div class="col-md-3">
                 <div class="widget widget-default widget-item-icon" id="">
                    <div class="widget-item-left">
                        <img src="../img/icons/Purchase.png" />
                    </div>
                    <div class="widget-data">
                        <div class="widget-int num-count" runat="server" id="dvDispatch">48</div>
                        <div class="widget-title">DP</div>
                        <div class="widget-subtitle">Today Total Dispatch</div>
                    </div>
                </div>
            </div>
        </div>
        <!-- END WIDGETS -->	

    </section>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmTranTATASKY" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranTATASKY" runat="server" />
</asp:Content>
