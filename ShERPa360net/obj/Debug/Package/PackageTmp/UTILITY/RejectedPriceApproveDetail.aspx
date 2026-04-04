<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="RejectedPriceApproveDetail.aspx.cs" Inherits="ShERPa360net.UTILITY.RejectedPriceApproveDetail" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong runat="server" id="stProductDetail"><span class="fa fa-file"></span>&nbsp;Rejected Price Detail</strong></h3>
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
                                                        <label class="col-md-5 control-label">ID. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtID" CssClass="form-control"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvID" Style="color: red!important;" runat="server" ControlToValidate="txtID" ValidationGroup="SearchDetail"
                                                                ErrorMessage="Please Enter ID To Search">Please Enter ID To Search</asp:RequiredFieldValidator>
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
                                                                        <%--Action Column --%>
                                                                        <asp:TemplateField HeaderText="Action" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server" ID="txtNewRate" Visible="false" Placeholder="New Rate" CssClass="form-control" Width="170"></asp:TextBox>
                                                                                <asp:TextBox runat="server" ID="txtMobexRate" Visible="false" Style="margin-top: 5px!important;" Placeholder="Mobex Rate" CssClass="form-control" Width="170"></asp:TextBox>
                                                                                <asp:TextBox runat="server" Visible="false" TextMode="MultiLine" ID="txtRejectReason" Style="margin-top: 5px!important;" Placeholder="Reject Reason" CssClass="form-control" Width="170"></asp:TextBox>
                                                                                <asp:Label runat="server" Visible="false" Style="margin-top: 5px!important;" ID="lblPurcPerValue"></asp:Label>
                                                                                <asp:Label runat="server" Visible="false" Style="margin-top: 5px!important;" ID="lblFkPerValue"></asp:Label>
                                                                                <asp:Label runat="server" Visible="false" Style="margin-top: 5px!important;" ID="lblAmzPerValue"></asp:Label>
                                                                                <asp:Label runat="server" Visible="false" Style="margin-top: 5px!important;" ID="lblWebPerValue"></asp:Label>
                                                                                <asp:TextBox runat="server" Visible="false" ID="txtFKRate" Enabled="false" Style="margin-top: 5px!important;" Placeholder="Flipcart Rate" CssClass="form-control" Width="170"></asp:TextBox>
                                                                                <asp:TextBox runat="server" Visible="false" ID="txtAmzRate" Enabled="false" Style="margin-top: 5px!important;" Placeholder="Amazon Rate" CssClass="form-control" Width="170"></asp:TextBox>
                                                                                <asp:TextBox runat="server" Visible="false" ID="txtWebRate" Enabled="false" Style="margin-top: 5px!important;" Placeholder="Web Rate" CssClass="form-control" Width="170"></asp:TextBox>
                                                                                <asp:LinkButton runat="server" Style="margin-top: 5px!important;" ID="btnApprove" CssClass="btn btn-primary" Text="Approved" OnClick="btnApprove_Click"></asp:LinkButton>
                                                                                <asp:LinkButton runat="server" Visible="false" Style="margin-top: 5px!important;" ID="btnReject" CssClass="btn btn-danger" Text="Reject" OnClick="btnReject_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <%--Action Column --%>


                                                                        <%--Show Hide Column --%>

                                                                        <asp:TemplateField HeaderText="ID" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblID" Text='<%# Bind("ID") %>'></asp:Label>
                                                                                <asp:HiddenField runat="server" ID="hdItemGrpId" Value='<%# Bind("ITEMGRPID") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="hdItemGrpName" Value='<%# Bind("ITEMGRPNAME") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="hdItemGrpShortName" Value='<%# Bind("ITEMGRPSHORTNAME") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="hdItemSubGrpId" Value='<%# Bind("ITEMSUBGRPID") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="hdItemSubGrpName" Value='<%# Bind("ITEMSUBGRPNAME") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="hdItemSubGrpShortName" Value='<%# Bind("ITEMSUBGRPSHORTNAME") %>'></asp:HiddenField>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Recomended Rate" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblRecomendedRate"></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Lock Amount" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblLockAmountRate" Text='<%# Bind("FinalApproveListingAmount") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="New Price" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblNewPrice" Text='<%# Bind("MOBILENEWRATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Vendor Rate" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVendorRate" Text='<%# Bind("VENDORPRICE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <%--Show Hide Column --%>


                                                                        <%--Regular Column --%>
                                                                        <asp:TemplateField HeaderText="Make" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField runat="server" ID="hdVendorGrade" Value='<%# Bind("VENDORGRADE") %>'></asp:HiddenField>
                                                                                <asp:Label runat="server" ID="lblMake" Text='<%# Bind("MAKE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Model" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblModel" Text='<%# Bind("MODEL") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Rom" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblRom" Text='<%# Bind("ROM") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Ram" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblRam" Text='<%# Bind("RAM") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Color" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblColor" Text='<%# Bind("COLOR") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Vendor Grade" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVendorGrade" Text='<%# Bind("VENDORGRADE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Vendor Name" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVendorName" Text='<%# Bind("VENDORNAME") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Entry Date" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField runat="server" ID="hdID" Value='<%# Bind("ID") %>'></asp:HiddenField>
                                                                                <asp:Label runat="server" ID="lblDate" Text='<%# Bind("CREATEDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Status" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblStatus" Text='<%# Bind("STATUS") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Reject Reason" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblRejectReason" Text='<%# Bind("STATUS") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="IMEINo" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblIMEINo" Text='<%# Bind("IMEINO") %>'></asp:Label>
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
