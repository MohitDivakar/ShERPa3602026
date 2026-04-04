<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="CreateMaterialAdjust.aspx.cs" Inherits="ShERPa360net.MM.CreateMaterialAdjust" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }
    </style>

    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            document.getElementById("busy-holder1").style.display = "";
            document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Inventory  </strong>Adjustment</h3>
                            <asp:LinkButton ID="imgCancel" runat="server" CssClass="btn btn-success pull-right" PostBackUrl="~/MM/ViewPR.aspx" Text="Cancel"><i class="fa fa-times"></i></asp:LinkButton>
                            <asp:LinkButton ID="imgSaveAll" runat="server" CssClass="btn btn-success pull-right" OnClientClick="ShowLoading()" OnClick="imgSaveAll_Click" Text="Save All" ValidationGroup="SaveAll"><i class="fa fa-save"></i></asp:LinkButton>
                            <div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                <label>Please wait...</label>
                            </div>

                        </div>

                        <div class="panel-body">

                            <div class="row">

                                <div class="col-md-12">
                                    <h4 style="color: #f05423">DOC. Details</h4>

                                    <div class="col-md-4">
                                        <div class="form-group">



                                            <label class="col-md-4 control-label">Doc. Type : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlDoctype" runat="server" CssClass="form-control " TabIndex="1"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvDocType" runat="server" ControlToValidate="ddlDoctype" ValidationGroup="SaveAll"
                                                    ErrorMessage="Please Select Doc Type" InitialValue="0">Please Select Doc Type</asp:RequiredFieldValidator>
                                            </div>



                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">


                                            <label class="col-md-4 control-label">Doc No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtDocNo" runat="server" CssClass="form-control" placeholder="Doc No." TabIndex="2" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvDocNo" runat="server" ControlToValidate="txtDocNo" ValidationGroup="SaveAll"
                                                    ErrorMessage="Please Enter Doc No">Please Enter Doc No</asp:RequiredFieldValidator>
                                            </div>



                                        </div>
                                    </div>


                                    <div class="col-md-4">
                                        <div class="form-group">

                                            <label class="col-md-4 control-label">Doc Date : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtDocDate" runat="server" placeholder="Doc Date" class="form-control datepicker" AutoCompleteType="None" TabIndex="3" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <asp:RequiredFieldValidator ID="rfvDocDate" runat="server" ControlToValidate="txtDocDate" ValidationGroup="SaveAll"
                                                ErrorMessage="Please Enter Doc Date">Please Enter Doc Date</asp:RequiredFieldValidator>



                                        </div>

                                    </div>
                                </div>

                                <div class="col-md-12">

                                    <div class="col-md-4">
                                        <div class="form-group">

                                            <label class="col-md-4 control-label">Ref. No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtRefno" runat="server" placeholder="Ref. No." class="form-control" TabIndex="4"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">

                                            <label class="col-md-4 control-label">Remark : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtREMARKS" runat="server" placeholder="Remarks" class="form-control" TabIndex="5" Text="ADJUSTEMENT ENTRY"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>



                                </div>



                                <asp:UpdatePanel ID="updItemDetails" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <%--<asp:PostBackTrigger ControlID="txtItemQty" />--%>
                                        <asp:AsyncPostBackTrigger ControlID="txtItemQty" EventName="TextChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="txtItemCode" EventName="TextChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlUOM" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlPLant" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <div id="mobile_View">

                                            <div class="col-md-4">



                                                <h4 style="color: #f05423">Material Details</h4>
                                                <asp:Label ID="lblSRNO" runat="server" Visible="false"></asp:Label>


                                                <div class="form-group" style="margin-top: 15px;">
                                                    <label class="col-md-3 control-label">Item Code :</label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtItemCode" runat="server" placeholder="Item Code" AutoPostBack="true" CssClass="form-control required_text_box" OnTextChanged="txtItemCode_TextChanged" TabIndex="7"></asp:TextBox>
                                                            <span class="input-group-btn">
                                                                <%--<button class="btn btn-success" type="button" data-toggle="modal" data-target="#modal-item"><span class="fa fa-search"></span></button>--%>
                                                                <asp:LinkButton ID="lnkOpenPoup" runat="server" CssClass="btn btn-success" OnClick="btnPopup_Click" Text="Open Popup"><span class="fa fa-search"></span></asp:LinkButton>

                                                                <%--<asp:LinkButton ID="lnkNewPartCode" runat="server" CssClass="btn btn-success" OnClick="lnkNewPartCode_Click" Text="Open Popup"><span class="fa fa-plus-circle"></span></asp:LinkButton>--%>


                                                            </span>

                                                        </div>
                                                        <asp:RequiredFieldValidator ID="rfvItemCode" ControlToValidate="txtItemCode" runat="server" ValidationGroup="SaveItem"
                                                            ErrorMessage="Please  Enter Item Code" Style="color: red;">Please  Enter Item Code</asp:RequiredFieldValidator>
                                                        <asp:Button ID="btnPopup" runat="server" AccessKey="Z" OnClick="btnPopup_Click" CssClass="hidden" />
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

                                                        <asp:TextBox ID="txtOnHandStock" runat="server" placeholder="Item Text" CssClass="form-control" Visible="false"></asp:TextBox>


                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Qty :</label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:TextBox ID="txtItemQty" runat="server" CssClass="form-control required_text_box" placeholder="Quantity" OnTextChanged="txtItemQty_TextChanged" AutoPostBack="true" TabIndex="8"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvItemQty" ControlToValidate="txtItemQty" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                            ErrorMessage="Please  Enter Item Qty.">Please  Enter Item Qty.</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Plant Code : </label>
                                                    <div class="col-md-9 col-xs-12">

                                                        <asp:DropDownList ID="ddlPLant" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPLant_SelectedIndexChanged" AutoPostBack="true" TabIndex="11"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvPLant" ControlToValidate="ddlPLant" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                            ErrorMessage="Please Select Plant" InitialValue="0">Please Select Plant</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Remark : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:TextBox ID="txtItemRemarks" runat="server" placeholder="Item Remark" CssClass="form-control" Width="775" TabIndex="15" Text="ADJUSTEMENT ENTRY"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>








                                            <div class="col-md-4">


                                                <h4 style="color: #f05423">&nbsp;</h4>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Item Desc. :</label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:TextBox ID="txtItemDesc" runat="server" placeholder="Item Description" class="form-control required_text_box" ReadOnly="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvItemDesc" ControlToValidate="txtItemDesc" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                            ErrorMessage="Please  Enter Item Desc.">Please  Enter Item Desc.</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>



                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">UOM :</label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:DropDownList ID="ddlUOM" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlUOM_SelectedIndexChanged" AutoPostBack="true" TabIndex="9"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvUOM" ControlToValidate="ddlUOM" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                            ErrorMessage="Please Select Item UOM" InitialValue="0">Please Select Item UOM</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Location : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control" TabIndex="12" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvLocation" ControlToValidate="ddlLocation" runat="server" ValidationGroup="SaveItem"
                                                            ErrorMessage="Please Select Location" InitialValue="0">Please Select Location</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="col-md-4">
                                                <h4 style="color: #f05423">&nbsp;</h4>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label"></label>
                                                    <div class="col-md-9 col-xs-12">
                                                        &nbsp;
                                                        <br />

                                                        &nbsp;
                                                        <br />
                                                        &nbsp;
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label"></label>
                                                    <div class="col-md-9 col-xs-12">
                                                        &nbsp;
                                                        <br />

                                                        &nbsp;
                                                        <br />
                                                        &nbsp;
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Cost Center : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <%--<asp:TextBox ID="txtCostCenter" runat="server" placeholder="Cost Center" CssClass="form-control" ReadOnly="true"></asp:TextBox>--%>
                                                        <asp:DropDownList ID="ddlCostCenter" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        <%--<asp:RequiredFieldValidator ID="rfvCostCenter" ControlToValidate="txtCostCenter" runat="server" ValidationGroup="SaveItem"
                                                            ErrorMessage="Please Enter Cost Center">*</asp:RequiredFieldValidator>--%>

                                                        <asp:RequiredFieldValidator ID="rfvCostCenter" ControlToValidate="ddlCostCenter" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                            ErrorMessage="Please Select Cost Center" InitialValue="0">Please Select Cost Center</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                            </div>

                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                            </div>

                        </div>
                        <div class="panel-footer">
                            <%--<button type="button" class="btn btn-success pull-right"><i class="fa fa-plus-square"></i></button>--%>
                            <asp:LinkButton ID="btnSaveDet" runat="server" CssClass="btn btn-success pull-right" OnClick="btnSaveDet_Click" Text="Save Item" ValidationGroup="SaveItem" TabIndex="16"><i class="fa fa-plus-square"></i></asp:LinkButton>

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
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblItemId" Text='<%# Bind("ITEMID") %>'></asp:Label>


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

                                                <%--<asp:TemplateField HeaderText="Rate" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblRate" Text='<%# Bind("ITEMRATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <%--<asp:TemplateField HeaderText="Amount" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblAmount" Text='<%# Bind("ITEMAMOUNT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

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

                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("ID") %>'
                                                            CommandName="eDelete" OnClientClick="return confirm('Are you sure you want to delete this record?');">Delete</asp:LinkButton>
                                                        |
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("ID") %>'
                                                        CommandName="eEdit">Edit</asp:LinkButton>
                                                    </ItemTemplate>
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
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranMMMatAdj" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />

</asp:Content>
