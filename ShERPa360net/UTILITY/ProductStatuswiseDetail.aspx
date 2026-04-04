<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="ProductStatuswiseDetail.aspx.cs" Inherits="ShERPa360net.UTILITY.ProductStatuswiseDetail" EnableEventValidation="false" %>

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
                            <h3 class="panel-title"><strong runat="server" id="stProductDetail"><span class="fa fa-file"></span>&nbsp;Product Status Report</strong></h3>
                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-right" Text="Export Report" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
                            <asp:LinkButton ID="imgReset" runat="server" CssClass="btn btn-success pull-right" OnClick="imgReset_Click" Text="Reset"><span tooltip="Detail" flow="down"><i class="fa fa-undo"></i> </span></asp:LinkButton>
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
                                                                <asp:GridView ID="gvProduct" OnRowDataBound="gvProduct_RowDataBound" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
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

                                                                        <asp:TemplateField HeaderText="Entry Date">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField runat="server" ID="hdActualoldDays" Value='<%# Bind("ACTUALOLDDAYS") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="hdID" Value='<%# Bind("ID") %>'></asp:HiddenField>
                                                                                <asp:Label runat="server" ID="lblDate" Text='<%# Bind("CREATEDATE") %>'></asp:Label>
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

                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblStatus" Text='<%# Bind("STATUS") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Year">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblYear" Text='<%# Bind("LAUNCHYEAR") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Orignal Kit">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblOrignalKit" Text='<%# Bind("ORIGINALKIT") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="IMEI No">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblIMEINo" Text='<%# Bind("IMEINO") %>'></asp:Label>
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
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Mobex Grade">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblMobexGrade" Text='<%# Bind("MOBEXGRADE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Vendor Stock">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVendorStock" Text='<%# Bind("VENDORQTY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Vendor Rate">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVendorRate" Text='<%# Bind("VENDORPRICE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="New Rate">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblNewRate" Text='<%# Bind("MOBILENEWRATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Mobex Purchase Rate">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblMobexPurchaseRate" Text='<%# Bind("MOBEXPRICE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Mobex Purchase Per(%)">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblMobexPurchasePer" Text='<%# Bind("MOBILEPURCHASEPERCENTAGE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>


                                                                        <%--<asp:TemplateField HeaderText="Listed FK" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblListedFK" Text='<%# Bind("ISAPPROVEDFK") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="FK Amt" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblFKAmt" Text='<%# Bind("FKAMT") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="FK Per" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblFKPer" Text='<%# Bind("FKPER") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>

                                                                        <asp:TemplateField HeaderText="Listed Amz">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblListedAmz" Text='<%# Bind("ISAPPROVEDAMZ") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Amz Amt">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblAmzAmt" Text='<%# Bind("AMZAMT") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Amz Per">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblAmzPer" Text='<%# Bind("AMZPER") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Listed Web">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblListedWeb" Text='<%# Bind("ISAPPROVEDWEB") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Web Amt">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblWebAmt" Text='<%# Bind("WEBAMT") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Web Per">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblWebPer" Text='<%# Bind("WEBPER") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Entry By">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblEntryBy" Text='<%# Bind("CREATEBY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Reject Reason">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblRejectReason" Text='<%# Bind("REJECTREASON") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="ASM">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblASMName" Text='<%# Bind("ASM") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Area">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblAreaName" Text='<%# Bind("AREA") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="UnListed Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblUnlistedDate" Text='<%# Bind("UNLISTEDDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="UnListed By">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblUnlistedBy" Text='<%# Bind("UNLISTBY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Designation">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDesignation" Text='<%# Bind("DESIGNATION") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="RESERVEDBY">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblRESERVEDBY" Text='<%# Bind("RESERVEDBY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="RESERVEDDATE">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblRESERVEDDATE" Text='<%# Bind("RESERVEDDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                         <asp:TemplateField HeaderText="STATE">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblSTATE" Text='<%# Bind("STATE") %>'></asp:Label>
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
    <input type="hidden" id="menutabCheckerid" value="tsmMobexSellerStatusReport" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />
</asp:Content>
