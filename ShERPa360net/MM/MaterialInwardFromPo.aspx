<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="MaterialInwardFromPo.aspx.cs" Inherits="ShERPa360net.MM.MaterialInwardFromPo" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }
    </style>

    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            /*debugger;*/
            var fu = $("#ContentPlaceHolder1_fuInvDoc").val();
            var challan = $("#ContentPlaceHolder1_txtChalanNo").val();
            var pono = $("#ContentPlaceHolder1_txtPoNo").val();

            if (fu != "" && challan != "" && pono != "") {
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Material </strong>Inward Entry</h3>
                            <asp:LinkButton ID="imgCancel" runat="server" CssClass="btn btn-success pull-right" OnClick="imgCancel_Click" Text="Cancel"><i class="fa fa-times"></i></asp:LinkButton>
                            <asp:LinkButton ID="imgSaveAll" runat="server" CssClass="btn btn-success pull-right" OnClientClick="ShowLoading()" Text="Save All" OnClick="imgSaveAll_Click" ValidationGroup="SaveAll"><i class="fa fa-save"></i></asp:LinkButton>
                            <div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                <label>Please wait...</label>
                            </div>
                        </div>



                        <div class="panel-body">
                            <div class="row">

                                <div class="col-md-12">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Upload Invoice : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <%--<asp:TextBox ID="TextBox1" runat="server" Enabled="false" CssClass="form-control required_text_box" placeholder="Doc No"></asp:TextBox>--%>
                                                        <asp:FileUpload ID="fuInvDoc" runat="server" CssClass="form-control" ViewStateMode="Enabled" />
                                                        <asp:RequiredFieldValidator ID="rfvFileUpload" Style="color: red;" runat="server" ControlToValidate="fuInvDoc" ValidationGroup="SaveAll"
                                                            ErrorMessage="Please Upload Invoice Image">
                                                            Please Upload Invoice Image</asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revFileUpload" runat="server" ErrorMessage="Only Image files are allowed" Style="color: red;"
                                                            ControlToValidate="fuInvDoc" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.jpg|.JPG|.png|.PNG|.jpeg|.JPEG|.pdf|.PDF)$">
                                                        </asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Upload PO : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <%--<asp:TextBox ID="TextBox1" runat="server" Enabled="false" CssClass="form-control required_text_box" placeholder="Doc No"></asp:TextBox>--%>
                                                        <asp:FileUpload ID="fuPODoc" runat="server" CssClass="form-control" ViewStateMode="Enabled" />
                                                        <asp:RequiredFieldValidator ID="rfvPOUpload" Style="color: red;" runat="server" ControlToValidate="fuPODoc" ValidationGroup="SaveAll"
                                                            ErrorMessage="Please Upload PO Image">
                                                            Please Upload PO Image</asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="revPOUpload" runat="server" ErrorMessage="Only Image files are allowed" Style="color: red;"
                                                            ControlToValidate="fuPODoc" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))+(.jpg|.JPG|.png|.PNG|.jpeg|.JPEG|.pdf|.PDF)$">
                                                        </asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                                <asp:UpdatePanel ID="pnlprimaryDetails" runat="server" UpdateMode="Conditional">
                                    <triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtPoNo" EventName="TextChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="txtItemCode" EventName="TextChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="txtTransporter" EventName="TextChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="btnSaveDet" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="gvDetail" EventName="RowCommand" />

                                    </triggers>
                                    <contenttemplate>
                                        <div class="col-md-12">
                                            <h4 style="color: #f05423">Primary Details</h4>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Doc. Type : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtDocType" runat="server" Text="103" Enabled="false" CssClass="form-control required_text_box" placeholder="Doc Type"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvDocType" Style="color: red;" runat="server" ControlToValidate="txtDocType" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Doc Type">
                                                                    Please Enter Doc Type</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Doc No : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtDocNo" runat="server" Enabled="false" CssClass="form-control required_text_box" placeholder="Doc No"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvDocNo" Style="color: red;" runat="server" ControlToValidate="txtDocNo" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Doc No">
                                                                    Please Enter Doc No</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Doc Date : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <div class="input-group">
                                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                    <asp:TextBox ID="txtDocDate" runat="server" placeholder="Doc Date" class="form-control datepicker required_text_box"  Enabled="false" Style="height : 30px !important;" AutoCompleteType="None"></asp:TextBox>
                                                                </div>
                                                                <asp:RequiredFieldValidator ID="rfvDocDate" Style="color: red;" runat="server" ControlToValidate="txtDocDate" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Doc Date">
                                                                    Please Enter Doc Date</asp:RequiredFieldValidator>
                                                            </div>

                                                        </div>
                                                    </div>





                                                </div>

                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Ref No : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtRefNo" runat="server" CssClass="form-control" placeholder="Ref No"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Po No : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtPoNo" runat="server" AutoPostBack="true" CssClass="form-control required_text_box" OnTextChanged="txtPoNo_TextChanged" placeholder="Po No"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvPoNo" runat="server" Style="color: red;" ControlToValidate="txtPoNo" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Po No">
                                                                    Please Enter Po No</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Transporter : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtTransporter" runat="server" CssClass="form-control" placeholder="Transporter Code" OnTextChanged="txtTransporter_TextChanged" MaxLength="10" TextMode="Number" AutoPostBack="true"></asp:TextBox>
                                                                <asp:Label ID="lblTransporterError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Transporter Name : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtTransporterName" runat="server" CssClass="form-control" placeholder="Transporter Name" ReadOnly="true"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Challan No : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtChalanNo" runat="server" CssClass="form-control required_text_box" placeholder="Challan No"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvChalanNo" Style="color: red;" runat="server" ControlToValidate="txtChalanNo" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Challan No">
                                                                    Please Enter Challan No</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Challan Date : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <div class="input-group">
                                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                    <asp:TextBox ID="txtChalanDt" runat="server" placeholder="Challan Date" class="form-control datepicker required_text_box" AutoCompleteType="None"></asp:TextBox>
                                                                </div>
                                                                <asp:RequiredFieldValidator ID="rfvChalandt" Style="color: red;" runat="server" ControlToValidate="txtChalanDt" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Challan Date">
                                                                    Please Enter Challan Date</asp:RequiredFieldValidator>
                                                            </div>

                                                        </div>
                                                    </div>


                                                    <div class="col-md-5">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">Remark : </label>
                                                            <div class="col-md-9 col-xs-12">
                                                                <asp:TextBox ID="txtRemark" runat="server" placeholder="Remarks" class="form-control"></asp:TextBox>
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
                                                                                <label class="col-md-4 control-label">Sr. No. : </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:TextBox ID="txtSRNo" runat="server" CssClass="form-control" placeholder="Sr. No." Enabled="false"></asp:TextBox>
                                                                                    <asp:TextBox ID="txtMaxSrNo" runat="server" placeholder="Max Sr. No." CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">PO Sr No. : </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:TextBox ID="txtPRNo" runat="server" CssClass="form-control" placeholder="PO Sr. No." Enabled="false"></asp:TextBox>
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
                                                                                        <asp:TextBox ID="txtItemCode" runat="server" placeholder="Item Code" AutoPostBack="true" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Item Desc. :</label>
                                                                                <div class="col-md-8">
                                                                                    <asp:TextBox ID="txtItemDesc" runat="server" placeholder="Item Description" class="form-control" ReadOnly="true"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Qty : </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:TextBox ID="txtItemQty" runat="server" CssClass="form-control required_text_box" placeholder="Quantity" AutoPostBack="true"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="rfvItemQty" Style="color: red;" ControlToValidate="txtItemQty" runat="server" ValidationGroup="SaveItem"
                                                                                        ErrorMessage="Please  Enter Item Qty">
                                                                                        Please  Enter Item Qty</asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Challan Qty : </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:TextBox ID="txtChalanQty" runat="server" CssClass="form-control" placeholder="Challan Qty"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <%--<div class="col-md-12" style="margin-top: 10px;">
                                                                <div class="col-md-3">
                                                                    <div class="form-group">
                                                                        <label class="col-md-4 control-label"></label>
                                                                        <div class="col-md-8">
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
                                                                </div>

                                                            </div>--%>

                                                                    <div class="col-md-12" style="margin-top: 10px;">

                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Received Qty : </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:TextBox ID="txtReceivedQty" runat="server" Enabled="false" CssClass="form-control" placeholder="Received Qty"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Po Qty : </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:TextBox ID="txtMPoQty" runat="server" Enabled="false" CssClass="form-control" placeholder="Po Qty"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">UOM : </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:DropDownList ID="ddlUOM" runat="server" CssClass="form-control" AutoPostBack="true" Enabled="false"></asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Plant : </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:DropDownList ID="ddlPLant" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>

                                                                    <div class="col-md-12" style="margin-top: 10px;">
                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Location : </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control required_text_box" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="rfvLocation" Style="color: red;" ControlToValidate="ddlLocation" runat="server" ValidationGroup="SaveItem"
                                                                                        ErrorMessage="Please Select Location" InitialValue="0">
                                                                                        Please Select Location</asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Cost Center : </label>
                                                                                <div class="col-md-8">
                                                                                    <%--<asp:TextBox ID="txtCostCenter" runat="server" placeholder="Cost Center" CssClass="form-control"></asp:TextBox>--%>

                                                                                    <asp:DropDownList ID="ddlCostCenter" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="rfvCostCenter" ControlToValidate="ddlCostCenter" runat="server" ValidationGroup="SaveItem"
                                                                                        ErrorMessage="Please Select Cost Center" InitialValue="0">
                                                                                        Please Select Cost Center</asp:RequiredFieldValidator>


                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                        <div class="col-md-6">
                                                                            <div class="form-group">
                                                                                <label class="col-md-2 control-label">Item Reamark : </label>
                                                                                <div class="col-md-10">
                                                                                    <asp:TextBox ID="txtItemRemark" runat="server" placeholder="Remark" CssClass="form-control"></asp:TextBox>
                                                                                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdMItemId" />
                                                                                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdMGrpId" />
                                                                                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdMRate" />
                                                                                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdMCAmt" />
                                                                                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdMprfcnt" />
                                                                                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdMGlcd" />
                                                                                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdMAssetCd" />
                                                                                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdMID" />
                                                                                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdMPoSrNo" />
                                                                                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdMItemGrpDesc" />
                                                                                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdMPoQty" />

                                                                                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hfdAPRVSTATUS" />
                                                                                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hfdAPRVSTATUSDESC" />

                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>

                                                                    <div class="col-md-12" style="margin-top: 10px;">
                                                                        <div class="col-md-12 text-center">
                                                                            <asp:LinkButton ID="btnSaveDet" Visible="false" OnClick="btnSaveDet_Click" runat="server" CssClass="btn btn-success" Text="Save Item" ValidationGroup="SaveItem"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="row" style="margin-top: 10px!important;">
                                                                <div class="col-md-12">
                                                                    <div class="panel panel-default">
                                                                        <div class="panel-body">
                                                                            <div class="row">
                                                                                <div class="col-md-12">
                                                                                    <div class="box">
                                                                                        <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                                                                            <asp:GridView ID="gvDetail" OnRowCommand="gvDetail_RowCommand" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                                                                                CssClass="table table-hover table-striped table-bordered nowrap">
                                                                                                <columns>
                                                                                                    <asp:BoundField DataField="PONO" HeaderText="PO No." />
                                                                                                    <asp:BoundField DataField="POSRNO" HeaderText="PO Sr. No." />
                                                                                                    <asp:BoundField DataField="SRNO" HeaderText="Sr. No." />
                                                                                                    <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                                                                    <asp:BoundField DataField="ITEMDESC" HeaderText="Item Description" />
                                                                                                    <asp:BoundField DataField="ITEMGRP" HeaderText="Item Group" />
                                                                                                    <asp:BoundField DataField="UOMDesc" HeaderText="UOM" />
                                                                                                    <asp:BoundField DataField="QTY" HeaderText="Quantity" />
                                                                                                    <asp:BoundField DataField="CHLNQTY" HeaderText="Challan Quantity" />
                                                                                                    <asp:BoundField DataField="GLCD" HeaderText="GL Code" />
                                                                                                    <asp:BoundField DataField="CSTCENTCD" HeaderText="Cost Center" />
                                                                                                    <asp:BoundField DataField="PLANTCD" HeaderText="Plant Code" />
                                                                                                    <asp:BoundField DataField="LOCCD" HeaderText="Location Code" />
                                                                                                    <asp:BoundField DataField="PRFCNT" HeaderText="Profit Center" />
                                                                                                    <asp:BoundField DataField="ASSETCD" HeaderText="Asset Code" />
                                                                                                    <asp:BoundField DataField="ITEMTEXT" HeaderText="Item Text" />
                                                                                                    <asp:TemplateField HeaderText="Action">
                                                                                                        <itemtemplate>
                                                                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("ID") %>'
                                                                                                                CommandName="eDelete" OnClientClick="return confirm('Are you sure you want to delete this record?');">
                                                                                                                Delete</asp:LinkButton>
                                                                                                            |
                                                                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("ID") %>'
                                                                                                                CommandName="eEdit">
                                                                                                                Edit</asp:LinkButton>
                                                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdPoNo" Value='<%# Bind("PONO") %>' />
                                                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdPoSrNo" Value='<%# Bind("POSRNO") %>' />
                                                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdSrNo" Value='<%# Bind("SRNO") %>' />
                                                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdItemCode" Value='<%# Bind("ITEMCODE") %>' />
                                                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdItemDesc" Value='<%# Bind("ITEMDESC") %>' />
                                                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdItemGrp" Value='<%# Bind("ITEMGRP") %>' />
                                                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdUomDesc" Value='<%# Bind("UOMDesc") %>' />
                                                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdQty" Value='<%# Bind("QTY") %>' />
                                                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdChalanQty" Value='<%# Bind("CHLNQTY") %>' />
                                                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdGlCd" Value='<%# Bind("GLCD") %>' />
                                                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdCostCenter" Value='<%# Bind("CSTCENTCD") %>' />
                                                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdPlantCode" Value='<%# Bind("PLANTCD") %>' />
                                                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdLoccd" Value='<%# Bind("LOCCD") %>' />
                                                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdprfct" Value='<%# Bind("PRFCNT") %>' />
                                                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdAssetcd" Value='<%# Bind("ASSETCD") %>' />
                                                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdItemText" Value='<%# Bind("ITEMTEXT") %>' />
                                                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdITEMID" Value='<%# Bind("ITEMID") %>' />
                                                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdITEMGRPID" Value='<%# Bind("ITEMGRPID") %>' />
                                                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdUOMID" Value='<%# Bind("UOM") %>' />
                                                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdRATE" Value='<%# Bind("RATE") %>' />
                                                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdCAMOUNT" Value='<%# Bind("CAMOUNT") %>' />
                                                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdID" Value='<%# Bind("ID") %>' />
                                                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdPoQty" Value='<%# Bind("POQTY") %>' />
                                                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdReceivedQty" Value='<%# Bind("RECVDQTY") %>' />
                                                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdAPRVSTATUS" Value='<%# Bind("APRVSTATUS") %>' />
                                                                                                            <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdAPRVSTATUSDESC" Value='<%# Bind("APRVSTATUSDESC") %>' />
                                                                                                        </itemtemplate>
                                                                                                    </asp:TemplateField>

                                                                                                    <asp:TemplateField HeaderText="APRVSTATUS" Visible="true">
                                                                                                        <itemtemplate>
                                                                                                            <asp:Label ID="lblAPRVSTATUS" runat="server" Text='<%# Eval("APRVSTATUS") %>'></asp:Label>
                                                                                                            <asp:Label ID="lblAPRVSTATUSDESC" runat="server" Text='<%# Eval("APRVSTATUSDESC") %>'></asp:Label>
                                                                                                        </itemtemplate>
                                                                                                    </asp:TemplateField>

                                                                                                </columns>
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

                                                        <div class="panel-footer">
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </contenttemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="page-content-wrap">
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <script type="text/javascript" src="../js/MaterialInward.js"></script>
    <input type="hidden" id="menutabid" value="tsmTranMMMIRPO" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />
</asp:Content>
