<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="CreatePartRequest.aspx.cs" Inherits="ShERPa360net.MM.CreatePartRequest" %>

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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Part  </strong>Request</h3>

                            <asp:LinkButton ID="lnkCancel" runat="server" CssClass="btn btn-success pull-right" PostBackUrl="~/MM/ViewPartRequest.aspx" Text="Cancel"><i class="fa fa-times"></i></asp:LinkButton>
                            <asp:LinkButton ID="lnkSaveAll" OnClientClick="ShowLoading()" runat="server" CssClass="btn btn-success pull-right" OnClick="lnkSaveAll_Click" Text="Save All" ValidationGroup="SaveAll"><i class="fa fa-save"></i></asp:LinkButton>
                            <div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                <label>Please wait...</label>
                            </div>
                        </div>


                        <asp:UpdatePanel ID="updItemDetails" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <%--<asp:PostBackTrigger ControlID="txtItemQty" />--%>
                                <asp:AsyncPostBackTrigger ControlID="txtJobId" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="txtItemCode" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlPLant" EventName="SelectedIndexChanged" />
                                <asp:AsyncPostBackTrigger ControlID="ddlUOM" EventName="SelectedIndexChanged" />
                            </Triggers>
                            <ContentTemplate>


                                <div class="panel-body">

                                    <div class="row">

                                        <div class="col-md-12">

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Job Id : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:TextBox ID="txtJobId" runat="server" CssClass="form-control required_text_box" placeholder="Job Id" OnTextChanged="txtJobId_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvJobId" runat="server" ControlToValidate="txtJobId" ValidationGroup="Save" Style="color: red;"
                                                            ErrorMessage="Please Enter Job Id">Please Enter Job Id</asp:RequiredFieldValidator>


                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <%--<label class="col-md-5 control-label">Segment : </label>--%>
                                                    <label class="col-md-5 control-label" id="lblSegment" runat="server" visible="false"></label>
                                                    <label class="col-md-5 control-label" id="lblSegmentDesc" runat="server"></label>

                                                    <div class="col-md-7 col-xs-12">

                                                        <%-- <asp:Label ID="lblSegment" runat="server" CssClass="form-control"></asp:Label>
                                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Job Id" OnTextChanged="txtJobId_TextChanged"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtJobId" ValidationGroup="Save"
                                                            ErrorMessage="Please Enter Job Id">*</asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <%--<label class="col-md-5 control-label">Job Status : </label>--%>
                                                    <label class="col-md-5 control-label" id="lblJobStatus" runat="server" visible="true"></label>
                                                    <div class="col-md-7 col-xs-12">

                                                        <%--<asp:Label ID="lblJobStatus" runat="server" CssClass="form-control"></asp:Label>
                                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="Job Id" OnTextChanged="txtJobId_TextChanged"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtJobId" ValidationGroup="Save"
                                                            ErrorMessage="Please Enter Job Id">*</asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Item Code : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <div class="input-group">

                                                            <asp:TextBox ID="txtItemCode" runat="server" CssClass="form-control required_text_box" placeholder="Item Code" OnTextChanged="txtItemCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            <span class="input-group-btn">
                                                                <asp:LinkButton ID="lnkOpenPoup" runat="server" CssClass="btn btn-success" OnClick="lnkOpenPoup_Click" Text="Open Popup"><span class="fa fa-search"></span></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkNewPartCode" runat="server" CssClass="btn btn-success" OnClick="lnkNewPartCode_Click" Text="Open Popup"><span class="fa fa-plus-circle"></span></asp:LinkButton>
                                                            </span>

                                                        </div>
                                                        <asp:RequiredFieldValidator ID="rfvItemCode" runat="server" ControlToValidate="txtItemCode" ValidationGroup="Save" Style="color: red;"
                                                            ErrorMessage="Please Enter Item Code">Please Enter Item Code</asp:RequiredFieldValidator>
                                                        <asp:Button ID="btnPopup" runat="server" AccessKey="Z" OnClick="lnkOpenPoup_Click" CssClass="hidden" />


                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <%--<label class="col-md-5 control-label">Item Desc. : </label>--%>
                                                    <label class="col-md-7 control-label" id="lblItemDesc" runat="server"></label>
                                                    <label class="col-md-5 control-label" id="lblItemId" runat="server" visible="false"></label>
                                                    <label class="col-md-5 control-label" id="lblSKU" runat="server" visible="false"></label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <%--<asp:Label ID="lblItemDesc" runat="server"></asp:Label>
                                                        <asp:Label ID="lblItemId" runat="server"></asp:Label>--%>
                                                        <%--<asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Item Code"></asp:TextBox>--%>
                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtItemCode" ValidationGroup="Save"
                                                            ErrorMessage="Please Enter Item Code">*</asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Quantity : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control required_text_box" placeholder="Quantity" Text="1"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ControlToValidate="txtQuantity" ValidationGroup="Save" Style="color: red;"
                                                            ErrorMessage="Please Enter Quantity">Please Enter Quantity</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">UOM :</label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:DropDownList ID="ddlUOM" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlUOM_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvUOM" ControlToValidate="ddlUOM" runat="server" ValidationGroup="Save" Style="color: red;"
                                                            ErrorMessage="Please Select Item UOM" InitialValue="0">Please Select Item UOM</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Plant Code : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:DropDownList ID="ddlPLant" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPLant_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvPLant" ControlToValidate="ddlPLant" runat="server" ValidationGroup="Save" Style="color: red;"
                                                            ErrorMessage="Please Select Plant" InitialValue="0">Please Select Plant</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Location : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvLocation" ControlToValidate="ddlLocation" runat="server" ValidationGroup="Save" Style="color: red;"
                                                            ErrorMessage="Please Select Location" InitialValue="0">Please Select Location</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Req. By : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:DropDownList ID="ddlReqBy" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvReqBy" ControlToValidate="ddlReqBy" runat="server" ValidationGroup="Save" Style="color: red;"
                                                            ErrorMessage="Please Select Requested By" InitialValue="0">Please Select Requested By</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>


                                        </div>
                                    </div>

                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div class="panel-footer">
                            <%--<button type="button" class="btn btn-success pull-right"><i class="fa fa-plus-square"></i></button>--%>
                            <asp:LinkButton ID="btnSaveDet" runat="server" CssClass="btn btn-success pull-right" OnClick="btnSaveDet_Click" Text="Save Item" ValidationGroup="Save"><i class="fa fa-plus-square"></i></asp:LinkButton>
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

                                                <asp:TemplateField HeaderText="DOC Date">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDocDt" Text='<%# Bind("DOCDT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Segmnet" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblGvSegment" Text='<%# Bind("SEGMENT") %>'></asp:Label>
                                                        <asp:Label runat="server" ID="lblGvSegmentDesc" Text='<%# Bind("SEGMENTDESC") %>'></asp:Label>
                                                        <asp:Label runat="server" ID="lblGvJobStatus" Text='<%# Bind("JOBSTATUS") %>'></asp:Label>
                                                        <asp:Label runat="server" ID="lblGvSku" Text='<%# Bind("SKU") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Job Id">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblJobID" Text='<%# Bind("JOBID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <%--<EditItemTemplate>
                                                    <asp:TextBox ID="txtEditItemCode" runat="server" Text='<%# Bind("ITEMCODE") %>' OnTextChanged="txtEditItemCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </EditItemTemplate>--%>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblGvItemId" Text='<%# Bind("ITEMID") %>'></asp:Label>


                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Item Code">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblGvItemCode" Text='<%# Bind("ITEMCODE") %>'></asp:Label>


                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Item Desc">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblGvItemDesc" Text='<%# Bind("ITEMDESC") %>'></asp:Label>


                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="UOM" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblUOM" Text='<%# Bind("UOM") %>'></asp:Label>


                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="UOM">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblUOMName" Text='<%# Bind("UOMNAME") %>'></asp:Label>


                                                    </ItemTemplate>
                                                </asp:TemplateField>





                                                <asp:TemplateField HeaderText="QTY">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblQTY" Text='<%# Bind("QTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="PLANTCD">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPlantcd" Text='<%# Bind("PLANTCD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="LOCCD">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblLoccd" Text='<%# Bind("LOCCD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <%--<EditItemTemplate>
                                                    <asp:DropDownList ID="ddlEditUOM" runat="server"></asp:DropDownList>
                                                </EditItemTemplate>--%>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="REQBY">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblReqBy" Text='<%# Bind("REQBY") %>' Visible="false"></asp:Label>
                                                        <asp:Label runat="server" ID="lblReqByName" Text='<%# Bind("REQBYNAME") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="STATUS" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblStatus" Text='<%# Bind("STATUS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <%--<EditItemTemplate>
                                                    <asp:TextBox ID="txtEditQty" runat="server" Text='<%# Bind("ITEMQTY") %>' OnTextChanged="txtEditQty_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </EditItemTemplate>--%>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CREATE BY" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblCreateBy" Text='<%# Bind("CREATEBY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <%--<EditItemTemplate>
                                                    <asp:TextBox ID="txtEditRate" runat="server" Text='<%# Bind("ITEMRATE") %>' OnTextChanged="txtEditRate_TextChanged" AutoPostBack="true"></asp:TextBox>
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

                                                <%--<asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblSKU" Text='<%# Bind("SKU") %>'></asp:Label>
                                                        <asp:Label runat="server" ID="lblMake" Text='<%# Bind("MAKE") %>'></asp:Label>
                                                        <asp:Label runat="server" ID="lblModel" Text='<%# Bind("MODEL") %>'></asp:Label>
                                                        <asp:Label runat="server" ID="lblDispName" Text='<%# Bind("DISPNAME") %>'></asp:Label>
                                                        <asp:Label runat="server" ID="lblDispMRP" Text='<%# Bind("DISPMRP") %>'></asp:Label>
                                                        <asp:Label runat="server" ID="lblValueLimit" Text='<%# Bind("VALUELIMIT") %>'></asp:Label>
                                                        <asp:Label runat="server" ID="lblMaxStkQty" Text='<%# Bind("MAXSTKQTY") %>'></asp:Label>
                                                        <asp:Label runat="server" ID="lblHSN" Text='<%# Bind("HSN") %>'></asp:Label>
                                                        <asp:Label runat="server" ID="lblHSNGroup" Text='<%# Bind("HSNGROUP") %>'></asp:Label>
                                                        <asp:Label runat="server" ID="lblHSNGroupDesc" Text='<%# Bind("HSNGROUPDESC") %>'></asp:Label>
                                                        <asp:Label runat="server" ID="lblCondType" Text='<%# Bind("CONDTYPE") %>'></asp:Label>
                                                        <asp:Label runat="server" ID="lblItemStatus" Text='<%# Bind("ITEMSTATUS") %>'></asp:Label>
                                                        <asp:Label runat="server" ID="lblOnHandStock" Text='<%# Bind("ONHANDSTOCK") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
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
    <%--  </ContentTemplate>
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
                                        <label>Item Spec. :</label>
                                        <asp:TextBox ID="txtNewItemSpecification" runat="server" CssClass="form-control" placeholder="Item Description"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvNewItemSpec" runat="server" ControlToValidate="txtNewItemSpecification" Style="color: red;"
                                            ErrorMessage="Enter Item Spec.." ValidationGroup="NewItem">Enter Item Spec..</asp:RequiredFieldValidator>
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







    <%--<asp:ValidationSummary ID="validate" runat="server" ShowSummary="false" ShowMessageBox="true" ValidationGroup="Save" />
    <asp:ValidationSummary ID="validate1" runat="server" ShowSummary="false" ShowMessageBox="true" ValidationGroup="SaveAll" />
    <asp:ValidationSummary ID="validate2" runat="server" ShowSummary="false" ShowMessageBox="true" ValidationGroup="NewItem" />--%>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmTranPPPartReq" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />
</asp:Content>
