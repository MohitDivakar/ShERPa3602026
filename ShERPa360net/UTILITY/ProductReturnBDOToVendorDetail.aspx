<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="ProductReturnBDOToVendorDetail.aspx.cs" Inherits="ShERPa360net.UTILITY.ProductReturnBDOToVendorDetail" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong runat="server" id="stProductDetail"><span class="fa fa-file"></span>&nbsp;Product Return Detail</strong></h3>
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
                                                        <label class="col-md-5 control-label">Vendor Name. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList TabIndex="2" ID="ddlVendor" runat="server" CssClass="form-control ddlVendor"></asp:DropDownList>
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
                                                                <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="false"  ShowHeaderWhenEmpty="true" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
                                                                    <EmptyDataTemplate>
                                                                        No Record Found!
                                                                    </EmptyDataTemplate>
                                                                    <Columns>
                                                                         <%--Action Column --%>
                                                                        <asp:TemplateField HeaderText="Print">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton Style="margin-top: 5px!important; margin-left: 64px!important;" runat="server" ID="btnPrint" Text="Print" CssClass="btn btn-primary" OnClick="btnPrint_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--Action Column --%>

                                                                       <%--Action Column --%>
                                                                        <asp:TemplateField HeaderText="HandOver To BDO">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton Style="margin-top: 5px!important; margin-left: 64px!important;" runat="server" ID="btnHandoverToBDO" Text="Return" CssClass="btn btn-primary" OnClick="btnHandoverToBDO_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--Action Column --%>

                                                                        <%--Regular Column --%>

                                                                        <%--Regular Column --%>
                                                                        <asp:TemplateField HeaderText="Entry Date">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField runat="server" ID="hdID" Value='<%# Bind("ID") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="hdVendorId" Value='<%# Bind("VENDORID") %>'></asp:HiddenField>
                                                                                <asp:Label runat="server" ID="lblDate" Text='<%# Bind("CREATEDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="ID">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblID" Text='<%# Bind("ID") %>'></asp:Label>
                                                                                <asp:Label runat="server" ID="lblVendorID" Visible="false" Text='<%# Bind("VENDORID") %>'></asp:Label>
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
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Vendor Rate">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVendorRate" Text='<%# Bind("VENDORPRICE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="IMEINo" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblIMEINo" Text='<%# Bind("IMEINO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblStatus" Text='<%# Bind("STATUS") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Inward Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblInwardDate" Text='<%# Bind("INWARDDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Inward By">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblInwardBy" Text='<%# Bind("INWARDBY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Return Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblReturnDate" Text='<%# Bind("RETURNDATETIME") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Return By">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblReturnBy" Text='<%# Bind("RETURNBY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Return Reason">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblReturnReason" Text='<%# Bind("RETURNREASON") %>'></asp:Label>
                                                                                <asp:Label runat="server" ID="lblBikerContactNo" Visible="false" Text='<%# Bind("BIKERCONTACT") %>'></asp:Label>
                                                                                <asp:Label runat="server" ID="lblAsmContactNo" Visible="false" Text='<%# Bind("ASMCONTACT") %>'></asp:Label>
                                                                                <asp:Label runat="server" ID="lblJpBhaiContactNo" Visible="false" Text='<%# Bind("JPBHAICONTACT") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                          <asp:TemplateField HeaderText="Contact No">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDealerContactNo" Text='<%# Bind("DEALERCONTACTNO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Contact No2">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDealerContactNo2" Text='<%# Bind("DEALERCONTACTNO2") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Contact No3">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDealerContactNo3" Text='<%# Bind("DEALERCONTACTNO3") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <%--Regular Column --%>

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

    <div class="modal fade" id="modalHandovertoVendor" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Return</strong> Details</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                           <div class="col-md-3">
                                <div class="form-group">
                                    <label>Make :</label>
                                    <asp:TextBox TabIndex="1" Enabled="false"  ClientIDMode="Static" ID="txtHandoverBDOMake"  Style="font-weight: bold!important; color: black!important;" runat="server"  CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Model :</label>
                                    <asp:TextBox TabIndex="2" Enabled="false"  ClientIDMode="Static" ID="txtHandoverBDOModel"  Style="font-weight: bold!important; color: black!important;" runat="server"  CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                             <div class="col-md-3">
                                <div class="form-group">
                                    <label>Ram :</label>
                                    <asp:TextBox TabIndex="3" Enabled="false"  ClientIDMode="Static" ID="txtHandoverBDORam"  Style="font-weight: bold!important; color: black!important;" runat="server"  CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Rom :</label>
                                    <asp:TextBox TabIndex="4" Enabled="false" ClientIDMode="Static" ID="txtHandoverBDORom"  Style="font-weight: bold!important; color: black!important;" runat="server"  CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Color :</label>
                                    <asp:TextBox TabIndex="5"  Enabled="false" ClientIDMode="Static" ID="txtHandoverBDOColor"  Style="font-weight: bold!important; color: black!important;" runat="server"  CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Vendor :</label>
                                    <asp:TextBox TabIndex="5"  Enabled="false" ClientIDMode="Static" ID="txtHandoverBDOVendor"  Style="font-weight: bold!important; color: black!important;" runat="server"  CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Inward By :</label>
                                    <asp:TextBox TabIndex="6" Enabled="false"  ClientIDMode="Static" ID="txtHandoverBDOInwardBy"  Style="font-weight: bold!important; color: black!important;" runat="server"  CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                             <div class="col-md-3">
                                <div class="form-group">
                                    <label>Inward Date :</label>
                                    <asp:TextBox TabIndex="7" Enabled="false"  ClientIDMode="Static" ID="txtHandoverBDOInwardDate"  Style="font-weight: bold!important; color: black!important;" runat="server"  CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>IMEI No :</label>
                                    <asp:TextBox TabIndex="7" Enabled="false"  ClientIDMode="Static" ID="txtHandoverBDOIMEINo"  Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="IMEI No"  CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>OTP :</label>
                                    <asp:TextBox TabIndex="3"  ClientIDMode="Static" ID="txtHandoverBDOOTP" Style="font-weight: bold!important; color: black!important;" runat="server"  placeholder="OTP" CssClass="form-control"></asp:TextBox>
                                    <asp:Label ID="lblHandoverOTP" Style="color: red!important; display: none;" runat="server" ClientIDMode="Static">Please Enter the OTP</asp:Label>
                                    <asp:Label ID="lblHandoverInvalidOTP" Style="color: red!important;"  Visible ="false" runat="server" ClientIDMode="Static">Invalid OTP Please enter valid OTP.</asp:Label>
                                </div>
                            </div>

                            <div class="col-md-12 text-center">
                                <div class="col-md-12" style="margin-top: 10px!important;">
                                    <asp:HiddenField runat="server" ID="hdHandoverBDOprimarykey" />
                                    <asp:HiddenField runat="server" ID="hdHandoverBDOBikerContactNo" />
                                    <asp:HiddenField runat="server" ID="hdHandoverBDOAsmContactNo" />
                                    <asp:HiddenField runat="server" ID="hdHandoverBDOJpBhaiContactNo" />
                                    <asp:HiddenField runat="server" ID="hdDealerContactNo" />
                                    <asp:HiddenField runat="server" ID="hdDealerContactNo2" />
                                    <asp:HiddenField runat="server" ID="hdDealerContactNo3" />
                                    <asp:HiddenField runat="server" ID="hdVendorID" />
                                    <asp:Button TabIndex="3" ID="btnSaveHandovertoBDO" OnClick="btnSaveHandover_Click" OnClientClick="return ValidateProductHandoverToBDO();" runat="server" Text="Update" CssClass="btn btn-primary"></asp:Button>
                                    <asp:Button TabIndex="3" ID="btnResetHandovertoBDO" OnClick="btnResetHandovertoBDO_Click" Style="margin-left: 40px!important" runat="server" Text="Reset" CssClass="btn btn-danger"></asp:Button>
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
    <input type="hidden" id="menutabCheckerid" value="tsmMobexSellerBDOVendorProductReturn" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />
</asp:Content>
