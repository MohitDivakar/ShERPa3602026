<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="CreatePO.aspx.cs" Inherits="ShERPa360net.MM.CreatePO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>

    <%--<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
  <link rel="stylesheet" href="/resources/demos/style.css">
  <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>--%>

    <script type="text/javascript">
        $("#lnkSaveCharges").click(function () {
            debugger;
            $("#tab-third").addClass('tab-pane active');
        });
    </script>

    <script lang="javascript" type='text/javascript'>

        function ShowLoading() {
            debugger;
            var vend = $("#ContentPlaceHolder1_txtVendor").val();
            var tran = $("#ContentPlaceHolder1_txtTransporter").val();
            if (vend != "" && tran != "") {
                document.getElementById("busy-holder1").style.display = "";
                document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";
            }
        }
    </script>

    <%--<script type="text/javascript">
        $(function () {
            debugger;
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

        //On UpdatePanel Refresh.
        function SetDatePicker() {
            $('.datepicker').datepicker({
                dateFormat: 'dd-mm-yyyy'
            }
            );
        }


    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="pnlPODetails" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtVendorName" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtTransporter" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtPRNo" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtPRSrNo" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtItemCode" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtItemQty" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtItemBRate" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtDiscount" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtRefNo" EventName="TextChanged" />
            <%--<asp:AsyncPostBackTrigger ControlID="txtTaxPOSrNo" EventName="TextChanged" />--%>
            <%--<asp:AsyncPostBackTrigger ControlID="txtBaseAmount" EventName="TextChanged" />--%>
            <asp:AsyncPostBackTrigger ControlID="ddlCharges" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlConditionType" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlUOM" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlPLant" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="lnkSaveCharges" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSaveDet" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="grvCharges" EventName="RowCommand" />
            <asp:AsyncPostBackTrigger ControlID="txtIMEINo" EventName="TextChanged" />
        </Triggers>
        <ContentTemplate>
            <div class="page-content-wrap">

                <div class="row">
                    <div class="col-md-12">

                        <div class="form-horizontal">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Create  </strong>Purchase Order</h3>
                                    <%--<ul class="panel-controls" style="margin: 0px 50px 6px 0px;">--%>


                                    <%--<button type="button" class="btn btn-success pull-right"><i class="fa fa-reply"></i></button>
                                    <button type="button" class="btn btn-success pull-right"><i class="fa fa-times"></i></button>
                                    <button type="button" class="btn btn-success pull-right"><i class="fa fa-save"></i></button>--%>
                                    <%--<asp:LinkButton ID="imgBackToList" runat="server" CssClass="btn btn-success pull-right" PostBackUrl="~/ViewPR.aspx" Text="Cancel"><i class="fa fa-reply"></i></asp:LinkButton>--%>
                                    <asp:LinkButton ID="imgCancel" runat="server" CssClass="btn btn-success pull-right" PostBackUrl="~/MM/ViewPO.aspx" Text="Cancel"><i class="fa fa-times"></i></asp:LinkButton>
                                    <asp:LinkButton ID="imgSaveAll" OnClientClick="ShowLoading()" runat="server" CssClass="btn btn-success pull-right" OnClick="imgSaveAll_Click" Text="Save All" ValidationGroup="SaveAll"><i class="fa fa-save"></i></asp:LinkButton>
                                    <div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                        <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                        <label>Please wait...</label>
                                    </div>
                                    <%--</ul>--%>
                                </div>

                                <div class="panel-body">

                                    <div class="row">






                                        <div class="col-md-12">
                                            <h4 style="color: #f05423">PO Details</h4>
                                            <div class="row">

                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <div class="form-group">



                                                            <label class="col-md-5 control-label">Doc. Type : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList ID="ddlDoctype" runat="server" CssClass="form-control "></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvDocType" runat="server" ControlToValidate="ddlDoctype" ValidationGroup="SaveAll" Style="color: red;"
                                                                    ErrorMessage="Please Select Doc Type" InitialValue="0">Please Select Doc Type</asp:RequiredFieldValidator>
                                                            </div>



                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">


                                                            <label class="col-md-5 control-label">PO No. : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtPONO" runat="server" CssClass="form-control" placeholder="PO No." Enabled="false"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvPONO" runat="server" ControlToValidate="txtPONO" ValidationGroup="SaveAll" Style="color: red;"
                                                                    ErrorMessage="Please Enter PO No">Please Enter PO No</asp:RequiredFieldValidator>
                                                            </div>



                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">

                                                            <label class="col-md-5 control-label">PO Date : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <div class="input-group">
                                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                    <asp:TextBox ID="txtPODATE" runat="server" placeholder="PO Date" class="form-control datepicker" Enabled="false" Style="height: 30px !important;"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <asp:RequiredFieldValidator ID="rfvPODate" runat="server" ControlToValidate="txtPODATE" ValidationGroup="SaveAll" Style="color: red;"
                                                                ErrorMessage="Please Enter PO Date">Please Enter PO Date</asp:RequiredFieldValidator>



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

                                                <div class="col-md-12">

                                                    <div class="col-md-3">
                                                        <div class="form-group">


                                                            <label class="col-md-5 control-label">Vendor : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <div class="input-group">
                                                                    <asp:TextBox ID="txtVendor" runat="server" CssClass="form-control" placeholder="Vendor Code" OnTextChanged="txtVendor_TextChanged" MaxLength="10" AutoPostBack="true"></asp:TextBox>
                                                                    <span class="input-group-btn">
                                                                        <asp:LinkButton ID="lnkOpenVendorPopup" runat="server" CssClass="btn btn-success" OnClick="lnkOpenVendorPopup_Click" Text="Open Popup" Enabled="true"><span class="fa fa-search"></span></asp:LinkButton>
                                                                    </span>
                                                                </div>
                                                                <asp:RegularExpressionValidator ControlToValidate="txtVendor" ID="RegularExpressionValidator1" ValidationExpression="[0-9]{10}" runat="server" ValidationGroup="SaveAll" ErrorMessage="Only Numeric allowed." ForeColor="Red">Only Numeric allowed.</asp:RegularExpressionValidator>
                                                                <asp:Label ID="lblVendorError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="rfvVendorCode" runat="server" ControlToValidate="txtVendor" ValidationGroup="SaveAll" Style="color: red;"
                                                                    ErrorMessage="Please Enter Vendor Code">Please Enter Vendor Code</asp:RequiredFieldValidator>
                                                            </div>



                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">


                                                            <label class="col-md-5 control-label">Vendor Name : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtVendorName" runat="server" CssClass="form-control" placeholder="Vendor Name" ReadOnly="true"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvVendorName" runat="server" ControlToValidate="txtVendorName" ValidationGroup="SaveAll" Style="color: red;"
                                                                    ErrorMessage="Please Enter Vendor Name">Please Enter Vendor Name</asp:RequiredFieldValidator>
                                                            </div>



                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">


                                                            <label class="col-md-5 control-label">Transporter : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <div class="input-group">
                                                                    <asp:TextBox ID="txtTransporter" runat="server" CssClass="form-control" placeholder="Transporter Code" OnTextChanged="txtTransporter_TextChanged" MaxLength="10" AutoPostBack="true"></asp:TextBox>
                                                                    <span class="input-group-btn">
                                                                        <asp:LinkButton ID="lnkOpenTransPopup" runat="server" CssClass="btn btn-success" OnClick="lnkOpenTransPopup_Click" Text="Open Popup" Enabled="true"><span class="fa fa-search"></span></asp:LinkButton>
                                                                    </span>
                                                                </div>
                                                                <asp:RegularExpressionValidator ControlToValidate="txtTransporter" ID="RegularExpressionValidator2" ValidationExpression="^[0-9]{10}" runat="server" ValidationGroup="SaveAll" ErrorMessage="Only Numeric allowed." ForeColor="Red">Only Numeric allowed.</asp:RegularExpressionValidator>
                                                                <asp:Label ID="lblTransporterError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="rfvTransporter" runat="server" ControlToValidate="txtTransporter" ValidationGroup="SaveAll" Style="color: red;"
                                                                    ErrorMessage="Please Enter Transporter Code">Please Enter Transporter Code</asp:RequiredFieldValidator>
                                                            </div>



                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">


                                                            <label class="col-md-5 control-label">Transporter Name : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtTransporterName" runat="server" CssClass="form-control" placeholder="Transporter Name" ReadOnly="true"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvTransporterName" runat="server" ControlToValidate="txtTransporterName" ValidationGroup="SaveAll" Style="color: red;"
                                                                    ErrorMessage="Please Enter Transporter Name">Please Enter Transporter Name</asp:RequiredFieldValidator>
                                                            </div>



                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <div class="form-group">


                                                            <label class="col-md-5 control-label">Payment Terms : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList ID="ddlPaymentTerms" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPaymentTerms_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvPaymentTerms" runat="server" ControlToValidate="ddlPaymentTerms" ValidationGroup="SaveAll" Style="color: red;"
                                                                    ErrorMessage="Please Select Payment Terms" InitialValue="0">Please Select Payment Terms</asp:RequiredFieldValidator>
                                                            </div>



                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">


                                                            <label class="col-md-5 control-label">Payment Terms Desc. : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtPaymentTermsDesc" runat="server" CssClass="form-control" placeholder="Payment Terms Desc." ReadOnly="false"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvPaymentTermsDesc" runat="server" ControlToValidate="txtPaymentTermsDesc" ValidationGroup="SaveAll" Style="color: red;"
                                                                    ErrorMessage="Payment Terms Desc.">Payment Terms Desc.</asp:RequiredFieldValidator>
                                                            </div>



                                                        </div>
                                                    </div>

                                                    <div class="col-md-5">
                                                        <div class="form-group">

                                                            <label class="col-md-3 control-label">Remark : </label>
                                                            <div class="col-md-9 col-xs-12">
                                                                <asp:TextBox ID="txtREMARKS" runat="server" placeholder="Remarks" class="form-control"></asp:TextBox>
                                                            </div>

                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="col-md-12">

                                                    <div class="col-md-3">
                                                        <div class="form-group">


                                                            <label class="col-md-5 control-label">Material Value : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtMaterialValue" runat="server" CssClass="form-control" placeholder="Material Value" ReadOnly="true" Text="0"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvMaterialValue" runat="server" ControlToValidate="txtMaterialValue" ValidationGroup="SaveAll" Style="color: red;"
                                                                    ErrorMessage="Please Enter Material Value">Please Enter Material Value</asp:RequiredFieldValidator>
                                                            </div>



                                                        </div>
                                                    </div>

                                                    <div class="col-md-2">
                                                        <div class="form-group">


                                                            <label class="col-md-5 control-label">Tax Amount : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtTaxAmount" runat="server" CssClass="form-control" placeholder="Tax Amount" ReadOnly="true" Text="0"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvTaxAmount" runat="server" ControlToValidate="txtTaxAmount" ValidationGroup="SaveAll" Style="color: red;"
                                                                    ErrorMessage="Please Enter Tax Amount">Please Enter Tax Amount</asp:RequiredFieldValidator>
                                                            </div>



                                                        </div>
                                                    </div>

                                                    <div class="col-md-2">
                                                        <div class="form-group">


                                                            <label class="col-md-5 control-label">Discount : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtDisacount" runat="server" CssClass="form-control" placeholder="Discount" ReadOnly="true" Text="0"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvDisacount" runat="server" ControlToValidate="txtDisacount" ValidationGroup="SaveAll" Style="color: red;"
                                                                    ErrorMessage="Please Enter Disacount">Please Enter Disacount</asp:RequiredFieldValidator>
                                                            </div>



                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">


                                                            <label class="col-md-5 control-label">Other Charges : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtOtherCharges" runat="server" CssClass="form-control" placeholder="Other Charges" ReadOnly="true" Text="0"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvOtherCharges" runat="server" ControlToValidate="txtOtherCharges" ValidationGroup="SaveAll" Style="color: red;"
                                                                    ErrorMessage="Please Enter Other Charges">Please Enter Other Charges</asp:RequiredFieldValidator>
                                                            </div>



                                                        </div>
                                                    </div>

                                                    <div class="col-md-2">
                                                        <div class="form-group">


                                                            <label class="col-md-5 control-label">Net Amount : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtNetAmount" runat="server" CssClass="form-control" placeholder="Net Amount" ReadOnly="true" Text="0"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvNetAmount" runat="server" ControlToValidate="txtNetAmount" ValidationGroup="SaveAll" Style="color: red;"
                                                                    ErrorMessage="Please Enter Net Amount">Please Enter Net Amount</asp:RequiredFieldValidator>
                                                            </div>



                                                        </div>
                                                    </div>

                                                </div>
                                            </div>

                                        </div>





                                        <%--<asp:UpdatePanel ID="updDetaisl" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                       
                                    </Triggers>
                                    <ContentTemplate>--%>



                                        <div class="page-content-wrap">


                                            <div class="row">
                                                <div class="col-md-12">

                                                    <div class="panel panel-default tabs">
                                                        <ul class="nav nav-tabs" role="tablist">
                                                            <li class="active"><a href="#tab-first" role="tab" data-toggle="tab">Material</a></li>
                                                            <%--<li><a href="#tab-second" role="tab" data-toggle="tab">Taxation </a></li>--%>
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
                                                                                        <label class="col-md-4 control-label">Sr. No. : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtSRNo" runat="server" CssClass="form-control" placeholder="Sr. No." Enabled="false"></asp:TextBox>
                                                                                            <asp:TextBox ID="txtMaxSrNo" runat="server" placeholder="Max Sr. No." CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Fetch Full PR : </label>
                                                                                        <div class="col-md-8">
                                                                                            <div class="input-group">
                                                                                                <asp:CheckBox ID="chkFullPR" runat="server" CssClass="form-control" Checked="true" />
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <%--                                                                            </div>


                                                                            <div class="col-md-12" style="margin-top: 10px;">--%>
                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">PR No. : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtPRNo" runat="server" CssClass="form-control" placeholder="PR No." OnTextChanged="txtPRNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtPRNo" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                                                                ErrorMessage="Please  Enter PR NO.">Please  Enter PR NO.</asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">PR Sr. No. : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtPRSrNo" runat="server" CssClass="form-control" placeholder="PR Sr. No." OnTextChanged="txtPRSrNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtPRSrNo" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                                                                ErrorMessage="Please  Enter PR Sr. NO.">Please  Enter PR Sr. NO.</asp:RequiredFieldValidator>
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
                                                                                                <asp:TextBox ID="txtItemCode" runat="server" placeholder="Item Code" AutoPostBack="true" CssClass="form-control required_text_box" OnTextChanged="txtItemCode_TextChanged" Enabled="false"></asp:TextBox>
                                                                                                <span class="input-group-btn">
                                                                                                    <%--<button class="btn btn-success" type="button" data-toggle="modal" data-target="#modal-item"><span class="fa fa-search"></span></button>--%>
                                                                                                    <asp:LinkButton ID="lnkOpenPoup" runat="server" CssClass="btn btn-success" OnClick="lnkOpenPoup_Click" Text="Open Popup" Enabled="false"><span class="fa fa-search"></span></asp:LinkButton>

                                                                                                    <%--<asp:LinkButton ID="lnkNewPartCode" runat="server" CssClass="btn btn-success" OnClick="lnkNewPartCode_Click" Text="Open Popup"><span class="fa fa-plus-circle"></span></asp:LinkButton>--%>


                                                                                                </span>


                                                                                            </div>

                                                                                            <%--<asp:Button ID="btnPopup" runat="server" AccessKey="Z" OnClick="btnPopup_Click" CssClass="hidden" />--%>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Item Desc. : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtItemDesc" runat="server" placeholder="Item Description" class="form-control required_text_box" ReadOnly="true"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="rfvItemDesc" ControlToValidate="txtItemDesc" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                                                                ErrorMessage="Please  Enter Item Desc.">Please  Enter Item Desc.</asp:RequiredFieldValidator>
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

                                                                                            <asp:TextBox ID="txtOnhandStock" runat="server" placeholder="Item Max Stock Qty" CssClass="form-control" Visible="false"></asp:TextBox>

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
                                                                                        <label class="col-md-4 control-label">Track No. : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtTrackNo" runat="server" CssClass="form-control" placeholder="Track No." MaxLength="10" OnTextChanged="txtTrackNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                            <asp:RegularExpressionValidator ID="revTrackNo" runat="server" ControlToValidate="txtTrackNo" ValidationGroup="SaveItem" Style="color: red;" Display="Dynamic"
                                                                                                ErrorMessage="Only numbers allowed." ValidationExpression='(^([0-9]*|\d*\d{1}?\d*)$)'>Only numbers allowed.</asp:RegularExpressionValidator>
                                                                                            <asp:RequiredFieldValidator ID="rfvTrackNo" ControlToValidate="txtTrackNo" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                                                                ErrorMessage="Please  Enter Job ID">Please  Enter Job ID</asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                            </div>

                                                                            <div class="col-md-12" style="margin-top: 0px;">
                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Qty : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtItemQty" runat="server" CssClass="form-control required_text_box" placeholder="Quantity" OnTextChanged="txtItemQty_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                            <asp:Label ID="lblCheckPrQty" runat="server" Visible="false"></asp:Label>
                                                                                            <asp:RequiredFieldValidator ID="rfvItemQty" ControlToValidate="txtItemQty" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                                                                ErrorMessage="Please  Enter Item Qty.">Please  Enter Item Qty.</asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">UOM : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:DropDownList ID="ddlUOM" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlUOM_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                            <asp:RequiredFieldValidator ID="rfvUOM" ControlToValidate="ddlUOM" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                                                                ErrorMessage="Please Select Item UOM" InitialValue="0">Please Select Item UOM</asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Rate : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtItemBRate" runat="server" placeholder="Rate" CssClass="form-control" OnTextChanged="txtItemRate_TextChanged" AutoPostBack="true" value="1"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="rfvItemRate" ControlToValidate="txtItemBRate" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                                                                ErrorMessage="Please  Enter Item Rate">Please  Enter Item Rate</asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                        <asp:TextBox ID="txtRate" runat="server" placeholder="Item Text" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                    </div>
                                                                                </div>


                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Amount : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtAmount" runat="server" placeholder="Amount" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="rfvAmount" ControlToValidate="txtAmount" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                                                                ErrorMessage="Please  Enter Amount">Please  Enter Amount</asp:RequiredFieldValidator>


                                                                                        </div>
                                                                                        <%--<div class="col-md-4">
                                                                                            <asp:Button ID="btnTaxation" runat="server" CssClass="form-control" Text="Taxation" />
                                                                                        </div>--%>
                                                                                    </div>
                                                                                </div>


                                                                            </div>

                                                                            <div class="col-md-12" style="margin-top: 0px;">
                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Discount : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtDiscount" runat="server" placeholder="Discount" CssClass="form-control" Text="0" OnTextChanged="txtDiscount_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtItemRate" runat="server" ValidationGroup="SaveItem"
                                                                                        ErrorMessage="Please  Enter Item Rate">*</asp:RequiredFieldValidator>--%>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Deli. Date : </label>
                                                                                        <div class="col-md-8">
                                                                                            <div class="input-group">
                                                                                                <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                                                <asp:TextBox ID="txtDeliveryDate" runat="server" placeholder="Delivery Date" CssClass="form-control datepicker" AutoCompleteType="None"></asp:TextBox>

                                                                                            </div>
                                                                                        </div>
                                                                                        <asp:RequiredFieldValidator ID="rfvDeliveryDate" ControlToValidate="txtDeliveryDate" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                                                            ErrorMessage="Please  Enter Delivery Date">Please  Enter Delivery Date</asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Ref. No. : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtRefNo" runat="server" placeholder="Ref. No." CssClass="form-control" AutoPostBack="true" OnTextChanged="txtRefNo_TextChanged"></asp:TextBox>
                                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtItemRate" runat="server" ValidationGroup="SaveItem"
                                                                                        ErrorMessage="Please  Enter Item Rate">*</asp:RequiredFieldValidator>--%>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">IMEI No. : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtIMEINo" runat="server" placeholder="IMEI No." CssClass="form-control" OnTextChanged="txtIMEINo_TextChanged" AutoPostBack="true" TextMode="Number"></asp:TextBox>
                                                                                            <asp:RegularExpressionValidator ID="revImeino" runat="server" ControlToValidate="txtIMEINo" ValidationGroup="SaveItem" Style="color: red;" Display="Dynamic"
                                                                                                ErrorMessage="Only numbers allowed." ValidationExpression='(^([A-Z][a-z][0-9]*|\d*\d{1}?\d*)$)'>Only numbers allowed.</asp:RegularExpressionValidator>
                                                                                            <asp:RequiredFieldValidator ID="rfvIMEINo" ControlToValidate="txtIMEINo" runat="server" ValidationGroup="SaveItem" Display="Dynamic" Style="color: red;"
                                                                                                ErrorMessage="Please Enter IMEI No">Please Enter IMEI No</asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>


                                                                            </div>

                                                                            <div class="col-md-12" style="margin-top: 0px;">
                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Item Reamark : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtItemRemark" runat="server" placeholder="Remark" CssClass="form-control"></asp:TextBox>
                                                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtItemRate" runat="server" ValidationGroup="SaveItem"
                                                                                        ErrorMessage="Please  Enter Item Rate">*</asp:RequiredFieldValidator>--%>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>





                                                                                <%--  </div>


                                                                            <div class="col-md-12" style="margin-top: 15px;">--%>
                                                                                <div class="col-md-3" id="divFromPlant" runat="server" style="display: none !important;">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">From Plant : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:DropDownList ID="ddlToPlant" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                            <asp:RequiredFieldValidator ID="rfvToPlant" ControlToValidate="ddlToPlant" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                                                                ErrorMessage="Please Select Plant" InitialValue="0">Please Select Plant</asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-md-3" id="divFromLocation" runat="server" style="display: none !important;">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">From Location : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:DropDownList ID="ddlToLocation" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                            <asp:RequiredFieldValidator ID="rfvToLocation" ControlToValidate="ddlToLocation" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                                                                ErrorMessage="Please Select Location" InitialValue="0">Please Select Location</asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>


                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Plant : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:DropDownList ID="ddlPLant" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPLant_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                            <asp:RequiredFieldValidator ID="rfvPLant" ControlToValidate="ddlPLant" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                                                                ErrorMessage="Please Select Plant" InitialValue="0">Please Select Plant</asp:RequiredFieldValidator>


                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Location : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                            <asp:RequiredFieldValidator ID="rfvLocation" ControlToValidate="ddlLocation" runat="server" ValidationGroup="SaveItem" Style="color: red;"
                                                                                                ErrorMessage="Please Select Location" InitialValue="0">Please Select Location</asp:RequiredFieldValidator>


                                                                                        </div>
                                                                                    </div>
                                                                                </div>


                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Cost Center : </label>
                                                                                        <div class="col-md-8">
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

                                                                            <div class="col-md-12" style="margin-top: 0px;">
                                                                                <asp:Label ID="lblTaxHeading" runat="server" Text="Taxation" Font-Bold="true" ForeColor="DarkRed" Font-Size="Larger"></asp:Label>
                                                                            </div>

                                                                            <div class="col-md-12" style="margin-top: 0px;">
                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Operator : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:DropDownList ID="ddlOperator" runat="server" CssClass="form-control">
                                                                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                                                                <asp:ListItem Text="+" Value="+"></asp:ListItem>
                                                                                                <asp:ListItem Text="-" Value="-"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Condition Type : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:DropDownList ID="ddlConditionType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlConditionType_SelectedIndexChanged"></asp:DropDownList>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>


                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Tax Amount : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtTaxTAmount" runat="server" CssClass="form-control" placeholder="Tax Amount" Enabled="false"></asp:TextBox>
                                                                                            <asp:TextBox ID="txtGLCdTax" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                            <asp:HiddenField ID="hfRate" runat="server" />
                                                                                            <asp:HiddenField ID="hfPID" runat="server" />
                                                                                            <asp:HiddenField ID="hfCONDID" runat="server" />
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>


                                                            <%-- <div class="tab-pane" id="tab-second">

                                                                <div class="panel-body">
                                                                    <div class="row">
                                                                        <div class="col-md-12">

                                                                            <div class="col-md-12">
                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Sr. No. : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtTaxRate" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                            <asp:TextBox ID="txtTaxSrNo" runat="server" CssClass="form-control" placeholder="Sr. No."></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">PO Sr. No. : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtSrNoTax" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                            <asp:TextBox ID="txtTaxPOSrNo" runat="server" CssClass="form-control" placeholder="PO Sr. No." AutoPostBack="true" OnTextChanged="txtTaxPOSrNo_TextChanged"></asp:TextBox>
                                                                                            <asp:TextBox ID="txtCondId" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                            <asp:TextBox ID="txtMaxSrNoTax" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                            </div>

                                                                            <div class="col-md-12" style="margin-top: 15px;">
                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Operator : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:DropDownList ID="ddlOperator" runat="server" CssClass="form-control">
                                                                                                <asp:ListItem Text="" Value=""></asp:ListItem>
                                                                                                <asp:ListItem Text="+" Value="+"></asp:ListItem>
                                                                                                <asp:ListItem Text="-" Value="-"></asp:ListItem>
                                                                                            </asp:DropDownList>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Condition Type : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:DropDownList ID="ddlConditionType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlConditionType_SelectedIndexChanged"></asp:DropDownList>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Base Amount : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtBaseAmount" runat="server" CssClass="form-control" placeholder="Base Amount" AutoPostBack="true" OnTextChanged="txtBaseAmount_TextChanged"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Tax Amount : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtTaxTAmount" runat="server" CssClass="form-control" placeholder="Tax Amount"></asp:TextBox>
                                                                                            <asp:TextBox ID="txtGLCdTax" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                            </div>

                                                                            <div class="col-md-12" style="margin-top: 15px;">
                                                                                <div class="panel-footer">
                                                                                    <asp:LinkButton ID="lnkSaveTaxation" runat="server" CssClass="btn btn-success pull-right" OnClick="lnkSaveTaxation_Click" Text="Save Taxation" ValidationGroup="SaveTax"><i class="fa fa-plus-square"></i></asp:LinkButton>

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
                                                                                                <div class="box-body">
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
                                                                                                                    <asp:Label runat="server" ID="lblTaxSrNo" Text='<%# Bind("TAXSRNO") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>

                                                                                                            <asp:TemplateField HeaderText="PO Sr. No.">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label runat="server" ID="lblTaxPOSrNo" Text='<%# Bind("POSRNO") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>

                                                                                                            <asp:TemplateField HeaderText="Condition Type">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label runat="server" ID="lblTaxCondType" Text='<%# Bind("CONDTYPE") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>

                                                                                                            <asp:TemplateField HeaderText="Rate">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label runat="server" ID="lblTaxRate" Text='<%# Bind("TAXRATE") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>

                                                                                                            <asp:TemplateField HeaderText="Base Amount">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label runat="server" ID="lblTaxBaseAmount" Text='<%# Bind("TAXBASEAMOUNT") %>'></asp:Label>
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>

                                                                                                            <asp:TemplateField HeaderText="Tax Amount">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label runat="server" ID="lblTaxAmount" Text='<%# Bind("TAXAMOUNT") %>'></asp:Label>
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


                                                            </div>--%>


                                                            <div class="tab-pane" id="tab-third">
                                                                <div class="panel-body">
                                                                    <div class="row">
                                                                        <div class="col-md-12">

                                                                            <div class="col-md-12">
                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Sr. No. : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtSrNoChg" runat="server" CssClass="form-control" placeholder="Sr. No."></asp:TextBox>
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
                                                                                            <asp:DropDownList ID="ddlCharges" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCharges_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                                <div class="col-md-3">
                                                                                    <div class="form-group">
                                                                                        <label class="col-md-4 control-label">Amount : </label>
                                                                                        <div class="col-md-8">
                                                                                            <asp:TextBox ID="txtChgAmt" runat="server" CssClass="form-control" placeholder="Charges Amount"></asp:TextBox>
                                                                                            <asp:RequiredFieldValidator ID="rfvChargeAmount" runat="server" ControlToValidate="txtChgAmt" Style="color: red;"
                                                                                                ErrorMessage="Enter Charges Amount" ValidationGroup="SaveCharge">Enter Charges Amount</asp:RequiredFieldValidator>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>

                                                                            </div>

                                                                            <div class="col-md-12" style="margin-top: 15px;">
                                                                                <div class="panel-footer">
                                                                                    <%--<button type="button" class="btn btn-success pull-right"><i class="fa fa-plus-square"></i></button>--%>
                                                                                    <asp:LinkButton ID="lnkSaveCharges" runat="server" CssClass="btn btn-success pull-right" OnClick="lnkSaveCharges_Click" Text="Save Charges" ValidationGroup="SaveCharge"><i class="fa fa-plus-square"></i></asp:LinkButton>

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






                                        <%--   </ContentTemplate>
                                </asp:UpdatePanel>--%>
                                    </div>
                                </div>
                                <div class="panel-footer">
                                    <%--<button type="button" class="btn btn-success pull-right"><i class="fa fa-plus-square"></i></button>--%>
                                    <asp:LinkButton ID="btnSaveDet" runat="server" CssClass="btn btn-success pull-right" OnClick="btnSaveDet_Click" Text="Save Item" ValidationGroup="SaveItem"><i class="fa fa-plus-square"></i></asp:LinkButton>

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
                                                    <%-- OnRowEditing="grvListItem_RowEditing" OnRowCancelingEdit="grvListItem_RowCancelingEdit" OnRowUpdating="grvListItem_RowUpdating">--%>
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="PR No.">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVPrNo" Text='<%# Bind("PRNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="PR Sr. No.">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVPRSrNo" Text='<%# Bind("ID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVID" Text='<%# Bind("POID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Item Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVItemCode" Text='<%# Bind("ITEMCODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<EditItemTemplate>
                                                    <asp:TextBox ID="txtEditItemCode" runat="server" Text='<%# Bind("ITEMCODE") %>' OnTextChanged="txtEditItemCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </EditItemTemplate>--%>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Desc.">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVItemDesc" Text='<%# Bind("ITEMDESC") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVItemId" Text='<%# Bind("ITEMID") %>'></asp:Label>


                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Group">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVItemGroup" Text='<%# Bind("ITEMGROUP") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Group Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVGroupId" Text='<%# Bind("ITEMGROUPID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="UOM">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVUOM" Text='<%# Bind("ITEMUOM") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<EditItemTemplate>
                                                    <asp:DropDownList ID="ddlEditUOM" runat="server"></asp:DropDownList>
                                                </EditItemTemplate>--%>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="UOM ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVUOMID" Text='<%# Bind("UOMID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Qty">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVQty" Text='<%# Bind("POQTY") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<EditItemTemplate>
                                                    <asp:TextBox ID="txtEditQty" runat="server" Text='<%# Bind("ITEMQTY") %>' OnTextChanged="txtEditQty_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </EditItemTemplate>--%>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Rate">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVRate" Text='<%# Bind("ITEMRATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<EditItemTemplate>
                                                    <asp:TextBox ID="txtEditRate" runat="server" Text='<%# Bind("ITEMRATE") %>' OnTextChanged="txtEditRate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </EditItemTemplate>--%>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Base Rate">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVBaseRate" Text='<%# Bind("ITEMBRATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<EditItemTemplate>
                                                    <asp:TextBox ID="txtEditRate" runat="server" Text='<%# Bind("ITEMRATE") %>' OnTextChanged="txtEditRate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </EditItemTemplate>--%>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVAmount" Text='<%# Bind("ITEMAMOUNT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Discount Amount">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVDiscount" Text='<%# Bind("DISCOUNTAMT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Deli. Date">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVDeliDate" Text='<%# Bind("DELIVERYDATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:BoundField DataField="DELIVERYDATE" HeaderText="Deli. Date" DataFormatString="{0:MM/dd/yyyy}" />--%>
                                                        <asp:TemplateField HeaderText="GL Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVGLCode" Text='<%# Bind("GLCODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Cost Center">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVCostCenter" Text='<%# Bind("COSTCENTER") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Plant Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVPlantCD" Text='<%# Bind("ITEMPLANTCD") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<EditItemTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlEditPlantCode" OnSelectedIndexChanged="ddlEditPlantCode_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </EditItemTemplate>--%>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Plant Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVPlantID" Text='<%# Bind("ITEMPLANTID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Location Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVLocationCD" Text='<%# Bind("ITEMLOCCD") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<EditItemTemplate>
                                                    <asp:DropDownList ID="ddlEditLocation" runat="server"></asp:DropDownList>
                                                </EditItemTemplate>--%>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Location Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVLocationCDID" Text='<%# Bind("LOCCDID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Profit Center">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVProfitCenter" Text='<%# Bind("PROFITCENTER") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Asset Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVAssetCode" Text='<%# Bind("ASSETCODE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Track No.">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVTrackNo" Text='<%# Bind("TRACKNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Text">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVItemText" Text='<%# Bind("ITEMTEXT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Part Req. No.">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVPartReqNo" Text='<%# Bind("PARTREQNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <%--<EditItemTemplate>
                                                    <asp:TextBox ID="txtEditPartReqNo" runat="server" Text='<%# Bind("PARTREQNO") %>'></asp:TextBox>
                                                </EditItemTemplate>--%>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVRemarks" Text='<%# Bind("ITEMREMARKS") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Ref. No.">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVRefNo" Text='<%# Bind("REFNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="IMEI">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVIMEI" Text='<%# Bind("IMEINO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="From Plant Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVFromPlantCode" Text='<%# Bind("FROMPLANTCD") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="From Location Code">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblGVFromLocationCode" Text='<%# Bind("FROMLOCCD") %>'></asp:Label>
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
                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblSKU" Text='<%# Bind("SKU") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblMake" Text='<%# Bind("MAKE") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblModel" Text='<%# Bind("MODEL") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblDispName" Text='<%# Bind("DISPNAME") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblDispMRP" Text='<%# Bind("DISPMRP") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblValueLimit" Text='<%# Bind("VALUELIMIT") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblMaxStkQty" Text='<%# Bind("MAXSTKQTY") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblHSN" Text='<%# Bind("HSN") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblHSNGroup" Text='<%# Bind("HSNGROUP") %>'></asp:Label>

                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblHSNGroupDesc" Text='<%# Bind("HSNGROUPDESC") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblCondType" Text='<%# Bind("CONDTYPE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblItemStatus" Text='<%# Bind("ITEMSTATUS") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblOnHandStock" Text='<%# Bind("ONHANDSTOCK") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Deviation Reason">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblDEVREASON" Text='<%# Bind("DEVREASON") %>'></asp:Label>
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
                                                                <asp:Label runat="server" ID="lblTaxSrNo" Text='<%# Bind("TAXSRNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="PO Sr. No.">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblTaxPOSrNo" Text='<%# Bind("POSRNO") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Condition Type">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblTaxCondType" Text='<%# Bind("CONDTYPE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Rate">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblTaxRate" Text='<%# Bind("TAXRATE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Base Amount">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblTaxBaseAmount" Text='<%# Bind("TAXBASEAMOUNT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Tax Amount">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblTaxAmount" Text='<%# Bind("TAXAMOUNT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="PID">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblPID" Text='<%# Bind("PID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="COND. ID">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblCONDID" Text='<%# Bind("CONDID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkTaxDelete" runat="server" CommandArgument='<%#Eval("TAXSRNO") %>'
                                                                    CommandName="eDelete" OnClientClick="return confirm('Are you sure you want to delete this record?');">Delete</asp:LinkButton>
                                                                <%--   |
                                                    <asp:LinkButton ID="lnkTaxEdit" runat="server" CommandArgument='<%#Eval("TAXSRNO") %>'
                                                        CommandName="eEdit">Edit</asp:LinkButton>--%>
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






        </ContentTemplate>
    </asp:UpdatePanel>



    <div class="modal fade" id="modal-Vendor" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7">Vendor List</h4>
                </div>


                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Vendor Code :</label>
                                        <asp:TextBox ID="txtPopupVendorCode" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>City :</label>
                                        <%--<asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlPopupCity" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Vendor Name :</label>
                                        <asp:TextBox ID="txtPopupVendorName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Vendor Type :</label>
                                        <%--<asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlPopupVendType" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" style="margin-top: 5px;">
                                <%--<asp:ImageButton ID="imgFind" CssClass="btnClass" runat="server" OnClick="imgFind_Click" ImageUrl="~/img/icons/find.png" AlternateText="Show Data" ToolTip="Show PR Data" />--%>
                                <asp:LinkButton ID="lnkPopupVendorShow" runat="server" CssClass="btn btn-success pull-left" OnClick="lnkPopupVendorShow_Click" Text="Show Item"><span class="fa fa-search"></span></asp:LinkButton>
                            </div>


                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll">
                                        <asp:GridView ID="grvPopVendor" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                            CssClass="table table-hover table-striped table-bordered nowrap" AutoGenerateSelectButton="true" OnSelectedIndexChanged="grvPopVendor_SelectedIndexChanged">
                                            <EmptyDataTemplate>
                                                <div style="text-align: center; color: red; font-size: 18px;">
                                                    No Record Not Found !
                                                </div>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="VENDTYPE" HeaderText="Vendor Type" />
                                                <asp:BoundField DataField="VENDCODETRIM" HeaderText="Vendor Code" />
                                                <asp:BoundField DataField="NAME" HeaderText="Name" />
                                                <asp:BoundField DataField="SHORTNM" HeaderText="Short Name" />
                                                <asp:BoundField DataField="ZONE" HeaderText="Zone" />
                                                <asp:BoundField DataField="REGION" HeaderText="Region" />
                                                <asp:BoundField DataField="STATE" HeaderText="State" />
                                                <asp:BoundField DataField="CITY" HeaderText="City" />
                                                <asp:BoundField DataField="POSTALCODE" HeaderText="Postal Code" />
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






    <div class="modal fade" id="modal-Trans" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7">Transporter List</h4>
                </div>


                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Transporter Code :</label>
                                        <asp:TextBox ID="txtPopupTransCode" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>City :</label>
                                        <%--<asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlPopupTransCity" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Transporter Name :</label>
                                        <asp:TextBox ID="ddlPopupTransName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Transporter Type :</label>
                                        <%--<asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                        <asp:DropDownList ID="ddlPopupTransType" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" style="margin-top: 5px;">
                                <%--<asp:ImageButton ID="imgFind" CssClass="btnClass" runat="server" OnClick="imgFind_Click" ImageUrl="~/img/icons/find.png" AlternateText="Show Data" ToolTip="Show PR Data" />--%>
                                <asp:LinkButton ID="lnkPopupTransShow" runat="server" CssClass="btn btn-success pull-left" OnClick="lnkPopupTransShow_Click" Text="Show Item"><span class="fa fa-search"></span></asp:LinkButton>
                            </div>


                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll">
                                        <asp:GridView ID="grvPopupTrans" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                            CssClass="table table-hover table-striped table-bordered nowrap" AutoGenerateSelectButton="true" OnSelectedIndexChanged="grvPopupTrans_SelectedIndexChanged">
                                            <EmptyDataTemplate>
                                                <div style="text-align: center; color: red; font-size: 18px;">
                                                    No Record Not Found !
                                                </div>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="VENDTYPE" HeaderText="Vendor Type" />
                                                <asp:BoundField DataField="VENDCODETRIM" HeaderText="Vendor Code" />
                                                <asp:BoundField DataField="NAME" HeaderText="Name" />
                                                <asp:BoundField DataField="SHORTNM" HeaderText="Short Name" />
                                                <asp:BoundField DataField="ZONE" HeaderText="Zone" />
                                                <asp:BoundField DataField="REGION" HeaderText="Region" />
                                                <asp:BoundField DataField="STATE" HeaderText="State" />
                                                <asp:BoundField DataField="CITY" HeaderText="City" />
                                                <asp:BoundField DataField="POSTALCODE" HeaderText="Postal Code" />
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





    <%--<asp:ValidationSummary ID="val1" runat="server" ValidationGroup="SaveItem" ShowMessageBox="true" ShowSummary="false" />
    <asp:ValidationSummary ID="val2" runat="server" ValidationGroup="SaveAll" ShowMessageBox="true" ShowSummary="false" />--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmTranMMPO" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />
</asp:Content>
