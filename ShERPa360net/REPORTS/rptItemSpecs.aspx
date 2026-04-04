<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptItemSpecs.aspx.cs" Inherits="ShERPa360net.REPORTS.rptItemSpecs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;View Product Specification</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">

                                    <div class="col-md-3">
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Job ID : </label>
                                                <div class="col-md-7 col-xs-12">
                                                    <asp:TextBox ID="txtJobID" runat="server" CssClass="form-control" placeholder="Job ID" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label">Item Code : </label>
                                                <div class="col-md-7 col-xs-12">
                                                    <asp:TextBox ID="txtItemCode" runat="server" CssClass="form-control" placeholder="Item Code" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label"></label>
                                                <div class="col-md-7 col-xs-12">
                                                    <asp:LinkButton runat="server" ID="lnkSerch" CssClass="btn btn-success" Text="Search Rate" OnClick="lnkSerch_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i></span></asp:LinkButton>
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


    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div class="page-content-wrap" id="divMain" runat="server" visible="true">
                <div class="row">
                    <div class="col-md-12">
                        <div class="col-md-3">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Item Code : </label>
                                    <div class="col-md-9 col-xs-12">
                                        <asp:Label ID="lblITEMCODE" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Item Desc. : </label>
                                    <div class="col-md-9 col-xs-12">
                                        <asp:Label ID="lblITEMDESC" runat="server" CssClass="form-control" Height="50px"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Brand : </label>
                                    <div class="col-md-9 col-xs-12">
                                        <asp:Label ID="lblMAKE" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Model : </label>
                                    <div class="col-md-9 col-xs-12">
                                        <asp:Label ID="lblMODEL" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="col-md-12" style="margin-top: 20px !important;">
                        <div class="panel panel-default">
                            <div class="panel-body">
                                <div class="col-md-12">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; overflow-y: scroll; max-height: 500px !important;">
                                        <asp:GridView ID="grvData" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Item ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblITEMID" runat="server" Text='<%# Eval("ITEMID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--                                                <asp:TemplateField HeaderText="Item Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblITEMCODE" runat="server" Text='<%# Eval("ITEMCODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Item Desc.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblITEMDESC" runat="server" Text='<%# Eval("ITEMDESC") %>'></asp:Label>
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
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="Spec Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSPECNAME" runat="server" Text='<%# Eval("SPECNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Spec Value">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSPECVALUE" runat="server" Text='<%# Eval("SPECVALUE") %>'></asp:Label>
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
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmRptItemSpec" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptMIS" runat="server" />

</asp:Content>
