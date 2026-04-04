<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="JangadKROReturnListingReport.aspx.cs" Inherits="ShERPa360net.UTILITY.JangadKROReturnListingReport" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .margin-top {
            margin-top: 25px;
        }

        .new {
            height: 100px;
            width: 100px;
        }

        .col-md-12 .margin-bottom img {
            margin: 20px;
        }

        .red {
            background: none;
            color: red;
            border: none;
        }

        .zoom:hover {
            margin-top: -50px !important;
            -ms-transform: scale(15); /* IE 9 */
            -webkit-transform: scale(15); /* Safari 3-8 */
            transform: scale(15);
            transform-origin: -1px;
        }

        .modal-h1 {
            margin-top: 5px !important;
            margin-bottom: 5px !important;
            font-size: 27px !important;
            font-weight: 400;
            color: #fff;
        }

        .pd-40 {
            padding-top: 40px;
            padding-bottom: 40px;
        }

        .pd-5 {
            padding-top: 5px;
        }
        .wdth-50
        {
            width: 50%;
        }
        .btn-center{
            display:flex;
            justify-content:center;
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
                            <h3 class="panel-title"><strong runat="server" id="stProductDetail"><span class="fa fa-file"></span>&nbsp;Jangad KRO Return Listing Report</strong></h3>
                            <asp:LinkButton ID="imgReset" runat="server" CssClass="btn btn-success pull-right" OnClick="imgReset_Click" Text="Reset">

                            <span tooltip="Detail" flow="down"><i class="fa fa-undo"></i> </span></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-right" Text="Export MR" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkSearh" CssClass="btn btn-success pull-right" Text="Search" OnClick="lnkSearh_Click" OnClientClick="return PoandPRFromInward()" ValidationGroup="SearchDetail"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <fieldset class="scheduler-border">
                                            <legend class="scheduler-border">Filter Detail</legend>
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Vendor Name. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList TabIndex="2" ID="ddlVendor" runat="server" CssClass="form-control ddlVendor"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label" style="padding-top: 10px">IMEI No :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox TabIndex="3" onkeyup="IsAllowonlyNumericKeyIMEINo();" ClientIDMode="Static" ID="txtIMEINo" MaxLength="15" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="IMEI No" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                 <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Listing. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList TabIndex="2" ID="ddlListingType" runat="server" CssClass="form-control">
                                                                <asp:ListItem Text="RETURN" Value="RETURN" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="SALES" Value="SALES"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                 <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Listing ID. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtListingID" CssClass="form-control" placeholder="Listing ID"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="page-content-wrap">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="panel panel-default tabs">
                                        <ul class="nav nav-tabs" role="tablist">
                                            <li class="active"><a href="#tab-first" role="tab" data-toggle="tab">Details</a></li>
                                        </ul>
                                        <div class="panel-body tab-content">
                                            <div class="tab-pane active" id="tab-first">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <fieldset class="scheduler-border">
                                                            <legend class="scheduler-border" id="lgrecordcount" runat="server"></legend>
                                                            <div class="col-md-12 divhorizontal" style="overflow-x: scroll!important;">
                                                                <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvProduct_RowDataBound" ShowHeaderWhenEmpty="true" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
                                                                    <EmptyDataTemplate>
                                                                        No Record Found!
                                                                    </EmptyDataTemplate>
                                                                    <Columns>
                                                                        <%--Action Column --%>
                                                                        <asp:TemplateField HeaderText="Return" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton Style="margin-top: 5px!important; margin-left: 64px!important;" runat="server" ID="btReturn" Text="Return" CssClass="btn btn-primary" OnClick="btReturn_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--Action Column --%>

                                                                        <asp:TemplateField HeaderText="Entry Date">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField runat="server" ID="hdID" Value='<%# Bind("ID") %>'></asp:HiddenField>
                                                                                <asp:Label runat="server" ID="lblDate" Text='<%# Bind("CREATEDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Inward Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblInwardDate" Text='<%# Bind("INWARDDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Due Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblActualDate" Text='<%# Bind("ACTUALCREATEDDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                       <asp:TemplateField HeaderText="Sales Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblSALESDATE" Text='<%# Bind("SALESDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Sales Return Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblSALESRETURNDATE" Text='<%# Bind("SALESRETURNDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Sales Old Days">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblRemaingDays" Text='<%# Bind("JANGADKRORETURNDAYS") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="ID" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblID" Text='<%# Bind("ID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Make">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblMake" Text='<%# Bind("MAKE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Model">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblModel" Text='<%# Bind("MODEL") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Rom">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblRom" Text='<%# Bind("ROM") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Ram">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblRam" Text='<%# Bind("RAM") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Color">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblColor" Text='<%# Bind("COLOR") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Vendor Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVendorName" Text='<%# Bind("VENDORNAME") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Vendor Grade">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVendorGrade" Text='<%# Bind("VENDORGRADE") %>'></asp:Label>
                                                                                <asp:Label runat="server" ID="lblVendorID" Visible="false" Text='<%# Bind("VENDORID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Vendor Rate">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVendorRate" Text='<%# Bind("VENDORPRICE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblStatus" Text='<%# Bind("STATUS") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="IMEINo" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblIMEINo" Text='<%# Bind("IMEINO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="List Type">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblListingType" Text='<%# Bind("LISTINGTYPE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                         <asp:TemplateField HeaderText="Inward By">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblInwardBy" Text='<%# Bind("INWARDBY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Listing Is KRO">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblLISTVENDORISKRO" Text='<%# Bind("LISTVENDORISKRO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="So No">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblSONO" Text='<%# Bind("SONO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                         <asp:TemplateField HeaderText="Item Code">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblItemCode" Text='<%# Bind("ITEMCODE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--Regular Column --%>
                                                                    </Columns>
                                                                </asp:GridView>
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
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabCheckerid" value="tsmTranJangadReturnListingReport" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />
</asp:Content>
