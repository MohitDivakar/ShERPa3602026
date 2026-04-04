<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="CreateMaterialReturn.aspx.cs" Inherits="ShERPa360net.MM.CreateMaterialReturn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            var vend = $("#ContentPlaceHolder1_txtVendor").val();
            if (vend != "") {
                document.getElementById("busy-holder1").style.display = "";
                document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";
            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Material Return</strong></h3>
                            <asp:LinkButton ID="imgCancel" runat="server" CssClass="btn btn-success pull-right" PostBackUrl="~/MM/ViewMaterialIssue.aspx?REQUESTFORM=MR" Text="Cancel"><i class="fa fa-times"></i></asp:LinkButton>
                            <asp:LinkButton ID="imgSaveAll" runat="server" OnClientClick="ShowLoading()" CssClass="btn btn-success pull-right" OnClick="imgSaveAll_Click" Text="Save All" ValidationGroup="SaveAll"><i class="fa fa-save"></i></asp:LinkButton>
                            <div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                <label>Please wait...</label>
                            </div>
                        </div>

                        <asp:UpdatePanel ID="pnlprimaryDetails" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtVendor" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="txtPoNo" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="txtFinalYear" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="txtMMDocNo" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="txtMMSrNo" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="txtItemCode" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlUOM" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlPLant" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="btnSaveDet" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="grvListItem" EventName="RowCommand" />
                                <asp:AsyncPostBackTrigger ControlID="grvListItem" EventName="RowDataBound" />
                            </Triggers>
                            <ContentTemplate>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h4 style="color: #f05423">Details</h4>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Doc. Type : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList ID="ddlDoctype" runat="server" CssClass="form-control "></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Doc No. : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtDocNo" runat="server" Enabled="false" CssClass="form-control" placeholder="Doc No."></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Doc Date : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <div class="input-group">
                                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                    <asp:TextBox ID="txtDocDt" runat="server" placeholder="DD/MM/YYYY" Width="90" class="form-control datepicker" AutoCompleteType="None"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <asp:RequiredFieldValidator ID="rfvDocDt" runat="server" Style="color: red!important;" ControlToValidate="txtDocDt" ValidationGroup="SaveAll"
                                                                ErrorMessage="Please Enter Doc Date">Please Enter Doc Date</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Vendor : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtVendor" runat="server" CssClass="form-control" placeholder="Vendor Code" TextMode="Number" MaxLength="10" AutoPostBack="true" Width="180" OnTextChanged="txtVendor_TextChanged"></asp:TextBox>
                                                                <asp:Label ID="lblVendorError" Style="color: red;" runat="server" Visible="false" ForeColor="Red">Please Enter the valid</asp:Label>
                                                                <asp:RequiredFieldValidator ID="rfvVendorCode" Width="150" Style="color: red;" runat="server" ControlToValidate="txtVendor" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Vendor Code">Please Enter Vendor Code</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Vendor Name : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtVendorName" runat="server" CssClass="form-control" placeholder="Vendor Name" ReadOnly="true"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-12" style="margin-top: -10px!important;">
                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Remark : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" placeholder="Remark" Width="332"></asp:TextBox>
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
                                                    <li class="active"><a href="#tab-first" role="tab" data-toggle="tab">Material Details</a></li>
                                                </ul>
                                                <div class="panel-body tab-content">
                                                    <div class="tab-pane active" id="tab-first">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label class="col-md-5 control-label">Sr. No. : </label>
                                                                            <div class="col-md-7">
                                                                                <asp:TextBox ID="txtSRNo" Width="130" runat="server" CssClass="form-control" Enabled="false" placeholder="Sr. No."></asp:TextBox>
                                                                                <asp:TextBox ID="txtMaxSrNo" runat="server" placeholder="Max Sr. No." CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="lblPRSRNO" runat="server" CssClass="hidden"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label class="col-md-5 control-label">PO No. : </label>
                                                                            <div class="col-md-7">
                                                                                <asp:TextBox ID="txtPoNo" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtPoNo_TextChanged" placeholder="Po No."></asp:TextBox>
                                                                                <asp:Label ID="lblPoNoError" runat="server" Visible="false" ForeColor="Red">Please Enter the valid Po No</asp:Label>
                                                                                <asp:RequiredFieldValidator ID="rfvPoNo" Style="color: red;" runat="server" ControlToValidate="txtPoNo" ValidationGroup="SaveItem"
                                                                                    ErrorMessage="Please valid PO No">Please valid PO No</asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label class="col-md-5 control-label">PO Sr No. : </label>
                                                                            <div class="col-md-7">
                                                                                <asp:TextBox ID="txtPoSrNo" Enabled="false" runat="server" CssClass="form-control" placeholder="Po Sr No."></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label class="col-md-5 control-label">Doc No. : </label>
                                                                            <div class="col-md-7">
                                                                                <asp:TextBox ID="txtMMDocNo" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtMMDocNo_TextChanged" placeholder="Doc No."></asp:TextBox>
                                                                                <asp:Label ID="lblMDocNoError" runat="server" Visible="false" ForeColor="Red">Please Enter the valid Doc No</asp:Label>
                                                                                <asp:RequiredFieldValidator ID="rfvDocNo" Style="color: red;" Width="120" runat="server" ControlToValidate="txtMMDocNo" ValidationGroup="SaveItem"
                                                                                    ErrorMessage="Please valid Doc No">Please valid Doc No</asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label class="col-md-5 control-label">Final Year. : </label>
                                                                            <div class="col-md-7">
                                                                                <asp:TextBox ID="txtFinalYear" runat="server" AutoPostBack="true" OnTextChanged="txtFinalYear_TextChanged" CssClass="form-control" placeholder="Final Year."></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="rfvFinalYear" Style="color: red;" runat="server" ControlToValidate="txtFinalYear" ValidationGroup="SaveItem"
                                                                                    ErrorMessage="Please Final Year">Please Final Year</asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label class="col-md-5 control-label">MM Sr No. : </label>
                                                                            <div class="col-md-7">
                                                                                <asp:TextBox ID="txtMMSrNo" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtMMSrNo_TextChanged" placeholder="MM Sr No."></asp:TextBox>
                                                                                <asp:Label ID="lblMMSrNoError" runat="server" Visible="false" ForeColor="Red">Please Enter the valid MM Sr No</asp:Label>
                                                                                <asp:RequiredFieldValidator Width="130" ID="rfvMMSrNo" Style="color: red;" runat="server" ControlToValidate="txtMMSrNo" ValidationGroup="SaveItem"
                                                                                    ErrorMessage="Please valid MM Sr No">Please valid MM Sr No</asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                </div>

                                                                <div class="col-md-12" style="margin-top: 10px!important;">
                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label class="col-md-5 control-label">Item Code : </label>
                                                                            <div class="col-md-7">
                                                                                <div class="input-group">
                                                                                    <asp:TextBox ID="txtItemCode" Width="130" runat="server" placeholder="Item Code" AutoPostBack="true" CssClass="form-control required_text_box" OnTextChanged="txtItemCode_TextChanged"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="rfvItemDesc" Style="color: red;" ControlToValidate="txtItemDesc" runat="server" ValidationGroup="SaveItem"
                                                                                        ErrorMessage="Please enter Item Desc.">Please enter valid Item Code</asp:RequiredFieldValidator>
                                                                                    <asp:TextBox ID="txtItemId" runat="server" placeholder="Item Id" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                </div>


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

                                                                                <asp:TextBox ID="txtrate" runat="server" placeholder="Rate" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                <asp:TextBox ID="txtchalanqty" runat="server" placeholder="Rate" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                <asp:TextBox ID="txtCAAmount" runat="server" placeholder="Amount" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label class="col-md-5 control-label">Desc. : </label>
                                                                            <div class="col-md-7">
                                                                                <asp:TextBox ID="txtItemDesc" Width="325" runat="server" placeholder="Item Description" class="form-control required_text_box" ReadOnly="true"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label class="col-md-5 control-label">Total Qty : </label>
                                                                            <div class="col-md-7">
                                                                                <asp:TextBox ID="txtTotalQty" runat="server" CssClass="form-control required_text_box" Enabled="false" placeholder="Total Qty"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label class="col-md-5 control-label">Return Qty : </label>
                                                                            <div class="col-md-7">
                                                                                <asp:TextBox ID="txtReturnQty" runat="server" CssClass="form-control required_text_box" placeholder="Return Qty"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label class="col-md-5 control-label">Accptd Qty : </label>
                                                                            <div class="col-md-7">
                                                                                <asp:TextBox ID="txtAcceptedQty" Enabled="false" runat="server" CssClass="form-control required_text_box" placeholder="Accepted Qty"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-12" style="margin-top: -18px!important;">
                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label class="col-md-5 control-label">Plant :</label>
                                                                            <div class="col-md-7">
                                                                                <asp:DropDownList ID="ddlPlant" Width="130" Enabled="false" runat="server" CssClass="form-control required_text_box" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvPlant" Style="color: red;" ControlToValidate="ddlPlant" runat="server" ValidationGroup="SaveItem"
                                                                                    ErrorMessage="Please Select Plant" InitialValue="0">Please Select Plant</asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label class="col-md-5 control-label">Location :</label>
                                                                            <div class="col-md-7">
                                                                                <asp:DropDownList ID="ddlLocation" Width="325" runat="server" CssClass="form-control required_text_box" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvLocation" Style="color: red;" ControlToValidate="ddlLocation" runat="server" ValidationGroup="SaveItem"
                                                                                    ErrorMessage="Please Select Location" InitialValue="0">Please Select Location</asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label class="col-md-5 control-label">UOM : </label>
                                                                            <div class="col-md-7">
                                                                                <asp:DropDownList ID="ddlUOM" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlUOM_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvUOM" Style="color: red;" ControlToValidate="ddlUOM" runat="server" ValidationGroup="SaveItem"
                                                                                    ErrorMessage="Please Select Item UOM" InitialValue="0">Please Select Item UOM</asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label class="col-md-5 control-label">Cost Cntr :</label>
                                                                            <div class="col-md-7">
                                                                                <%--<asp:TextBox ID="txtCostCenter" runat="server" CssClass="form-control" placeholder="Cost Center"></asp:TextBox>--%>
                                                                                <asp:DropDownList ID="ddlCostCenter" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvCostCenter" ControlToValidate="ddlCostCenter" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                                                    ErrorMessage="Please Select Cost Center" InitialValue="0">Please Select Cost Center</asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label class="col-md-5 control-label">Trackng No :</label>
                                                                            <div class="col-md-7">
                                                                                <asp:TextBox ID="txtTrackingNO" runat="server" CssClass="form-control" placeholder="Tracking No."></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                </div>

                                                                <div class="col-md-12" style="margin-top: -23px!important;">
                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label class="col-md-5 control-label">Prv Qty : </label>
                                                                            <div class="col-md-7">
                                                                                <asp:TextBox ID="txtPreviousQty" Enabled="false" Width="130" runat="server" CssClass="form-control required_text_box" placeholder="Previous Qty"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-2">
                                                                        <div class="form-group">
                                                                            <label class="col-md-5 control-label">Remark : </label>
                                                                            <div class="col-md-7">
                                                                                <asp:TextBox ID="txtDetailRemark" runat="server" CssClass="form-control" placeholder="Remark" Width="520"></asp:TextBox>
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
                                <div class="panel-footer">
                                    <asp:LinkButton ID="btnSaveDet" runat="server" CssClass="btn btn-success pull-right" OnClick="btnSaveDet_Click" Text="Save Item" ValidationGroup="SaveItem"><i class="fa fa-plus-square"></i></asp:LinkButton>
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
                                                                        OnRowCommand="grvListItem_RowCommand" OnRowDataBound="grvListItem_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Doc No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblDocNo" Text='<%# Bind("DOCNO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblID" Text='<%# Bind("ID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Po No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblPoNo" Text='<%# Bind("PONO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Po Sr No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblPoSrNo" Text='<%# Bind("POSRNO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblQty" Text='<%# Bind("Qty") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Chalan Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblchalantQty" Text='<%# Bind("CHLNQTY") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>


                                                                            <asp:TemplateField HeaderText="Accepted Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblAcceptedqty" Text='<%# Bind("ACPTQTY") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>


                                                                            <asp:TemplateField HeaderText="Return Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblReturnQty" Text='<%# Bind("RTNQTY") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="MM Doc No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblMMDocNo" Text='<%# Bind("MMDOCNO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="MM Final Year">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblMMFinalYear" Text='<%# Bind("MMFINYEAR") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="MM Sr No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblMMSrNo" Text='<%# Bind("MMSRNO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Item Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblItemCode" Text='<%# Bind("ITEMCODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Item Description">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblItemDescription" Text='<%# Bind("ITEMDESC") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="UOM">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblUOM" Text='<%# Bind("UOMDesc") %>'></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblUOMID" Text='<%# Bind("UOMID") %>' Visible="false"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>


                                                                            <asp:TemplateField HeaderText="Plant">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblPlantText" Text='<%# Bind("PLANTTEXT") %>'></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblPlant" Text='<%# Bind("PLANTCD") %>' Visible="false"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Location">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblLocationText" Text='<%# Bind("LOCTEXT") %>'></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblLocation" Text='<%# Bind("LOCCD") %>' Visible="false"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Cost Center">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblCostCenter" Text='<%# Bind("CSTCENTCD") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Track No.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblTrackNo" Text='<%# Bind("TRACKNO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Remarks">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblRemarks" Text='<%# Bind("ITEMTEXT") %>'></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblRate" Text='<%# Bind("RATE") %>' Visible="false"></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblPrfcnt" Text='<%# Bind("PRFCNT") %>' Visible="false"></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblGlCde" Text='<%# Bind("GLCD") %>' Visible="false"></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblAssetCode" Text='<%# Bind("ASSETCD") %>' Visible="false"></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblItemGroupId" Text='<%# Bind("ITEMGRPID") %>' Visible="false"></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblItemId" Text='<%# Bind("ITEMID") %>' Visible="false"></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblCAAmount" Text='<%# Bind("CAMOUNT") %>' Visible="false"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Action">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("ID") %>'
                                                                                        CommandName="eDelete" OnClientClick="return confirm('Are you sure you want to delete this record?');">Delete</asp:LinkButton>
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
    <input type="hidden" id="menutabid" value="tsmTranMMMatIss" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />
</asp:Content>
