<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="ReturnableDCDetail.aspx.cs" Inherits="ShERPa360net.UTILITY.ReturnableDCDetail" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong runat="server" id="stReturnableDetail"><span class="fa fa-file"></span>&nbsp;Returnable DC</strong></h3>
                            <asp:LinkButton ID="imgReset" runat="server" CssClass="btn btn-success pull-right" OnClick="imgReset_Click" Text="Reset"><i class="fa fa-undo"></i></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkSearh" CssClass="btn btn-success pull-right" Text="Search" OnClick="lnkSearh_Click" ValidationGroup="SearchDetail"><i class="fa fa-search"></i></asp:LinkButton>
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
                                                        <label class="col-md-5 control-label">Status. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList ID="ddlStatus" ClientIDMode="Static" runat="server"  CssClass="form-control">
                                                                <asp:ListItem Text="PENDING"  Value="12330"></asp:ListItem>
                                                                <asp:ListItem Text="COMPLETED" Value="12331"></asp:ListItem>
                                                                <asp:ListItem Text="RETURNREQUESTGENERATED" Selected="True" Value="12334"></asp:ListItem>
                                                                <asp:ListItem Text="RETURNED" Value="12332"></asp:ListItem>
                                                                <asp:ListItem Text="DELIVERED" Value="12335"></asp:ListItem>
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
                                                                <asp:GridView ID="gvReturnableDC" ClientIDMode="Static" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
                                                                    <EmptyDataTemplate>
                                                                        No Record Found!
                                                                    </EmptyDataTemplate>
                                                                    <Columns>
                                                                        <%--Action Column --%>
                                                                        <asp:TemplateField HeaderText="Action" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton runat="server" Style="margin-top: 5px!important;" ID="btnReturnReceived" CssClass="btn btn-primary" Text="Received" OnClick="btnReturnReceived_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--Action Column --%>

                                                                        <%--Regular Column --%>
                                                                        <asp:TemplateField HeaderText="DC NO">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDCNO" Text='<%# Bind("DCNO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Dc Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDcDate" Text='<%# Bind("DCDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Job Id">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblJobId" Text='<%# Bind("JOBID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="So No">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblSONO" Text='<%# Bind("SONO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Ref No">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblRefNo" Text='<%# Bind("REFNO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Listing ID">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblListingID" Text='<%# Bind("LISTINGID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Customer Name" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblCustomerName" Text='<%# Bind("CUSTNAME") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Address">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblAddress" Text='<%# Bind("CUSTOMERADDRESS") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Mobile No">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblMobileNo" Text='<%# Bind("CUSTMOBILENO") %>'></asp:Label>
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

                                                                        <asp:TemplateField HeaderText="IMEI No">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblIMEINo" Text='<%# Bind("IMEINO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Created Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblCreatedDate" Text='<%# Bind("CREATEDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Biker Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblBikerName" Text='<%# Bind("BIKERNAME") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Item Detail">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblItemDetail" Text='<%# Bind("ITEMDESC") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                         <asp:TemplateField HeaderText="Return Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblReturnDate" Text='<%# Bind("RETURNABLEDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                         <asp:TemplateField HeaderText="Return Received By">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblReturnReceivedBy" Text='<%# Bind("RETURNABLEUPDATEBY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                         <asp:TemplateField HeaderText="Return Reason">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblReturnReason" Text='<%# Bind("RETURNREASON") %>'></asp:Label>
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
    <input type="hidden" id="menutabApproverid" value="tsmMobexSellerRejectedtoApprvd" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />
</asp:Content>
