<%@ Page Title="" Language="C#" MasterPageFile="~/TS/MasterTS.Master" AutoEventWireup="true" CodeBehind="CategoryMaster.aspx.cs" Inherits="ShERPa360net.TS.CategoryMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <asp:UpdatePanel ID="pnlprimaryDetails" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="imgReset" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="imgSaveAll" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="ddlCategory" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="grvCategoryItem" EventName="RowCommand" />
                            </Triggers>
                            <ContentTemplate>
                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Category Master</strong></h3>
                                    <asp:LinkButton ID="imgReset" runat="server" CssClass="btn btn-success pull-right" OnClick="imgReset_Click" Text="Reset"><i class="fa fa-undo"></i></asp:LinkButton>
                                    <asp:LinkButton ID="imgSaveAll" runat="server" CssClass="btn btn-success pull-right" OnClick="imgSaveAll_Click" Text="Save All" ValidationGroup="SaveAll"><i class="fa fa-save"></i></asp:LinkButton>
                                </div>

                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h4 style="color: #f05423">Entry</h4>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Category Name. :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdCategoryId" Value="0" />
                                                                <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvCategory" Style="color: red;" ControlToValidate="ddlCategory" runat="server" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Select Category" InitialValue="0">Please Select Category</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Category Value. :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtCategoryValue" runat="server" CssClass="form-control" placeholder="Category Value"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvCategoryValue" Style="color: red!important;" runat="server" ControlToValidate="txtCategoryValue" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Category Value">Please Enter Category Value</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Category Code. :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtCategoryCode" runat="server" CssClass="form-control" placeholder="Category Code"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Part Name. :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtPartName" runat="server" CssClass="form-control" placeholder="Part Name"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>


                                                </div>

                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Model. :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList ID="ddlModel" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Material Code. :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtMeterialCode" runat="server" CssClass="form-control" placeholder="Material Code"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>


                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Status. :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvStatus" Style="color: red;" ControlToValidate="ddlStatus" runat="server" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Select Status" InitialValue="-1">Please Select Status</asp:RequiredFieldValidator>
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
                                            <div class="panel panel-default tabs">
                                                <ul class="nav nav-tabs" role="tablist">
                                                    <li class="active"><a href="#tab-first" role="tab" data-toggle="tab">Details</a></li>
                                                </ul>
                                                <div class="panel-body tab-content">
                                                    <div class="tab-pane active" id="tab-first">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-md-12 divhorizontal" style="overflow-x: scroll;">
                                                                    <asp:GridView ID="grvCategoryItem" OnRowCommand="grvCategoryItem_RowCommand" runat="server" AutoGenerateColumns="false" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Status">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton runat="server" ID="lnkEdit" CommandArgument='<%#Eval("CATID") %>' CommandName="eEdit">Edit</asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Category Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblCategoryName" Text='<%# Bind("CATNAME") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Category Value">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblCategoryValue" Text='<%# Bind("CATVALUE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Category Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblCategoryCode" Text='<%# Bind("CATCODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Part Name">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblPartName" Text='<%# Bind("CATCHILDVALUE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                           <asp:TemplateField HeaderText="Model">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblModelName" Text='<%# Bind("CATCHILDKEYVALUE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Material Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblMaterialCode" Text='<%# Bind("CATMATERIALCODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Status">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblStatus" Text='<%# Bind("CATISACTIVEVALUE") %>'></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblStatusID" Text='<%# Bind("CATISACTIVE") %>' Visible="false"></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblID" Text='<%# Bind("CATID") %>' Visible="false"></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblModelKey" Text='<%# Bind("CATCHILDKEY") %>' Visible="false"></asp:Label>
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmTranTATASKYCATEGORY" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranTATASKY" runat="server" />
</asp:Content>
