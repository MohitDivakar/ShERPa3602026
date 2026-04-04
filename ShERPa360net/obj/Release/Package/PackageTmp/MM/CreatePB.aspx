<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="CreatePB.aspx.cs" Inherits="ShERPa360net.MM.CreatePB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script type="text/javascript">
        $("#lnkSaveCharges").click(function () {
            debugger;
            $("#tab-third").addClass('tab-pane active');
        });

        $(function () {
            SetDatePicker();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        SetDatePicker();
                    }
                });
            };
        });

        function SetDatePicker() {
            $('.datepicker').datepicker({
                dateFormat: 'dd-mm-yyyy'
            }
            );
        }
    </script>

<%--    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {

            var bill = $("#ContentPlaceHolder1_txtBillDate").val();
            var vend = $("#ContentPlaceHolder1_txtVendor").val();
            var name = $("#ContentPlaceHolder1_txtVendorName").val();
            var blno = $("#ContentPlaceHolder1_txtBillNo").val();
            if (bill != "" && vend != "" && name != "" && blno != "") {
                document.getElementById("busy-holder1").style.display = "";
                document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";
            }
        }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="pnlPODetails" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtVendor" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlPaymentTerms" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtPoNo" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtGRNNo" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtGRNSrNo" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlUOM" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtItemBRate" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlPLant" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlConditionType" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtDiscount" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlCharges" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="lnkSaveCharges" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSaveDet" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="grvListItem" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="grvTaxation" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="grvCharges" EventName="RowCommand" />
        </Triggers>
        <ContentTemplate>
            <div class="page-content-wrap">

                <div class="row">
                    <div class="col-md-12">

                        <div class="form-horizontal">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Create  </strong>Purchase Bill</h3>
                                    <asp:LinkButton ID="imgCancel" runat="server" CssClass="btn btn-success pull-right" PostBackUrl="~/MM/ViewPB.aspx" Text="Cancel"><i class="fa fa-times"></i></asp:LinkButton>
                                    <asp:LinkButton ID="imgSaveAll" runat="server" CssClass="btn btn-success pull-right" OnClick="imgSaveAll_Click" OnClientClick="return CheckValidDate();" Text="Save All" ValidationGroup="SaveAll"><i class="fa fa-save"></i></asp:LinkButton>
                                    <div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                        <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                        <label>Please wait...</label>
                                    </div>
                                </div>

                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <h4 style="color: #f05423">PB Details</h4>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Doc. Type :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList ID="ddlDoctype" Enabled="false" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">PB No :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtPBNO" runat="server" Enabled="false" CssClass="form-control" placeholder="PB No"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">PB Date :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <div class="input-group">
                                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                    <asp:TextBox ID="txtPBDATE" runat="server" placeholder="DD-MM-YYYY" CssClass="form-control datepicker required_text_box" TabIndex="1" AutoCompleteType="None"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <asp:RequiredFieldValidator ForeColor="Red" Font-Bold="true" Style="margin-left: 150px!important;" ID="rfvPBDate" runat="server" ControlToValidate="txtPBDATE" ValidationGroup="SaveAll"
                                                                ErrorMessage="Please Enter PB Date">Please Enter PB Date</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="col-md-12">

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Vendor Code :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtVendor" runat="server" CssClass="form-control required_text_box" placeholder="Vendor Code" TabIndex="2" OnTextChanged="txtVendor_TextChanged" MaxLength="10" AutoPostBack="true"></asp:TextBox>
                                                                <asp:Label ID="lblVendorError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="rfvVendorCode" ForeColor="Red" Font-Bold="true" runat="server" ControlToValidate="txtVendor" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Vendor Code">Please Enter Vendor Code</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Vendor Name :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtVendorName" runat="server" CssClass="form-control" placeholder="Vendor Name" ReadOnly="true"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Bill No :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtBillNo" runat="server" CssClass="form-control required_text_box" placeholder="Bill No" TabIndex="3"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvBillNo" ForeColor="Red" Font-Bold="true" runat="server" ControlToValidate="txtBillNo" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Bill No">Please Enter Bill No</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">


                                                            <label class="col-md-5 control-label">Bill Date :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <div class="input-group">
                                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                    <asp:TextBox ID="txtBillDate" runat="server" placeholder="DD-MM-YYYY" TabIndex="4" CssClass="form-control datepicker required_text_box" AutoCompleteType="None"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <asp:RequiredFieldValidator ForeColor="Red" Style="margin-left: 150px!important;" Font-Bold="true" ID="rfvBillDate" runat="server" ControlToValidate="txtBillDate" ValidationGroup="SaveAll"
                                                                ErrorMessage="Please Enter Bill Date">Please Enter Bill Date</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Payment Terms :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList ID="ddlPaymentTerms" TabIndex="5" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPaymentTerms_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Paymnt Term Desc :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtPaymentTermsDesc" runat="server" CssClass="form-control" placeholder="Payment Terms Desc." Enabled="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-5">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">Remark :</label>
                                                            <div class="col-md-9 col-xs-12">
                                                                <asp:TextBox ID="txtREMARKS" Style="width: 500px!important;" runat="server" TabIndex="6" placeholder="Remarks" class="form-control"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>
                                                    <div class="col-md-1">
                                                    </div>

                                                </div>

                                                <div class="col-md-12" style="margin-top: 15px!important;">

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Material Value :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtMaterialValue" runat="server" CssClass="form-control" placeholder="Material Value" Enabled="false" Text="0"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Tax Amnt :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtTaxAmount" runat="server" CssClass="form-control" placeholder="Tax Amount" Enabled="false" Text="0"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Discount :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtNetDiscount" runat="server" CssClass="form-control" placeholder="Discount" Enabled="false" Text="0"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Other Charges :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtOtherCharges" runat="server" CssClass="form-control" placeholder="Other Charges" Enabled="false" Text="0"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>



                                                </div>

                                                <div class="col-md-12" style="margin-top: 15px!important;">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Net Amnt :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtNetAmount" runat="server" CssClass="form-control" placeholder="Net Amount" Enabled="false" Text="0"></asp:TextBox>
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
                                                            <li class="active"><a href="#tab-first" role="tab" data-toggle="tab">Material</a></li>
                                                            <li><a href="#tab-third" role="tab" data-toggle="tab">Other Charges </a></li>
                                                        </ul>
                                                        <div class="panel-body tab-content">

                                                            <div class="tab-pane active" id="tab-first">
                                                                <div class="panel-body">
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <div class="col-md-12">

                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Sr. No :</label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtSRNo" runat="server" Enabled="false" CssClass="form-control" placeholder="Sr. No."></asp:TextBox>
                                                                                            <asp:TextBox ID="txtMaxSrNo" runat="server" placeholder="Max Sr. No." CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                            <asp:TextBox ID="txtPoSrNo" runat="server" Visible="false"></asp:TextBox>
                                                                                            <asp:TextBox ID="txtReceivedQty" runat="server" Visible="false"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>


                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Po No :</label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtPoNo" runat="server" TabIndex="7" CssClass="form-control required_text_box" placeholder="Po No" OnTextChanged="txtPoNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="rfvPoNo" ForeColor="Red" Font-Bold="true" runat="server" ControlToValidate="txtPoNo" ValidationGroup="SaveDet"
                                                                                                ErrorMessage="Please Enter Po No">Please Enter Po No</asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">GRN No :</label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtGRNNo" runat="server" TabIndex="8" CssClass="form-control required_text_box" placeholder="GRN No" OnTextChanged="txtGRNNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="rfvGRNNo" ForeColor="Red" Font-Bold="true" runat="server" ControlToValidate="txtGRNNo" ValidationGroup="SaveDet"
                                                                                                ErrorMessage="Please Enter GRN No">Please Enter GRN No</asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>



                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">GRN Sr No :</label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtGRNSrNo" runat="server" TabIndex="9" CssClass="form-control required_text_box" placeholder="GRN Sr No" OnTextChanged="txtGRNSrNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="rfvGRNSrNo" ForeColor="Red" Font-Bold="true" runat="server" ControlToValidate="txtGRNSrNo" ValidationGroup="SaveDet"
                                                                                                ErrorMessage="Please Enter GRN Sr No">Please Enter GRN Sr No</asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-12">
                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Item Code :</label>
                                                                                        <div class="col-md-8">
                                                                                            <div class="input-group">
                                                                                                <asp:TextBox ID="txtItemCode" Enabled="false" runat="server" placeholder="Item Code" CssClass="form-control"></asp:TextBox>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Item Desc :</label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtItemDesc" runat="server" Enabled="false" placeholder="Item Description" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

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


                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Track No :</label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtTrackNo" TabIndex="10" runat="server" CssClass="form-control" placeholder="Track No."></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                            </div>

                                                                            <div class="col-md-12" style="margin-top: 10px;">
                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Qty :</label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtItemQty" runat="server" CssClass="form-control" placeholder="Quantity" Enabled="false"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">UOM :</label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:DropDownList ID="ddlUOM" runat="server" Enabled="false" CssClass="form-control required_text_box" OnSelectedIndexChanged="ddlUOM_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                            <asp:RequiredFieldValidator ForeColor="Red" Font-Bold="true" ID="rfvUOM" ControlToValidate="ddlUOM" runat="server" ValidationGroup="SaveDet"
                                                                                                ErrorMessage="Please Select Item UOM" InitialValue="0">Please Select Item UOM</asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Rate :</label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtItemBRate" runat="server" TabIndex="11" placeholder="Rate" CssClass="form-control required_text_box" OnTextChanged="txtItemBRate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ForeColor="Red" Font-Bold="true" ID="rfvItemRate" ControlToValidate="txtItemBRate" runat="server" ValidationGroup="SaveDet"
                                                                                                ErrorMessage="Please  Enter Item Rate">Please  Enter Item Rate</asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                        <asp:TextBox ID="txtRate" runat="server" placeholder="Item Text" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                    </div>
                                                                                </div>


                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Amount :</label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtAmount" runat="server" placeholder="Amount" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>


                                                                            </div>

                                                                            <div class="col-md-12" style="margin-top: 0px;">
                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Discount :</label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtDiscount" TabIndex="12" runat="server" placeholder="Discount" CssClass="form-control" Text="0" OnTextChanged="txtDiscount_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>


                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Ref. No :</label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtRefNo" runat="server" placeholder="Ref. No." Enabled="false" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>


                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Plant :</label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:DropDownList ID="ddlPLant" Enabled="false" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPLant_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Location :</label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:DropDownList ID="ddlLocation" Enabled="false" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                            </div>

                                                                            <div class="col-md-12" style="margin-top: 10px;">
                                                                                <div class="col-md-6">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-2 control-label">Remark :</label>
                                                                                        <div class="col-md-10">
                                                                                            <asp:TextBox ID="txtItemRemark" TabIndex="13" runat="server" placeholder="Remark" CssClass="form-control"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Cost Center :</label>
                                                                                        <div class="col-md-8">
                                                                                            <%--<asp:TextBox ID="txtCostCenter" runat="server" placeholder="Cost Center" CssClass="form-control" Enabled="false"></asp:TextBox>--%>

                                                                                            <asp:DropDownList ID="ddlCostCenter" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                            <asp:RequiredFieldValidator ID="rfvCostCenter" ControlToValidate="ddlCostCenter" runat="server" ValidationGroup="SaveDet" Style="color: red;"
                                                                                                ErrorMessage="Please Select Cost Center" InitialValue="0">Please Select Cost Center</asp:RequiredFieldValidator>


                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                            </div>

                                                                            <div class="col-md-12" style="margin-top: 0px;">
                                                                                <asp:Label ID="lblTaxHeading" runat="server" Text="Taxation" Font-Bold="true" ForeColor="DarkRed" Font-Size="Larger"></asp:Label>
                                                                            </div>

                                                                            <div class="col-md-12" style="margin-top: 0px;">
                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Operator : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:DropDownList ID="ddlOperator" TabIndex="14" runat="server" CssClass="form-control required_text_box">
                                                                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                                                                <asp:ListItem Text="+" Value="+"></asp:ListItem>
                                                                                                <asp:ListItem Text="-" Value="-"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Condi Type :</label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:DropDownList ID="ddlConditionType" TabIndex="15" runat="server" CssClass="form-control required_text_box" AutoPostBack="true" OnSelectedIndexChanged="ddlConditionType_SelectedIndexChanged"></asp:DropDownList>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>


                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Tax Amount : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtTaxTAmount" runat="server" Enabled="false" CssClass="form-control required_text_box" placeholder="Tax Amount"></asp:TextBox>
                                                                                            <asp:TextBox ID="txtGLCdTax" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                            <asp:HiddenField ID="hfRate" runat="server" />
                                                                                            <asp:HiddenField ID="hfPID" runat="server" />
                                                                                            <asp:HiddenField ID="hfCONDID" runat="server" />
                                                                                            <asp:HiddenField ID="hdTaxSrNo" runat="server" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>


                                                            <div class="tab-pane" id="tab-third">
                                                                <div class="panel-body">
                                                                    <div class="row">
                                                                        <div class="col-md-12">

                                                                            <div class="col-md-12">
                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Sr. No. : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtSrNoChg" runat="server" CssClass="form-control required_text_box" placeholder="Sr. No."></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="rfvChgSrNo" ForeColor="Red" Font-Bold="true" runat="server" ControlToValidate="txtSrNoChg"
                                                                                                ErrorMessage="Enter Charges Sr No" ValidationGroup="SaveCharge">Enter Charges Sr No</asp:RequiredFieldValidator>
                                                                                            <asp:TextBox ID="txtMaxSrNoChg" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                            </div>

                                                                            <div class="col-md-12" style="margin-top: 15px;">
                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Charges : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:DropDownList ID="ddlCharges" TabIndex="16" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCharges_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Amount : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtChgAmt" TabIndex="17" runat="server" CssClass="form-control required_text_box" placeholder="Charges Amount"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="rfvChargeAmount" ForeColor="Red" Font-Bold="true" runat="server" ControlToValidate="txtChgAmt"
                                                                                                ErrorMessage="Enter Charges Amount" ValidationGroup="SaveCharge">Enter Charges Amount</asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                            </div>

                                                                            <div class="col-md-12" style="margin-top: 15px;">
                                                                                <div class="panel-footer">
                                                                                    <asp:LinkButton ID="lnkSaveCharges" TabIndex="18" runat="server" CssClass="btn btn-success pull-right" OnClick="lnkSaveCharges_Click" Text="Save Charges" ValidationGroup="SaveCharge"><i class="fa fa-plus-square"></i></asp:LinkButton>
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
                                                                                                    <asp:GridView ID="grvCharges" runat="server" AutoGenerateColumns="false" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%"
                                                                                                        OnRowCommand="grvCharges_RowCommand">
                                                                                                        <Columns>

                                                                                                            <asp:TemplateField HeaderText="Sr. No.">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label runat="server" ID="lblChrgSrNo" Text='<%# Bind("CHRGSRNO") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>

                                                                                                            <asp:TemplateField HeaderText="Charges Type">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label runat="server" ID="lblChrgCondType" Text='<%# Bind("CHRGTYPE") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>



                                                                                                            <asp:TemplateField HeaderText="Charges Amount">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label runat="server" ID="lblChrgAmount" Text='<%# Bind("CHRGAMOUNT") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>



                                                                                                            <asp:TemplateField HeaderText="Action">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("CHRGSRNO") %>'
                                                                                                                        CommandName="eDelete" OnClientClick="return confirm('Are you sure you want to delete this record?');">Delete</asp:LinkButton>
                                                                                                                    |
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("CHRGSRNO") %>'
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
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="panel-footer">
                                    <asp:LinkButton ID="btnSaveDet" runat="server" CssClass="btn btn-success pull-right" OnClick="btnSaveDet_Click" Text="Save Item" ValidationGroup="SaveDet"><i class="fa fa-plus-square"></i></asp:LinkButton>
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
                                                    OnRowCommand="grvListItem_RowCommand" EmptyDataText="No Record Found !">
                                                    <EmptyDataTemplate>
                                                        <asp:Label ID="lblGVEmpty" runat="server" Text="No Data Found"></asp:Label>
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Po No.">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVPoNo" Text='<%# Bind("PONO") %>'></asp:Label>
                                                                <asp:Label runat="server" Visible="false" ID="lblGVPoSrNo" Text='<%# Bind("POSRNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="GRN. No.">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVGRNNo" Text='<%# Bind("MIRNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="GRN Sr No.">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVGRNSrNo" Text='<%# Bind("MIRSRNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Sr No.">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVSrNo" Text='<%# Bind("SRNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Visible="false" ID="lblGVItemId" Text='<%# Bind("ITEMID") %>'></asp:Label>
                                                                <asp:Label runat="server" ID="lblGVItemCode" Text='<%# Bind("ITEMCODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Desc.">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVItemDesc" Text='<%# Bind("ITEMDESC") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Group">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Visible="false" ID="lblGVGroupId" Text='<%# Bind("ITEMGRPID") %>'></asp:Label>
                                                                <asp:Label runat="server" ID="lblGVItemGroup" Text='<%# Bind("ITEMGRP") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="UOM">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" Visible="false" ID="lblGVUOMID" Text='<%# Bind("UOM") %>'></asp:Label>
                                                                <asp:Label runat="server" ID="lblGVUOM" Text='<%# Bind("UOMDesc") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Qty">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVQty" Text='<%# Bind("PBQTY") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Base Rate">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVBaseRate" Text='<%# Bind("BRate") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Rate">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVRate" Text='<%# Bind("RATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVAmount" Text='<%# Bind("CAMOUNT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Discount Amount">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVDiscount" Text='<%# Bind("DISCAMT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="GL Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVGLCode" Text='<%# Bind("GLCD") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Cost Center">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVCostCenter" Text='<%# Bind("CSTCENTCD") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Plant Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVPlantCD" Text='<%# Bind("PLANTCD") %>'></asp:Label>
                                                                <%--<asp:Label runat="server" ID="lblGVPlantID" Text='<%# Bind("ITEMPLANTID") %>'></asp:Label>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Location Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVLocationCD" Text='<%# Bind("LOCCD") %>'></asp:Label>
                                                                <%--<asp:Label runat="server" ID="lblGVLocationCDID" Visible="false" Text='<%# Bind("LOCCDID") %>'></asp:Label>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Profit Center">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVProfitCenter" Text='<%# Bind("PRFCNT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Track No.">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVTrackNo" Text='<%# Bind("TRNUM") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Asset Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVAssetCode" Text='<%# Bind("ASSETCD") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Remark">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVItemText" Text='<%# Bind("ITEMTEXT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Ref. No.">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVRefNo" Text='<%# Bind("REFNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDeleteMaterial" runat="server" CommandArgument='<%#Eval("SRNO") %>'
                                                                    CommandName="eDelete" OnClientClick="return confirm('Are you sure you want to delete this record?');">Delete</asp:LinkButton>
                                                                |
                                                    <asp:LinkButton ID="lnkEditMaterial" runat="server" CommandArgument='<%#Eval("SRNO") %>'
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


            <div class="page-content-wrap">

                <div class="row">
                    <div class="col-md-12">


                        <div class="panel panel-default">


                            <div class="panel-body">


                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="box">
                                            <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                                <asp:GridView ID="grvTaxation" runat="server" AutoGenerateColumns="false" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%"
                                                    OnRowCommand="grvTaxation_RowCommand">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Operator">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblTaxOperator" Text='<%# Bind("OPERATOR") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblTaxSrNo" Text='<%# Bind("CONDORDER") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="PB No.">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblTaxPBNo" Text='<%# Bind("PBNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="PB Sr No.">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblPBSrNo" Text='<%# Bind("PBSRNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="COND. ID">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblCONDID" Text='<%# Bind("CONDID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Condition Type">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblTaxCondType" Text='<%# Bind("CONDTYPE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="GL Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGLCode" Text='<%# Bind("GLCODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Rate">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblTaxRate" Text='<%# Bind("RATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Base Amount">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblTaxBaseAmount" Text='<%# Bind("BASEAMT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="PID">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblPID" Text='<%# Bind("PID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Tax Amount">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblTaxAmount" Text='<%# Bind("TAXAMT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTaxDelete" runat="server" CommandArgument='<%#Eval("CONDORDER") %>'
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
    <asp:ValidationSummary ID="val1" runat="server" ValidationGroup="SaveItem" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="val2" runat="server" ValidationGroup="SaveAll" ShowMessageBox="true" ShowSummary="false" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmTranMMPO" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />
</asp:Content>
