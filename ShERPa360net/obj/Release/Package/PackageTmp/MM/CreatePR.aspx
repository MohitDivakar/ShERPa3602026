<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="CreatePR.aspx.cs" Inherits="ShERPa360net.CreatePR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Create  </strong>Requisition</h3>
                            <%--<ul class="panel-controls" style="margin: 0px 50px 6px 0px;">--%>


                            <%--<button type="button" class="btn btn-success pull-right"><i class="fa fa-reply"></i></button>
                                    <button type="button" class="btn btn-success pull-right"><i class="fa fa-times"></i></button>
                                    <button type="button" class="btn btn-success pull-right"><i class="fa fa-save"></i></button>--%>
                            <%--<asp:LinkButton ID="imgBackToList" runat="server" CssClass="btn btn-success pull-right" PostBackUrl="~/ViewPR.aspx" Text="Cancel"><i class="fa fa-reply"></i></asp:LinkButton>--%>
                            <asp:LinkButton ID="imgCancel" runat="server" CssClass="btn btn-success pull-right" PostBackUrl="~/MM/ViewPR.aspx" Text="Cancel"><i class="fa fa-times"></i></asp:LinkButton>
                            <asp:LinkButton ID="imgSaveAll" runat="server" OnClientClick="ShowLoading()" CssClass="btn btn-success pull-right" OnClick="imgSaveAll_Click" Text="Save All" ValidationGroup="SaveAll"><i class="fa fa-save"></i></asp:LinkButton>
                            <div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                <label>Please wait...</label>
                            </div>
                            <%--</ul>--%>
                        </div>

                        <div class="panel-body">

                            <div class="row">

                                <div class="col-md-12">
                                    <h4 style="color: #f05423">PR Details</h4>

                                    <div class="col-md-2">
                                        <div class="form-group">



                                            <label class="col-md-5 control-label">Doc. Type : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <asp:DropDownList ID="ddlDoctype" runat="server" CssClass="form-control " TabIndex="1"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvDocType" runat="server" ControlToValidate="ddlDoctype" ValidationGroup="SaveAll" Style="color: red;"
                                                    ErrorMessage="Please Select Doc Type" InitialValue="0">Please Select Doc Type</asp:RequiredFieldValidator>
                                            </div>



                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">


                                            <label class="col-md-5 control-label">PR No. : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <asp:TextBox ID="txtPRNO" runat="server" CssClass="form-control" placeholder="PR No." TabIndex="2" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvPRNO" runat="server" ControlToValidate="txtPRNO" ValidationGroup="SaveAll" Style="color: red;"
                                                    ErrorMessage="Please Enter PR No">Please Enter PR No</asp:RequiredFieldValidator>
                                            </div>



                                        </div>
                                    </div>


                                    <div class="col-md-2">
                                        <div class="form-group">

                                            <label class="col-md-4 control-label">PR Date : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtPRDATE" runat="server" placeholder="PR Date" class="form-control datepicker" AutoCompleteType="None" ReadOnly="true" TabIndex="3"></asp:TextBox>
                                                </div>
                                            </div>
                                            <asp:RequiredFieldValidator ID="rfvPRDate" runat="server" ControlToValidate="txtPRDATE" ValidationGroup="SaveAll" Style="color: red;"
                                                ErrorMessage="Please Enter PR Date">Please Enter PR Date</asp:RequiredFieldValidator>



                                        </div>

                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">

                                            <label class="col-md-3 control-label">Remark : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <asp:TextBox ID="txtREMARKS" runat="server" placeholder="Remarks" class="form-control" TabIndex="4"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">

                                            <label class="col-md-4 control-label">Department : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control " TabIndex="5"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="efvDepartment" runat="server" ControlToValidate="ddlDepartment" ValidationGroup="SaveAll" Style="color: red;"
                                                    ErrorMessage="Please Select Department" InitialValue="0">Please Select Department</asp:RequiredFieldValidator>
                                            </div>

                                        </div>
                                    </div>

                                </div>


                                <asp:UpdatePanel ID="updItemDetails" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <%--<asp:PostBackTrigger ControlID="txtItemQty" />--%>
                                        <asp:AsyncPostBackTrigger ControlID="txtItemQty" EventName="TextChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="txtItemCode" EventName="TextChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="txtTrackNo" EventName="TextChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="txtItemRate" EventName="TextChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="txtPartReqNo" EventName="TextChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlUOM" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlPLant" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <div id="mobile_View">

                                            <div class="col-md-4">



                                                <h4 style="color: #f05423">Material Details</h4>

                                                <asp:Label ID="lblPRSRNO" runat="server" Visible="false"></asp:Label>


                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Part Req. No. :</label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:TextBox ID="txtPartReqNo" runat="server" placeholder="Part Req. No." class="form-control" OnTextChanged="txtPartReqNo_TextChanged" AutoPostBack="true" TabIndex="6"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="rfvPartReqNo" ControlToValidate="txtPartReqNo" runat="server" ValidationGroup="SaveItem"
                                                            ErrorMessage="Please Enter Part Req. No.">*</asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>



                                                <div class="form-group" style="margin-top: 15px;">
                                                    <label class="col-md-3 control-label">Item Code :</label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtItemCode" runat="server" placeholder="Item Code" AutoPostBack="true" CssClass="form-control required_text_box" OnTextChanged="txtItemCode_TextChanged" TabIndex="7"></asp:TextBox>
                                                            <span class="input-group-btn">
                                                                <%--<button class="btn btn-success" type="button" data-toggle="modal" data-target="#modal-item"><span class="fa fa-search"></span></button>--%>
                                                                <asp:LinkButton ID="lnkOpenPoup" runat="server" CssClass="btn btn-success" OnClick="btnPopup_Click" Text="Open Popup"><span class="fa fa-search"></span></asp:LinkButton>

                                                                <asp:LinkButton ID="lnkNewPartCode" runat="server" CssClass="btn btn-success" OnClick="lnkNewPartCode_Click" Text="Open Popup"><span class="fa fa-plus-circle"></span></asp:LinkButton>


                                                            </span>

                                                        </div>
                                                        <asp:RequiredFieldValidator ID="rfvItemCode" ControlToValidate="txtItemCode" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                            ErrorMessage="Please  Enter Item Code">Please  Enter Item Code</asp:RequiredFieldValidator>
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



                                                <div class="form-group" style="display: none;">
                                                    <label class="col-md-3 control-label">Rate : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:TextBox ID="txtItemRate" runat="server" placeholder="Rate" CssClass="form-control" OnTextChanged="txtItemQty_TextChanged" AutoPostBack="true" value="1"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvItemRate" ControlToValidate="txtItemRate" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                            ErrorMessage="Please  Enter Item Rate">Please  Enter Item Rate</asp:RequiredFieldValidator>
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
                                                    <label class="col-md-3 control-label">Tracking No. : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:TextBox ID="txtTrackNo" runat="server" placeholder="Tracking No." CssClass="form-control" OnTextChanged="txtTrackNo_TextChanged" AutoPostBack="true" TabIndex="13"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="rfvTrackNo" ControlToValidate="txtTrackNo" runat="server" ValidationGroup="SaveItem"
                                                            ErrorMessage="Please  Enter Tracking No.">*</asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>


                                                <div class="form-group" style="margin-top: 15px;">
                                                    <label class="col-md-3 control-label">Remark : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:TextBox ID="txtItemRemarks" runat="server" placeholder="Item Remark" CssClass="form-control" Width="775" TabIndex="15"></asp:TextBox>
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

                                                <div class="form-group" style="display: none;">
                                                    <label class="col-md-3 control-label">Amount : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:TextBox ID="txtAmount" runat="server" placeholder="Amount" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvAmount" ControlToValidate="txtAmount" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                            ErrorMessage="Please  Enter Amount">Please  Enter Amount</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Location : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control" TabIndex="12" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvLocation" ControlToValidate="ddlLocation" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                            ErrorMessage="Please Select Location" InitialValue="0">Please Select Location</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Delivery Date : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <div class="input-group">
                                                            <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                            <asp:TextBox ID="txtDeliveryDate" runat="server" placeholder="Delivery Date" CssClass="form-control datepicker" AutoCompleteType="None" TabIndex="14"></asp:TextBox>

                                                        </div>
                                                    </div>
                                                    <asp:RequiredFieldValidator ID="rfvDeliveryDate" ControlToValidate="txtDeliveryDate" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                        ErrorMessage="Please  Enter Delivery Date">Please  Enter Delivery Date</asp:RequiredFieldValidator>
                                                </div>


                                            </div>



                                            <div class="col-md-4">



                                                <h4 style="color: #f05423">&nbsp; Other Details</h4>

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
                                                    <label class="col-md-3 control-label">On Hand Stock : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:TextBox ID="txtOnHandStock" runat="server" CssClass="form-control required_text_box" placeholder="On Hand Stock" ReadOnly="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvOnHandStock" ControlToValidate="txtOnHandStock" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                            ErrorMessage="Please  Enter Item On Hand Stock">Please  Enter Item On Hand Stock</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>


                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Requisitioner : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:TextBox ID="txtRequisitioner" runat="server" placeholder="Requisitioner" CssClass="form-control" TabIndex="10"></asp:TextBox>
                                                    </div>
                                                </div>


                                                <div class="form-group" style="margin-top: 15px;">
                                                    <label class="col-md-3 control-label">Cost Center : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <%--<asp:TextBox ID="txtCostCenter" runat="server" placeholder="Cost Center" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvCostCenter" ControlToValidate="txtCostCenter" runat="server" ValidationGroup="SaveItem"
                                                            ErrorMessage="Please Enter Cost Center">*</asp:RequiredFieldValidator>--%>

                                                        <asp:DropDownList ID="ddlCostCenter" runat="server" CssClass="form-control"></asp:DropDownList>
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
                                            <%-- OnRowEditing="grvListItem_RowEditing" OnRowCancelingEdit="grvListItem_RowCancelingEdit" OnRowUpdating="grvListItem_RowUpdating">--%>
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
                                                    <%--<EditItemTemplate>
                                                    <asp:TextBox ID="txtEditItemCode" runat="server" Text='<%# Bind("ITEMCODE") %>' OnTextChanged="txtEditItemCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </EditItemTemplate>--%>
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
                                                    <asp:TextBox ID="txtEditQty" runat="server" Text='<%# Bind("ITEMQTY") %>' OnTextChanged="txtEditQty_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </EditItemTemplate>--%>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Rate" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblRate" Text='<%# Bind("ITEMRATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <%--<EditItemTemplate>
                                                    <asp:TextBox ID="txtEditRate" runat="server" Text='<%# Bind("ITEMRATE") %>' OnTextChanged="txtEditRate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </EditItemTemplate>--%>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Amount" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblAmount" Text='<%# Bind("ITEMAMOUNT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Deli. Date">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDeliDate" Text='<%# Bind("DELIVERYDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="DELIVERYDATE" HeaderText="Deli. Date" DataFormatString="{0:MM/dd/yyyy}" />--%>
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

                                                <asp:TemplateField HeaderText="Requisitioner">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblRequisitioner" Text='<%# Bind("REQUISITIONER") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <%--<EditItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtEditRequisitioner" Text='<%# Bind("REQUISITIONER") %>'></asp:TextBox>
                                                </EditItemTemplate>--%>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Track No.">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblTrackNo" Text='<%# Bind("TRACKNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <%--<EditItemTemplate>
                                                    <asp:TextBox ID="txtEditTrackNo" runat="server" Text='<%# Bind("TRACKNO") %>' OnTextChanged="txtEditTrackNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </EditItemTemplate>--%>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Item Text">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblItemText" Text='<%# Bind("ITEMTEXT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Part Req. No.">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPartReqNo" Text='<%# Bind("PARTREQNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <%--<EditItemTemplate>
                                                    <asp:TextBox ID="txtEditPartReqNo" runat="server" Text='<%# Bind("PARTREQNO") %>'></asp:TextBox>
                                                </EditItemTemplate>--%>
                                                </asp:TemplateField>

                                                <%--<asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblRemarks" Text='<%# Bind("ITEMREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>--%>
                                                <%--<EditItemTemplate>
                                                    <asp:TextBox ID="txtEditRemark" runat="server" Text='<%# Bind("ITEMREMARKS") %>'></asp:TextBox>
                                                </EditItemTemplate>--%>
                                                <%--</asp:TemplateField>--%>
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
                                                <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblMRID" Text='<%# Bind("MRID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/cssjs/images/1293958391_fileclose32.png" />--%>
                                                <%--<asp:CommandField ShowEditButton="True" ButtonType="Image" EditImageUrl="~/cssjs/images/1293953735_edit-notes32.png" />--%>
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



    <%--<asp:UpdatePanel ID="updPopup" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlpopMake" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="grvPopItem" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="btnShowItem" EventName="Click" />
        </Triggers>
        <ContentTemplate>--%>
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
                                <asp:LinkButton ID="btnShowItem" runat="server" CssClass="btn btn-success pull-left" OnClick="imgFind_Click" Text="Show Item"><span class="fa fa-search"></span></asp:LinkButton>
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
    <%--    </ContentTemplate>
    </asp:UpdatePanel>--%>

    <div class="modal fade" id="modal-Newitem" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7">Request New Item </h4>
                </div>



                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">


                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Category :</label>
                                        <asp:DropDownList ID="ddlNewCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Group :</label>
                                        <asp:DropDownList ID="ddlNewGroup" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Sub Group :</label>
                                        <asp:DropDownList ID="ddlNewSubGroup" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Item Name :</label>
                                        <asp:TextBox ID="txtNewItemName" runat="server" CssClass="form-control" placeholder="Item Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNewItemName" runat="server" ControlToValidate="txtNewItemName" Style="color: red;"
                                            ErrorMessage="Enter Item Name" ValidationGroup="NewItem">Enter Item Name</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Description :</label>
                                        <asp:TextBox ID="txtNewItemDesc" runat="server" CssClass="form-control" placeholder="Item Description"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNewItemDesc" runat="server" ControlToValidate="txtNewItemDesc" Style="color: red;"
                                            ErrorMessage="Enter Item Desc." ValidationGroup="NewItem">Enter Item Desc.</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Item Specification :</label>
                                        <asp:TextBox ID="txtNewItemSpecification" runat="server" CssClass="form-control" placeholder="Item Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNewItemSpec" runat="server" ControlToValidate="txtNewItemSpecification" Style="color: red;"
                                            ErrorMessage="Enter Item Specification" ValidationGroup="NewItem">Enter Item Specification</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12" style="margin-top: 5px;">
                                <asp:LinkButton ID="lnkNewItemSave" runat="server" CssClass="btn btn-success pull-left" OnClick="lnkNewItemSave_Click" ValidationGroup="NewItem" Text="Request Item"><span class="fa fa-floppy-o"></span></asp:LinkButton>
                            </div>

                        </div>
                    </div>

                </div>

            </div>
        </div>
    </div>


    <%--<asp:ValidationSummary ID="validate" runat="server" ShowSummary="false" ShowMessageBox="true" ValidationGroup="SaveItem" />
    <asp:ValidationSummary ID="validate1" runat="server" ShowSummary="false" ShowMessageBox="true" ValidationGroup="SaveAll" />
    <asp:ValidationSummary ID="validate2" runat="server" ShowSummary="false" ShowMessageBox="true" ValidationGroup="NewItem" />--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmTranMMPR" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />

</asp:Content>
