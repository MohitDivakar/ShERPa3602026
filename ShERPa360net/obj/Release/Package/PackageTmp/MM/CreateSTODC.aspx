<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="CreateSTODC.aspx.cs" Inherits="ShERPa360net.MM.CreateSTODC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }
    </style>

    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            var pono = $("#ContentPlaceHolder1_txtPONO").val();
            var tran = $("#ContentPlaceHolder1_txtTranCode").val();
            if (pono != "" && tran != "") {
                document.getElementById("busy-holder1").style.display = "";
                document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";
            }
        }
    </script>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="pnlprimaryDetails" runat="server" UpdateMode="Conditional">
        <Triggers>
        </Triggers>
        <ContentTemplate>
            <div class="page-content-wrap">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; STO </strong>- Delivery Challan</h3>
                                    <asp:LinkButton ID="imgCancel" runat="server" CssClass="btn btn-success pull-right" PostBackUrl="~/MM/ViewMaterialIssue.aspx?REQUESTFORM=STO" Text="Cancel"><i class="fa fa-times"></i></asp:LinkButton>
                                    <asp:LinkButton ID="imgSaveAll" runat="server" CssClass="btn btn-success pull-right" OnClientClick="ShowLoading()" Text="Save All" OnClick="imgSaveAll_Click" ValidationGroup="SaveAll"><i class="fa fa-save"></i></asp:LinkButton>
                                    <div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                        <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                        <label>Please wait...</label>
                                    </div>
                                </div>
                                <div class="panel-body">
                                    <div class="row">

                                        <div class="col-md-12">
                                            <h4 style="color: #f05423">Primary Details</h4>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Doc. Type : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList ID="ddlDoctype" runat="server" CssClass="form-control " TabIndex="1" Enabled="false"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvDocType" runat="server" ControlToValidate="ddlDoctype" ValidationGroup="SaveAll" Style="color: red"
                                                                    ErrorMessage="Please Select Doc. Type" InitialValue="0">Please Select Doc. Type</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Doc. No. : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtDocNo" runat="server" Enabled="false" CssClass="form-control required_text_box" placeholder="Doc. No."></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvDocNo" Style="color: red;" runat="server" ControlToValidate="txtDocNo" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Doc. No.">Please Enter Doc. No.</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Doc. Date : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtDocDt" runat="server" Enabled="false" CssClass="form-control required_text_box" placeholder="Doc. Date"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RfvDocDate" Style="color: red;" runat="server" ControlToValidate="txtDocDt" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Doc Date">Please Enter Doc Date</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Ref. No. : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtRefno" runat="server" CssClass="form-control" placeholder="Ref. No."></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Style="color: red;" runat="server" ControlToValidate="txtDocType" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Doc Type">*</asp:RequiredFieldValidator>--%>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">PO No. : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtPONO" runat="server" CssClass="form-control" placeholder="PO No." OnTextChanged="txtPONO_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvPONO" Style="color: red;" runat="server" ControlToValidate="txtPONO" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter PO No.">Please Enter PO No.</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Transporter : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtTranCode" runat="server" CssClass="form-control" placeholder="Transporter Code" OnTextChanged="txtTranCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="color: red;" runat="server" ControlToValidate="txtTranCode" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Transporter Code">Please Enter Transporter Code</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Transporter Name : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtTransporterName" runat="server" CssClass="form-control" placeholder="Transporter Name" Enabled="false" Width="505"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" runat="server" ControlToValidate="txtTransporterName" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Transporter Code">Please Enter Transporter Code</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Docket No. : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtDocketNo" runat="server" CssClass="form-control" placeholder="Docket No."></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Style="color: red;" runat="server" ControlToValidate="txtPONO" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter PO No.">*</asp:RequiredFieldValidator>--%>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">No. Of Boxes : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtNoOfBoxes" runat="server" CssClass="form-control" placeholder="No. of Boxes"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" Style="color: red;" runat="server" ControlToValidate="txtTranCode" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Transporter Code">*</asp:RequiredFieldValidator>--%>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Reamark : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" placeholder="Ramark" Width="505"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator5" Style="color: red;" runat="server" ControlToValidate="txtTranCode" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Transporter Code">*</asp:RequiredFieldValidator>--%>
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
                                                                    <h4 style="color: #f05423">Material Details</h4>
                                                                    <div class="col-md-12">

                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-5 control-label">Sr No. : </label>
                                                                                <div class="col-md-7">
                                                                                    <asp:TextBox ID="txtSrNo" runat="server" CssClass="form-control" placeholder="Sr No." Enabled="false"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-5 control-label">PO Sr. No. : </label>
                                                                                <div class="col-md-7">
                                                                                    <asp:TextBox ID="txtPOSrNo" runat="server" CssClass="form-control" placeholder="PO Sr. No." Enabled="true" OnTextChanged="txtPOSrNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="rfvPoSrNo" ControlToValidate="txtPOSrNo" runat="server" ValidationGroup="SaveItem" Style="color: red"
                                                                                        ErrorMessage="Please  Enter PO Sr. No.">Please  Enter PO Sr. No.</asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>

                                                                    <div class="col-md-12">

                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-5 control-label">Item Code : </label>
                                                                                <div class="col-md-7">
                                                                                    <asp:TextBox ID="txtItemCode" runat="server" CssClass="form-control" placeholder="Item Code" Enabled="false"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="rfvItemCode" ControlToValidate="txtItemCode" runat="server" ValidationGroup="SaveItem" Style="color: red"
                                                                                        ErrorMessage="Please  Enter Item Code">Please  Enter Item Code</asp:RequiredFieldValidator>
                                                                                </div>
                                                                                <asp:TextBox ID="txtItemId" runat="server" placeholder="Item Id" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                <asp:TextBox ID="txtSku" runat="server" placeholder="SKU" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                <asp:TextBox ID="txtItemGroup" runat="server" placeholder="Item Group" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                <asp:TextBox ID="txtItemGroupId" runat="server" placeholder="Item Group Id" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                <asp:TextBox ID="txtItemMake" runat="server" placeholder="MAKE" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                <asp:TextBox ID="txtItemModel" runat="server" placeholder="Model" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                <asp:TextBox ID="txtItemDispName" runat="server" placeholder="Item Name" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                <asp:TextBox ID="txtItemDispMRP" runat="server" placeholder="Item MRP" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                <asp:TextBox ID="txtItemValueLimit" runat="server" placeholder="Item Value Limit" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                <asp:TextBox ID="txtItemMaxStkQty" runat="server" placeholder="Item Max Stock Qty" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                <asp:TextBox ID="txtItemHSN" runat="server" placeholder="Item HSN" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                <asp:TextBox ID="txtItemHSNGroup" runat="server" placeholder="Item HSN Group Code" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                <asp:TextBox ID="txtItemHSNGroupDesc" runat="server" placeholder="Item HSN Group Code Desc." CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                <asp:TextBox ID="txtItemCondType" runat="server" placeholder="Item Condition Type" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                <asp:TextBox ID="txtItemStatus" runat="server" placeholder="Item Status" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                <asp:TextBox ID="txtGLCode" runat="server" placeholder="GL Code" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                <asp:TextBox ID="txtProfitCenter" runat="server" placeholder="Profit Center" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                <asp:TextBox ID="txtItemText" runat="server" placeholder="Item Text" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                <asp:TextBox ID="txtAssetCode" runat="server" placeholder="Item Text" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                <asp:TextBox ID="txtRate" runat="server" placeholder="Item Rate" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                <asp:TextBox ID="txtAmount" runat="server" placeholder="Item Amount" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-5 control-label">Item Desc. : </label>
                                                                                <div class="col-md-7">
                                                                                    <asp:TextBox ID="txtItemDesc" runat="server" CssClass="form-control" placeholder="Item Desc" Enabled="false"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="rfvItemDesc" ControlToValidate="txtItemDesc" runat="server" ValidationGroup="SaveItem" Style="color: red"
                                                                                        ErrorMessage="Please  Enter Item Desc.">Please  Enter Item Desc.</asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-5 control-label">Qty : </label>
                                                                                <div class="col-md-7">
                                                                                    <asp:TextBox ID="txtQty" runat="server" CssClass="form-control" placeholder="Item Qty" Enabled="true" OnTextChanged="txtQty_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="rfvQty" ControlToValidate="txtQty" runat="server" ValidationGroup="SaveItem" Style="color: red"
                                                                                        ErrorMessage="Please  Enter Item Qty.">Please  Enter Item Qty.</asp:RequiredFieldValidator>
                                                                                    <asp:HiddenField ID="hfPOQty" runat="server" />
                                                                                    <asp:HiddenField ID="hfPndQty" runat="server" />
                                                                                    <asp:HiddenField ID="hfChlQty" runat="server" />
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-5 control-label">UOM : </label>
                                                                                <div class="col-md-7">
                                                                                    <asp:DropDownList ID="ddlUOM" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlUOM_SelectedIndexChanged" AutoPostBack="true" TabIndex="9"></asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="rfvUOM" ControlToValidate="ddlUOM" runat="server" ValidationGroup="SaveItem" Style="color: red"
                                                                                        ErrorMessage="Please Select Item UOM" InitialValue="0">Please Select Item UOM</asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>


                                                                    <div class="col-md-12">
                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-5 control-label">From Plant : </label>
                                                                                <div class="col-md-7">
                                                                                    <asp:DropDownList ID="ddlFromPLant" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlFromPLant_SelectedIndexChanged" AutoPostBack="true" TabIndex="11"></asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="rfvPLant" ControlToValidate="ddlFromPLant" runat="server" ValidationGroup="SaveItem" Style="color: red"
                                                                                        ErrorMessage="Please Select From Plant" InitialValue="0">Please Select From Plant</asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-5 control-label">From Location : </label>
                                                                                <div class="col-md-7">
                                                                                    <asp:DropDownList ID="ddlFromLocation" runat="server" CssClass="form-control" TabIndex="12"></asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="rfvLocation" ControlToValidate="ddlFromLocation" runat="server" ValidationGroup="SaveItem" Style="color: red"
                                                                                        ErrorMessage="Please Select From Location" InitialValue="0">Please Select From Location</asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-5 control-label">To Plant : </label>
                                                                                <div class="col-md-7">
                                                                                    <asp:DropDownList ID="ddlToPlant" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlToPlant_SelectedIndexChanged" AutoPostBack="true" TabIndex="11"></asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="rfvToPlant" ControlToValidate="ddlToPlant" runat="server" ValidationGroup="SaveItem" Style="color: red"
                                                                                        ErrorMessage="Please Select To Plant" InitialValue="0">Please Select To Plant</asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-5 control-label">To Location : </label>
                                                                                <div class="col-md-7">
                                                                                    <asp:DropDownList ID="ddlToLocation" runat="server" CssClass="form-control" TabIndex="12" OnSelectedIndexChanged="ddlToLocation_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="rfvToLocation" ControlToValidate="ddlToLocation" runat="server" ValidationGroup="SaveItem" Style="color: red"
                                                                                        ErrorMessage="Please Select To Location" InitialValue="0">Please Select To Location</asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>

                                                                    <div class="col-md-12">
                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-5 control-label">Cost Center : </label>
                                                                                <div class="col-md-7">
                                                                                    <%--<asp:TextBox ID="txtCostCenter" runat="server" CssClass="form-control" placeholder="Cost Center" Enabled="true"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="rfvCostCenter" ControlToValidate="txtCostCenter" runat="server" ValidationGroup="SaveItem"
                                                                                        ErrorMessage="Please  Enter Cost Center">*</asp:RequiredFieldValidator>--%>

                                                                                    <asp:DropDownList ID="ddlCostCenter" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="rfvCostCenter" ControlToValidate="ddlCostCenter" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                                                        ErrorMessage="Please Select Cost Center" InitialValue="0">Please Select Cost Center</asp:RequiredFieldValidator>

                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-5 control-label">Item Reamrk : </label>
                                                                                <div class="col-md-7">
                                                                                    <asp:TextBox ID="txtItemReamrk" runat="server" CssClass="form-control" placeholder="Item Remark" Enabled="true"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>

                                                                </div>
                                                                <div class="panel-footer">
                                                                    <%--<button type="button" class="btn btn-success pull-right"><i class="fa fa-plus-square"></i></button>--%>
                                                                    <asp:LinkButton ID="btnSaveDet" runat="server" CssClass="btn btn-success pull-right" OnClick="btnSaveDet_Click" Text="Save Item" ValidationGroup="SaveItem" TabIndex="16" Visible="false"><i class="fa fa-plus-square"></i></asp:LinkButton>

                                                                </div>

                                                                <%-- <div class="row" style="margin-top: 10px!important;">
                                                                    <div class="col-md-12">
                                                                        <div class="panel panel-default">
                                                                            <div class="panel-body">
                                                                                <div class="row">
                                                                                    <div class="col-md-12">
                                                                                        <div class="box">
                                                                                            <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>--%>
                                                            </div>

                                                            <%--<div class="panel-footer">
                                                            </div>--%>
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
                                        <div class="box">
                                            <div class="box-body divhorizontal" style="overflow-x: scroll;">

                                                <asp:GridView ID="grvListItem" runat="server" AutoGenerateColumns="false" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%"
                                                    OnRowCommand="grvListItem_RowCommand">
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="lblGVEmpty" runat="server" Text="No Data Found"></asp:Label>
                                                    </EmptyDataTemplate>
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="PO No.">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVPONO" Text='<%# Bind("PONO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="PO Sr. No.">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVPoSrNo" Text='<%# Bind("POSRNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVSrNo" Text='<%# Bind("SRNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVItemCode" Text='<%# Bind("ITEMCODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Desc.">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVItemDesc" Text='<%# Bind("ITEMDESC") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVItemID" Text='<%# Bind("ITEMID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Group">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVItemGroup" Text='<%# Bind("ITEMGROUP") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Group Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblItemGroupID" Text='<%# Bind("ITEMGROUPID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Unit">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVUOM" Text='<%# Bind("ITEMUOM") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Unit ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVUOMID" Text='<%# Bind("UOMID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="PO Qty">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVPOQty" Text='<%# Bind("POQTY") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Challan Qty">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVChlnQty" Text='<%# Bind("CHLNQTY") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Rate" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVItemRate" Text='<%# Bind("ITEMRATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Amount" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVItemAmount" Text='<%# Bind("ITEMAMOUNT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="GL CODE">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVGLCODE" Text='<%# Bind("GLCODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Cost Center">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVCostCenter" Text='<%# Bind("COSTCENTER") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="From Plant">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVFromPlantCD" Text='<%# Bind("ITEMFROMPLANTCD") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="From Plant ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVFromPlantID" Text='<%# Bind("ITEMFROMPLANTID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="From Location">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVFromLocationCD" Text='<%# Bind("ITEMFROMLOCCD") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="From Location ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVFromLocationID" Text='<%# Bind("FROMLOCCDID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="To Plant">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVToPlantCD" Text='<%# Bind("ITEMTOPLANTCD") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="From Plant ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVToPlantID" Text='<%# Bind("ITEMTOPLANTID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="To Location">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVToLocationCD" Text='<%# Bind("ITEMTOLOCCD") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="To Location ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVToLocationID" Text='<%# Bind("TOLOCCDID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Profit Center">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVProfitCenter" Text='<%# Bind("PROFITCENTER") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Asset Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVAssetcode" Text='<%# Bind("ASSETCODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Text">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVItemText" Text='<%# Bind("ITEMTEXT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("SRNO") %>'
                                                                    CommandName="eDelete" OnClientClick="return confirm('Are you sure you want to delete this record?');">Delete</asp:LinkButton>
                                                                |
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("SRNO") %>'
                                                        CommandName="eEdit">Edit</asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblSKU" Text='<%# Bind("SKU") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblMake" Text='<%# Bind("MAKE") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblModel" Text='<%# Bind("MODEL") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblDispName" Text='<%# Bind("DISPNAME") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblDispMRP" Text='<%# Bind("DISPMRP") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblValueLimit" Text='<%# Bind("VALUELIMIT") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblMaxStkQty" Text='<%# Bind("MAXSTKQTY") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblHSN" Text='<%# Bind("HSN") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblHSNGroup" Text='<%# Bind("HSNGROUP") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblHSNGroupDesc" Text='<%# Bind("HSNGROUPDESC") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblCondType" Text='<%# Bind("CONDTYPE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblItemStatus" Text='<%# Bind("ITEMSTATUS") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblHFPOQty" Text='<%# Bind("HFPOQTY") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblHFPndQty" Text='<%# Bind("HFPNDQTY") %>'></asp:Label>
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

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <script type="text/javascript" src="../js/MaterialInward.js"></script>
    <input type="hidden" id="menutabid" value="tsmTranMMSTODc" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />
</asp:Content>
