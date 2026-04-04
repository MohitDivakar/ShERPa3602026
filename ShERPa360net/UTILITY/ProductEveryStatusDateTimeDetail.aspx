<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="ProductEveryStatusDateTimeDetail.aspx.cs" Inherits="ShERPa360net.UTILITY.ProductEveryStatusDateTimeDetail" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        body .table thead th {
            position: sticky;
            top: 0px;
        }
    </style>
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong runat="server" id="stProductDetail"><span class="fa fa-file"></span>&nbsp;Product Each Status Track Report</strong></h3>
                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-right" Text="Export Report" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkSearh" CssClass="btn btn-success pull-right" Text="Search" OnClick="lnkSearh_Click" ValidationGroup="SearchDetail"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
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
                                                        <label class="col-md-5 control-label">Status. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList TabIndex="2" ID="ddlStatus" runat="server" CssClass="form-control">
                                                                <asp:ListItem Text="ALL" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="PENDING" Value="11227"></asp:ListItem>
                                                                <asp:ListItem Text="TESTED" Value="11228"></asp:ListItem>
                                                                <asp:ListItem Text="APPROVED" Value="11235"></asp:ListItem>
                                                                <asp:ListItem Text="REJECTED" Value="11233"></asp:ListItem>
                                                                <asp:ListItem Text="LISTED" Selected="True" Value="11229"></asp:ListItem>
                                                                <asp:ListItem Text="UNLISTED" Value="11238"></asp:ListItem>
                                                                <asp:ListItem Text="ORDERRECEIVED" Value="11301"></asp:ListItem>
                                                                <asp:ListItem Text="PURCHASE" Value="11398"></asp:ListItem>
                                                                <asp:ListItem Text="RESERVED" Value="11925"></asp:ListItem>
                                                                <asp:ListItem Text="LISTED/RESERVED" Value="100"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Actual Days. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList TabIndex="2" ID="ddlActualDays" runat="server" CssClass="form-control">
                                                                <asp:ListItem Text="ALL" Value="ALL"></asp:ListItem>
                                                                <asp:ListItem Text="LESSTHEN10" Value="LESSTHEN10"></asp:ListItem>
                                                                <asp:ListItem Text="GREATERTHEN10" Value="GREATERTHEN10"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Rate. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList TabIndex="2" ID="ddlRate" runat="server" CssClass="form-control">
                                                                <asp:ListItem Text="ALL" Value="ALL"></asp:ListItem>
                                                                <asp:ListItem Text="LESSTHEN8" Value="LESSTHEN8"></asp:ListItem>
                                                                <asp:ListItem Text="GREATERTHEN8" Value="GREATERTHEN8"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Grade. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList TabIndex="2" ID="ddlGrade" runat="server" CssClass="form-control">
                                                                <asp:ListItem Text="ALL" Value="ALL"></asp:ListItem>
                                                                <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                                                <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                                                <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                                            </asp:DropDownList>
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
                                                                <asp:GridView ID="gvProduct" ClientIDMode="Static"  runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
                                                                    <EmptyDataTemplate>
                                                                        No Record Found!
                                                                    </EmptyDataTemplate>
                                                                    <Columns>
                                                                        <%--Show Hide Column --%>
                                                                        <asp:TemplateField HeaderText="ID">
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

                                                                        <asp:TemplateField HeaderText="Ram">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblRam" Text='<%# Bind("RAM") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Rom">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblRom" Text='<%# Bind("ROM") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Color">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblColor" Text='<%# Bind("COLOR") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Vendor Grade">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVendorGrade" Text='<%# Bind("VENDORGRADE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblStatus" Text='<%# Bind("STATUS") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Entry Date">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField runat="server" ID="hdActualoldDays" Value='<%# Bind("ACTUALOLDDAYS") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="hdID" Value='<%# Bind("ID") %>'></asp:HiddenField>
                                                                                <asp:Label runat="server" ID="lblDate" Text='<%# Bind("CREATEDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Appr/Reject Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblAprvRejectDt" Text='<%# Bind("NEGAPRVDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Listed Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblListedDt" Text='<%# Bind("LISTEDDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Blancco Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblBlanccoDt" Text='<%# Bind("BLANCCOQCDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Reserved Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblReservedDt" Text='<%# Bind("RESERVEDDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Inward Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblInwardDt" Text='<%# Bind("INWARDDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="UnListed Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblUnListedDt" Text='<%# Bind("UNLISTEDDATE") %>'></asp:Label>
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
    <input type="hidden" id="menutabCheckerid" value="tsmMobexSellerIntervalListedUnlistedReport" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />
</asp:Content>
