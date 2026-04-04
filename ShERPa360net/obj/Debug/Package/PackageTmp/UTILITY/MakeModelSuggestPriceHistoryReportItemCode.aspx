<%@ Page Title="Product Spec Detail with Itemcode Asin" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="MakeModelSuggestPriceHistoryReportItemCode.aspx.cs" Inherits="ShERPa360net.UTILITY.MakeModelSuggestPriceHistoryReportItemCode" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;</strong>Product Spec Detail with Itemcode Asin</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
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

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Make. :</label>
                                            <div class="col-md-7 col-xs-12">
                                                <asp:DropDownList TabIndex="2" ID="ddlMake" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged" runat="server" CssClass="form-control ddlMake required_text_box"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Model. :</label>
                                            <div class="col-md-7 col-xs-12">
                                                <asp:DropDownList TabIndex="2" ID="ddlModel" runat="server" CssClass="form-control ddlModel required_text_box"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkNewMR" CssClass="btn btn-success pull-left" Text="New Spec" PostBackUrl="~/UTILITY/frmAddProdSpec.aspx"><span tooltip="Add New" flow="down"><i class="fa fa-plus"  aria-hidden="true"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkSerchSpec" CssClass="btn btn-success pull-left" Text="Search Spec" OnClick="lnkSerchSpec_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export Spec" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
                                        </div>
                                    </div>

                                </div>

                                <div id="mobile_View">
                                    <div class="col-md-1" style="margin-top: 5px;">&nbsp;</div>
                                    <div class="col-md-3" style="margin-top: 5px;">
                                        <div class="form-group">
                                            <div class="col-md-9 col-xs-12">
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
    </div>

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Label runat="server" ClientIDMode="Static" Style="font-weight: bold!important;" ID="lblRowCount">RowCount : 0</asp:Label>
                            </div>
                            <div class="col-md-12">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                        <asp:GridView ID="gvList" OnRowDataBound="gvList_RowDataBound" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="BRAND_ID" HeaderText="BRAND ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="BRAND_DESC" HeaderText="Make" />
                                                <asp:BoundField DataField="MODEL_ID" HeaderText="MODEL ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="MODEL_NAME" HeaderText="Model" />
                                                <asp:BoundField DataField="RAMSIZE" HeaderText="RAM" />
                                                <asp:BoundField DataField="ROMSIZE" HeaderText="ROM" />
                                                <asp:BoundField DataField="COLOR" HeaderText="Color" />
                                                <asp:BoundField DataField="NEWRATE" HeaderText="New Rate" />
                                                <asp:TemplateField HeaderText="Sugg%">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Style="font-weight:bold!important;" ID="lblSuggestPer"></asp:Label>
                                                </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="BASICPURRATE" HeaderText="A Gd Rate" />
                                                <asp:BoundField DataField="BASICPURRATEFORBGRADE" HeaderText="B Gd Rate" />
                                                <asp:BoundField DataField="BASICPURRATEFORCGRADE" HeaderText="C Gd Rate" />
                                                <asp:BoundField DataField="CREATEDBY" HeaderText="Create By" />
                                                <asp:BoundField DataField="CREATEDATE" HeaderText="Create Date" />
                                                <asp:BoundField DataField="UPDATEBY" HeaderText="Update By" />
                                                <asp:BoundField DataField="UPDATEDATE" HeaderText="Update Date" />
                                                <asp:BoundField DataField="ISACTIVE" HeaderText="Active" />
                                                <asp:BoundField DataField="LAUNCHYEAR" HeaderText="Launch Year" />
                                                <asp:BoundField DataField="ASIN" HeaderText="ASIN" />
                                                <asp:BoundField DataField="FinalApproveListingAmount" HeaderText="FinalApproveListingAmount" />
                                                <asp:BoundField DataField="AGRADEITEMCOD" HeaderText="AGRADEITEMCOD" />
                                                <asp:BoundField DataField="BGRADEITEMCOD" HeaderText="BGRADEITEMCOD" />
                                                <asp:BoundField DataField="CGRADEITEMCOD" HeaderText="CGRADEITEMCOD" />
                                            </Columns>
                                        </asp:GridView>
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
    <input type="hidden" id="menutabid" value="tsmMobexSellerMakeModelSuggestHistoryReport" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />
</asp:Content>
