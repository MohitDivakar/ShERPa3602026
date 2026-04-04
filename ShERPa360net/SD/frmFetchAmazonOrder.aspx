<%@ Page Title="" Language="C#" MasterPageFile="~/SD/SDMaster.Master" AutoEventWireup="true" CodeBehind="frmFetchAmazonOrder.aspx.cs" Inherits="ShERPa360net.SD.frmFetchAmazonOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Fetch Amazon Order</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Amazon Order Data </strong></h3>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12" style="margin-top: 10px !important;">
                                        <div class="box">
                                            <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                                <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                    CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" ShowHeader="true">
                                                    <EmptyDataTemplate>
                                                        No Record Found!
                                                    </EmptyDataTemplate>
                                                    <HeaderStyle CssClass="header" />
                                                    <Columns>
                                                        <asp:BoundField DataField="STOCKTYPE" HeaderText="Stock Type" />
                                                      
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12" style="margin-top: 10px !important;">
                                        <div class="col-md-6">
                                            <asp:LinkButton runat="server" ID="lnkDownload" CssClass="btn btn-success pull-right" Text="Search" OnClick="lnkDownload_Click"><i class="fa fa-download"></i>Download</asp:LinkButton>
                                        </div>
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

    <input type="hidden" id="menutabid" value="tsmTranAmazonOrders" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranSD" runat="server" />

</asp:Content>
