<%@ Page Title="" Language="C#" MasterPageFile="~/SD/SDMaster.Master" AutoEventWireup="true" CodeBehind="frmPOS.aspx.cs" Inherits="ShERPa360net.SD.frmPOS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        .icon1 {
            text-align: center;
            vertical-align: middle;
            height: 116px;
            width: 116px;
            border-radius: 50%;
            color: #999999;
            margin: 0 auto 20px;
            border: 4px solid #fe970a;
            transition: all 0.2s;
            -webkit-transition: all 0.2s;
        }

        .imgcircle {
            height: 65px !important;
            width: 65px !important;
            margin-top: 20px !important;
        }

        .shadow {
            -moz-box-shadow: 3px 3px 5px 6px #fe970a !important;
            -webkit-box-shadow: 0 0 5px #cccccc87 !important;
            /*  box-shadow:         3px 3px 5px 6px #fe970a!important;*/
            border: 5px solid #cccccc87 !important;
            box-shadow: 0 0 5px #cccccc87 !important;
            background: #fff;
        }

        .fullbodycl {
            background-color: #f2f2f2 !important;
        }

        .textboxbordercolor {
            border-color: #3d2a70b3 !important;
        }
    </style>

    <style type="text/css">
        .chclass {
        }

            .chclass label {
                position: relative;
                top: -2px;
                left: -5px;
            }

            .chclass input[type="radio"] {
                margin: 10px 10px 0px;
            }
        /*  .chclass input {
                position: absolute;
                top: 10px;
                left: 10px;
            }*/
    </style>

    <script type="text/javascript" src="http://jqueryjs.googlecode.com/files/jquery-1.3.1.js">
    </script>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>



    <%--<script type="text/javascript">
        function askConfirmation() {
            var result = confirm("Is Demo/Installation required for this product..?");
            document.getElementById('<%= hdnUserChoice.ClientID %>').value = result ? "Yes" : "No";
            return true; // Allow postback
        }
    </script>--%>

    <script type="text/javascript">
        function askConfirmation() {
            event.preventDefault(); // Prevent default form submit

            Swal.fire({
                title: 'Is Demo/Installation required for this product..?',
                icon: 'question',
                showCancelButton: true,
                confirmButtonText: 'Yes',
                cancelButtonText: 'No',
                reverseButtons: false
            }).then((result) => {
                if (result.isConfirmed) {
                    document.getElementById('<%= hdnUserChoice.ClientID %>').value = 'Yes';
                } else {
                    document.getElementById('<%= hdnUserChoice.ClientID %>').value = 'No';
                }
                // Trigger postback
                __doPostBack('<%= btnSearch.UniqueID %>', '');
            });

            return false; // Prevent initial postback
        }
    </script>

    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            //debugger;
            var txtMobileNo = document.getElementById('<%= txtMobileNo.ClientID %>');
            var txtName = document.getElementById('<%= txtName.ClientID %>');
            var txtAddress = document.getElementById('<%= txtAddress.ClientID %>');
            var txtPincode = document.getElementById('<%= txtPincode.ClientID %>');
            var txtPartialAmount = document.getElementById('<%= txtPartialAmount.ClientID %>');
            var lblTotalAmt = document.getElementById('<%= lblTotalAmt.ClientID %>');
            //debugger;
            var ddlCommAgent = document.getElementById('<%= ddlCommAgent.ClientID %>');
            var ddlCommAgentindex = ddlCommAgent.selectedIndex;
            //debugger;
            var ddlState = document.getElementById('<%= ddlState.ClientID %>');
            var ddlStateindex = ddlState.selectedIndex;
            //debugger;
            var ddlCity = document.getElementById('<%= ddlCity.ClientID %>');
            var ddlCityindex = ddlCity.selectedIndex;
            // debugger;
            var ddlPayMode = document.getElementById('<%= ddlPayMode.ClientID %>');
            var ddlPayModeindex = ddlPayMode.selectedIndex;
            var ddlPayModevalue = ddlPayMode.value;
            // debugger;
            if (ddlPayModevalue == "5" || ddlPayModevalue == "6" || ddlPayModevalue == "12" || ddlPayModevalue == "15") {
                var txtTXNID = document.getElementById('<%= txtTXNID.ClientID %>');
                var txtTXNDT = document.getElementById('<%= txtTXNDT.ClientID %>');
                var ddlPayGateway = document.getElementById('<%= ddlPayGateway.ClientID %>');
                var ddlPayGatewayindex = ddlPayGateway.selectedIndex;
            }

            var ddlReference = document.getElementById('<%= ddlReference.ClientID %>');
            var ddlReferenceindex = ddlReference.selectedIndex;
            var ddlReferencevalue = ddlReference.value;
            if (ddlReferencevalue == "22492" || ddlReferencevalue == "154") {
                var txtRefName = document.getElementById('<%= txtRefName.ClientID %>');
            }

            // debugger;
            var gvList = document.getElementById('<%= gvList.ClientID %>');
            var rows = gvList.getElementsByTagName('tr');
            //debugger;

            if (rows.length > 1) {
                //debugger;
                if (txtMobileNo.value != "" && txtName.value != "" && txtAddress.value != "" && txtPincode.value != "" && txtPartialAmount.value != "" && lblTotalAmt.innerText != "" && ddlCommAgentindex > 0 && ddlStateindex > 0 && ddlCityindex > 0 && ddlPayModeindex > 0 && ddlReferenceindex > 0) {
                    //debugger;

                    if (ddlPayModevalue == "9" && (txtPartialAmount.value != lblTotalAmt.innerText)) {
                        alert("Received Amount and Total amount needs to ba same for Cash Collact..!");
                    }
                    else {
                        //alert("Java script success..!");
                        document.getElementById("busy-holder1").style.display = "";
                        document.getElementById("ContentPlaceHolder1_btnSave").style.display = "none";
                    }
                }
                else {
                    //debugger;
                    alert("Please fill require values..!");
                }
            }
            else {
                //debugger;
                alert("Please add items to create invoce..!");
            }

            //if (txtMobileNo != "" && txtName != "" && txtAddress != "" && txtPincode != "" && txtPartialAmount != "" && comm != "" && custn != "" && shipn != "" && pay != "" && scheme != "" && remarks != "" &&
            //    salesch != "" && custna != "" && addr1 != "" && addr2 != "" && addr3 != "" && pin != "" && state != "" && city != "" && country != "" && mbile != "" && email != "") {
            //    document.getElementById("busy-holder1").style.display = "";
            //    document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";
            //}
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12" style="box-shadow: 5px 10px 18px darkblue;">
                <div class="form-horizontal">
                    <%--<div class="panel panel-default">--%>

                    <%--<div class="panel-heading">--%>
                    <%--<h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Vendor Registration</strong></h3>--%>
                    <%--<asp:UpdatePanel ID="upd1" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger EventName="TextChanged" ControlID="txtMobileNo" />

                        </Triggers>
                        <ContentTemplate>--%>


                    <div class="col-md-12">
                        <div class="col-md-9">
                            <asp:Image ID="imgPOS" runat="server" CssClass="btn btn-success pull-left" AlternateText="POS System" ImageUrl="~/img/images.png" Style="background: unset; height: 125px;" />
                        </div>
                        <%--<asp:Image ID="imgPOS2" runat="server" CssClass="btn btn-success pull-right" AlternateText="POS System" ImageUrl="~/img/images.png" />--%>
                        <div class="col-md-3">
                            <div class="row" style="padding: 10px !important;">
                                <div class="col-md-4 col-xs-12">
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 col-xs-12">
                                    <asp:Label ID="lblTotalLabel" runat="server" CssClass="pull-left" Text="Total : " Font-Size="Larger"></asp:Label>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12 col-xs-12">
                                    <asp:Label ID="lblTotalAmt" runat="server" CssClass="pull-left" Text="0" Font-Size="XX-Large"></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-9 col-xs-12">
                                    <asp:TextBox ID="txtCouponCode" runat="server" CssClass="form-control pull-right" Placeholder="Coupon Code" OnTextChanged="txtCouponCode_TextChanged" AutoPostBack="true" Visible="false"></asp:TextBox>
                                </div>

                                <div class="col-md-3 col-xs-12">
                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success pull-right" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" OnClientClick="ShowLoading()" />
                                    <div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                        <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                        <label>Please wait...</label>
                                    </div>
                                </div>
                            </div>
                        </div>


                    </div>

                    <div class="col-md-12">
                        <div class="row" style="padding: 10px !important;">
                            <div class="col-md-4 col-xs-12">
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <fieldset class="scheduler-border shadow" runat="server" id="dvPersonalDetail" visible="true">
                            <legend class="scheduler-border">Customer Details</legend>
                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">Mobile No. : </label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" placeholder="Mobile Number" OnTextChanged="txtMobileNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvMobleNo" Style="color: red;" ControlToValidate="txtMobileNo" runat="server" ValidationGroup="Save"
                                                ErrorMessage="Enter Mobile Number" Display="Dynamic">
                                                Enter Mobile Number</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">Name : </label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Customer Name"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvName" Style="color: red;" ControlToValidate="txtName" runat="server" ValidationGroup="Save"
                                                ErrorMessage="Enter Customer Name" Display="Dynamic">
                                                Enter Customer Name</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">GST No. : </label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:TextBox ID="txtGSTNO" runat="server" CssClass="form-control" placeholder="GST No."></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="rgvGSTNo" runat="server" ControlToValidate="txtGSTNO" ValidationGroup="Save" Enabled="true"
                                                Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid GST No." ValidationExpression="[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}[A-Z]{1}[0-9A-Z]{1}$">
                                                Invalid GST No.</asp:RegularExpressionValidator>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Style="color: red;" ControlToValidate="txtMobileNo" runat="server" ValidationGroup="Save"
                                                ErrorMessage="Enter Mobile Number" Display="Dynamic">Enter Mobile Number</asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">Comm. Agent : </label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:DropDownList ID="ddlCommAgent" runat="server" CssClass="form-control required_text_box" Enabled="true" placeholder="Comm. Agent"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCommAgent" ValidationGroup="Save" Style="color: red;"
                                                Display="Dynamic" ErrorMessage="Please Select Comm. Agent" InitialValue="0">
                                                Please Select Comm. Agent</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">Address 1 : </label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" placeholder="Address 1" MaxLength="30"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvAddress" Style="color: red;" ControlToValidate="txtAddress" runat="server" ValidationGroup="Save"
                                                ErrorMessage="Enter Address" Display="Dynamic">
                                                Enter Address</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">Address 2 : </label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control" placeholder="Address 2" MaxLength="30"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" ControlToValidate="txtAddress" runat="server" ValidationGroup="Save"
                                                ErrorMessage="Enter Address" Display="Dynamic">Enter Address</asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">Address 3 : </label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:TextBox ID="txtAddress3" runat="server" CssClass="form-control" placeholder="Address 3"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Style="color: red;" ControlToValidate="txtAddress" runat="server" ValidationGroup="Save"
                                                ErrorMessage="Enter Address" Display="Dynamic">Enter Address</asp:RequiredFieldValidator>--%>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">Pincode : </label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:TextBox ID="txtPincode" runat="server" CssClass="form-control" placeholder="Pincode" MaxLength="6" OnTextChanged="txtPincode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPincode" Style="color: red;" ControlToValidate="txtPincode" runat="server" ValidationGroup="Save"
                                                ErrorMessage="Enter Pincode" Display="Dynamic">
                                                Enter Pincode</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">Payment Mode : </label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:DropDownList ID="ddlPayMode" runat="server" CssClass="form-control required_text_box" OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvPaymode" runat="server" ControlToValidate="ddlPayMode" ValidationGroup="Save" Style="color: red;"
                                                Display="Dynamic" ErrorMessage="Select Payment Mode" InitialValue="0">
                                                Select Payment Mode</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3" id="divPartial" runat="server" visible="true">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">Rcvd. Amount</label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:TextBox ID="txtPartialAmount" runat="server" CssClass="form-control" Visible="true" placeholder="Received Amount"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPartialAmount" runat="server" ControlToValidate="txtPartialAmount" ValidationGroup="Save" Style="color: red;"
                                                Display="Dynamic" ErrorMessage="Enter Received Amount" Enabled="true">
                                                Enter Received Amount</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">State : </label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control required_text_box" Enabled="true" placeholder="State" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="ddlState" ValidationGroup="Save" Style="color: red;"
                                                Display="Dynamic" ErrorMessage="Select State" InitialValue="0">
                                                Select State</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">City : </label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control required_text_box" Enabled="true" placeholder="City"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="ddlCity" ValidationGroup="Save" Style="color: red;"
                                                Display="Dynamic" ErrorMessage="Select City" InitialValue="0">
                                                Select City</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12" style="margin-top: 10px !important;" id="divTransaction" runat="server" visible="false">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">Transaction ID : </label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:TextBox ID="txtTXNID" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvTXNID" runat="server" ControlToValidate="txtTXNID" ValidationGroup="Save" Style="color: red;"
                                                Display="Dynamic" ErrorMessage="Enter Transaction ID" Enabled="false">
                                                Enter Transaction ID</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">Transaction Date : </label>
                                        <div class="col-md-7 col-xs-12">
                                            <div class="input-group">
                                                <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                <asp:TextBox ID="txtTXNDT" runat="server" placeholder="SO Date" class="form-control datepicker" Enabled="true"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="rfvTXNDT" runat="server" ControlToValidate="txtTXNDT" ValidationGroup="Save" Style="color: red;"
                                                Display="Dynamic" ErrorMessage="Please Enter Transaction Date" Enabled="false">
                                                Please Enter Transaction Date</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">Payment Gateway : </label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:DropDownList ID="ddlPayGateway" runat="server" CssClass="form-control"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvPayGateway" runat="server" ControlToValidate="ddlPayGateway" ValidationGroup="Save" Style="color: red;"
                                                Display="Dynamic" ErrorMessage="Select Payment gateway" InitialValue="0" Enabled="false">
                                                Select Payment gateway</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">Reference : </label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:DropDownList ID="ddlReference" runat="server" CssClass="form-control required_text_box" OnSelectedIndexChanged="ddlReference_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rfvReference" runat="server" ControlToValidate="ddlReference" ValidationGroup="Save" Style="color: red;"
                                                Display="Dynamic" ErrorMessage="Select Reference" InitialValue="0">
                                                Select Reference</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3" runat="server" id="divrefname" visible="false">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">Reference Name : </label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:TextBox ID="txtRefName" runat="server" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvRefName" runat="server" ControlToValidate="txtRefName" ValidationGroup="Save" Style="color: red;"
                                                Display="Dynamic" ErrorMessage="Enter Reference Name" Enabled="false">
                                                Enter Reference Name</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </fieldset>
                    </div>

                    <div class="col-md-12">
                        <div class="row" style="padding: 10px !important;">
                            <div class="col-md-4 col-xs-12">
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <fieldset class="scheduler-border shadow" runat="server" id="Fieldset1" visible="true">
                            <legend class="scheduler-border">Item Details</legend>
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">Search By : </label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:RadioButtonList ID="rblSearchBy" runat="server" RepeatDirection="Horizontal" CssClass="chclass">
                                                <asp:ListItem Text="Job ID" Value="1" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="Serial No." Value="2"></asp:ListItem>
                                                <%--<asp:ListItem Text="Item Code" Value="3"></asp:ListItem>--%>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">Search Value : </label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:TextBox ID="txtSearchVaue" runat="server" CssClass="form-control" placeholder="Search Value"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvSearchValue" Style="color: red;" ControlToValidate="txtSearchVaue" runat="server" ValidationGroup="Search"
                                                ErrorMessage="Enter Search Value" Display="Dynamic">
                                                Enter Search Value</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label"></label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:HiddenField ID="hdnUserChoice" runat="server" />

                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success" Text="Search" ValidationGroup="Search" OnClick="btnSearch_Click" OnClientClick="return askConfirmation();" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>

                    <div class="col-md-12">
                        <div class="row" style="padding: 10px !important;">
                            <div class="col-md-4 col-xs-12">
                            </div>
                        </div>
                    </div>

                    <div class="col-md-12">
                        <fieldset class="scheduler-border shadow" runat="server" id="Fieldset2" visible="true">
                            <legend class="scheduler-border">Selected Item</legend>
                            <div class="col-md-12">
                                <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                    CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" OnRowDataBound="gvList_RowDataBound">
                                    <%--<EmptyDataTemplate>
                                        No Record Found!
                                    </EmptyDataTemplate>--%>
                                    <Columns>
                                        <%--0--%>
                                        <asp:TemplateField HeaderText="Sr. No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSRNO" runat="server" Text='<%# Eval("SRNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--1--%>
                                        <asp:TemplateField HeaderText="Item ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemID" runat="server" Text='<%# Eval("ITEMID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--2--%>
                                        <asp:TemplateField HeaderText="Item Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ITEMCODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--3--%>
                                        <asp:TemplateField HeaderText="Item Desc.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemDesc" runat="server" Text='<%# Eval("ITEMDESC") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--4--%>
                                        <asp:TemplateField HeaderText="Item Grp." Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblITEMGROUPID" runat="server" Text='<%# Eval("ITEMGROUPID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--5--%>
                                        <asp:TemplateField HeaderText="Job ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblJobid" runat="server" Text='<%# Eval("JOBID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--6--%>
                                        <asp:TemplateField HeaderText="Serial No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSerialNo" runat="server" Text='<%# Eval("SERIALNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--7--%>
                                        <asp:TemplateField HeaderText="Qty.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSOQTY" runat="server" Text='<%# Eval("SOQTY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--8--%>
                                        <asp:TemplateField HeaderText="UOM" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--9--%>
                                        <asp:TemplateField HeaderText="Plant" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPLANTCODE" runat="server" Text='<%# Eval("PLANTCODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--10--%>
                                        <asp:TemplateField HeaderText="Location" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLOCCD" runat="server" Text='<%# Eval("LOCCD") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--11--%>
                                        <asp:TemplateField HeaderText="Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("PRICE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--12--%>
                                        <asp:TemplateField HeaderText="Discount">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblPrice" runat="server" Text='<%# Eval("DISCOUNT") %>'></asp:Label>--%>
                                                <asp:TextBox ID="txtDiscount" runat="server" CssClass="form-control" Text='<%# Eval("DISCOUNT") %>' OnTextChanged="txtDiscount_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--13--%>
                                        <asp:TemplateField HeaderText="Net Amt.">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblPrice" runat="server" Text='<%# Eval("DISCOUNT") %>'></asp:Label>--%>
                                                <asp:Label ID="lblNetAmt" runat="server" Text='<%# Eval("NETAMT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--14--%>
                                        <asp:TemplateField HeaderText="Approver">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblPrice" runat="server" Text='<%# Eval("PRICE") %>'></asp:Label>--%>
                                                <asp:TextBox ID="txtByWhome" runat="server" CssClass="form-control" Text='<%# Eval("DISCBYWHOM") %>' OnTextChanged="txtByWhome_TextChanged" AutoPostBack="true"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--15--%>
                                        <asp:TemplateField HeaderText="Demo/Insta.">
                                            <ItemTemplate>
                                                <%--<asp:CheckBox ID="chkDemoInstallation" runat="server" Text="Demo / Installation" OnCheckedChanged="chkDemoInstallation_CheckedChanged" AutoPostBack="true" />--%>
                                                <asp:CheckBox ID="chkDemoInstallation" runat="server" OnCheckedChanged="chkDemoInstallation_CheckedChanged" AutoPostBack="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--16--%>
                                        <asp:TemplateField HeaderText="Tax Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRATE" runat="server" Text='<%# Eval("RATE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--17--%>
                                        <asp:TemplateField HeaderText="Cond id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCONDID" runat="server" Text='<%# Eval("CONDID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--18--%>
                                        <asp:TemplateField HeaderText="Cond Type" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCONDTYPE" runat="server" Text='<%# Eval("CONDTYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--19--%>
                                        <asp:TemplateField HeaderText="Segment" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSEGMENT" runat="server" Text='<%# Eval("SEGMENT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--20--%>
                                        <asp:TemplateField HeaderText="Dist. Chnl." Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDISTCHNL" runat="server" Text='<%# Eval("DISTCHNL") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--21--%>
                                        <asp:TemplateField HeaderText="Deli. Date">
                                            <ItemTemplate>
                                                <%--<asp:UpdatePanel ID="upd2" runat="server">
                                                            <Triggers>
                                                                <asp:AsyncPostBackTrigger ControlID="txtDeliveryDate" EventName="TextChanged" />
                                                            </Triggers>
                                                            <ContentTemplate>--%>
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtDeliveryDate" runat="server" placeholder="Delivery Date" Text='<%# Eval("DELIDATE") %>' CssClass="form-control datepicker" AutoCompleteType="None" OnTextChanged="txtDeliveryDate_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </div>
                                                <%--</ContentTemplate>
                                                        </asp:UpdatePanel>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--22--%>
                                        <asp:TemplateField HeaderText="Job Status" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblJOBSTATUS" runat="server" Text='<%# Eval("JOBSTATUS") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--23--%>
                                        <asp:TemplateField HeaderText="Demo/Insta." Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDEMINST" runat="server" Text='<%# Eval("DEMINST") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--24--%>
                                        <asp:TemplateField HeaderText="EW Charges" Visible="true">
                                            <ItemTemplate>
                                                <asp:RadioButtonList ID="rblEWCharges" runat="server" OnSelectedIndexChanged="rblEWCharges_SelectedIndexChanged" AutoPostBack="true"></asp:RadioButtonList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--25--%>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkTempDelete" runat="server" Text="Remove" OnClick="lnkTempDelete_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </fieldset>
                    </div>

                    <div class="col-md-12">
                        <div class="row" style="padding: 10px !important;">
                            <div class="col-md-4 col-xs-12">
                            </div>
                        </div>
                    </div>

                    <%--  </ContentTemplate>
                    </asp:UpdatePanel>--%>
                </div>
            </div>
        </div>
    </div>



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranSDPOS" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranSD" runat="server" />

</asp:Content>
