<%@ Page Title="" Language="C#" MasterPageFile="~/SD/SDMaster.Master" AutoEventWireup="true" CodeBehind="frmSO.aspx.cs" Inherits="ShERPa360net.SD.frmSO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>SO</title>
    <style type="text/css">
        ul, li, body {
            margin: 0;
            padding: 0;
        }
        /* MultiView Tab Using Menu Control */
        .tabs {
            position: relative;
            top: 1px;
            z-index: 2;
        }

        #ContentPlaceHolder1_Menu1 a.static {
            text-decoration: none;
            border-style: solid !important;
            padding-left: 0em !important;
            padding-right: 0em !important;
        }

        .tab {
            border: 4px solid white;
            background-image: url(images/navigation.jpg);
            background-repeat: repeat-x;
            color: black;
            background-color: aqua !important;
            padding: 3px 10px;
        }

        .selectedtab {
            background: none;
            background-color: blue;
            background-repeat: repeat-x;
            color: black;
        }

        .tabcontents {
            border: 1px solid black;
            padding: 10px;
            width: 600px;
            height: 500px;
            background-color: white;
        }
    </style>
    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            debugger;
            var SO = $("#ContentPlaceHolder1_txtSONO").val();
            var CUST = $("#ContentPlaceHolder1_txtCustomer").val();
            var SHIP = $("#ContentPlaceHolder1_txtShipment").val();
            var CUST = $("#ContentPlaceHolder1_txtSODATE").val();
            var refno = $("#ContentPlaceHolder1_txtRefNo").val();
            var refdat = $("#ContentPlaceHolder1_txtRefDate").val();
            var comm = $("#ContentPlaceHolder1_ddlCommAgent").val();
            var custn = $("#ContentPlaceHolder1_txtCustomerName").val();
            var shipn = $("#ContentPlaceHolder1_txtShipmentName").val();
            var pay = $("#ContentPlaceHolder1_ddlPaymentTerms").val();
            var scheme = $("#ContentPlaceHolder1_ddlSalesScheme").val();
            var remarks = $("#ContentPlaceHolder1_txtRemarks").val();
            var salesch = $("#ContentPlaceHolder1_ddlSalesChannel").val();
            var custna = $("#ContentPlaceHolder1_txtCustName").val();
            var addr1 = $("#ContentPlaceHolder1_txtAddress1").val();
            var addr2 = $("#ContentPlaceHolder1_txtAddress2").val();
            var addr3 = $("#ContentPlaceHolder1_txtAddress3").val();
            var pin = $("#ContentPlaceHolder1_txtPincode").val();
            var state = $("#ContentPlaceHolder1_ddlState").val();
            var city = $("#ContentPlaceHolder1_ddlCity").val();
            var country = $("#ContentPlaceHolder1_ddlCountry").val();
            var mbile = $("#ContentPlaceHolder1_txtMobileNo").val();
            var email = $("#ContentPlaceHolder1_txtEmail").val();

            if (SO != "" && CUST != "" && SHIP != "" && refno != "" && refdat != "" && comm != "" && custn != "" && shipn != "" && pay != "" && scheme != "" && remarks != "" &&
                salesch != "" && custna != "" && addr1 != "" && addr2 != "" && addr3 != "" && pin != "" && state != "" && city != "" && country != "" && mbile != "" && email != "") {
                document.getElementById("busy-holder1").style.display = "";
                document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";
            }
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="updItem" runat="server" UpdateMode="Always">
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="ddlUOM" EventName="SelectedIndexChanged" />--%>
            <%--<asp:PostBackTrigger ControlID="ddlUOM" />--%>
        </Triggers>
        <ContentTemplate>

            <div class="page-content-wrap">

                <div class="row">
                    <div class="col-md-12">

                        <div class="form-horizontal">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Create  </strong>Sales Order</h3>


                                    <asp:LinkButton ID="imgCancel" runat="server" CssClass="btn btn-success pull-right" PostBackUrl="~/SD/frmViewSO.aspx" Text="Cancel"><i class="fa fa-times"></i></asp:LinkButton>
                                    <asp:LinkButton ID="imgSaveAll" OnClientClick="ShowLoading()" runat="server" CssClass="btn btn-success pull-right" OnClick="imgSaveAll_Click" Text="Save All" ValidationGroup="SaveAll"><i class="fa fa-save"></i></asp:LinkButton>
                                    <div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                        <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                        <label>Please wait...</label>
                                    </div>
                                </div>

                                <div class="panel-body">

                                    <div class="row">


                                        <div class="col-md-12">
                                            <h4 style="color: #f05423">SO Details</h4>
                                            <div class="row">

                                                <div class="col-md-12">

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Doc. Type : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList ID="ddlDoctype" runat="server" CssClass="form-control required_text_box" Enabled="true" placeholder="Doc. type" OnSelectedIndexChanged="ddlDoctype_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvDocType" runat="server" ControlToValidate="ddlDoctype" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Select Doc Type" InitialValue="0">Please Select Doc Type</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Segment : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList ID="ddlSegment" runat="server" CssClass="form-control required_text_box" Enabled="true" placeholder="Segment"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvSegment" runat="server" ControlToValidate="ddlSegment" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Select Segment" InitialValue="0">Please Select Segment</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Dist. Chnl. : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList ID="ddlDistChnl" runat="server" CssClass="form-control required_text_box" Enabled="true" placeholder="Dist. Chnl."></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvDistChnl" runat="server" ControlToValidate="ddlDistChnl" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Select Dist. Chnl." InitialValue="0">Please Select Dist. Chnl.</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">SO Date : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <div class="input-group">
                                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                    <asp:TextBox ID="txtSODATE" runat="server" placeholder="SO Date" class="form-control datepicker" Enabled="true"></asp:TextBox>

                                                                </div>
                                                                <asp:RequiredFieldValidator ID="rfvPODate" runat="server" ControlToValidate="txtSODATE" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter SO Date">Please Enter SO Date</asp:RequiredFieldValidator>
                                                            </div>

                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="col-md-12" style="margin-top: 10px !important;">

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">SO No. : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtSONO" runat="server" CssClass="form-control required_text_box" Enabled="false" placeholder="SO No."></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvSONO" runat="server" ControlToValidate="txtSONO" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter SO Number">Please Enter SO Number</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Ref. No. : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtRefNo" runat="server" CssClass="form-control required_text_box" Enabled="true" placeholder="Ref. No." OnTextChanged="txtRefNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvRefNo" runat="server" ControlToValidate="txtRefNo" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Ref. Number">Please Enter Ref. Number</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Ref. Date : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <div class="input-group">
                                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                    <asp:TextBox ID="txtRefDate" runat="server" placeholder="Ref. Date" class="form-control datepicker" Enabled="true"></asp:TextBox>
                                                                </div>
                                                                <asp:RequiredFieldValidator ID="rfvRefDate" runat="server" ControlToValidate="txtRefDate" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Ref. Date">Please Enter Ref. Date</asp:RequiredFieldValidator>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Comm. Agent : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList ID="ddlCommAgent" runat="server" CssClass="form-control required_text_box" Enabled="true" placeholder="Comm. Agent"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlCommAgent" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Select Comm. Agent" InitialValue="0">Please Select Comm. Agent</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>


                                                </div>

                                                <div class="col-md-12" style="margin-top: 10px !important;">

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Customer : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <div class="input-group">
                                                                    <asp:TextBox ID="txtCustomer" runat="server" CssClass="form-control required_text_box" Enabled="true" placeholder="Customer Code" OnTextChanged="txtCustomer_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                    <span class="input-group-btn">
                                                                        <asp:LinkButton ID="lnkOpenCustomerPopup" runat="server" CssClass="btn btn-success" OnClick="lnkOpenCustomerPopup_Click" Text="Open Popup" Enabled="true"><span class="fa fa-search"></span></asp:LinkButton>
                                                                    </span>
                                                                </div>
                                                                <asp:RequiredFieldValidator ID="rfvCustomer" runat="server" ControlToValidate="txtCustomer" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Customer Code">Please Enter Customer Code</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <div class="col-md-12 col-xs-12">
                                                                <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control required_text_box" Enabled="false" placeholder="Customer Name"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvCustomerName" runat="server" ControlToValidate="txtCustomerName" ValidationGroup="SaveAll" Style="color: red;"
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
                                                                    <span class="input-group-btn">
                                                                        <asp:LinkButton ID="lnkOpenShipmentPopup" runat="server" CssClass="btn btn-success" OnClick="lnkOpenShipmentPopup_Click" Text="Open Popup" Enabled="true"><span class="fa fa-search"></span></asp:LinkButton>
                                                                    </span>
                                                                </div>
                                                                <asp:RequiredFieldValidator ID="rfvShipment" runat="server" ControlToValidate="txtShipment" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Shipment Code">Please Enter Shipment Code</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <div class="col-md-12 col-xs-12">
                                                                <asp:TextBox ID="txtShipmentName" runat="server" CssClass="form-control required_text_box" Enabled="false" placeholder="Shipment Name"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvShipmentName" runat="server" ControlToValidate="txtShipmentName" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Shipment Name">Please Enter Shipment Name</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="col-md-12" style="margin-top: 10px !important;">


                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Payment Terms : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList ID="ddlPaymentTerms" runat="server" CssClass="form-control required_text_box" Enabled="true" placeholder="Payment Terms"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvPaymentTerms" runat="server" ControlToValidate="ddlPaymentTerms" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Select Payment Terms" InitialValue="0">Please Select Payment Terms</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">Sales Scheme : </label>
                                                            <div class="col-md-9 col-xs-12">
                                                                <asp:DropDownList ID="ddlSalesScheme" runat="server" CssClass="form-control required_text_box" Enabled="true" placeholder="Sales Scheme" AutoPostBack="true" OnSelectedIndexChanged="ddlSalesScheme_SelectedIndexChanged"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvSalesScheme" runat="server" ControlToValidate="ddlSalesScheme" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Select Sales Scheme" InitialValue="0">Please Select Sales Scheme</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Remarks : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" Enabled="true" placeholder="Remarks"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">GST No. : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtGSTNo" runat="server" CssClass="form-control" Visible="true"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPartialAmount" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Enter Partial Amount" Enabled="false">Enter Partial Amount</asp:RequiredFieldValidator>--%>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="col-md-12" style="margin-top: 10px !important;">

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Payment Mode : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList ID="ddlPayMode" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPayMode_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvPaymode" runat="server" ControlToValidate="ddlPayMode" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Select Payment Mode" InitialValue="0">Select Payment Mode</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label" id="lblOldSoNo" runat="server" visible="false">Old SO No. : </label>
                                                            <div class="col-md-9 col-xs-12">
                                                                <asp:TextBox ID="txtOldSONo" runat="server" CssClass="form-control" OnTextChanged="txtOldSONo_TextChanged" AutoPostBack="true" Visible="false" MaxLength="10"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvoldSoNo" runat="server" ControlToValidate="txtOldSONo" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Enter Old SO No." Enabled="false">Enter Old SO No.</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label"></label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtPartialAmount" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvPartialAmount" runat="server" ControlToValidate="txtPartialAmount" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Enter Partial Amount" Enabled="false">Enter Partial Amount</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>



                                                </div>

                                                <div class="col-md-12" style="margin-top: 10px !important;" id="divTransaction" runat="server">

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Transaction ID : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtTXNID" runat="server" CssClass="form-control"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvTXNID" runat="server" ControlToValidate="txtTXNID" ValidationGroup="SaveAll" Style="color: red;"
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
                                                                <asp:RequiredFieldValidator ID="rfvTXNDT" runat="server" ControlToValidate="txtTXNDT" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Transaction Date" Enabled="false">Please Enter Transaction Date</asp:RequiredFieldValidator>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Payment Gateway : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList ID="ddlPayGateway" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvPayGateway" runat="server" ControlToValidate="ddlPayGateway" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Select Payment gateway" InitialValue="0" Enabled="false">Select Payment gateway</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="col-md-12" style="margin-top: 10px !important;">

                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Net Amount : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtNetAmount" runat="server" CssClass="form-control required_text_box" Enabled="false" placeholder="Net Amount" Text="0"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvNetAmount" runat="server" ControlToValidate="txtNetAmount" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Net Amount">Please Enter Net Amount</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Tax Amount : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtTaxAmount" runat="server" CssClass="form-control required_text_box" Enabled="false" placeholder="Tax Amount" Text="0"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvTaxAmount" runat="server" ControlToValidate="txtTaxAmount" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Tax Amount">Please Enter Tax Amount</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Discount : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtDiscount" runat="server" CssClass="form-control required_text_box" Enabled="false" placeholder="Discount Amount" Text="0"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvDiscount" runat="server" ControlToValidate="txtDiscount" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Discount Amount">Please Enter Discount Amount</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>

                                                <div class="col-md-12" style="margin-top: 10px !important;">

                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Total Amount : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtTotalAmount" runat="server" CssClass="form-control required_text_box" Enabled="false" placeholder="Total Amount" Text="0"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvTotalAmount" runat="server" ControlToValidate="txtTotalAmount" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Total Amount">Please Enter Total Amount</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Others : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtOthers" runat="server" CssClass="form-control required_text_box" Enabled="false" placeholder="Others" Text="0"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvOthers" runat="server" ControlToValidate="txtOthers" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Others">Please Enter Others</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-4">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Raound Off : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtRoundOff" runat="server" CssClass="form-control required_text_box" Enabled="false" placeholder="Round Off" Text="0"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvRoundOff" runat="server" ControlToValidate="txtRoundOff" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Round Off Amount">Please Enter Round Off Amount</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>

                                            </div>

                                        </div>

                                        <div class="page-content-wrap" style="margin-top: 10px !important;">


                                            <div class="row">
                                                <div class="col-md-12">


                                                    <%--<div class="panel panel-default tabs">
                                                        <ul class="nav nav-tabs" role="tablist">
                                                            <li class="active" id="lnkFirst" runat="server"><a href="#tab-first" role="tab" data-toggle="tab">Customer Detail</a></li>
                                                            <li id="lnkSecond" runat="server"><a href="#tab-second" role="tab" data-toggle="tab">Item Detail </a></li>
                                                            <li id="lnkThird" runat="server"><a href="#tab-third" role="tab" data-toggle="tab">Other Charges </a></li>
                                                        </ul>
                                                        <div class="panel-body tab-content">

                                                            <div class="tab-pane active" id="tab-first">
                                                               
                                                            </div>



                                                            <div class="tab-pane" id="tab-second">
                                                               
                                                            </div>



                                                            <div class="tab-pane" id="tab-third">
                                                              
                                                            </div>

                                                        </div>

                                                    </div>--%>

                                                    <asp:Menu ID="Menu1" Orientation="Horizontal" StaticMenuItemStyle-CssClass="tab"
                                                        StaticSelectedStyle-CssClass="selectedtab" CssClass="tabs" runat="server"
                                                        OnMenuItemClick="Menu1_MenuItemClick">
                                                        <Items>
                                                            <asp:MenuItem Text="Customer Details" Value="0" Selected="true"></asp:MenuItem>
                                                            <asp:MenuItem Text="Item Details" Value="1"></asp:MenuItem>
                                                            <asp:MenuItem Text="Other Charges" Value="2"></asp:MenuItem>
                                                        </Items>
                                                    </asp:Menu>
                                                    <asp:MultiView ID="MultiView1" ActiveViewIndex="0" runat="server">

                                                        <asp:View ID="View1" runat="server">
                                                            <div class="panel-body">
                                                                <div class="row">
                                                                    <div class="col-md-12">
                                                                        <div class="col-md-12">

                                                                            <div class="col-md-4">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Sales Channel : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:DropDownList ID="ddlSalesChannel" runat="server" CssClass="form-control required_text_box" Enabled="true" placeholder="Sales Channel" OnSelectedIndexChanged="ddlSalesChannel_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator ID="rfvSalesChannel" runat="server" ControlToValidate="ddlSalesChannel" ValidationGroup="SaveAll" Style="color: red;"
                                                                                            Display="Dynamic" ErrorMessage="Please Select Sales Channel" InitialValue="0">Please Select Sales Channel</asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-4">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Customer Name : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:TextBox ID="txtCustName" runat="server" CssClass="form-control required_text_box" placeholder="Customer Name"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="rfvCustName" runat="server" ControlToValidate="txtCustName" ValidationGroup="SaveAll" Style="color: red;"
                                                                                            Display="Dynamic" ErrorMessage="Please Enter Customer Name">Please Enter Customer Name</asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </div>

                                                                        <div class="col-md-12" style="margin-top: 10px !important;">

                                                                            <div class="col-md-4">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Address 1 : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control required_text_box" placeholder="Address 1"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="rfvAddress1" runat="server" ControlToValidate="txtAddress1" ValidationGroup="SaveAll" Style="color: red;"
                                                                                            Display="Dynamic" ErrorMessage="Please Enter Address Line 1">Please Enter Address Line 1</asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-4">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Address 2 : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control" placeholder="Address 2"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-4">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Address 2 : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:TextBox ID="txtAddress3" runat="server" CssClass="form-control" placeholder="Address 3"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </div>

                                                                        <div class="col-md-12" style="margin-top: 10px !important;">

                                                                            <div class="col-md-4">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Pincode : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:TextBox ID="txtPincode" runat="server" CssClass="form-control required_text_box" placeholder="Pin Code" OnTextChanged="txtPincode_TextChanged" AutoPostBack="true" TextMode="Number"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="rfvPincode" runat="server" ControlToValidate="txtPincode" ValidationGroup="SaveAll" Style="color: red;"
                                                                                            Display="Dynamic" ErrorMessage="Please Enter Pincode">Please Enter Pincode</asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-4">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">State : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control required_text_box" Enabled="true" placeholder="State" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="ddlState" ValidationGroup="SaveAll" Style="color: red;"
                                                                                            Display="Dynamic" ErrorMessage="Please Select State" InitialValue="0">Please Select State</asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-4">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">City : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control required_text_box" Enabled="true" placeholder="City"></asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="ddlCity" ValidationGroup="SaveAll" Style="color: red;"
                                                                                            Display="Dynamic" ErrorMessage="Please Select City" InitialValue="0">Please Select City</asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </div>

                                                                        <div class="col-md-12" style="margin-top: 10px !important;">

                                                                            <div class="col-md-4">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Country : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control required_text_box" Enabled="true" placeholder="Country"></asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate="ddlCountry" ValidationGroup="SaveAll" Style="color: red;"
                                                                                            Display="Dynamic" ErrorMessage="Please Select Country" InitialValue="0">Please Select Country</asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-4">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Mobile No. : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control required_text_box" placeholder="Mobile Number" MaxLength="10" TextMode="Number"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="rfvMobileNo" runat="server" ControlToValidate="txtMobileNo" ValidationGroup="SaveAll" Style="color: red;"
                                                                                            Display="Dynamic" ErrorMessage="Please Enter Mobile No.">Please Enter Mobile No.</asp:RequiredFieldValidator>
                                                                                        <asp:RegularExpressionValidator ID="revMobileNo" runat="server" ControlToValidate="txtMobileNo" ValidationGroup="SaveAll"
                                                                                            Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid Mobile No." ValidationExpression="^[0-9]{10}$">Invalid Mobile No.</asp:RegularExpressionValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-4">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Email : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control required_text_box" placeholder="Email"></asp:TextBox>
                                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPincode" ValidationGroup="SaveDel" Style="color: red;"
                                                                                        Display="Dynamic" ErrorMessage="Please Enter Pincode">Please Enter Pincode</asp:RequiredFieldValidator>--%>
                                                                                        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ForeColor="Red"
                                                                                            ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid Email</asp:RegularExpressionValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </div>

                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:View>

                                                        <asp:View ID="View2" runat="server">
                                                            <div class="panel-body">
                                                                <div class="row">

                                                                    <div class="col-md-12">
                                                                        <div class="col-md-12">

                                                                            <div class="col-md-3">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Sr. No. : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:TextBox ID="txtSRNO" runat="server" CssClass="form-control required_text_box" placeholder="Sr. No." Text="0"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="rfvSRNO" runat="server" ControlToValidate="txtSRNO" ValidationGroup="SaveDet" Style="color: red;" Enabled="false"
                                                                                            Display="Dynamic" ErrorMessage="Please Enter Sr. No.">Please Enter Sr. No.</asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-3">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">IMEI : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:TextBox ID="txtIMEI" runat="server" CssClass="form-control required_text_box" placeholder="IMEI" OnTextChanged="txtIMEI_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                        <%--<asp:RequiredFieldValidator ID="rfvIMEI" runat="server" ControlToValidate="txtIMEI" ValidationGroup="SaveDet" Style="color: red;"
                                                                                                Display="Dynamic" ErrorMessage="Please Enter IMEI">Please Enter IMEI</asp:RequiredFieldValidator>--%>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-3">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">JOB ID : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:TextBox ID="txtJobId" runat="server" CssClass="form-control required_text_box" placeholder="JOB ID" OnTextChanged="txtJobId_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                        <%--<asp:RequiredFieldValidator ID="rfvJobID" runat="server" ControlToValidate="txtJobId" ValidationGroup="SaveDet" Style="color: red;"
                                                                                                Display="Dynamic" ErrorMessage="Please Enter Job Id">Please Enter Job Id</asp:RequiredFieldValidator>--%>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-3">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Jobsheet IMEI : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:Label ID="lblJSIMEI" runat="server" CssClass="form-control"></asp:Label>
                                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtJobId" ValidationGroup="SaveDet" Style="color: red;"
                                                                                        Display="Dynamic" ErrorMessage="Please Enter Job Id">Please Enter Job Id</asp:RequiredFieldValidator>--%>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </div>

                                                                        <div class="col-md-12" style="margin-top: 10px;">

                                                                            <div class="col-md-3">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Item Code : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <div class="input-group">
                                                                                            <asp:TextBox ID="txtItemCode" runat="server" placeholder="Item Code" AutoPostBack="true" CssClass="form-control required_text_box" OnTextChanged="txtItemCode_TextChanged" Enabled="true"></asp:TextBox>
                                                                                            <span class="input-group-btn">
                                                                                                <asp:LinkButton ID="lnkOpenPoup" runat="server" CssClass="btn btn-success" OnClick="lnkOpenPoup_Click" Text="Open Popup" Enabled="true"><span class="fa fa-search"></span></asp:LinkButton>
                                                                                            </span>
                                                                                        </div>
                                                                                        <asp:RequiredFieldValidator ID="rfvItemCode" ControlToValidate="txtItemCode" runat="server" ValidationGroup="SaveDet" Style="color: red;"
                                                                                            ErrorMessage="Please  Enter Item Code" Display="Dynamic">Please  Enter Item Code</asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-3">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Item Desc. : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:TextBox ID="txtItemDesc" runat="server" placeholder="Item Description" class="form-control required_text_box" ReadOnly="true"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="rfvItemDesc" ControlToValidate="txtItemDesc" runat="server" ValidationGroup="SaveDet" Style="color: red;"
                                                                                            ErrorMessage="Please  Enter Item Desc." Display="Dynamic">Please  Enter Item Desc.</asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-3">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Grade : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:DropDownList ID="ddlGrade" runat="server" CssClass="form-control required_text_box" placeholder="Grade"></asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator ID="rfvGrade" ControlToValidate="ddlGrade" runat="server" ValidationGroup="SaveDet" Style="color: red;"
                                                                                            ErrorMessage="Please Select Grade" InitialValue="0" Display="Dynamic">Please Select Grade</asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>


                                                                            <div class="col-md-3">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label"></label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:TextBox ID="txtItemId" runat="server" placeholder="Item Id" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                        <asp:TextBox ID="txtItemGroup" runat="server" placeholder="Item Group" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                        <asp:TextBox ID="txtItemGroupId" runat="server" placeholder="Item Group Id" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                        <asp:TextBox ID="txtGLCode" runat="server" placeholder="GL Code" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                        <asp:TextBox ID="txtProfitCenter" runat="server" placeholder="Profit Center" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                        <asp:TextBox ID="txtItemText" runat="server" placeholder="Item Text" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                        <asp:TextBox ID="txtSku" runat="server" placeholder="SKU" CssClass="form-control" Visible="false"></asp:TextBox>

                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </div>

                                                                        <div class="col-md-12" style="margin-top: 10px;">
                                                                            <div class="col-md-3">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Qty : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:TextBox ID="txtItemQty" runat="server" CssClass="form-control required_text_box" placeholder="Quantity" Text="1" TextMode="Number"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="rfvItemQty" ControlToValidate="txtItemQty" runat="server" ValidationGroup="SaveDet" Style="color: red;"
                                                                                            ErrorMessage="Please  Enter Item Qty." Display="Dynamic">Please  Enter Item Qty.</asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                            <asp:UpdatePanel ID="upd1" runat="server" UpdateMode="Conditional">
                                                                                <ContentTemplate>
                                                                                    <div class="col-md-3">
                                                                                        <div class="form-group">
                                                                                            <label class="col-md-5 control-label">UOM : </label>
                                                                                            <div class="col-md-7 col-xs-12">
                                                                                                <asp:DropDownList ID="ddlUOM" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlUOM_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                                <asp:RequiredFieldValidator ID="rfvUOM" ControlToValidate="ddlUOM" runat="server" ValidationGroup="SaveDet" Style="color: red;"
                                                                                                    ErrorMessage="Please Select Item UOM" Display="Dynamic" InitialValue="0">Please Select Item UOM</asp:RequiredFieldValidator>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>

                                                                            <div class="col-md-3">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Rate : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:TextBox ID="txtItemBRate" runat="server" placeholder="Rate" CssClass="form-control" OnTextChanged="txtItemBRate_TextChanged" AutoPostBack="true" TextMode="Number"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="rfvItemRate" ControlToValidate="txtItemBRate" runat="server" ValidationGroup="SaveDet" Style="color: red;"
                                                                                            ErrorMessage="Please  Enter Item Rate" Display="Dynamic">Please  Enter Item Rate</asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                    <asp:TextBox ID="txtRate" runat="server" placeholder="Item Text" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                </div>
                                                                            </div>


                                                                            <div class="col-md-3">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Amount : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:TextBox ID="txtAmount" runat="server" placeholder="Amount" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="rfvAmount" ControlToValidate="txtAmount" runat="server" ValidationGroup="SaveDet" Style="color: red;"
                                                                                            ErrorMessage="Please  Enter Amount" Display="Dynamic">Please  Enter Amount</asp:RequiredFieldValidator>


                                                                                    </div>
                                                                                </div>
                                                                            </div>


                                                                        </div>


                                                                        <div class="col-md-12" style="margin-top: 10px;">

                                                                            <div class="col-md-3">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Discount : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:TextBox ID="txtDiscountRate" runat="server" placeholder="Discount" CssClass="form-control" Text="0" OnTextChanged="txtDiscountRate_TextChanged" AutoPostBack="true" TextMode="Number"></asp:TextBox>
                                                                                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtItemRate" runat="server" ValidationGroup="SaveDet"
                                                                                        ErrorMessage="Please  Enter Item Rate"Display="Dynamic">*</asp:RequiredFieldValidator>--%>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-3">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Deli. Date : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <div class="input-group">
                                                                                            <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                                            <asp:TextBox ID="txtDeliveryDate" runat="server" placeholder="Delivery Date" CssClass="form-control datepicker" AutoCompleteType="None"></asp:TextBox>
                                                                                        </div>
                                                                                        <asp:RequiredFieldValidator ID="rfvDeliveryDate" ControlToValidate="txtDeliveryDate" runat="server" ValidationGroup="SaveDet" Style="color: red;"
                                                                                            ErrorMessage="Please  Enter Delivery Date" Display="Dynamic">Please  Enter Delivery Date</asp:RequiredFieldValidator>
                                                                                    </div>

                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-3">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Cust. Part No. : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:TextBox ID="txtCustPartNo" runat="server" placeholder="Cust. Part No." CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-3">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Cust. Part Desc. : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:TextBox ID="txtCustPartDesc" runat="server" placeholder="Cust. Part Desc." CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </div>

                                                                        <div class="col-md-12" style="margin-top: 10px;">

                                                                            <div class="col-md-3">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Plant : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:DropDownList ID="ddlPLant" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPLant_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator ID="rfvPLant" ControlToValidate="ddlPLant" runat="server" ValidationGroup="SaveDet" Style="color: red;"
                                                                                            Display="Dynamic" ErrorMessage="Please Select Plant" InitialValue="0">Please Select Plant</asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-3">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Location : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator ID="rfvLocation" ControlToValidate="ddlLocation" runat="server" ValidationGroup="SaveDet" Style="color: red;"
                                                                                            Display="Dynamic" ErrorMessage="Please Select Location" InitialValue="0">Please Select Location</asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>


                                                                            <div class="col-md-3">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Cost Center : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:DropDownList ID="ddlCostCenter" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                                        <asp:RequiredFieldValidator ID="rfvCostCenter" ControlToValidate="ddlCostCenter" runat="server" ValidationGroup="SaveDet" Style="color: red;"
                                                                                            Display="Dynamic" ErrorMessage="Please Select Cost Center" InitialValue="0">Please Select Cost Center</asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-3">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Item Remars : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:TextBox ID="txtItemRemarks" runat="server" placeholder="Item Remarks" CssClass="form-control"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </div>

                                                                        <div class="col-md-12" style="margin-top: 10px;">

                                                                            <div class="col-md-3">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Operator : </label>
                                                                                    <div class="col-md-7 col-xs-12">
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
                                                                                    <label class="col-md-5 control-label">Condition Type : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:DropDownList ID="ddlConditionType" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlConditionType_SelectedIndexChanged"></asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-3">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Tax Amount : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:TextBox ID="txtTaxTAmount" runat="server" CssClass="form-control" placeholder="Tax Amount" Enabled="false"></asp:TextBox>
                                                                                        <asp:TextBox ID="txtGLCdTax" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                        <asp:HiddenField ID="hfRate" runat="server" />
                                                                                        <asp:HiddenField ID="hfPID" runat="server" />
                                                                                        <asp:HiddenField ID="hfCONDID" runat="server" />
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </div>

                                                                        <div class="col-md-12" style="margin-top: 10px;">
                                                                            <div class="panel-footer">
                                                                                <asp:LinkButton ID="btnSaveDet" runat="server" CssClass="btn btn-success pull-right" OnClick="btnSaveDet_Click" Text="Save Item" ValidationGroup="SaveDet"><i class="fa fa-plus-square"></i></asp:LinkButton>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-12" style="margin-top: 10px;">
                                                                            <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                                                                <asp:GridView ID="grvListItem" runat="server" AutoGenerateColumns="false" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%"
                                                                                    OnRowCommand="grvListItem_RowCommand" EmptyDataText="No Record Found !">
                                                                                    <EmptyDataTemplate>
                                                                                        <asp:Label ID="lblGVEmpty" runat="server" Text="No Data Found"></asp:Label>
                                                                                    </EmptyDataTemplate>
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Sr. No.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label runat="server" ID="lblGVID" Text='<%# Bind("ID") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>


                                                                                        <asp:TemplateField HeaderText="Item Code">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label runat="server" ID="lblGVItemCode" Text='<%# Bind("ITEMCODE") %>'></asp:Label>
                                                                                            </ItemTemplate>
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
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="UOM ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label runat="server" ID="lblGVUOMID" Text='<%# Bind("UOMID") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Grade">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label runat="server" ID="lblGVGrade" Text='<%# Bind("GRADE") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Grade ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label runat="server" ID="lblGVGradeID" Text='<%# Bind("GRADEID") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Qty">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label runat="server" ID="lblGVQty" Text='<%# Bind("ITEMQTY") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Rate">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label runat="server" ID="lblGVRate" Text='<%# Bind("ITEMRATE") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Base Rate">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label runat="server" ID="lblGVBaseRate" Text='<%# Bind("ITEMBRATE") %>'></asp:Label>
                                                                                            </ItemTemplate>
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

                                                                                        <asp:TemplateField HeaderText="Job Id">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label runat="server" ID="lblGVTrackNo" Text='<%# Bind("JOBID") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Remarks">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label runat="server" ID="lblGVRemarks" Text='<%# Bind("ITEMREMARKS") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="IMEI">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label runat="server" ID="lblGVIMEI" Text='<%# Bind("IMEINO") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Item Text">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label runat="server" ID="lblGVITEMTEXT" Text='<%# Bind("ITEMTEXT") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Cust. Part No.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label runat="server" ID="lblGVCUSTPARTNO" Text='<%# Bind("CUSTPARTNO") %>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                        <asp:TemplateField HeaderText="Cust. Part Desc.">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label runat="server" ID="lblGVCUSTPARTDESC" Text='<%# Bind("CUSTPARTDESC") %>'></asp:Label>
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

                                                                                        <asp:TemplateField HeaderText="Lock Amt">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label runat="server" ID="lblLockAmt" Text='<%# Bind("LOCKAMT") %>'></asp:Label>

                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>

                                                                                    </Columns>
                                                                                </asp:GridView>
                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-12" style="margin-top: 10px;">
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
                                                                                                <asp:Label runat="server" ID="lblTaxPOSrNo" Text='<%# Bind("SOSRNO") %>'></asp:Label>
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
                                                        </asp:View>

                                                        <asp:View ID="View3" runat="server">
                                                            <div class="panel-body">
                                                                <div class="row">
                                                                    <div class="col-md-12">

                                                                        <div class="col-md-12">
                                                                            <div class="col-md-3">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Sr. No. : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:TextBox ID="txtSrNoChg" runat="server" CssClass="form-control" placeholder="Sr. No."></asp:TextBox>
                                                                                        <asp:TextBox ID="txtMaxSrNoChg" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </div>

                                                                        <div class="col-md-12" style="margin-top: 15px;">
                                                                            <div class="col-md-3">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Charges : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:DropDownList ID="ddlCharges" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlCharges_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                            <div class="col-md-3">
                                                                                <div class="form-group">
                                                                                    <label class="col-md-5 control-label">Amount : </label>
                                                                                    <div class="col-md-7 col-xs-12">
                                                                                        <asp:TextBox ID="txtChgAmt" runat="server" CssClass="form-control" placeholder="Charges Amount"></asp:TextBox>
                                                                                        <asp:RequiredFieldValidator ID="rfvChargeAmount" runat="server" ControlToValidate="txtChgAmt" Style="color: red;" Display="Dynamic"
                                                                                            ErrorMessage="Enter Charges Amount" ValidationGroup="SaveCharge">Enter Charges Amount</asp:RequiredFieldValidator>
                                                                                    </div>
                                                                                </div>
                                                                            </div>

                                                                        </div>

                                                                        <div class="col-md-12" style="margin-top: 15px;">
                                                                            <div class="panel-footer">
                                                                                <asp:LinkButton ID="lnkSaveCharges" runat="server" CssClass="btn btn-success pull-right" OnClick="lnkSaveCharges_Click" Text="Save Charges" ValidationGroup="SaveCharge"><i class="fa fa-plus-square"></i></asp:LinkButton>

                                                                            </div>
                                                                        </div>

                                                                        <div class="col-md-12" style="margin-top: 15px;">
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
                                                        </asp:View>
                                                    </asp:MultiView>


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
            <div class="modal fade" id="modal-Customer" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" style="color: #337ab7">Customer List</h4>
                        </div>


                        <div class="modal-body">
                            <div class="box-body">
                                <div class="row">

                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Customer Code :</label>
                                                <asp:TextBox ID="txtPopupCustomerCode" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>City :</label>
                                                <%--<asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                <asp:DropDownList ID="ddlPopupCustomerCity" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Customer Name :</label>
                                                <asp:TextBox ID="txtPopupCustomerName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Customer Type :</label>
                                                <%--<asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                <asp:DropDownList ID="ddlPopupCustomerType" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <%--<asp:ImageButton ID="imgFind" CssClass="btnClass" runat="server" OnClick="imgFind_Click" ImageUrl="~/img/icons/find.png" AlternateText="Show Data" ToolTip="Show PR Data" />--%>
                                        <asp:LinkButton ID="lnkPopupCustomerShow" runat="server" CssClass="btn btn-success pull-left" OnClick="lnkPopupCustomerShow_Click" Text="Show Customer"><span class="fa fa-search"></span></asp:LinkButton>
                                    </div>


                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="box">
                                            <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                <asp:GridView ID="grvPopCustomer" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                                    CssClass="table table-hover table-striped table-bordered nowrap" AutoGenerateSelectButton="true" OnSelectedIndexChanged="grvPopCustomer_SelectedIndexChanged">
                                                    <EmptyDataTemplate>
                                                        <div style="text-align: center; color: red; font-size: 18px;">
                                                            No Record Not Found !
                                                        </div>
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:BoundField DataField="CUSTTYPE" HeaderText="Cust. Type" />
                                                        <asp:BoundField DataField="CUSTCODE" HeaderText="Cust. Code" />
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


            <div class="modal fade" id="modal-Shipment" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" style="color: #337ab7">Shipment List</h4>
                        </div>


                        <div class="modal-body">
                            <div class="box-body">
                                <div class="row">

                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Shipper Code :</label>
                                                <asp:TextBox ID="txtPopupShipperCode" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>City :</label>
                                                <%--<asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                <asp:DropDownList ID="ddlPopupShipperCity" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Shipper Name :</label>
                                                <asp:TextBox ID="txtPopupShipperCustName" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Shipper Type :</label>
                                                <%--<asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>--%>
                                                <asp:DropDownList ID="ddlPopupShipperType" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <%--<asp:ImageButton ID="imgFind" CssClass="btnClass" runat="server" OnClick="imgFind_Click" ImageUrl="~/img/icons/find.png" AlternateText="Show Data" ToolTip="Show PR Data" />--%>
                                        <asp:LinkButton ID="lnkPopupShipperShow" runat="server" CssClass="btn btn-success pull-left" OnClick="lnkPopupShipperShow_Click" Text="Show Shipper"><span class="fa fa-search"></span></asp:LinkButton>
                                    </div>


                                    <div class="col-md-12" style="margin-top: 5px;">
                                        <div class="box">
                                            <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                <asp:GridView ID="gvShipperList" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                                    CssClass="table table-hover table-striped table-bordered nowrap" AutoGenerateSelectButton="true" OnSelectedIndexChanged="gvShipperList_SelectedIndexChanged">
                                                    <EmptyDataTemplate>
                                                        <div style="text-align: center; color: red; font-size: 18px;">
                                                            No Record Not Found !
                                                        </div>
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:BoundField DataField="CUSTTYPE" HeaderText="Cust. Type" />
                                                        <asp:BoundField DataField="CUSTCODE" HeaderText="Cust. Code" />
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


            <div class="modal fade" id="modal-SODet" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" style="color: #337ab7">SO Detail</h4>
                        </div>


                        <div class="modal-body">
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label ID="lblSODET" runat="server"></asp:Label>
                                    </div>

                                    <div class="col-md-12" style="margin-top: 10px !important;">
                                        <asp:Label ID="lblSODETNEW" runat="server"></asp:Label>
                                    </div>

                                    <div class="col-md-12" style="margin-top: 10px !important;">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <%--<label>Shipper Name :</label>--%>
                                                <asp:Button ID="btnSODETYES" runat="server" OnClick="btnSODETYES_Click" CssClass="btn btn-success pull-right" Text="YES" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <%--<label>Shipper Name :</label>--%>
                                                <asp:Button ID="btnSODETNO" runat="server" OnClick="btnSODETNO_Click" CssClass="btn btn-danger pull-left" Text="NO" />
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
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlLocation" EventName="SelectedIndexChanged" />
            <%--<asp:PostBackTrigger ControlID="ddlLocation" />--%>
        </Triggers>
    </asp:UpdatePanel>





</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranSDSO" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranSD" runat="server" />

</asp:Content>
