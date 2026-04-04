<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="frmListingAssign.aspx.cs" Inherits="ShERPa360net.UTILITY.frmListingAssign" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Pickup Assign</title>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Pickup  </strong>Assign</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-5">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Listing ID : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtListingID" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvListingID" runat="server" ControlToValidate="txtListingID" Display="Dynamic" Style="color: red;" ValidationGroup="Search"
                                                        ErrorMessage="Please Enter atleast one Listing ID">Please Enter atleast one Listing ID</asp:RequiredFieldValidator>
                                                </div>
                                                <asp:Label ID="lblNote" runat="server" ForeColor="Blue">Use Comma ( , ) to search multiple Listing ID. </asp:Label>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label"></label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:LinkButton runat="server" ID="lnkSerch" CssClass="btn btn-success " Text="Search Listing ID" OnClick="lnkSerch_Click" ValidationGroup="Search"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                                    <%--<asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success" Text="Export " OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>--%>
                                                </div>
                                            </div>
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
                            <div class="col-md-12" style="margin-top: 10px !important;">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <div class="col-md-12 col-xs-12">
                                            <div class="input-group">
                                                <asp:LinkButton ID="lnkAllAssignToPorter" runat="server" Text="Assign to Porter" OnClick="lnkAllAssignToPorter_Click" CssClass="btn btn-success" Font-Size="Larger" Enabled="false" Visible="false"> Assign to Porter </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12" style="margin-top: 10px !important;">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                        <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <HeaderStyle CssClass="header" />
                                            <Columns>

                                                <asp:TemplateField HeaderText="Select">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkSelectAll" runat="server" OnCheckedChanged="chkSelectAll_CheckedChanged" AutoPostBack="true" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" OnCheckedChanged="chkSelect_CheckedChanged" AutoPostBack="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Assign">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="btnAssign" Text="Assign to Porter" OnClick="btnAssign_Click" CssClass="btn btn-success"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Listing ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Make">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMAKE" runat="server" Text='<%# Eval("MAKE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Model">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMODEL" runat="server" Text='<%# Eval("MODEL") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Color">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCOLOR" runat="server" Text='<%# Eval("COLOR") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="RAM">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRAM" runat="server" Text='<%# Eval("RAM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ROM">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblROM" runat="server" Text='<%# Eval("ROM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Vendor Grade">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVENDORGRADE" runat="server" Text='<%# Eval("VENDORGRADE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Vendor ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVENDORID" runat="server" Text='<%# Eval("VENDORID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Vendor Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblVENDORNAME" runat="server" Text='<%# Eval("VENDORNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Listed By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUSERNAME" runat="server" Text='<%# Eval("USERNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Listed Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCREATEDATE" runat="server" Text='<%# Eval("CREATEDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLISTDESC" runat="server" Text='<%# Eval("LISTDESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Lat.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLAT" runat="server" Text='<%# Eval("LAT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Lon.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLONG" runat="server" Text='<%# Eval("LONG") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

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

    <input type="hidden" id="menutabid" value="tsmTranMobexListingAssign" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />

</asp:Content>
