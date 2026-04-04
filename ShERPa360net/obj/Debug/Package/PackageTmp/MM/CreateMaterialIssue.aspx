<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="CreateMaterialIssue.aspx.cs" Inherits="ShERPa360net.MM.CreateMaterialIssue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            var emp = $("#ContentPlaceHolder1_txtEmployeeName").val();
            if (emp != "") {


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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Material Issue </strong></h3>
                            <asp:LinkButton ID="imgCancel" runat="server" CssClass="btn btn-success pull-right" PostBackUrl="~/MM/ViewMaterialIssue.aspx?REQUESTFORM=MI" Text="Cancel"><i class="fa fa-times"></i></asp:LinkButton>
                            <asp:LinkButton ID="imgSaveAll" runat="server" OnClientClick="ShowLoading()" CssClass="btn btn-success pull-right" OnClick="imgSaveAll_Click" Text="Save All" ValidationGroup="SaveAll"><i class="fa fa-save"></i></asp:LinkButton>
                            <div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                <label>Please wait...</label>
                            </div>
                        </div>

                        <asp:UpdatePanel ID="pnlprimaryDetails" runat="server" UpdateMode="Conditional">
                            <Triggers>
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
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Doc. Type : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList ID="ddlDoctype" runat="server" CssClass="form-control "></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Doc No. : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtDocNo" runat="server" Enabled="false" CssClass="form-control" placeholder="Doc No."></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvDocNo" Style="color: red!important;" runat="server" ControlToValidate="txtDocNo" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Doc No">Please Enter Doc No</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Doc Date : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <div class="input-group">
                                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                    <asp:TextBox ID="txtDocDt"  Enabled="false" Style="height : 30px !important;" runat="server" placeholder="DD/MM/YYYY" class="form-control datepicker" AutoCompleteType="None"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <asp:RequiredFieldValidator ID="rfvDocDt" runat="server" Style="color: red!important;" ControlToValidate="txtDocDt" ValidationGroup="SaveAll"
                                                                ErrorMessage="Please Enter Doc Date">Please Enter Doc Date</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Issue Dept</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList ID="ddlIssueDepartment" runat="server" CssClass="form-control required_text_box" AutoPostBack="false"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvDepartment" Style="color: red;" ControlToValidate="ddlIssueDepartment" runat="server" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Select Issue Dept" InitialValue="0">Please Select Issue Dept</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-12">

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Emp Name : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtEmployeeName" runat="server" CssClass="form-control required_text_box" placeholder="Employee Name"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvEmployeeName" Style="color: red;" ControlToValidate="txtEmployeeName" runat="server" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Emp Name">Please Enter Emp Name</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Ref. No. : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtRefNo" runat="server" CssClass="form-control" placeholder="Ref. No."></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-6">
                                                        <div class="form-group">
                                                            <label class="col-md-2 control-label">Remark : </label>
                                                            <div class="col-md-10 col-xs-12">
                                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" Style="margin-left: 29px!important; width: 500px!important;" placeholder="Remarks" Width="500"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="col-md-12">
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
                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label class="col-md-4 control-label">Sr. No. : </label>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtSRNo" runat="server" CssClass="form-control" Enabled="false" placeholder="Sr. No."></asp:TextBox>
                                                                                <asp:TextBox ID="txtMaxSrNo" runat="server" placeholder="Max Sr. No." CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="lblPRSRNO" runat="server" CssClass="hidden"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-12" style="margin-top: 10px;">
                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label class="col-md-4 control-label">Item Code : </label>
                                                                            <div class="col-md-8">
                                                                                <div class="input-group">
                                                                                    <asp:TextBox ID="txtItemCode" runat="server" placeholder="Item Code" AutoPostBack="true" CssClass="form-control required_text_box" OnTextChanged="txtItemCode_TextChanged"></asp:TextBox>
                                                                                    <span class="input-group-btn">
                                                                                        <asp:LinkButton ID="lnkOpenPoup" runat="server" CssClass="btn btn-success" OnClick="lnkOpenPoup_Click" Text="Open Popup"><span class="fa fa-search"></span></asp:LinkButton>
                                                                                    </span>
                                                                                </div>
                                                                                <asp:RequiredFieldValidator ID="rfvItemDesc" Style="color: red;" ControlToValidate="txtItemDesc" runat="server" ValidationGroup="SaveItem"
                                                                                    ErrorMessage="Please enter Item Desc.">Please enter valid Item Code</asp:RequiredFieldValidator>
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
                                                                                <asp:Button ID="btnPopup" runat="server" AccessKey="Z" OnClick="lnkOpenPoup_Click" CssClass="hidden" />
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label class="col-md-4 control-label">Item Desc. : </label>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtItemDesc" runat="server" placeholder="Item Description" class="form-control required_text_box" ReadOnly="true" Width="520"></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-12" style="margin-top: 0px;">
                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label class="col-md-4 control-label">Qty : </label>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtItemQty" runat="server" CssClass="form-control required_text_box" placeholder="Quantity"></asp:TextBox>
                                                                                <asp:RequiredFieldValidator ID="rfvItemQty" Style="color: red;" ControlToValidate="txtItemQty" runat="server" ValidationGroup="SaveItem"
                                                                                    ErrorMessage="Please Enter Qty">Please Enter Qty</asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label class="col-md-4 control-label">UOM : </label>
                                                                            <div class="col-md-8">
                                                                                <asp:DropDownList ID="ddlUOM" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlUOM_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvUOM" Style="color: red;" ControlToValidate="ddlUOM" runat="server" ValidationGroup="SaveItem"
                                                                                    ErrorMessage="Please Select Item UOM" InitialValue="0">Please Select Item UOM</asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label class="col-md-4 control-label">Plant :</label>
                                                                            <div class="col-md-8">
                                                                                <asp:DropDownList ID="ddlPlant" runat="server" CssClass="form-control required_text_box" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvPlant" Style="color: red;" ControlToValidate="ddlPlant" runat="server" ValidationGroup="SaveItem"
                                                                                    ErrorMessage="Please Select Plant" InitialValue="0">Please Select Plant</asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label class="col-md-4 control-label">Location :</label>
                                                                            <div class="col-md-8">
                                                                                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control required_text_box" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvLocation" Style="color: red;" ControlToValidate="ddlLocation" runat="server" ValidationGroup="SaveItem"
                                                                                    ErrorMessage="Please Select Location" InitialValue="0">Please Select Location</asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-12">
                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label class="col-md-4 control-label">Cost Center :</label>
                                                                            <div class="col-md-8">
                                                                                <%--<asp:TextBox ID="txtCostCenter" runat="server" CssClass="form-control" placeholder="Cost Center"></asp:TextBox>--%>
                                                                                <asp:DropDownList ID="ddlCostCenter" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                <asp:RequiredFieldValidator ID="rfvCostCenter" ControlToValidate="ddlCostCenter" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                                                    ErrorMessage="Please Select Cost Center" InitialValue="0">Please Select Cost Center</asp:RequiredFieldValidator>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label class="col-md-4 control-label">Tracking No. :</label>
                                                                            <div class="col-md-8">
                                                                                <asp:TextBox ID="txtTrackingNO" runat="server" CssClass="form-control" placeholder="Tracking No."></asp:TextBox>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-3">
                                                                        <div class="form-group">
                                                                            <label class="col-md-4 control-label">Remark : </label>
                                                                            <div class="col-md-8">
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
                                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblID" Text='<%# Bind("ID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Item Code">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblItemCode" Text='<%# Bind("ITEMCODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Item Desc.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblItemDesc" Text='<%# Bind("ITEMDESC") %>'></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblItemId" Text='<%# Bind("ITEMID") %>' Visible="false"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Item Group">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblItemGroup" Text='<%# Bind("ITEMGROUP") %>'></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblGroupId" Text='<%# Bind("ITEMGROUPID") %>' Visible="false"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="UOM">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblUOM" Text='<%# Bind("ITEMUOM") %>'></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblUOMID" Text='<%# Bind("UOMID") %>' Visible="false"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>


                                                                            <asp:TemplateField HeaderText="Qty">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblQty" Text='<%# Bind("ITEMQTY") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>


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
                                                                            <asp:TemplateField HeaderText="Plant">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblPlant" Text='<%# Bind("ITEMFROMPLANTCD") %>'></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblPlantID" Text='<%# Bind("ITEMFROMPLANTID") %>' Visible="false"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Location">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblLocation" Text='<%# Bind("ITEMFROMLOCCD") %>'></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblLocationCDID" Text='<%# Bind("LOCCDFROMID") %>' Visible="false"></asp:Label>
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


                                                                            <asp:TemplateField HeaderText="Track No.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblTrackNo" Text='<%# Bind("TRACKNO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Remarks">
                                                                                <ItemTemplate>
                                                                                    <asp:Label runat="server" ID="lblRemarks" Text='<%# Bind("ITEMREMARKS") %>'></asp:Label>
                                                                                    <asp:Label runat="server" Visible="false" ID="lblSKU" Text='<%# Bind("SKU") %>'></asp:Label>
                                                                                    <asp:Label runat="server" ID="lblMake" Text='<%# Bind("MAKE") %>' Visible="false"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Action">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("ID") %>'
                                                                                        CommandName="eDelete" OnClientClick="return confirm('Are you sure you want to delete this record?');">Delete</asp:LinkButton>
                                                                                    |
                                                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("ID") %>'
                                                                                        CommandName="eEdit">Edit</asp:LinkButton>
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
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmTranMMMatIss" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />
</asp:Content>
