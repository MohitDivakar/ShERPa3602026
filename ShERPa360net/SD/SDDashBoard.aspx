<%@ Page Title="" Language="C#" MasterPageFile="~/SD/SDMaster.Master" AutoEventWireup="true" CodeBehind="SDDashBoard.aspx.cs" Inherits="ShERPa360net.SD.SDDashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>SD Dashboard</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="page-content-wrap">

        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">




                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Today's History </strong></h3>
                        </div>

                        <div class="row" style="margin-top: 10px !important;">

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <%--<a >--%>
                                <asp:LinkButton ID="lvkOrderBooked" runat="server" CssClass="tile tile-warning">
                                    <asp:Label ID="lblOrderBooked" runat="server" Text="0"></asp:Label>
                                    <p>Order Booked</p>
                                </asp:LinkButton>
                                <%--</a>--%>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkOrderReturned" runat="server" CssClass="tile tile-info" BackColor="#3E8768" BorderColor="#3E8768">
                                    <asp:Label ID="lblOrderReturned" runat="server" Text="0"></asp:Label>
                                    <p style="color: white;">Order Returned</p>
                                    <%--<div class="informer informer-default dir-bl"><span class="fa fa-globe"></span>Lates Somethink</div>--%>
                                </asp:LinkButton>
                            </div>


                            <div class="col-md-2" style="margin-top: 10px !important;">
                                  <asp:LinkButton ID="lnkOrderCancelled" runat="server" CssClass="tile tile-danger">
                                    <asp:Label ID="lblOrderCancelled" runat="server" Text="0"></asp:Label>
                             
                                    <p style="color: white;">Order Cancelled</p>
                                    <%--<div class="informer informer-default"><span class="fa fa-shopping-cart"></span></div>--%>
                                </asp:LinkButton>
                            </div>


                            <div class="col-md-2" style="margin-top: 10px !important;">
                                 <asp:LinkButton ID="lnkDispatched" runat="server" CssClass="tile tile-default" BackColor="Red" BorderColor="Red">
                                    <asp:Label ID="lblDispatched" runat="server" Text="0" ForeColor="White"></asp:Label>
                                    <p style="color: white;">Dispatched</p>
                                    <%--<div class="informer informer-default dir-tr"><span class="fa fa-calendar"></span></div>--%>
                                </asp:LinkButton>
                            </div>






                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkDelivered" runat="server" CssClass="tile tile-primary">
                                    <asp:Label ID="lblDelivered" runat="server" Text="0"></asp:Label>
                                    <p>Delivered</p>
                                    <%--<div class="informer informer-danger dir-tr"><span class="fa fa-caret-down"></span></div>--%>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkInvoice" runat="server" CssClass="tile tile-success">
                                    <asp:Label ID="lblInovice" runat="server" Text="0"></asp:Label>
                                    <p>Invoice Generated</p>
                                </asp:LinkButton>
                            </div>

                        </div>
                    </div>

                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; DASHBOARD </strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-2 control-label"></label>
                                            <div class="col-md-10 col-xs-12 pull-right">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker pull-right" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-2 control-label pull-left" style="padding-top: 7px;">To</label>
                                            <div class="col-md-10 col-xs-12 pull-right">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker pull-right" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:LinkButton runat="server" ID="lnkSearch" CssClass="btn btn-success pull-left" Text="Search" OnClick="lnkSearch_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Order Management</strong></h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12" style="margin-top: 10px;">
                                    <div class="row">
                                        <fieldset class="scheduler-border">
                                            <div class="col-md-12" id="divName" runat="server" visible="true">
                                                <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                    <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                        CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" ShowFooter="true">
                                                        <EmptyDataTemplate>
                                                            <div style="text-align: center; color: red; font-size: 18px;">
                                                                List is empty !
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:BoundField DataField="SALESCHANNEL" HeaderText="Sales From" />
                                                            <asp:BoundField DataField="PLANT" HeaderText="Plant" />
                                                            <asp:BoundField DataField="ORDERBOOKED" HeaderText="Order Booked" />
                                                            <asp:BoundField DataField="RETURNORDER" HeaderText="Order Returned" />
                                                            <asp:BoundField DataField="ORDERCANCEL" HeaderText="Order Cancelled" />
                                                            <asp:BoundField DataField="DISPATCH" HeaderText="Dispatched" />
                                                            <asp:BoundField DataField="DELIVERED" HeaderText="Delivered" />
                                                            <asp:BoundField DataField="INVOICE" HeaderText="Invoice Generated" />
                                                        </Columns>
                                                    </asp:GridView>
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


    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Open Sales</strong></h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12" style="margin-top: 10px;">
                                    <div class="row">
                                        <fieldset class="scheduler-border">
                                            <div class="col-md-12" id="div1" runat="server" visible="true">
                                                <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                    <asp:GridView ID="gvList2" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                        CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" ShowFooter="true">
                                                        <EmptyDataTemplate>
                                                            <div style="text-align: center; color: red; font-size: 18px;">
                                                                List is empty !
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:BoundField DataField="DELIDT" HeaderText="DELI. DT." />
                                                            <asp:BoundField DataField="AMAZON" HeaderText="AMAZON" />
                                                            <asp:BoundField DataField="2GUD" HeaderText="2GUD" />
                                                            <asp:BoundField DataField="AFFILIATE" HeaderText="AFFILIATE" />
                                                            <asp:BoundField DataField="FLIPKART" HeaderText="FLIPAKRT" />
                                                            <asp:BoundField DataField="MOBEX WEBSITE" HeaderText="MOBEX WEBSITE" />
                                                            <asp:BoundField DataField="STALL RAKHIAL" HeaderText="STALL RAKHIYAL" />
                                                            <asp:BoundField DataField="AGEING" HeaderText="AGEING" />
                                                        </Columns>
                                                    </asp:GridView>
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

    <input type="hidden" id="menutabid" value="tsmRptSDDashboard" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptSD" />

</asp:Content>

