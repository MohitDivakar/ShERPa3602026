<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="CreateMaterialSplit.aspx.cs" Inherits="ShERPa360net.MM.CreateMaterialSplit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }
    </style>

    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            var code = $("#ContentPlaceHolder1_txtFromItemCode").val();
            var desc = $("#ContentPlaceHolder1_txtFromItemDesc").val();
            var stk = $("#ContentPlaceHolder1_txtCurrStock").val();
            if (code != "" && desc != "" && stk != "") {
                document.getElementById("busy-holder1").style.display = "";
                document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="pnlprimaryDetails" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtFromItemCode" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtToItemCode" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtQty" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtDist" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlUOM" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlPlant" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlLocation" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlToPlant" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlpopMake" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnSaveDet" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="imgSaveAll" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="grvListItem" EventName="RowCommand" />
        </Triggers>
        <ContentTemplate>

            <div class="page-content-wrap">

                <div class="row">
                    <div class="col-md-12">

                        <div class="form-horizontal">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Split  </strong>Material</h3>
                                    <asp:LinkButton ID="imgCancel" runat="server" CssClass="btn btn-success pull-right" PostBackUrl="~/MM/ViewMaterialIssue.aspx?REQUESTFORM=IS" Text="Cancel"><i class="fa fa-times"></i></asp:LinkButton>
                                    <asp:LinkButton ID="imgSaveAll" runat="server" OnClientClick="ShowLoading()" CssClass="btn btn-success pull-right" OnClick="imgSaveAll_Click" Text="Save All" ValidationGroup="SaveAll"><i class="fa fa-save"></i></asp:LinkButton>
                                    <div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                        <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                        <label>Please wait...</label>
                                    </div>
                                </div>




                                <div class="panel-body">

                                    <div class="row">

                                        <div class="col-md-12">
                                            <h4 style="color: #f05423">Doc. Details</h4>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Doc. Type : </label>
                                                    <div class="col-md-8 col-xs-12">
                                                        <asp:DropDownList ID="ddlDoctype" runat="server" CssClass="form-control " TabIndex="1"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvDocType" runat="server" ControlToValidate="ddlDoctype" ValidationGroup="SaveAll" Style="color: red;"
                                                            ErrorMessage="Please Select Doc Type" InitialValue="0">Please Select Doc Type</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Doc No. : </label>
                                                    <div class="col-md-8 col-xs-12">
                                                        <asp:TextBox ID="txtDocNo" runat="server" CssClass="form-control" placeholder="Doc No." TabIndex="2" Enabled="false"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvDocNo" runat="server" ControlToValidate="txtDocNo" ValidationGroup="SaveAll" Style="color: red;"
                                                            ErrorMessage="Please Enter Doc No">Please Enter Doc No</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Doc Date : </label>
                                                    <div class="col-md-8 col-xs-12">
                                                        <div class="input-group">
                                                            <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                            <asp:TextBox ID="txtDocDate" runat="server" placeholder="Doc Date" class="form-control datepicker" AutoCompleteType="None" TabIndex="3" ReadOnly="true"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <asp:RequiredFieldValidator ID="rfvDocDate" runat="server" ControlToValidate="txtDocDate" ValidationGroup="SaveAll" Style="color: red;"
                                                        ErrorMessage="Please Enter Doc Date">Please Enter Doc Date</asp:RequiredFieldValidator>
                                                </div>

                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Ref. No. : </label>
                                                    <div class="col-md-8 col-xs-12">
                                                        <asp:TextBox ID="txtRefno" runat="server" placeholder="Ref. No." class="form-control" TabIndex="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>

                                        <div class="col-md-12">

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Plant : </label>
                                                    <div class="col-md-8 col-xs-12">
                                                        <asp:DropDownList ID="ddlPlant" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged" TabIndex="1"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvPlant" runat="server" ControlToValidate="ddlPlant" ValidationGroup="SaveAll" Style="color: red;"
                                                            ErrorMessage="Please Select Plant" InitialValue="0">Please Select Plant</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Location : </label>
                                                    <div class="col-md-8 col-xs-12">
                                                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control " TabIndex="1" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvLocation" runat="server" ControlToValidate="ddlLocation" Style="color: red;" ValidationGroup="SaveAll"
                                                            ErrorMessage="Please Select Location" InitialValue="0">Please Select Location</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="col-md-12">

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">From Item : </label>
                                                    <div class="col-md-8 col-xs-12">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtFromItemCode" runat="server" placeholder="Item Code" AutoPostBack="true" OnTextChanged="txtFromItemCode_TextChanged" CssClass="form-control required_text_box" TabIndex="7"></asp:TextBox>
                                                            <span class="input-group-btn">
                                                                <asp:LinkButton ID="lnkOpenPoup" runat="server" CssClass="btn btn-success" OnClick="lnkOpenPoup_Click" Text="Open Popup"><span class="fa fa-search"></span></asp:LinkButton>
                                                            </span>
                                                        </div>
                                                        <asp:RequiredFieldValidator ID="rfvFromItem" runat="server" ControlToValidate="txtFromItemCode" ValidationGroup="SaveAll" Style="color: red;"
                                                            ErrorMessage="Please Enter From Item">Please Enter From Item</asp:RequiredFieldValidator>

                                                        <asp:TextBox ID="txtFromItemId" runat="server" placeholder="Item Id" CssClass="form-control" Visible="false"></asp:TextBox>

                                                        <asp:TextBox ID="txtFromSku" runat="server" placeholder="SKU" CssClass="form-control" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="txtFromItemGroup" runat="server" placeholder="Item Group" CssClass="form-control" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="txtFromItemGroupId" runat="server" placeholder="Item Group Id" CssClass="form-control" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="txtFromItemMake" runat="server" placeholder="MAKE" CssClass="form-control" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="txtFromItemModel" runat="server" placeholder="Model" CssClass="form-control" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="txtFromItemDispName" runat="server" placeholder="Item Name" CssClass="form-control" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="txtFromItemDispMRP" runat="server" placeholder="Item MRP" CssClass="form-control" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="txtFromItemValueLimit" runat="server" placeholder="Item Value Limit" CssClass="form-control" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="txtFromItemMaxStkQty" runat="server" placeholder="Item Max Stock Qty" CssClass="form-control" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="txtFromItemHSN" runat="server" placeholder="Item HSN" CssClass="form-control" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="txtFromItemHSNGroup" runat="server" placeholder="Item HSN Group Code" CssClass="form-control" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="txtFromItemHSNGroupDesc" runat="server" placeholder="Item HSN Group Code Desc." CssClass="form-control" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="txtFromItemCondType" runat="server" placeholder="Item Condition Type" CssClass="form-control" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="txtFromItemStatus" runat="server" placeholder="Item Status" CssClass="form-control" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="txtFromGLCode" runat="server" placeholder="GL Code" CssClass="form-control" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="txtFromProfitCenter" runat="server" placeholder="Profit Center" CssClass="form-control" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="txtFromItemText" runat="server" placeholder="Item Text" CssClass="form-control" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="txtFromAssetCode" runat="server" placeholder="Item Text" CssClass="form-control" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="txtFromOnHandStock" runat="server" placeholder="Item Text" CssClass="form-control" Visible="false"></asp:TextBox>
                                                        <asp:TextBox ID="txtMRate" runat="server" placeholder="Item Rate" CssClass="form-control" Visible="false"></asp:TextBox>

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="col-md-2 control-label">Item Desc. : </label>
                                                    <div class="col-md-10 col-xs-12">
                                                        <asp:TextBox ID="txtFromItemDesc" runat="server" placeholder="Item Description" class="form-control required_text_box" ReadOnly="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvItemDesc" runat="server" ControlToValidate="txtFromItemDesc" ValidationGroup="SaveAll" Style="color: red;"
                                                            ErrorMessage="Please Enter Item Desc.">Please Enter Item Desc.</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Curr. Stock : </label>
                                                    <div class="col-md-8 col-xs-12">
                                                        <asp:TextBox ID="txtCurrStock" runat="server" placeholder="Curr. Stock" class="form-control required_text_box" ReadOnly="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvCurrStock" runat="server" ControlToValidate="txtCurrStock" ValidationGroup="SaveAll" Style="color: red;"
                                                            ErrorMessage="Please Enter Curr. Stock">Please Enter Curr. Stock</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>

                                        <div class="col-md-12">

                                            <div class="col-md-3">
                                                <div class="form-group">

                                                    <label class="col-md-4 control-label">Remark : </label>
                                                    <div class="col-md-8 col-xs-12">
                                                        <asp:TextBox ID="txtREMARKS" runat="server" placeholder="Remarks" class="form-control" TabIndex="5"></asp:TextBox>
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
                                                                <h4 style="color: #f05423">IS Details</h4>

                                                                <div class="col-md-12">

                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label class="col-md-4 control-label">Sr No. : </label>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtSrNo" runat="server" CssClass="form-control" placeholder="Sr No." Enabled="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>


                                                                </div>

                                                                <div class="col-md-12" style="padding-top: 15px;">

                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label class="col-md-4 control-label">To Item : </label>
                                                                            <div class="col-md-8 col-xs-12">
                                                                                <div class="input-group">
                                                                                    <asp:TextBox ID="txtToItemCode" runat="server" placeholder="Item Code" AutoPostBack="true" OnTextChanged="txtToItemCode_TextChanged" CssClass="form-control required_text_box" TabIndex="7" Enabled="false"></asp:TextBox>
                                                                                    <span class="input-group-btn">
                                                                                        <asp:LinkButton ID="lnkToOpenPoup" runat="server" CssClass="btn btn-success" OnClick="lnkToOpenPoup_Click" Text="Open Popup" Enabled="false"><span class="fa fa-search"></span></asp:LinkButton>
                                                                                    </span>
                                                                                </div>
                                                                                <asp:RequiredFieldValidator ID="rfvToItem" runat="server" ControlToValidate="txtToItemCode" ValidationGroup="SaveItem" Style="color: red;"
                                                                                    ErrorMessage="Please Enter To Item">Please Enter To Item</asp:RequiredFieldValidator>

                                                                                <asp:TextBox ID="txtToItemId" runat="server" placeholder="Item Id" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtToSku" runat="server" placeholder="SKU" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtToItemGroup" runat="server" placeholder="Item Group" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtToItemGroupId" runat="server" placeholder="Item Group Id" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtToItemMake" runat="server" placeholder="MAKE" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtToItemModel" runat="server" placeholder="Model" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtToItemDispName" runat="server" placeholder="Item Name" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtToItemDispMRP" runat="server" placeholder="Item MRP" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtToItemValueLimit" runat="server" placeholder="Item Value Limit" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtToItemMaxStkQty" runat="server" placeholder="Item Max Stock Qty" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtToItemHSN" runat="server" placeholder="Item HSN" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtToItemHSNGroup" runat="server" placeholder="Item HSN Group Code" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtToItemHSNGroupDesc" runat="server" placeholder="Item HSN Group Code Desc." CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtToItemCondType" runat="server" placeholder="Item Condition Type" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtToItemStatus" runat="server" placeholder="Item Status" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtToGLCode" runat="server" placeholder="GL Code" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtToProfitCenter" runat="server" placeholder="Profit Center" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtToItemText" runat="server" placeholder="Item Text" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtToAssetCode" runat="server" placeholder="Item Text" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtToRate" runat="server" placeholder="Item Rate" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtToAmount" runat="server" placeholder="Item Amount" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="txtToOnHandStock" runat="server" placeholder="On Hand Stock" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-6">
                                                                        <div class="form-group">
                                                                            <label class="col-md-2 control-label">Item Desc. : </label>
                                                                            <div class="col-md-10 col-xs-12">
                                                                                <asp:TextBox ID="txtToItemDesc" runat="server" placeholder="Item Description" class="form-control required_text_box" ReadOnly="true"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="rfvToItemDesc" runat="server" ControlToValidate="txtToItemDesc" ValidationGroup="SaveItem" Style="color: red;"
                                                                                    ErrorMessage="Please Enter Item Desc.">Please Enter Item Desc.</asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>



                                                                </div>

                                                                <div class="col-md-12">

                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label class="col-md-4 control-label">Qty : </label>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtQty" runat="server" CssClass="form-control" placeholder="Item Qty" Enabled="true" OnTextChanged="txtQty_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="rfvQty" ControlToValidate="txtQty" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                                                    ErrorMessage="Please  Enter Item Qty.">Please  Enter Item Qty.</asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label class="col-md-4 control-label">UOM : </label>
                                                                            <div class="col-md-8">
                                                                                <asp:DropDownList ID="ddlUOM" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlUOM_SelectedIndexChanged" AutoPostBack="true" TabIndex="9"></asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvUOM" ControlToValidate="ddlUOM" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                                                    ErrorMessage="Please Select Item UOM" InitialValue="0">Please Select Item UOM</asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label class="col-md-4 control-label">Dist. % : </label>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtDist" runat="server" CssClass="form-control" placeholder="Dist. %" Enabled="true" OnTextChanged="txtDist_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="rfvDist" ControlToValidate="txtDist" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                                                    ErrorMessage="Please  Enter Dist. %">Please  Enter Dist. %</asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label class="col-md-4 control-label">Pending % : </label>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtPending" runat="server" CssClass="form-control" placeholder="Pending %" Enabled="false" AutoPostBack="true"></asp:TextBox>
                                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPending" runat="server" ValidationGroup="SaveItem"
                                                                            ErrorMessage="Please  Enter Pending. %">*</asp:RequiredFieldValidator>--%>
                                                                            </div>
                                                                        </div>
                                                                    </div>


                                                                </div>

                                                                <div class="col-md-12">

                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label class="col-md-4 control-label">To Plant : </label>
                                                                            <div class="col-md-8">
                                                                                <asp:DropDownList ID="ddlToPlant" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlToPlant_SelectedIndexChanged" AutoPostBack="true" TabIndex="11" Enabled="false"></asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvToPlant" ControlToValidate="ddlToPlant" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                                                    ErrorMessage="Please Select To Plant" InitialValue="0">Please Select To Plant</asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label class="col-md-4 control-label">To Location : </label>
                                                                            <div class="col-md-8">
                                                                                <asp:DropDownList ID="ddlToLocation" runat="server" CssClass="form-control" TabIndex="12" Enabled="false"></asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvToLocation" ControlToValidate="ddlToLocation" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                                                    ErrorMessage="Please Select To Location" InitialValue="0">Please Select To Location</asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label class="col-md-4 control-label">Cost Center : </label>
                                                                            <div class="col-md-8">
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
                                                                            <label class="col-md-4 control-label">Item Reamrk : </label>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtItemReamrk" runat="server" CssClass="form-control" placeholder="Item Remark" Enabled="true"></asp:TextBox>
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







                                    <div class="panel-footer">
                                        <%--<button type="button" class="btn btn-success pull-right"><i class="fa fa-plus-square"></i></button>--%>
                                        <asp:LinkButton ID="btnSaveDet" runat="server" CssClass="btn btn-success pull-right" OnClick="btnSaveDet_Click" Text="Save Item" ValidationGroup="SaveItem" TabIndex="16"><i class="fa fa-plus-square"></i></asp:LinkButton>

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
                                                                <div class="box-body divhorizontal" style="overflow-x: scroll; overflow-y: scroll; max-height: 500px;">



                                                                    <asp:GridView ID="grvListItem" runat="server" AutoGenerateColumns="false" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%"
                                                                        OnRowCommand="grvListItem_RowCommand">
                                                                        <Columns>

                                                                            <asp:TemplateField HeaderText="Action">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("SRNO") %>'
                                                                                        CommandName="eDelete" OnClientClick="return confirm('Are you sure you want to delete this record?');">Delete</asp:LinkButton>
                                                                                    |
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("SRNO") %>'
                                                        CommandName="eEdit">Split</asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>


                                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblID" Text='<%# Bind("SRNO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="From Item Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblItemCode" Text='<%# Bind("ITEMCODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="From Item Desc.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblItemDesc" Text='<%# Bind("ITEMDESC") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="From Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblItemId" Text='<%# Bind("ITEMID") %>'></asp:Label>


                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="To Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblToItemId" Text='<%# Bind("TOITEMID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Item Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblToItemCode" Text='<%# Bind("TOITEMCODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Item Desc.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblToItemDesc" Text='<%# Bind("TOITEMDESC") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>



                                                                            <asp:TemplateField HeaderText="Item Group">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblItemGroup" Text='<%# Bind("ITEMGROUP") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Item Group Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblGroupId" Text='<%# Bind("ITEMGROUPID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="UOM">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblUOM" Text='<%# Bind("ITEMUOM") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <%--<EditItemTemplate>
                                                    <asp:DropDownList ID="ddlEditUOM" runat="server"></asp:DropDownList>
                                                </EditItemTemplate>--%>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="UOM ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblUOMID" Text='<%# Bind("UOMID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblQty" Text='<%# Bind("ITEMQTY") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <%--<EditItemTemplate>
                                                </EditItemTemplate>--%>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="CRDR">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblCRDR" Text='<%# Bind("CRDR") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <%--<EditItemTemplate>
                                                </EditItemTemplate>--%>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Rate">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblRate" Text='<%# Bind("ITEMRATE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Amount">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblAmount" Text='<%# Bind("ITEMAMOUNT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <%--<asp:TemplateField HeaderText="Deli. Date">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDeliDate" Text='<%# Bind("DELIVERYDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                                            <asp:TemplateField HeaderText="GL Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblGLCode" Text='<%# Bind("GLCODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Cost Center">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblCostCenter" Text='<%# Bind("COSTCENTER") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Plant Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblPlantCD" Text='<%# Bind("ITEMPLANTCD") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <%--<EditItemTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlEditPlantCode" OnSelectedIndexChanged="ddlEditPlantCode_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </EditItemTemplate>--%>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Plant Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblPlantID" Text='<%# Bind("ITEMPLANTID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Location Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblLocationCD" Text='<%# Bind("ITEMLOCCD") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <%--<EditItemTemplate>
                                                    <asp:DropDownList ID="ddlEditLocation" runat="server"></asp:DropDownList>
                                                </EditItemTemplate>--%>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Location Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblLocationCDID" Text='<%# Bind("LOCCDID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Profit Center">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblProfitCenter" Text='<%# Bind("PROFITCENTER") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Asset Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblAssetCode" Text='<%# Bind("ASSETCODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>





                                                                            <asp:TemplateField HeaderText="Item Text">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblItemText" Text='<%# Bind("ITEMTEXT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>



                                                                            <asp:TemplateField HeaderText="Remarks">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblRemarks" Text='<%# Bind("ITEMREMARKS") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <%--<EditItemTemplate>
                                                    <asp:TextBox ID="txtEditRemark" runat="server" Text='<%# Bind("ITEMREMARKS") %>'></asp:TextBox>
                                                </EditItemTemplate>--%>
                                                                            </asp:TemplateField>


                                                                            <%--27--%>
                                                                            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblSKU" Text='<%# Bind("SKU") %>'></asp:Label>

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--28--%>
                                                                            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblMake" Text='<%# Bind("MAKE") %>'></asp:Label>

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--29--%>
                                                                            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblModel" Text='<%# Bind("MODEL") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--30--%>
                                                                            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblDispName" Text='<%# Bind("DISPNAME") %>'></asp:Label>

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--31--%>
                                                                            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblDispMRP" Text='<%# Bind("DISPMRP") %>'></asp:Label>

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--32--%>
                                                                            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblValueLimit" Text='<%# Bind("VALUELIMIT") %>'></asp:Label>

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--33--%>
                                                                            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblMaxStkQty" Text='<%# Bind("MAXSTKQTY") %>'></asp:Label>

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--34--%>
                                                                            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblHSN" Text='<%# Bind("HSN") %>'></asp:Label>

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--35--%>
                                                                            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblHSNGroup" Text='<%# Bind("HSNGROUP") %>'></asp:Label>

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--36--%>
                                                                            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblHSNGroupDesc" Text='<%# Bind("HSNGROUPDESC") %>'></asp:Label>

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--37--%>
                                                                            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblCondType" Text='<%# Bind("CONDTYPE") %>'></asp:Label>

                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--38--%>
                                                                            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblItemStatus" Text='<%# Bind("ITEMSTATUS") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <%--39--%>
                                                                            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblOnHandStock" Text='<%# Bind("ONHANDSTOCK") %>'></asp:Label>
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




                            </div>

                        </div>
                    </div>

                </div>




                <div class="modal fade" id="modal-item" data-backdrop="static">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" style="color: #337ab7">Item List</h4>
                            </div>



                            <div class="modal-body">
                                <div class="box-body">
                                    <div class="row">




                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Item Code :</label>
                                                    <asp:TextBox ID="txtpopItemCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Item Desc. :</label>
                                                    <asp:TextBox ID="txtPopupItemDesc" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Category :</label>
                                                    <asp:DropDownList ID="ddlpopCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Group :</label>
                                                    <asp:DropDownList ID="ddlpopGroup" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Sub Group :</label>
                                                    <asp:DropDownList ID="ddlpopSubGroup" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Make :</label>
                                                    <asp:DropDownList ID="ddlpopMake" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlpopMake_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Model :</label>
                                                    <asp:DropDownList ID="ddlpopModel" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="col-md-12" style="margin-top: 5px;">
                                            <%--<asp:ImageButton ID="imgFind" CssClass="btnClass" runat="server" OnClick="imgFind_Click" ImageUrl="~/img/icons/find.png" AlternateText="Show Data" ToolTip="Show PR Data" />--%>
                                            <asp:LinkButton ID="btnShowItem" runat="server" CssClass="btn btn-success pull-left" OnClick="btnShowItem_Click" Text="Show Item"><span class="fa fa-search"></span></asp:LinkButton>
                                        </div>





                                        <div class="col-md-12" style="margin-top: 5px;">
                                            <div class="box">
                                                <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                    <asp:GridView ID="grvPopItem" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                                        CssClass="table table-hover table-striped table-bordered nowrap" AutoGenerateSelectButton="true" OnSelectedIndexChanged="grvPopItem_SelectedIndexChanged">
                                                        <EmptyDataTemplate>
                                                            <div style="text-align: center; color: red; font-size: 18px;">
                                                                No Record Not Found !
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                            <asp:BoundField DataField="Desciption" HeaderText="Item Desc." />
                                                            <asp:BoundField DataField="MAKE" HeaderText="Make" />
                                                            <asp:BoundField DataField="MODEL" HeaderText="Model" />
                                                            <asp:BoundField DataField="DisplayName" HeaderText="Display Name" />
                                                            <asp:BoundField DataField="ItemCategory" HeaderText="Item Category" />
                                                            <asp:BoundField DataField="ItemGroup" HeaderText="Item Group" />
                                                            <asp:BoundField DataField="ItemSubGroup" HeaderText="Item Sub Group" />
                                                            <asp:BoundField DataField="HSNGroup" HeaderText="HSN Group" />
                                                            <asp:BoundField DataField="ItemStatus" HeaderText="Item Status" />
                                                            <asp:BoundField DataField="CREATEBY" HeaderText="Create By" />
                                                            <asp:BoundField DataField="CREATEDATE" HeaderText="Create Date" DataFormatString="{0:d}" />
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





                <div class="modal fade" id="modal-Toitem" data-backdrop="static">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" style="color: #337ab7">Item List</h4>
                            </div>



                            <div class="modal-body">
                                <div class="box-body">
                                    <div class="row">




                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Item Code :</label>
                                                    <asp:TextBox ID="txtpopToItemCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Item Desc. :</label>
                                                    <asp:TextBox ID="txtPopupToItemDesc" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Category :</label>
                                                    <asp:DropDownList ID="ddlpopToCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Group :</label>
                                                    <asp:DropDownList ID="ddlpopToGroup" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Sub Group :</label>
                                                    <asp:DropDownList ID="ddlpopToSubGroup" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>


                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Make :</label>
                                                    <asp:DropDownList ID="ddlpopToMake" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlpopToMake_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label>Model :</label>
                                                    <asp:DropDownList ID="ddlpopToModel" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="col-md-12" style="margin-top: 5px;">
                                            <%--<asp:ImageButton ID="imgFind" CssClass="btnClass" runat="server" OnClick="imgFind_Click" ImageUrl="~/img/icons/find.png" AlternateText="Show Data" ToolTip="Show PR Data" />--%>
                                            <asp:LinkButton ID="btnToShowItem" runat="server" CssClass="btn btn-success pull-left" OnClick="btnToShowItem_Click" Text="Show Item"><span class="fa fa-search"></span></asp:LinkButton>
                                        </div>





                                        <div class="col-md-12" style="margin-top: 5px;">
                                            <div class="box">
                                                <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                    <asp:GridView ID="grvToPopItem" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                                        CssClass="table table-hover table-striped table-bordered nowrap" AutoGenerateSelectButton="true" OnSelectedIndexChanged="grvToPopItem_SelectedIndexChanged">
                                                        <EmptyDataTemplate>
                                                            <div style="text-align: center; color: red; font-size: 18px;">
                                                                No Record Not Found !
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                            <asp:BoundField DataField="Desciption" HeaderText="Item Desc." />
                                                            <asp:BoundField DataField="MAKE" HeaderText="Make" />
                                                            <asp:BoundField DataField="MODEL" HeaderText="Model" />
                                                            <asp:BoundField DataField="DisplayName" HeaderText="Display Name" />
                                                            <asp:BoundField DataField="ItemCategory" HeaderText="Item Category" />
                                                            <asp:BoundField DataField="ItemGroup" HeaderText="Item Group" />
                                                            <asp:BoundField DataField="ItemSubGroup" HeaderText="Item Sub Group" />
                                                            <asp:BoundField DataField="HSNGroup" HeaderText="HSN Group" />
                                                            <asp:BoundField DataField="ItemStatus" HeaderText="Item Status" />
                                                            <asp:BoundField DataField="CREATEBY" HeaderText="Create By" />
                                                            <asp:BoundField DataField="CREATEDATE" HeaderText="Create Date" DataFormatString="{0:d}" />
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


            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranMMMatSpl" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />

</asp:Content>
