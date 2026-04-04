<%@ Page Title="Bulk SO Upload" Language="C#" MasterPageFile="~/SD/SDMaster.Master" AutoEventWireup="true" CodeBehind="frmBulkSOUpload.aspx.cs" Inherits="ShERPa360net.SD.frmBulkSOUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Bulk SO Upload</title>

    <script lang="javascript" type='text/javascript'>
        function ValidateBulkSoCreation() {
            var Isvalidate = true;
            $("#lblfilealert").css("display", "none");
            if ($("#fuImage").get(0).files.length == 0) {
                $("#lblfilealert").text("Please Select the File.");
                $("#lblfilealert").css("display", "block");
                Isvalidate = false;
            }

            if ($("#fuImage").get(0).files.length > 0) {
                var isimglfile = false;
                var filename = $("#fuImage").get(0).files[0].name;
                var fileextensionarray = filename.split(".");
                var fileextension = fileextensionarray[(fileextensionarray.length - 1)];
                if ((fileextension.toUpperCase()).includes("XLSX") || (fileextension.toUpperCase()).includes("XLS")) {
                    var isimglfile = true;
                }

                if (isimglfile == false) {
                    $("#lblfilealert").text("Please Select the only Excel.");
                    $("#lblfilealert").css("display", "block");
                    Isvalidate = false;
                }
            }
            return Isvalidate;
        }

        function ShowSaveLoading() {
            $("#progress").show();
            return true;
        }
    </script>


    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            //debugger;
            var txtCustomer = document.getElementById('<%= txtCustomer.ClientID %>');
            var txtCustomerName = document.getElementById('<%= txtCustomerName.ClientID %>');
            var txtShipment = document.getElementById('<%= txtShipment.ClientID %>');
            var txtShipmentName = document.getElementById('<%= txtShipmentName.ClientID %>');


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
                if (txtMobileNo.value != "" && txtName.value != "" && txtAddress.value != "" && txtPincode.value != "" && txtPartialAmount.value != "" && lblTotalAmt.innerText != "" && ddlCommAgentindex > 0 && ddlStateindex > 0 && ddlCityindex > 0 && ddlPayModeindex > 0
                    && txtCustomer.value != "" && txtCustomerName.value != "" && txtShipment.value != "" && txtShipmentName.value != "" && ddlReferenceindex > 0) {
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

    <style>
        img.file-preview-image {
            width: 100% !important
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Upload SO</strong></h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">

                                        <div class="col-md-6">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">File : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:FileUpload ID="fuImage" ClientIDMode="Static" runat="server" CssClass="file-simple" />
                                                        <asp:Label Style="color: red!important; font-weight: bold!important; display: none!important; padding: 6px;"
                                                            runat="server" ClientIDMode="Static" ID="lblfilealert">Please Select the File to Upload.</asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <asp:LinkButton ID="btnUpload" runat="server" CssClass="btn btn-success" Text="Save All" OnClick="btnUpload_Click" OnClientClick="return ValidateBulkSoCreation();">Upload</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <asp:LinkButton ID="lnkDownload" runat="server" CssClass="btn btn-success" Text="Download Format" OnClick="lnkDownload_Click">Download Format</asp:LinkButton>
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
                                    <fieldset class="scheduler-border shadow" runat="server" id="FSTotal" visible="false">
                                        <div class="col-md-12">
                                            <div class="row" style="padding: 10px !important;">
                                                <div class="col-md-4 col-xs-12">
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12 col-xs-12">
                                                    <asp:Label ID="lblTotalLabel" runat="server" CssClass="pull-right" Text="Total : " Font-Size="Larger"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12 col-xs-12">
                                                    <asp:Label ID="lblTotalAmt" runat="server" CssClass="pull-right" Text="0" Font-Size="XX-Large"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12 col-xs-12">
                                                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-success pull-right" Text="Save" ValidationGroup="Save" OnClick="btnSave_Click" OnClientClick="ShowLoading()" />
                                                    <div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                                        <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                                        <label>Please wait...</label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                </div>

                                <div class="col-md-12">
                                    <fieldset class="scheduler-border shadow" runat="server" id="FSCust" visible="false">
                                        <legend class="scheduler-border">Customer Details</legend>

                                        <div class="col-md-12">

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Customer : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtCustomer" runat="server" CssClass="form-control required_text_box" Enabled="true" placeholder="Customer Code" OnTextChanged="txtCustomer_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            <%--<span class="input-group-btn">
                                                                <asp:LinkButton ID="lnkOpenCustomerPopup" runat="server" CssClass="btn btn-success" OnClick="lnkOpenCustomerPopup_Click" Text="Open Popup" Enabled="true"><span class="fa fa-search"></span></asp:LinkButton>
                                                            </span>--%>
                                                        </div>
                                                        <asp:RequiredFieldValidator ID="rfvCustomer" runat="server" ControlToValidate="txtCustomer" ValidationGroup="Save" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Customer Code">Please Enter Customer Code</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <div class="col-md-12 col-xs-12">
                                                        <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control required_text_box" Enabled="false" placeholder="Customer Name"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvCustomerName" runat="server" ControlToValidate="txtCustomerName" ValidationGroup="Save" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Customer Name">Please Enter Customer Name</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Shipment : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtShipment" runat="server" CssClass="form-control required_text_box" Enabled="true" placeholder="Shipment Code" OnTextChanged="txtShipment_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            <%--<span class="input-group-btn">
                                                                <asp:LinkButton ID="lnkOpenShipmentPopup" runat="server" CssClass="btn btn-success" OnClick="lnkOpenShipmentPopup_Click" Text="Open Popup" Enabled="true"><span class="fa fa-search"></span></asp:LinkButton>
                                                            </span>--%>
                                                        </div>
                                                        <asp:RequiredFieldValidator ID="rfvShipment" runat="server" ControlToValidate="txtShipment" ValidationGroup="Save" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Shipment Code">Please Enter Shipment Code</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <div class="col-md-12 col-xs-12">
                                                        <asp:TextBox ID="txtShipmentName" runat="server" CssClass="form-control required_text_box" Enabled="false" placeholder="Shipment Name"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvShipmentName" runat="server" ControlToValidate="txtShipmentName" ValidationGroup="Save" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Shipment Name">Please Enter Shipment Name</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="col-md-12">

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Mobile No. : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control" placeholder="Mobile Number" OnTextChanged="txtMobileNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvMobleNo" Style="color: red;" ControlToValidate="txtMobileNo" runat="server" ValidationGroup="Save"
                                                            ErrorMessage="Enter Mobile Number" Display="Dynamic">Enter Mobile Number</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Name : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" placeholder="Customer Name"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvName" Style="color: red;" ControlToValidate="txtName" runat="server" ValidationGroup="Save"
                                                            ErrorMessage="Enter Customer Name" Display="Dynamic">Enter Customer Name</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">GST No. : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtGSTNO" runat="server" CssClass="form-control" placeholder="GST No."></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="rgvGSTNo" runat="server" ControlToValidate="txtGSTNO" ValidationGroup="Save" Enabled="true"
                                                            Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid GST No." ValidationExpression="[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}[A-Z]{1}[0-9A-Z]{1}$">Invalid GST No.</asp:RegularExpressionValidator>
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
                                                            Display="Dynamic" ErrorMessage="Please Select Comm. Agent" InitialValue="0">Please Select Comm. Agent</asp:RequiredFieldValidator>
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
                                                            ErrorMessage="Enter Address" Display="Dynamic">Enter Address</asp:RequiredFieldValidator>
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
                                                            ErrorMessage="Enter Pincode" Display="Dynamic">Enter Pincode</asp:RequiredFieldValidator>
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
                                                            Display="Dynamic" ErrorMessage="Select Payment Mode" InitialValue="0">Select Payment Mode</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-3" id="divPartial" runat="server" visible="true">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Rcvd. Amount</label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtPartialAmount" runat="server" CssClass="form-control" Visible="true" placeholder="Received Amount"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvPartialAmount" runat="server" ControlToValidate="txtPartialAmount" ValidationGroup="Save" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Enter Received Amount" Enabled="true">Enter Received Amount</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">State : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control required_text_box" Enabled="true" placeholder="State" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="ddlState" ValidationGroup="Save" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Select State" InitialValue="0">Select State</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">City : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control required_text_box" Enabled="true" placeholder="City"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="ddlCity" ValidationGroup="Save" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Select City" InitialValue="0">Select City</asp:RequiredFieldValidator>
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
                                                            Display="Dynamic" ErrorMessage="Enter Transaction ID" Enabled="false">Enter Transaction ID</asp:RequiredFieldValidator>
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
                                                            Display="Dynamic" ErrorMessage="Please Enter Transaction Date" Enabled="false">Please Enter Transaction Date</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Payment Gateway : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:DropDownList ID="ddlPayGateway" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvPayGateway" runat="server" ControlToValidate="ddlPayGateway" ValidationGroup="Save" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Select Payment gateway" InitialValue="0" Enabled="false">Select Payment gateway</asp:RequiredFieldValidator>
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
                                                            Display="Dynamic" ErrorMessage="Select Reference" InitialValue="0">Select Reference</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3" runat="server" id="divrefname" visible="false">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Reference Name : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtRefName" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvRefName" runat="server" ControlToValidate="txtRefName" ValidationGroup="Save" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Enter Reference Name" Enabled="false">Enter Reference Name</asp:RequiredFieldValidator>
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
                                    <fieldset class="scheduler-border shadow" runat="server" id="FSItem" visible="false">
                                        <legend class="scheduler-border">Selected Item</legend>
                                        <div class="col-md-12">
                                            <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
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
                                                    <%--<asp:TemplateField HeaderText="Discount">
                                                        <ItemTemplate>--%>
                                                    <%--<asp:Label ID="lblPrice" runat="server" Text='<%# Eval("DISCOUNT") %>'></asp:Label>--%>
                                                    <%--<asp:TextBox ID="txtDiscount" runat="server" CssClass="form-control" Text='<%# Eval("DISCOUNT") %>' OnTextChanged="txtDiscount_TextChanged" AutoPostBack="true"></asp:TextBox>--%>
                                                    <%--</ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <%--13--%>
                                                    <asp:TemplateField HeaderText="Net Amt.">
                                                        <ItemTemplate>
                                                            <%--<asp:Label ID="lblPrice" runat="server" Text='<%# Eval("DISCOUNT") %>'></asp:Label>--%>
                                                            <asp:Label ID="lblNetAmt" runat="server" Text='<%# Eval("NETAMT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--14--%>
                                                    <%--<asp:TemplateField HeaderText="Approver">
                                                        <ItemTemplate>--%>
                                                    <%--<asp:Label ID="lblPrice" runat="server" Text='<%# Eval("PRICE") %>'></asp:Label>--%>
                                                    <%--<asp:TextBox ID="txtByWhome" runat="server" CssClass="form-control" Text='<%# Eval("DISCBYWHOM") %>' OnTextChanged="txtByWhome_TextChanged" AutoPostBack="true"></asp:TextBox>--%>
                                                    <%--</ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <%--15--%>
                                                    <%--<asp:TemplateField HeaderText="Demo/Insta.">
                                                        <ItemTemplate>--%>
                                                    <%--<asp:CheckBox ID="chkDemoInstallation" runat="server" Text="Demo / Installation" OnCheckedChanged="chkDemoInstallation_CheckedChanged" AutoPostBack="true" />--%>
                                                    <%--<asp:CheckBox ID="chkDemoInstallation" runat="server" OnCheckedChanged="chkDemoInstallation_CheckedChanged" AutoPostBack="true" />--%>
                                                    <%--</ItemTemplate>
                                                    </asp:TemplateField>--%>
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
                                                                <asp:TextBox ID="txtDeliveryDate" runat="server" placeholder="Delivery Date" Text='<%# Eval("DELIDATE") %>' CssClass="form-control datepicker" AutoCompleteType="None" Enabled="false"></asp:TextBox>
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
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </fieldset>
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

    <input type="hidden" id="menutabid" value="tsmTranSDSO" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranSD" runat="server" />

</asp:Content>
