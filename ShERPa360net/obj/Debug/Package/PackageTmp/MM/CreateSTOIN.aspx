<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="CreateSTOIN.aspx.cs" Inherits="ShERPa360net.MM.CreateSTOIN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }
    </style>


    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            var pono = $("#ContentPlaceHolder1_txtPoNo").val();
            var challan = $("#ContentPlaceHolder1_txtChalanNo").val();
            var tran = $("#ContentPlaceHolder1_txtTransporter").val();
            var name = $("#ContentPlaceHolder1_txtTransporterName").val();
            if (pono != "" && challan != "" && tran != "" && name != "") {
                document.getElementById("busy-holder1").style.display = "";
                document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";
            }
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--   <asp:UpdatePanel ID="pnlprimaryDetails" runat="server" UpdateMode="Conditional">
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
                                    <asp:LinkButton ID="imgCancel" runat="server" CssClass="btn btn-success pull-right" PostBackUrl="~/MM/ViewMaterialIssue.aspx?REQUESTFORM=STOIN" Text="Cancel"><i class="fa fa-times"></i></asp:LinkButton>
                                    <asp:LinkButton ID="imgSaveAll" runat="server" CssClass="btn btn-success pull-right" Text="Save All" OnClick="imgSaveAll_Click" ValidationGroup="SaveAll"><i class="fa fa-save"></i></asp:LinkButton>
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
                                                                <asp:RequiredFieldValidator ID="rfvDocType" runat="server" ControlToValidate="ddlDoctype" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Select Doc. Type" InitialValue="0">*</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Doc. No. : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtDocNo" runat="server" Enabled="false" CssClass="form-control required_text_box" placeholder="Doc. No."></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvDocNo" Style="color: red;" runat="server" ControlToValidate="txtDocNo" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Doc. No.">*</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Doc. Date : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtDocDt" runat="server" Enabled="false" CssClass="form-control required_text_box" placeholder="Doc. Date"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RfvDocDate" Style="color: red;" runat="server" ControlToValidate="txtDocDt" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Doc Date">*</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Ref. No. : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtRefno" runat="server" CssClass="form-control" placeholder="Ref. No."></asp:TextBox>
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
                                                                    ErrorMessage="Please Enter PO No.">*</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Transporter : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtTranCode" runat="server" CssClass="form-control" placeholder="Transporter Code" OnTextChanged="txtTranCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="color: red;" runat="server" ControlToValidate="txtTranCode" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Transporter Code">*</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Transporter Name : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtTransporterName" runat="server" CssClass="form-control" placeholder="Transporter Name" Enabled="false" Width="505"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" runat="server" ControlToValidate="txtTransporterName" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Transporter Code">*</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Challan No. : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtChallanNo" runat="server" CssClass="form-control" placeholder="Challan No."></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Style="color: red;" runat="server" ControlToValidate="txtPONO" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter PO No.">*</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">No. Of Boxes : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtNoOfBoxes" runat="server" CssClass="form-control" placeholder="No. of Boxes"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Reamark : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" placeholder="Ramark" Width="505"></asp:TextBox>
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

            <div class="page-content-wrap">

                <div class="row">
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
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>--%>


    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Material </strong>Inward Entry</h3>
                            <asp:LinkButton ID="imgCancel" runat="server" CssClass="btn btn-success pull-right" PostBackUrl="~/MM/ViewMaterialIssue.aspx?REQUESTFORM=STOIN" Text="Cancel"><i class="fa fa-times"></i></asp:LinkButton>
                            <asp:LinkButton ID="imgSaveAll" runat="server" CssClass="btn btn-success pull-right" OnClientClick="ShowLoading()" Text="Save All" OnClick="imgSaveAll_Click" ValidationGroup="SaveAll"><i class="fa fa-save"></i></asp:LinkButton>
                            <div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                <label>Please wait...</label>
                            </div>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <asp:UpdatePanel ID="pnlprimaryDetails" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtPoNo" EventName="TextChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="txtItemCode" EventName="TextChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="txtTransporter" EventName="TextChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="btnSaveDet" EventName="Click" />
                                        <asp:AsyncPostBackTrigger ControlID="gvDetail" EventName="RowCommand" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <div class="col-md-12">
                                            <h4 style="color: #f05423">Primary Details</h4>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Doc. Type : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtDocType" runat="server" Text="603" Enabled="false" CssClass="form-control required_text_box" placeholder="Doc Type"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvDocType" Style="color: red;" runat="server" ControlToValidate="txtDocType" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Doc Type">Please Enter Doc Type</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Doc No : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtDocNo" runat="server" Enabled="false" CssClass="form-control required_text_box" placeholder="Doc No"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvDocNo" Style="color: red;" runat="server" ControlToValidate="txtDocNo" ValidationGroup="SaveAll"
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
                                                                    <asp:TextBox ID="txtDocDate" runat="server" placeholder="Doc Date" class="form-control datepicker required_text_box" AutoCompleteType="None"></asp:TextBox>
                                                                </div>
                                                                <asp:RequiredFieldValidator ID="rfvDocDate" Style="color: red;" runat="server" ControlToValidate="txtDocDate" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Doc Date">Please Enter Doc Date</asp:RequiredFieldValidator>
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
                                                                    ErrorMessage="Please Enter Po No">Please Enter Po No</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Transporter : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtTransporter" runat="server" CssClass="form-control" placeholder="Transporter Code" OnTextChanged="txtTransporter_TextChanged" MaxLength="10" TextMode="Number" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                                <asp:Label ID="lblTransporterError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="rfvTransporter" runat="server" Style="color: red;" ControlToValidate="txtTransporter" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Transpoerter Code">Please Enter Transpoerter Code</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Transporter Name : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtTransporterName" runat="server" CssClass="form-control" placeholder="Transporter Name" ReadOnly="true"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvTransporterName" runat="server" Style="color: red;" ControlToValidate="txtTransporterName" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Transpoerter Name">Please Enter Transpoerter Name</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Challan No : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtChalanNo" runat="server" CssClass="form-control required_text_box" placeholder="Challan No" OnTextChanged="txtChalanNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvChalanNo" Style="color: red;" runat="server" ControlToValidate="txtChalanNo" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Challan No">Please Enter Challan No</asp:RequiredFieldValidator>
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
                                                                    ErrorMessage="Please Enter Challan Date">Please Enter Challan Date</asp:RequiredFieldValidator>
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
                                                                                    <asp:TextBox ID="txtPoSrNo" runat="server" CssClass="form-control" placeholder="PO Sr. No." Enabled="false"></asp:TextBox>
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
                                                                                    <asp:TextBox ID="txtItemQty" runat="server" CssClass="form-control required_text_box" placeholder="Quantity" OnTextChanged="txtItemQty_TextChanged" AutoPostBack="true" Enabled="false"></asp:TextBox>
                                                                                    <asp:RequiredFieldValidator ID="rfvItemQty" Style="color: red;" ControlToValidate="txtItemQty" runat="server" ValidationGroup="SaveItem"
                                                                                        ErrorMessage="Please  Enter Item Qty">Please  Enter Item Qty</asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Challan Qty : </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:TextBox ID="txtChalanQty" runat="server" CssClass="form-control" placeholder="Challan Qty" Enabled="false"></asp:TextBox>
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

                                                                        <%--<div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Received Qty : </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:TextBox ID="txtReceivedQty" runat="server" Enabled="false" CssClass="form-control" placeholder="Received Qty"></asp:TextBox>
                                                                                </div>
                                                                            </div>
                                                                        </div>--%>

                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Po Qty : </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:TextBox ID="txtMPoQty" runat="server" Enabled="false" CssClass="form-control" placeholder="Po Qty"></asp:TextBox>
                                                                                    <asp:HiddenField ID="hfPndQty" runat="server" />
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

                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Location : </label>
                                                                                <div class="col-md-8">
                                                                                    <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control required_text_box" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="rfvLocation" Style="color: red;" ControlToValidate="ddlLocation" runat="server" ValidationGroup="SaveItem"
                                                                                        ErrorMessage="Please Select Location" InitialValue="0">Please Select Location</asp:RequiredFieldValidator>
                                                                                </div>
                                                                            </div>
                                                                        </div>

                                                                    </div>

                                                                    <div class="col-md-12" style="margin-top: 10px;">


                                                                        <div class="col-md-3">
                                                                            <div class="form-group">
                                                                                <label class="col-md-4 control-label">Cost Center : </label>
                                                                                <div class="col-md-8">
                                                                                    <%--<asp:TextBox ID="txtCostCenter" runat="server" placeholder="Cost Center" CssClass="form-control"></asp:TextBox>--%>

                                                                                    <asp:DropDownList ID="ddlCostCenter" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="rfvCostCenter" ControlToValidate="ddlCostCenter" runat="server" ValidationGroup="SaveItem"
                                                                                        ErrorMessage="Please Select Cost Center" InitialValue="0">*</asp:RequiredFieldValidator>

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
                                                                                                <Columns>
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
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("ID") %>'
                                                                                                                CommandName="eDelete" OnClientClick="return confirm('Are you sure you want to delete this record?');">Delete</asp:LinkButton>
                                                                                                            |
                                                                                                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("ID") %>'
                                                                                                                CommandName="eEdit">Edit</asp:LinkButton>
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
                                                                                                            <%--<asp:HiddenField runat="server" ClientIDMode="Static" ID="hdReceivedQty" Value='<%# Bind("RECVDQTY") %>' />--%>
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

                                                        <div class="panel-footer">
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
    <div class="page-content-wrap">
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmTranMMSTOIn" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />
</asp:Content>
