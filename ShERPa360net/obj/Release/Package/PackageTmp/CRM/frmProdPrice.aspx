<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/MasterCustomerRltionShipMng.Master" AutoEventWireup="true" CodeBehind="frmProdPrice.aspx.cs" Inherits="ShERPa360net.CRM.frmProdPrice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Product Price</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">

        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Product  </strong>Price</h3>
                            <%--<asp:LinkButton ID="imgCancel" runat="server" CssClass="btn btn-success pull-right" PostBackUrl="~/UTILITY/frmItemMapping.aspx" Text="Reset" TabIndex="16"><i class="fa fa-refresh"></i></asp:LinkButton>--%>
                            <%--<asp:LinkButton ID="imgSaveAll" runat="server" CssClass="btn btn-success pull-right" OnClick="imgSaveAll_Click" Text="Save" ValidationGroup="SaveAll" TabIndex="15"><i class="fa fa-save"></i></asp:LinkButton>--%>
                            <%--<div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                <label>Please wait...</label>
                            </div>--%>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <%--<h4 style="color: #f05423">Item Mapping</h4>--%>
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Make : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:DropDownList ID="ddlMake" runat="server" CssClass="form-control ddlMake required_text_box" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvMake" runat="server" ControlToValidate="ddlMake" ValidationGroup="Search" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Select Make" InitialValue="0">
                                                            Please Select Make</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Model : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:DropDownList ID="ddlModel" runat="server" CssClass="form-control ddlModel required_text_box"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvModel" runat="server" ControlToValidate="ddlModel" ValidationGroup="Search" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Select Model" InitialValue="0">
                                                            Please Select Model</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">RAM : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:DropDownList ID="ddlRAM" runat="server" CssClass="form-control ddlRAM required_text_box"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvRAM" runat="server" ControlToValidate="ddlRAM" ValidationGroup="Search" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Select RAM" InitialValue="0">
                                                            Please Select RAM</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">ROM : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:DropDownList ID="ddlROM" runat="server" CssClass="form-control ddlROM required_text_box"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvROM" runat="server" ControlToValidate="ddlROM" ValidationGroup="Search" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Select ROM" InitialValue="0">
                                                            Please Select ROM</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12" style="margin-top: 10px;">

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Color : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:DropDownList ID="ddlColor" runat="server" CssClass="form-control ddlColor required_text_box"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvColor" runat="server" ControlToValidate="ddlColor" ValidationGroup="Search" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Select Color" InitialValue="0">
                                                            Please Select Color</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Grade : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:DropDownList ID="ddlGrade" runat="server" CssClass="form-control ddlGrade required_text_box"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvGrade" runat="server" ControlToValidate="ddlGrade" ValidationGroup="Search" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Select Grade" InitialValue="0">
                                                            Please Select Grade</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="col-md-12" style="margin-top: 10px;">
                                            <center>
                                                <h4 style="font-weight: bold;">---- OR ----</h4>
                                            </center>
                                        </div>

                                        <div class="col-md-12" style="margin-top: 10px;">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label class="col-md-2 control-label">Item Desc.  : </label>
                                                    <div class="col-md-10 col-xs-12">
                                                        <asp:TextBox ID="txtSearchItemDesc" runat="server" CssClass="form-control" OnTextChanged="txtSearchItemDesc_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <cc:AutoCompleteExtender ServiceMethod="GetItemCode" MinimumPrefixLength="1" CompletionInterval="10"
                                                            EnableCaching="false" CompletionSetCount="1" TargetControlID="txtSearchItemDesc" ID="Auto1" runat="server"
                                                            FirstRowSelected="false" ShowOnlyCurrentWordInCompletionListItem="true">
                                                        </cc:AutoCompleteExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12" style="margin-top: 10px;">
                                            <div class="col-md-3">&nbsp;</div>
                                            <div class="col-md-2">&nbsp;</div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:LinkButton runat="server" ID="lnkSerchItem" CssClass="btn btn-success pull-left" Text="Search" OnClick="lnkSerchItem_Click" ValidationGroup="Search">
<span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
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



    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">


                <div class="panel panel-default">


                    <div class="panel-body">


                        <div class="row">
                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">Item Code : </label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:Label ID="lblItemCode" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-9">
                                    <div class="form-group">
                                        <label class="col-md-2 control-label">Item Desc. : </label>
                                        <div class="col-md-10 col-xs-12">
                                            <asp:TextBox ID="lblItemDesc" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12" style="margin-top: 10px;">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">Regular Price : </label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:Label ID="lblRegularPrice" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">Sale Price : </label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:Label ID="lblSalePrice" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">Stock Status : </label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:Label ID="lblStockStatus" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">Available Stock : </label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:Label ID="lblAvailStock" runat="server" CssClass="form-control"></asp:Label>
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

                            <div class="col-md-12" style="margin-top: 10px;">
                                <div class="col-md-3">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll">

                                        <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                <div style="text-align: center; color: red; font-size: 18px;">
                                                    List is empty !
                                                </div>
                                            </EmptyDataTemplate>
                                            <Columns>

                                                <asp:TemplateField HeaderText="ID">
                                                    <ItemTemplate>
                                                        <%--<asp:LinkButton ID="lnkEdit" runat="server" Text='<%# Eval("JOBID") %>' OnClick="lnkEdit_Click" Visible="true"></asp:LinkButton>--%>
                                                        <asp:Label ID="lblJobid" runat="server" Text='<%# Eval("JOBID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ID Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIDType" runat="server" Text='<%# Eval("IDTYPE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                </div>
                                <%--<div class="col-md-6">
                                    <asp:Label ID="lblImageLabel" runat="server" Text="Image"></asp:Label>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmViewProdPrice" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" />

</asp:Content>
