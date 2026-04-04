<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="CreateMR.aspx.cs" Inherits="ShERPa360net.MM.CreateMR" %>

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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Create  </strong>Material Requisition</h3>
                            <asp:LinkButton ID="imgCancel" runat="server" CssClass="btn btn-success pull-right" PostBackUrl="~/MM/ViewMR.aspx" Text="Cancel"><i class="fa fa-times"></i></asp:LinkButton>
                            <asp:LinkButton ID="imgSaveAll" runat="server" CssClass="btn btn-success pull-right" OnClick="imgSaveAll_Click" Text="Save All" OnClientClick="ShowLoading()" ValidationGroup="SaveAll"><i class="fa fa-save"></i></asp:LinkButton>
                            <div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                <label>Please wait...</label>
                            </div>
                        </div>

                        <div class="panel-body">

                            <div class="row">

                                <div class="col-md-12">
                                    <h4 style="color: #f05423">MR Details</h4>

                                    <div class="col-md-2">
                                        <div class="form-group">



                                            <label class="col-md-5 control-label">Doc. Type : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <asp:DropDownList ID="ddlDoctype" runat="server" CssClass="form-control" TabIndex="1"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvDocType" runat="server" ControlToValidate="ddlDoctype" ValidationGroup="SaveAll" Style="color: red;"
                                                    ErrorMessage="Please Select Doc Type" InitialValue="0">Please Select Doc Type</asp:RequiredFieldValidator>
                                            </div>



                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">


                                            <label class="col-md-5 control-label">MR No. : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <asp:TextBox ID="txtMRNO" runat="server" CssClass="form-control" placeholder="MR No." TabIndex="2" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvMRNO" runat="server" ControlToValidate="txtMRNO" ValidationGroup="SaveAll" Style="color: red;"
                                                    ErrorMessage="Please Enter MR No">Please Enter MR No</asp:RequiredFieldValidator>
                                            </div>



                                        </div>
                                    </div>


                                    <div class="col-md-2">
                                        <div class="form-group">

                                            <label class="col-md-4 control-label">MR Date : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtMRDATE" runat="server" placeholder="MR Date" class="form-control" AutoCompleteType="None" TabIndex="3" ReadOnly="true"></asp:TextBox>
                                                </div>
                                            </div>
                                            <asp:RequiredFieldValidator ID="rfvMRDate" runat="server" ControlToValidate="txtMRDATE" ValidationGroup="SaveAll" Style="color: red;"
                                                ErrorMessage="Please Enter MR Date">Please Enter MR Date</asp:RequiredFieldValidator>



                                        </div>

                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">

                                            <label class="col-md-3 control-label">Remark : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <asp:TextBox ID="txtREMARKS" runat="server" placeholder="Remarks" class="form-control" TabIndex="4"></asp:TextBox>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">

                                            <label class="col-md-4 control-label">Department : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control " TabIndex="5"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="efvDepartment" runat="server" ControlToValidate="ddlDepartment" ValidationGroup="SaveAll" Style="color: red;"
                                                    ErrorMessage="Please Select Department" InitialValue="0">Please Select Department</asp:RequiredFieldValidator>
                                            </div>

                                        </div>
                                    </div>

                                    <%-- </div>
                                <div class="col-md-12">--%>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Upload Invoice : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <asp:FileUpload ID="fuInvDoc" runat="server" CssClass="form-control" ViewStateMode="Enabled" />
                                                <%--<asp:RequiredFieldValidator ID="rfvFileUpload" Style="color: red;" runat="server" ControlToValidate="fuInvDoc" ValidationGroup="SaveAll"
                                                    ErrorMessage="Please Upload Invoice Image">
                                                            Please Upload Invoice Image</asp:RequiredFieldValidator>--%>
                                                <asp:RegularExpressionValidator ID="revFileUpload" runat="server" ErrorMessage="Only PDF & Image files are allowed" Style="color: red;"
                                                    ControlToValidate="fuInvDoc" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.jpg|.JPG|.png|.PNG|.jpeg|.JPEG|.pdf|.PDF)$">
                                                </asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <asp:UpdatePanel ID="updItemDetails" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <%--<asp:PostBackTrigger ControlID="txtItemQty" />--%>
                                        <asp:AsyncPostBackTrigger ControlID="txtItemQty" EventName="TextChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="txtItemRate" EventName="TextChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlPLant" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlLocation" EventName="SelectedIndexChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="btnSaveDet" EventName="Click" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <div id="mobile_View">

                                            <div class="col-md-4">



                                                <h4 style="color: #f05423">Material Details</h4>

                                                <asp:Label ID="lblMRSRNO" runat="server" Visible="false"></asp:Label>


                                                <%--<div class="form-group">
                                                    <label class="col-md-3 control-label">Part Req. No. :</label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:TextBox ID="txtPartReqNo" runat="server" placeholder="Part Req. No." class="form-control" OnTextChanged="txtPartReqNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </div>
                                                </div>--%>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Item Desc. :</label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:TextBox ID="txtItemDesc" runat="server" placeholder="Item Description" class="form-control required_text_box" ReadOnly="false" TabIndex="6"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvItemDesc" ControlToValidate="txtItemDesc" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                            ErrorMessage="Please  Enter Item Desc.">Please  Enter Item Desc.</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>











                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Qty :</label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:TextBox ID="txtItemQty" runat="server" CssClass="form-control required_text_box" placeholder="Quantity" OnTextChanged="txtItemQty_TextChanged" AutoPostBack="true" TabIndex="9" value="1"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvItemQty" ControlToValidate="txtItemQty" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                            ErrorMessage="Please  Enter Item Qty.">Please  Enter Item Qty.</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>


                                                <%--<div class="form-group" style="display: none;">--%>
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Rate : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:TextBox ID="txtItemRate" runat="server" placeholder="Rate" CssClass="form-control" value="1" OnTextChanged="txtItemQty_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvItemRate" ControlToValidate="txtItemRate" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                            ErrorMessage="Please  Enter Item Rate">Please  Enter Item Rate</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Plant Code : </label>
                                                    <div class="col-md-9 col-xs-12">

                                                        <asp:DropDownList ID="ddlPLant" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPLant_SelectedIndexChanged" AutoPostBack="true" TabIndex="12"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvPLant" ControlToValidate="ddlPLant" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                            ErrorMessage="Please Select Plant" InitialValue="0">Please Select Plant</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>




                                                <div class="form-group" style="margin-top: 15px;">
                                                    <label class="col-md-3 control-label">Remark : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:TextBox ID="txtItemRemarks" runat="server" placeholder="Item Remark" CssClass="form-control" Width="775" TabIndex="18"></asp:TextBox>
                                                    </div>
                                                </div>

                                            </div>








                                            <div class="col-md-4">


                                                <h4 style="color: #f05423">&nbsp;</h4>

                                                <div class="form-group" style="margin-top: 15px;">




                                                    <label class="col-md-3 control-label">Item Spec. :</label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <%--<div class="input-group">--%>
                                                        <asp:TextBox ID="txtItemSpec" runat="server" placeholder="Item Specification" CssClass="form-control required_text_box" TabIndex="7"></asp:TextBox>
                                                        <%--<span class="input-group-btn">
                                                                <asp:LinkButton ID="lnkOpenPoup" runat="server" CssClass="btn btn-success" OnClick="btnPopup_Click" Text="Open Popup"><span class="fa fa-search"></span></asp:LinkButton>

                                                                <asp:LinkButton ID="lnkNewPartCode" runat="server" CssClass="btn btn-success" OnClick="lnkNewPartCode_Click" Text="Open Popup"><span class="fa fa-plus-circle"></span></asp:LinkButton>


                                                            </span>--%>
                                                        <%--</div>--%>
                                                        <asp:RequiredFieldValidator ID="rfvItemCode" ControlToValidate="txtItemSpec" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                            ErrorMessage="Please  Enter Item Code">Please  Enter Item Code</asp:RequiredFieldValidator>
                                                        <%--<asp:Button ID="btnPopup" runat="server" AccessKey="Z" OnClick="btnPopup_Click" CssClass="hidden" />--%>


                                                        <%--<asp:TextBox ID="txtItemId" runat="server" placeholder="Item Id" CssClass="form-control" Visible="false"></asp:TextBox>

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

                                                        <asp:TextBox ID="txtAssetCode" runat="server" placeholder="Item Text" CssClass="form-control" Visible="false"></asp:TextBox>--%>
                                                    </div>
                                                </div>



                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">UOM :</label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:DropDownList ID="ddlUOM" runat="server" CssClass="form-control" TabIndex="10"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvUOM" ControlToValidate="ddlUOM" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                            ErrorMessage="Please Select Item UOM" InitialValue="0">Please Select Item UOM</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <%--<div class="form-group" style="display: none;">--%>
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Amount : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:TextBox ID="txtAmount" runat="server" placeholder="Amount" CssClass="form-control" ReadOnly="true" TabIndex="13" value="1"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvAmount" ControlToValidate="txtAmount" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                            ErrorMessage="Please Enter Amount">Please Enter Amount</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Location : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control" TabIndex="13" AutoPostBack="true" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvLocation" ControlToValidate="ddlLocation" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                            ErrorMessage="Please Select Location" InitialValue="0">Please Select Location</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                            </div>



                                            <div class="col-md-4">



                                                <h4 style="color: #f05423">&nbsp; Other Details</h4>

                                                <div class="form-group" style="margin-top: 15px;">
                                                    <label class="col-md-3 control-label">Item Group : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:DropDownList ID="ddlItemGroup" runat="server" CssClass="form-control" TabIndex="8"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlItemGroup" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                            ErrorMessage="Please Select Item Group" InitialValue="0">Please Select Item Group</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Requisitioner : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:TextBox ID="txtRequisitioner" runat="server" placeholder="Requisitioner" CssClass="form-control" TabIndex="11"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group" style="margin-top: 20px;">
                                                    <label class="col-md-3 control-label">Tracking No. : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:TextBox ID="txtTrackNo" runat="server" placeholder="Tracking No." CssClass="form-control" TabIndex="17"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator ID="rfvTrackNo" ControlToValidate="txtTrackNo" runat="server" ValidationGroup="SaveItem"
                                                            ErrorMessage="Please  Enter Tracking No.">*</asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>


                                                <div class="form-group" style="margin-top: 20px;">
                                                    <label class="col-md-3 control-label">Cost Center : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <%--<asp:TextBox ID="txtCostCenter" runat="server" placeholder="Cost Center" CssClass="form-control" ReadOnly="true" TabIndex="14"></asp:TextBox>--%>
                                                        <asp:DropDownList ID="ddlCostCenter" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvCostCenter" ControlToValidate="ddlCostCenter" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                            ErrorMessage="Please Select Cost Center" InitialValue="0">Please Select Cost Center</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>








                                            </div>



                                        </div>

                                        <div class="panel-footer">
                                            <asp:LinkButton ID="btnSaveDet" runat="server" CssClass="btn btn-success pull-right" OnClick="btnSaveDet_Click" Text="Save Item" ValidationGroup="SaveItem" TabIndex="19"><i class="fa fa-plus-square"></i></asp:LinkButton>

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

                                                <asp:TemplateField HeaderText="Item Desc.">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblItemDesc" Text='<%# Bind("ITEMDESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Spec.">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblItemSpec" Text='<%# Bind("ITEMSPEC") %>'></asp:Label>
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
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Rate" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">--%>
                                                <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblRate" Text='<%# Bind("ITEMRATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Amount" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">--%>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblAmount" Text='<%# Bind("ITEMAMOUNT") %>'></asp:Label>
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
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Location Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblLocationCDID" Text='<%# Bind("LOCCDID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Requisitioner">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblRequisitioner" Text='<%# Bind("REQUISITIONER") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Track No.">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblTrackNo" Text='<%# Bind("TRACKNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Remark">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblItemText" Text='<%# Bind("ITEMTEXT") %>'></asp:Label>
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

            </div>
        </div>

    </div>


    








    <%--<asp:ValidationSummary ID="validate" runat="server" ShowSummary="false" ShowMessageBox="true" ValidationGroup="SaveItem" />
    <asp:ValidationSummary ID="validate1" runat="server" ShowSummary="false" ShowMessageBox="true" ValidationGroup="SaveAll" />--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranMMMR" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />

</asp:Content>
