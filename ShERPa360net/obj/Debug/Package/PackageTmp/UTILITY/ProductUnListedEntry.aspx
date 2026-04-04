<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="ProductUnListedEntry.aspx.cs" Inherits="ShERPa360net.UTILITY.ProductUnListedEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <asp:UpdatePanel ID="pnlprimaryDetails" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddlVendor" EventName="SelectedIndexChanged" />
<%--                                <asp:AsyncPostBackTrigger ControlID="btnUnlist" EventName="Click" />--%>
                            </Triggers>
                            <ContentTemplate>
                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Product UnListed Entry</strong></h3>
                                </div>

                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="row">
                                                <fieldset class="scheduler-border">
                                                    <legend class="scheduler-border"></legend>
                                                    <div class="col-md-12">
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">Vendor. :</label>
                                                                <div class="col-md-9 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ID="ddlVendor"   runat="server" CssClass="form-control ddlVendor required_text_box"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">ID. :</label>
                                                                <div class="col-md-9 col-xs-12">
                                                                    <asp:TextBox runat="server" ClientIDMode="Static" ID="txtID" CssClass="form-control" PlaceOrder="ID"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">Aging. :</label>
                                                                <div class="col-md-9 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ID="ddlAging" runat="server" CssClass="form-control">
                                                                        <asp:ListItem Text="All" Value="-1" Selected="True"></asp:ListItem>
                                                                        <asp:ListItem Text="Relisting Listing" Value="1"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-1">
                                                            <div class="form-group">
                                                                    <asp:LinkButton runat="server" ID="lnkSearh" CssClass="btn btn-success pull-right" Text="Search" OnClick="lnkSearh_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                                            </div>
                                                        </div>



                                                    </div>
                                                    <div class="col-md-12" style="margin-top:10px!important;">
                                                        <fieldset class="scheduler-border">
                                                            <legend class="scheduler-border" id="lgrecordcount" runat="server"></legend>
                                                            <div class="col-md-12 divhorizontal" style="overflow-x: scroll!important;">
                                                                <asp:GridView ID="gvProduct" OnRowDataBound="gvProduct_RowDataBound"  runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
                                                                    <EmptyDataTemplate>
                                                                        No Record Found!
                                                                    </EmptyDataTemplate>
                                                                    <Columns>
                                                                        <%--Action Column --%>
                                                                        <asp:TemplateField HeaderText="UnList" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton Style="margin-top: 5px!important; margin-left: 10px!important;" runat="server" ID="btnUnlist" Text="Unlist" CssClass="btn btn-primary" OnClick="btnUnlist_Click"></asp:LinkButton>
                                                                                 <asp:Label runat="server" ID="lblISAPPLICABLE" Visible="false" Text='<%# Bind("ISAPPLICABLE") %>'></asp:Label>
                                                                                <asp:LinkButton Style="margin-top: 5px!important; margin-left: 10px!important;" runat="server" ID="btnConfirm" Text="Relist" CssClass="btn btn-primary" OnClientClick="return confirm('Are you sure you want to Relist this Model are available at this Vendor?');" OnClick="btnConfirm_Click"></asp:LinkButton>
                                                                                <br />
                                                                                <asp:TextBox runat="server" ID="txtorderno" style="margin-top:5px!important;" Placeholder="Order No" CssClass="form-control" Width="170"></asp:TextBox>
                                                                                <asp:Label ID="lblordernoalert" runat="server"  Style="color: red!important; font-weight: bold!important;margin-left:21px!important; display: none">Please Enter Order No</asp:Label>
                                                                                <asp:LinkButton Style="margin-top: 5px!important; margin-left: 38px!important;" runat="server" ID="btnReserved" Text="Reserved" CssClass="btn btn-primary"  OnClick="btnReserved_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--Action Column --%>

                                                                        <%--Regular Column --%>

                                                                         <asp:TemplateField HeaderText="ID" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblID" Text='<%# Bind("ID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Entry Date" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField runat="server" ID="hdID" Value='<%# Bind("ID") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="lblFIRSTCREATEDDATE" Value='<%# Bind("FIRSTCREATEDDATE") %>'></asp:HiddenField>
                                                                                <asp:Label runat="server" ID="lblDate" Text='<%# Bind("CREATEDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Make" Visible="true">
                                                                            <ItemTemplate>
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

                                                                        <asp:TemplateField HeaderText="Orignal Kit" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblOrignalKit" Text='<%# Bind("ORIGINALKIT") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Vendor Name" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVendorName" Text='<%# Bind("VENDORNAME") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Vendor Grade" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVendorGrade" Text='<%# Bind("VENDORGRADE") %>'></asp:Label>
                                                                                <asp:Label runat="server" ID="lblVendorID" Visible="false" Text='<%# Bind("VENDORID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Status" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblStatus" Text='<%# Bind("STATUS") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="IMEI" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblIMEINO" Text='<%# Bind("IMEINO") %>'></asp:Label>
                                                                                <asp:HiddenField runat="server" ID="hdACTUALSTATUS" Value='<%# Bind("ACTUALSTATUS") %>'></asp:HiddenField>
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

                                                                        <asp:TemplateField HeaderText="Vendor Price" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVENDORPRICE" Text='<%# Bind("VENDORPRICE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>



                                                                        <%--<asp:TemplateField HeaderText="New Price" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblNewPrice" Text='<%# Bind("MOBILENEWRATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Mobex Price" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblMobexPrice" Text='<%# Bind("MOBEXPRICE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Vendor Rate" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVendorRate" Text='<%# Bind("VENDORPRICE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Mobex Grade" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblMobexGrade" Text='<%# Bind("MOBEXGRADE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>--%>

                                                                        <%--Regular Column --%>
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </fieldset>
                                                    </div>

                                                </fieldset>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmTranMobexSellerUnListing" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />
</asp:Content>

