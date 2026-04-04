<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="ReservedExcelUpload.aspx.cs" Inherits="ShERPa360net.UTILITY.ReservedExcelUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:HiddenField runat="server" ID="hdIsAvailable" Value="0" />
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Reserved Upload Entry</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <h4 style="color: #f05423">Reserved Entry</h4>
                                    <div class="row">
                                        <fieldset class="scheduler-border">
                                            <legend class="scheduler-border">Reserved Upload</legend>
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Select File. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:FileUpload runat="server" ClientIDMode="Static" ID="flReserUpload" />
                                                            <asp:Label Style="color: red!important; font-weight: bold!important; display: none!important;" runat="server" ClientIDMode="Static" ID="lblfilealert">Please Select the File to Upload.</asp:Label>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-2">
                                                    <asp:Button runat="server" ClientIDMode="Static" ID="btnUpload" OnClick="btnUpload_Click" OnClientClick="return ValidateReservedExcelUpload();" Text="Upload" CssClass="btn btn-primary" />

                                                    <asp:Button runat="server" Style="margin-left: 5px!important;" OnClick="btnCancel_Click" ClientIDMode="Static" ID="btnCancel" Text="Cancel" CssClass="btn btn-danger" />
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

                                                            <div class="col-md-12" style="margin-top: 5px!important; margin-bottom: 5px!important;">
                                                                <div class="col-md-2">
                                                                    <legend class="scheduler-border" id="lgrecordcount" runat="server"></legend>
                                                                </div>

                                                                <div class="col-md-8 text-center">
                                                                    <asp:Button runat="server" ClientIDMode="Static" OnClick="btnSaveDetail_Click" ID="btnSaveDetail" CssClass="btn btn-success" Text="Save" />
                                                                </div>
                                                            </div>
                                                            <div class="col-md-12 divhorizontal" style="overflow-x: scroll;">
                                                                <asp:GridView ID="gvProduct" TabIndex="9" runat="server" AutoGenerateColumns="false" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
                                                                    <Columns>
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
                                                                                <asp:Label runat="server" ID="lblMODEL" Text='<%# Bind("MODEL") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Ram">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblRam" Text='<%# Bind("RAM") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Rom">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblROM" Text='<%# Bind("ROM") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Color">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblCOLOR" Text='<%# Bind("COLOR") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Grade">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVENDORGRADE" Text='<%# Bind("VENDORGRADE") %>'></asp:Label>
                                                                                <asp:Label runat="server" ID="lblVendorID" Visible="false" Text='<%# Bind("VENDORID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Grade">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVENDORNAME" Text='<%# Bind("VENDORNAME") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
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
    <input type="hidden" id="menutabid" value="tsmTranMobexDeviceReserved" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />
</asp:Content>

