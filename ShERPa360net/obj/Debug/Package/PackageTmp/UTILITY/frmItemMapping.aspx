<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="frmItemMapping.aspx.cs" Inherits="ShERPa360net.UTILITY.frmItemMapping" EnableEventValidation="false" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Item Mapping</title>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">

        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Item Mapping  </strong>Entry</h3>


                            <asp:LinkButton ID="imgCancel" runat="server" CssClass="btn btn-success pull-right" PostBackUrl="~/UTILITY/frmItemMapping.aspx" Text="Reset" TabIndex="16"><i class="fa fa-refresh"></i></asp:LinkButton>
                            <asp:LinkButton ID="imgSaveAll" runat="server" CssClass="btn btn-success pull-right" OnClick="imgSaveAll_Click" Text="Save" ValidationGroup="SaveAll" TabIndex="15"><%--<i class="fa fa-save"></i>--%></asp:LinkButton>
                            <%--<div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                <label>Please wait...</label>
                            </div>--%>
                        </div>

                        <div class="panel-body">

                            <div class="row">



                                <div class="col-md-12">
                                    <h4 style="color: #f05423">Item Mapping</h4>
                                    <div class="row">

                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Make : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:DropDownList ID="ddlMake" runat="server" CssClass="form-control ddlMake required_text_box" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvMake" runat="server" ControlToValidate="ddlMake" ValidationGroup="Search" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Select Make" InitialValue="0">Please Select Make</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Model : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:DropDownList ID="ddlModel" runat="server" CssClass="form-control ddlModel required_text_box"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvModel" runat="server" ControlToValidate="ddlModel" ValidationGroup="Search" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Select Model" InitialValue="0">Please Select Model</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">RAM : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:DropDownList ID="ddlRAM" runat="server" CssClass="form-control ddlRAM required_text_box"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvRAM" runat="server" ControlToValidate="ddlRAM" ValidationGroup="Search" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Select RAM" InitialValue="0">Please Select RAM</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">ROM : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:DropDownList ID="ddlROM" runat="server" CssClass="form-control ddlROM required_text_box"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvROM" runat="server" ControlToValidate="ddlROM" ValidationGroup="Search" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Select ROM" InitialValue="0">Please Select ROM</asp:RequiredFieldValidator>
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
                                                            Display="Dynamic" ErrorMessage="Please Select Color" InitialValue="0">Please Select Color</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Grade : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:DropDownList ID="ddlGrade" runat="server" CssClass="form-control ddlGrade required_text_box"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvGrade" runat="server" ControlToValidate="ddlGrade" ValidationGroup="Search" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Select Grade" InitialValue="0">Please Select Grade</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <asp:LinkButton runat="server" ID="lnkSerchItem" CssClass="btn btn-success pull-left" Text="Search" OnClick="lnkSerchItem_Click" ValidationGroup="Search"><i class="fa fa-search"></i></asp:LinkButton>
                                                </div>
                                            </div>

                                        </div>


                                        <div class="col-md-12" style="margin-top: 10px;">
                                            <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Action">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkSelect" runat="server" Text="Select" OnClick="lnkSelect_Click" Visible="true"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemcode" runat="server" Text='<%# Eval("ITEMCODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item Desc.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemDesc" runat="server" Text='<%# Eval("ITEMDESC") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>


                                        <div class="col-md-12" style="margin-top: 10px;">
                                            <div class="col-md-3">&nbsp;</div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Item Code : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtItemCode" runat="server" CssClass="form-control required_text_box" OnTextChanged="txtItemCode_TextChanged" AutoPostBack="true" MaxLength="10"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvItemCode" runat="server" ControlToValidate="txtItemCode" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Item Code">Please Enter Item Code</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="col-md-2 control-label">Item Desc. : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtItemDesc" runat="server" CssClass="form-control required_text_box" Enabled="false"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvItemDesc" runat="server" ControlToValidate="txtItemDesc" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Item Desc.">Please Enter Item Desc.</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="col-md-12" style="margin-top: 10px;">
                                            <div class="col-md-3">&nbsp;</div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">SKU : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtSKU" runat="server" CssClass="form-control required_text_box" OnTextChanged="txtSKU_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvSKU" runat="server" ControlToValidate="txtSKU" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter SKU">Please Enter SKU</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12" style="margin-top: 10px;">
                                            <div class="col-md-3">&nbsp;</div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Flipkart : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtFlipakrt" runat="server" CssClass="form-control required_text_box" OnTextChanged="txtFlipakrt_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvFlipkart" runat="server" ControlToValidate="txtFlipakrt" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Flipakrt Code">Please Enter Flipakrt Code</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12" style="margin-top: 10px;">
                                            <div class="col-md-3">&nbsp;</div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Amazon : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtAmazon" runat="server" CssClass="form-control required_text_box" OnTextChanged="txtAmazon_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvAmazon" runat="server" ControlToValidate="txtAmazon" ValidationGroup="SaveAll" Style="color: red;" Enabled="true"
                                                            Display="Dynamic" ErrorMessage="Please Enter Amazon Code">Please Enter Amazon Code</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12" style="margin-top: 10px;">
                                            <div class="col-md-3">&nbsp;</div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Website : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control required_text_box" OnTextChanged="txtWebsite_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvWebsite" runat="server" ControlToValidate="txtWebsite" ValidationGroup="SaveAll" Style="color: red;" Enabled="true"
                                                            Display="Dynamic" ErrorMessage="Please Enter Website Code">Please Enter Website Code</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12" style="margin-top: 10px;">
                                            <div class="col-md-3">&nbsp;</div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Stock Clearance Website : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtSCWebsite" runat="server" CssClass="form-control required_text_box" OnTextChanged="txtSCWebsite_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvScWebsite" runat="server" ControlToValidate="txtSCWebsite" ValidationGroup="SaveAll" Style="color: red;" Enabled="true"
                                                            Display="Dynamic" ErrorMessage="Please Enter Stock Clearance Website Code">Please Enter Stock Clearance Website Code</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                         <div class="col-md-12" style="margin-top: 10px;">
                                            <div class="col-md-3">&nbsp;</div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Amazon New Device : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtNewAmazon" runat="server" CssClass="form-control required_text_box"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSCWebsite" ValidationGroup="SaveAll" Style="color: red;" Enabled="true"
                                                            Display="Dynamic" ErrorMessage="Please Enter Stock Clearance Website Code">Please Enter Stock Clearance Website Code</asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                         <div class="col-md-12" style="margin-top: 10px;">
                                            <div class="col-md-3">&nbsp;</div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">FC URL : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtCFURL" runat="server" CssClass="form-control required_text_box"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSCWebsite" ValidationGroup="SaveAll" Style="color: red;" Enabled="true"
                                                            Display="Dynamic" ErrorMessage="Please Enter Stock Clearance Website Code">Please Enter Stock Clearance Website Code</asp:RequiredFieldValidator>--%>
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

    <%-- </div>--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmMstMMItemMapped" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmMstMM" />

</asp:Content>
