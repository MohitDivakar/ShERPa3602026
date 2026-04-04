<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/MasterCustomerRltionShipMng.Master" AutoEventWireup="true" CodeBehind="frmVendorMaster.aspx.cs" Inherits="ShERPa360net.CRM.frmVendorMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Vendor Master</title>
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

      <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
            <script type="text/javascript">
                $(document).ready(function () {
                    // restrict special characters
                    $('#txtContactPerson').keypress(function (key) {
                        var regexpns = new RegExp("^[a-zA-Z0-9]+$");
                        var key = String.fromCharCode(event.charCode ? event.which : event.charCode);
                        if (!regexpns.test(key)) {
                            event.preventDefault();
                            document.getElementById("revName1").style.display = 'block';
                            return false;                       
                        }
                        else
                        {
                            document.getElementById("revName1").style.display = 'none';
                        }
                    });
                });
            </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap fullbodycl">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Vendor Registration</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <fieldset class="scheduler-border shadow" runat="server" id="dvPersonalDetail" visible="false">
                                            <legend class="scheduler-border">Personal Details</legend>
                                            <div class="col-md-12" id="divVendType" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Vendor Type :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList ID="ddlVendType" runat="server" CssClass="form-control required_text_box textboxbordercolor" TabIndex="1" Enabled="false"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvVendType" Style="color: red;" ControlToValidate="ddlVendType" runat="server" ValidationGroup="Save1"
                                                                ErrorMessage="Select Vendor Type" Display="Dynamic" InitialValue="0">Select Vendor Type</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divVendCode" runat="server" visible="false">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Vendor Code :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtVendCode" runat="server" CssClass="form-control textboxbordercolor" Enabled="false" TabIndex="2"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvVendCOde" Style="color: red;" ControlToValidate="txtVendCode" runat="server" ValidationGroup="Save1"
                                                                ErrorMessage="Enter Vendor Code" Display="Dynamic">Enter Vendor Code</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divVendCat" runat="server" visible="false">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Vendor Cat. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList ID="ddlVendCategory" runat="server" CssClass="form-control required_text_box textboxbordercolor" TabIndex="3" Enabled="false"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvVendCate" Style="color: red;" ControlToValidate="ddlVendCategory" runat="server" ValidationGroup="Save1"
                                                                ErrorMessage="Select Vendor Category" Display="Dynamic" InitialValue="0">Select Vendor Category</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divNext1" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:LinkButton runat="server" TabIndex="4" ID="lnkNext1" CssClass="btn btn-success pull-right" Text="Next" OnClick="lnkNext1_Click" ValidationGroup="Save1"><i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divDealer" runat="server" visible="false">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Dealer Name :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control required_text_box" TabIndex="5" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvDealer" Style="color: red;" ControlToValidate="ddlDealer" runat="server" ValidationGroup="Save2"
                                                                ErrorMessage="Select Dealer Name" Display="Dynamic" InitialValue="0">Select Dealer Name</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divMobileNo" runat="server" visible="false" style="padding-top: 10px;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Mobile No :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control required_text_box" placeholder="Mobile No" TabIndex="6" MaxLength="10"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvMobieNo" Style="color: red;" ControlToValidate="txtMobileNo" runat="server" ValidationGroup="Save2"
                                                                ErrorMessage="Please Enter Mobile No.">Please Enter Mobile No.</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="revMobileNo" runat="server" ControlToValidate="txtMobileNo" ValidationGroup="Save2"
                                                                Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid Mobile No." ValidationExpression="[0-9]{10}$">Invalid Mobile No.</asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divContactPerson" runat="server" visible="false">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Payment To :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control required_text_box" placeholder="Payment To Name" TabIndex="7" OnTextChanged="txtContactPerson_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvPersonName" Style="color: red;" ControlToValidate="txtContactPerson" runat="server" ValidationGroup="Save2"
                                                                ErrorMessage="Please Enter Person Name">Enter Payment to Name</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="revName1" runat="server" Style="color: red;" ControlToValidate="txtContactPerson" ValidationGroup="Save2" ValidationExpression="^[A-Za-z0-9 _]*$"
                                                                Display="Dynamic" ErrorMessage="Special Character are not allowed.">Special Character are not allowed.</asp:RegularExpressionValidator>
                                                            <asp:Label ID="lblpaymentto" CssClass="lblpaymentto"   runat="server" ClientIDMode="Static" Style="color: red!important; font-weight: bold!important; display: none">Payment To already exists.</asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divContactNo" runat="server" visible="false">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Contact No :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control required_text_box" placeholder="Contact No" TabIndex="8" MaxLength="10" onfocusin="VALIDATEPAYMENTNO()"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvContactNo" Style="color: red;" ControlToValidate="txtContactNo" runat="server" ValidationGroup="Save2"
                                                                ErrorMessage="Please Enter Contact No.">Please Enter Contact No.</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="revContactNo" runat="server" ControlToValidate="txtContactNo" ValidationGroup="Save2"
                                                                Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid Contact No." ValidationExpression="[0-9]{10}$">Invalid Contact No.</asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divSuggestedName" runat="server" visible="false">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Vendor Name :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtSuggestedName" runat="server" CssClass="form-control required_text_box" placeholder="Name" TabIndex="9" onfocusout="ValidateVendorName()"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvSuggestedName" Style="color: red;" ControlToValidate="txtSuggestedName" runat="server" ValidationGroup="Save2"
                                                                ErrorMessage="Please Enter Enter Name">Please Enter Vendor Name</asp:RequiredFieldValidator>
                                                             <asp:RegularExpressionValidator ID="rfvVendorName" runat="server" Style="color: red;" ControlToValidate="txtSuggestedName" ValidationGroup="Save2" ValidationExpression="^[A-Za-z0-9 _]*$"
                                                                Display="Dynamic" ErrorMessage="Special Character are not allowed.">Special Character are not allowed.</asp:RegularExpressionValidator>
                                                            <asp:Label ID="lblvendorname" CssClass="lblvendorname"   runat="server" ClientIDMode="Static" Style="color: red!important; font-weight: bold!important; display: none">Vendor Name already exists.</asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divGroup" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Tally Group :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList ID="ddlTallyGroup" runat="server" CssClass="form-control" TabIndex="9"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="color: red;" ControlToValidate="ddlTallyGroup" runat="server" ValidationGroup="Save2"
                                                                ErrorMessage="Please Select Tally Group" InitialValue="0">Please Select Tally Group</asp:RequiredFieldValidator>
                                                            <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtContactNo" ValidationGroup="Save2"
                                                                Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid Contact No." ValidationExpression="[0-9]{10}$">Invalid Contact No.</asp:RegularExpressionValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divNext2" runat="server" visible="false">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:LinkButton runat="server" ID="lnkPrev2" CssClass="btn btn-success pull-left" TabIndex="10" Text="Previous" OnClick="lnkPrev2_Click" Visible="false"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                                                        <asp:LinkButton runat="server" TabIndex="11" ID="lnkNext2" CssClass="btn btn-success pull-right" Text="Next" OnClick="lnkNext2_Click" ValidationGroup="Save2" OnClientClick="return ValidateVendorNAME();"><i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>


                                        <fieldset class="scheduler-border shadow" runat="server" id="dvContactDetail1" visible="false">
                                            <legend class="scheduler-border">Contact Details</legend>

                                            <div class="col-md-12" id="divAddress1" runat="server" visible="false">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Address 1 :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control required_text_box" TabIndex="12" placeholder="Address 1"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvAddress1" Style="color: red;" ControlToValidate="txtAddress1" runat="server" ValidationGroup="Save3"
                                                                ErrorMessage="Please Enter Dealer Address" Display="Dynamic">Please Enter Dealer Address</asp:RequiredFieldValidator>
                                                             <asp:RegularExpressionValidator ID="rfvAddreess1" runat="server" Style="color: red;" ControlToValidate="txtAddress1" ValidationGroup="Save3" ValidationExpression="^[A-Za-z0-9 _]*$"
                                                                Display="Dynamic" ErrorMessage="Special Character are not allowed.">Special Character are not allowed.</asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divAddress2" runat="server" visible="false" style="padding-top: 5px;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Address 2 :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control" placeholder="Address 2" TabIndex="13"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" ControlToValidate="txtShortName" runat="server" ValidationGroup="Save3"
                                                                        ErrorMessage="Please Enter Dealer Short Name">Please Enter Dealer Short Name</asp:RequiredFieldValidator>--%>
                                                             <asp:RegularExpressionValidator ID="rfvAddreess2" runat="server" Style="color: red;" ControlToValidate="txtAddress2" ValidationGroup="Save3" ValidationExpression="^[A-Za-z0-9 _]*$"
                                                                Display="Dynamic" ErrorMessage="Special Character are not allowed.">Special Character are not allowed.</asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divAddress3" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Address 3 :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtAddress3" runat="server" CssClass="form-control" placeholder="Address 2" TabIndex="14"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Style="color: red;" ControlToValidate="txtShortName" runat="server" ValidationGroup="Save3"
                                                                        ErrorMessage="Please Enter Dealer Short Name">Please Enter Dealer Short Name</asp:RequiredFieldValidator>--%>
                                                             <asp:RegularExpressionValidator ID="rfvAdrress3" runat="server" Style="color: red;" ControlToValidate="txtAddress3" ValidationGroup="Save3" ValidationExpression="^[A-Za-z0-9 _]*$"
                                                                Display="Dynamic" ErrorMessage="Special Character are not allowed.">Special Character are not allowed.</asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divNext3" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:LinkButton runat="server" ID="lnkPrev3" CssClass="btn btn-success pull-left" Text="Previous" TabIndex="15" OnClick="lnkPrev3_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lnkNext3" CssClass="btn btn-success pull-right" Text="Next" TabIndex="16" OnClick="lnkNext3_Click" ValidationGroup="Save3"><i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>

                                        <fieldset class="scheduler-border shadow" runat="server" id="dvContactDetail2" visible="false">
                                            <legend class="scheduler-border">Contact Details</legend>
                                            <div class="col-md-12" id="divPincode" runat="server" visible="false">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Pin Code :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtPincode" runat="server" CssClass="form-control required_text_box" placeholder="Pincode" TabIndex="17" MaxLength="6" OnTextChanged="txtPincode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvPincode" Style="color: red;" ControlToValidate="txtPincode" runat="server" ValidationGroup="Save4"
                                                                ErrorMessage="Please Enter Pincode" Display="Dynamic">Please Enter Pincode</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="revPincode" runat="server" ControlToValidate="txtPincode"
                                                                Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid Pin Code" ValidationExpression="[0-9]{6}$">Invalid Pin Code</asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divState" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">State :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <%--<asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="Address 2"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" TabIndex="18"></asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" ControlToValidate="txtShortName" runat="server" ValidationGroup="Save4"
                                                                        ErrorMessage="Please Enter Dealer Short Name">Please Enter Dealer Short Name</asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divCity" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">City :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control required_text_box" placeholder="City" TabIndex="19"></asp:TextBox>
                                                            <%--<asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                                            <asp:RequiredFieldValidator ID="rfvCity" Style="color: red;" ControlToValidate="txtCity" runat="server" ValidationGroup="Save4"
                                                                ErrorMessage="Please Enter City Name" Display="Dynamic">Please Enter City Name</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divCountry" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Country :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <%--<asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" placeholder="Address 2"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" TabIndex="20"></asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" Style="color: red;" ControlToValidate="txtShortName" runat="server" ValidationGroup="Save4"
                                                                        ErrorMessage="Please Enter Dealer Short Name">Please Enter Dealer Short Name</asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divNext4" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:LinkButton runat="server" ID="lnkPrev4" CssClass="btn btn-success pull-left" TabIndex="21" Text="Previous" OnClick="lnkPrev4_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lnkNext4" CssClass="btn btn-success pull-right" TabIndex="22" Text="Next" OnClick="lnkNext4_Click" ValidationGroup="Save4"><i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>


                                        <fieldset class="scheduler-border shadow" runat="server" id="dvBuisnessDetail1" visible="false">
                                            <legend class="scheduler-border">Shop/Business Details</legend>
                                            <div class="col-md-12" id="divCommunication" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Designation :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList ID="ddlDesignation" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" ControlToValidate="txtShortName" runat="server" ValidationGroup="Save5"
                                                                        ErrorMessage="Please Enter Vendor Short Name">Please Enter Vendor Short Name</asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divManagerName" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Manager Name :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtManagerName" runat="server" CssClass="form-control" placeholder="Manager Name" TabIndex="30"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" ControlToValidate="txtShortName" runat="server" ValidationGroup="Save5"
                                                                        ErrorMessage="Please Enter Vendor Short Name">Please Enter Vendor Short Name</asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divManagerContact" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Manager Contact No. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtManagerContact" runat="server" CssClass="form-control" placeholder="Contact No" TabIndex="31" MaxLength="10"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" ControlToValidate="txtShortName" runat="server" ValidationGroup="Save5"
                                                                        ErrorMessage="Please Enter Vendor Short Name">Please Enter Vendor Short Name</asp:RequiredFieldValidator>--%>
                                                            <asp:RegularExpressionValidator ID="rfvManagerContact" runat="server" ControlToValidate="txtManagerContact"
                                                                Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid Mobile No." ValidationExpression="[0-9]{10}$">Invalid Mobile No.</asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divManagerEmail" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Manager Email :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtManagerEmail" runat="server" CssClass="form-control" placeholder="Email Id" TabIndex="32"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" ControlToValidate="txtShortName" runat="server" ValidationGroup="Save5"
                                                                        ErrorMessage="Please Enter Vendor Short Name">Please Enter Vendor Short Name</asp:RequiredFieldValidator>--%>


                                                            <asp:RegularExpressionValidator ID="revManagerEmail" runat="server" ControlToValidate="txtManagerEmail" Display="Dynamic" ForeColor="Red"
                                                                ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid Email</asp:RegularExpressionValidator>

                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divGridview" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:GridView ID="GridView1" runat="server" CssClass="Grid" AutoGenerateColumns="false"
                                                            EmptyDataText="No records has been added.">
                                                            <Columns>

                                                                <asp:TemplateField HeaderText="Designation">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDesignation" runat="server" Text='<%# Eval("Designation") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Contact">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblContact" runat="server" Text='<%# Eval("Contact") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Email">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblEmail" runat="server" Text='<%# Eval("Email") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <%--<asp:BoundField DataField="Designation" HeaderText="Designation" ItemStyle-Width="120" />
                                                                <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Width="120" />
                                                                <asp:BoundField DataField="Contact" HeaderText="Contact" ItemStyle-Width="120" />
                                                                <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-Width="120" />--%>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divNext5" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:LinkButton runat="server" ID="lnkPrev5" CssClass="btn btn-success pull-left" TabIndex="28" Text="Previous" OnClick="lnkPrev5_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lnkNext5" CssClass="btn btn-success pull-right" TabIndex="29" Text="Next" OnClick="lnkNext5_Click"><i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>


                                        <fieldset class="scheduler-border shadow" runat="server" id="dvBuisnessDetail2" visible="false">
                                            <legend class="scheduler-border">Shop/Business Details</legend>
                                            <div class="col-md-12" id="divEmail" runat="server" visible="false">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Email :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control " placeholder="Email" TabIndex="23"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="color: red;" ControlToValidate="txtMobileNo" runat="server" ValidationGroup="Save6"
                                                                        ErrorMessage="Please Enter Mobile No" Display="Dynamic">Please Enter Mobile No</asp:RequiredFieldValidator>--%>
                                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ForeColor="Red"
                                                                ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid Email</asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divWebsite" runat="server" visible="false" style="padding-top: 5px;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Website :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control " placeholder="Website" TabIndex="24"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="color: red;" ControlToValidate="txtMobileNo" runat="server" ValidationGroup="Save6"
                                                                        ErrorMessage="Please Enter Mobile No" Display="Dynamic">Please Enter Mobile No</asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divAgreement" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Agreement Recieved ? :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <%--<asp:CheckBox ID="chkAgreement" runat="server" CssClass="form-control" TabIndex="47" Checked="true" />--%>
                                                            <asp:RadioButtonList ID="rblAgreement" runat="server" TabIndex="47" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                <asp:ListItem Value="0" Text="NO" Selected="True" class="radio-inline"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="YES"  class="radio-inline"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divSelling" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Mobile selling authorization linked with each Sale ? :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <%--<asp:CheckBox ID="chkMobileSelleing" runat="server" CssClass="form-control" TabIndex="48" Checked="true" />--%>
                                                            <asp:RadioButtonList ID="rblMobileSelling" runat="server" TabIndex="48" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                                <asp:ListItem Value="0" Text="NO" class="radio-inline"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="YES" Selected="True" class="radio-inline"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divNext6" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:LinkButton runat="server" ID="lnkPrev6" CssClass="btn btn-success pull-left" TabIndex="33" Text="Previous" OnClick="lnkPrev6_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lnkNext6" CssClass="btn btn-success pull-right" TabIndex="34" Text="Next" OnClick="lnkNext6_Click"><i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>

                                        <fieldset class="scheduler-border shadow" runat="server" id="dvBuisnessDetail3" visible="false">
                                            <legend class="scheduler-border">Shop/Business Details</legend>

                                            <div class="col-md-12" id="divPAN" runat="server" visible="false" style="padding-top: 5px;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                       
                                                        <label class="col-md-5 control-label">PAN :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtPAN" runat="server" CssClass="form-control " placeholder="PAN" TabIndex="25" MaxLength="20" onfocusout="ValidatePanNo()"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="rfvPAN" Style="color: red;" ControlToValidate="txtPAN" runat="server" ValidationGroup="Save7"
                                                                ErrorMessage="Please Enter PAN" Display="Dynamic">Please Enter PAN</asp:RequiredFieldValidator>--%>
                                                            <asp:RegularExpressionValidator ID="revPAN" runat="server" ControlToValidate="txtPAN" ValidationGroup="Save7"
                                                                Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid PAN Card" ValidationExpression="[A-Z]{5}\d{4}[A-Z]{1}">Invalid PAN Card</asp:RegularExpressionValidator>
                                                            <span id="message"></span>
                                                                    <asp:Label ID="lblpanno" CssClass="lblpanno"   runat="server" ClientIDMode="Static" Style="color: red!important; font-weight: bold!important; display: none">Pancard no already exists.</asp:Label>
                                       
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divFUPAN" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">PAN Image :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:FileUpload ID="fuPAN" runat="server" CssClass="form-control" />
                                                            <%--<input type="file" id="fuPAN" runat="server" class="form-control" accept="image/png, image/jpeg" name="fuPAN" />--%>
                                                            <%--<input type="file" id="avatar" name="avatar" accept="image/png, image/jpeg" runat="server" class="form-control">--%>
                                                            <%--<asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="Account No." TabIndex="25"></asp:TextBox>--%>
                                                            <%--<asp:RequiredFieldValidator ID="rfvFUPAN" Style="color: red;" ControlToValidate="fuPAN" runat="server" ValidationGroup="Save7"
                                                                ErrorMessage="Please Upload PAN Image">Please Upload PAN Image</asp:RequiredFieldValidator>
                                                            <label id="lblPancardalert" style="display:none!important;font-weight:bold;color:red!important;">Please Select the only JPEG,JPG,TIFF,PNG.</label>  --%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>



                                            <div class="col-md-12" id="divAadhar" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Aadhar No. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtAadharNo" runat="server" CssClass="form-control" placeholder="Aadhar No" TabIndex="27" MaxLength="12" onfocusout="ValidateAdharNo()"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvAadhar" Style="color: red;" ControlToValidate="txtAadharNo" runat="server" ValidationGroup="Save7"
                                                                ErrorMessage="Please Enter Aadhar No.">Please Enter Aadhar No.</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="revAadhar" runat="server" ControlToValidate="txtAadharNo" ValidationGroup="Save7"
                                                                Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid Aadhar No." ValidationExpression="[0-9]{12}$">Invalid Aadhar No.</asp:RegularExpressionValidator>

                                                             <asp:Label ID="lbladharno" CssClass="lbladharno"   runat="server" ClientIDMode="Static" Style="color: red!important; font-weight: bold!important; display: none">Adhar no already exists.</asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divIDProof" runat="server" visible="false">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">ID Proof :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:FileUpload ID="fuIDProof" runat="server" CssClass="form-control" />
                                                            <%--<asp:TextBox ID="TextBox1" runat="server" CssClass="form-control " placeholder="Bank Name" TabIndex="24"></asp:TextBox>--%>
                                                            <asp:RequiredFieldValidator ID="rfvFUIDProof" Style="color: red;" ControlToValidate="fuIDProof" runat="server" ValidationGroup="Save7"
                                                                ErrorMessage="Please Upload ID Proof Image" Display="Dynamic">Please Upload ID Proof Image</asp:RequiredFieldValidator>
                                                            <label id="lblIdproofalert" style="display: none!important; font-weight: bold; color: red!important;">Please Select the only JPEG,JPG,TIFF,PNG.</label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>



                                            <div class="col-md-12" id="divNext7" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:LinkButton runat="server" ID="lnkPrev7" CssClass="btn btn-success pull-left" TabIndex="39" Text="Previous" OnClick="lnkPrev7_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lnkNext7" CssClass="btn btn-success pull-right" TabIndex="40"  Text="Next" OnClick="lnkNext7_Click" ValidationGroup="Save7" OnClientClick="return ValidatePanAdhar();"><i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>


                                        <fieldset class="scheduler-border shadow" runat="server" id="dvBuisnessDetail4" visible="false">
                                            <legend class="scheduler-border">Shop/Business Details</legend>
                                            <div class="col-md-12" id="divMaginScheme" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Are you holding GSTIN No ? :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:RadioButtonList ID="rblUnderMarginScheme" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"
                                                                OnSelectedIndexChanged="rblUnderMarginScheme_SelectedIndexChanged" AutoPostBack="true" >
                                                                <asp:ListItem Value="0" Text="NO" Selected="True" class="radio-inline"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="YES" class="radio-inline"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <%--<asp:CheckBox ID="chkUnderMarginScheme" runat="server" CssClass="form-control" TabIndex="46" Checked="true" />--%>
                                                            <%--<asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" placeholder="City" TabIndex="16"></asp:TextBox>--%>
                                                            <%--<asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" ControlToValidate="txtCity" runat="server" ValidationGroup="Save8"
                                                                        ErrorMessage="Please Enter City Name">Please Enter City Name</asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divGST" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">GST :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtGST" runat="server" CssClass="form-control" placeholder="GST" TabIndex="26" MaxLength="15" onfocusout="ValidateGSTNO()"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvGST" Style="color: red;" ControlToValidate="txtGST" runat="server" ValidationGroup="Save8" Enabled="false"
                                                                ErrorMessage="Please Enter GST No.">Please Enter GST No.</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="rgvGSTNo" runat="server" ControlToValidate="txtGST" ValidationGroup="Save8" Enabled="false"
                                                                Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid GST No." ValidationExpression="[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}[A-Z]{1}[0-9A-Z]{1}$">Invalid GST No.</asp:RegularExpressionValidator>

                                                            <asp:Label ID="lblgst" CssClass="lblgst" ClientMode="Static"  runat="server" Style="color: red!important; font-weight: bold!important; display: none">GSTNO already exists.</asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divFUGST" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">GST Certi :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:FileUpload ID="fuGSTCerti" runat="server" CssClass="form-control" />
                                                            <%--<asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" placeholder="IFSC Code" TabIndex="26"></asp:TextBox>--%>
                                                            <%--<asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                                            <asp:RequiredFieldValidator ID="rfvFUGST" Style="color: red;" ControlToValidate="fuGSTCerti" runat="server" ValidationGroup="Save8" Enabled="false"
                                                                ErrorMessage="Please Upload GST Certificate Image">Please Upload GST Certificate Image</asp:RequiredFieldValidator>
                                                            <label id="lblGStCeralert" style="display: none!important; font-weight: bold; color: red!important;">Please Select the only JPEG,JPG,TIFF,PNG.</label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divMSME" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">MSME :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:RadioButtonList ID="rblMSME" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"
                                                                OnSelectedIndexChanged="rblMSME_SelectedIndexChanged" AutoPostBack="true">
                                                                <asp:ListItem Value="0" Text="NO" Selected="True" class="radio-inline"></asp:ListItem>
                                                                <asp:ListItem Value="1" Text="YES" class="radio-inline"></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                            <%--<asp:CheckBox ID="chkUnderMarginScheme" runat="server" CssClass="form-control" TabIndex="46" Checked="true" />--%>
                                                            <%--<asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" placeholder="City" TabIndex="16"></asp:TextBox>--%>
                                                            <%--<asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" ControlToValidate="txtCity" runat="server" ValidationGroup="Save8"
                                                                        ErrorMessage="Please Enter City Name">Please Enter City Name</asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divFUMSME" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">MSME Certi :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:FileUpload ID="fuMSMECerti" runat="server" CssClass="form-control" />
                                                            <%--<asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" placeholder="IFSC Code" TabIndex="26"></asp:TextBox>--%>
                                                            <%--<asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                                            <asp:RequiredFieldValidator ID="rfvMSMECerti" Style="color: red;" ControlToValidate="fuMSMECerti" runat="server" ValidationGroup="Save8" Enabled="false"
                                                                ErrorMessage="Please Upload MSME Certificate Image">Please Upload MSME Certificate Image</asp:RequiredFieldValidator>
                                                            <label id="lblMSMECertialert" style="display: none!important; font-weight: bold; color: red!important;">Please Select the only JPEG,JPG,TIFF,PNG.</label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-md-12" id="divNext8" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:LinkButton runat="server" ID="lnkPrev8" CssClass="btn btn-success pull-left" TabIndex="44" Text="Previous" OnClick="lnkPrev8_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lnkNext8" CssClass="btn btn-success pull-right" TabIndex="45" Text="Next"  OnClick="lnkNext8_Click" ValidationGroup="Save8" OnClientClick="return ValidatePANNO();"><i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>


                                        <fieldset class="scheduler-border shadow" runat="server" id="dvBankDetail1" visible="false">
                                            <legend class="scheduler-border">Banking Details</legend>
                                            <div class="col-md-12" id="divIFSC" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">IFSC Code :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="form-control" placeholder="IFSC Code" TabIndex="37"
                                                                OnTextChanged="txtIFSCCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            <%--<asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                                            <asp:RequiredFieldValidator ID="rfvIFSCCode" Style="color: red;" ControlToValidate="txtIFSCCode" runat="server" ValidationGroup="Save9"
                                                                ErrorMessage="Please Enter IFSC Code" Enabled="false" Display="Dynamic">Please Enter IFSC Code</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divBank" runat="server" visible="false">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Bank Name :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control " placeholder="Bank Name" TabIndex="35"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvBankName" Style="color: red;" ControlToValidate="txtBankName" runat="server" ValidationGroup="Save9"
                                                                ErrorMessage="Please Enter Bank Name" Enabled="false" Display="Dynamic">Please Enter Bank Name</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divACNo" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Account No. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtACNo" runat="server" CssClass="form-control" placeholder="Account No." TabIndex="36" onfocusout="ValidateAccountNumber()"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvAccountNo" Style="color: red;" ControlToValidate="txtACNo" runat="server" ValidationGroup="Save9"
                                                                ErrorMessage="Please Enter Account No" Display="Dynamic" Enabled="false">Please Enter Account No</asp:RequiredFieldValidator>
                                                            <asp:Label ID="lblAccntNoExist" CssClass="lblpaymentto"   runat="server" ClientIDMode="Static" Style="color: red!important; font-weight: bold!important; display: none">Account No already exists.</asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>



                                            <div class="col-md-12" id="divAccType" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Account Type :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <%--<asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" placeholder="Address 2"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="form-control" TabIndex="38"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvAccountType" Style="color: red;" ControlToValidate="ddlAccountType" runat="server" ValidationGroup="Save9"
                                                                ErrorMessage="Select Account Type" Display="Dynamic" Enabled="false" InitialValue="0">Select Account Type</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divCheque" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Cancelled Cheque / Passbook :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:FileUpload ID="fuCheque" runat="server" CssClass="form-control" />
                                                            <%--<asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" placeholder="IFSC Code" TabIndex="26"></asp:TextBox>--%>
                                                            <%--<asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                                            <asp:RequiredFieldValidator ID="rfvFUCheque" Style="color: red;" ControlToValidate="fuCheque" runat="server" ValidationGroup="Save9"
                                                                ErrorMessage="Please Upload Cancelled Cheque / Passbook Image" Enabled="false">Please Upload Cancelled Cheque / Passbook Image</asp:RequiredFieldValidator>
                                                            <label id="lblChequealert" style="display: none!important; font-weight: bold; color: red!important;">Please Select the only JPEG,JPG,TIFF,PNG.</label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divUPIWallet" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">UPI / Wallet Type :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <%--<asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" placeholder="Address 2"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddlUPIWalletType" runat="server" CssClass="form-control" TabIndex="41" OnSelectedIndexChanged="ddlUPIWalletType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvWalletType" Style="color: red;" ControlToValidate="ddlUPIWalletType" runat="server" ValidationGroup="Save9"
                                                                ErrorMessage="Select UPI / Wallet Type" Display="Dynamic" Enabled="false" InitialValue="0">Select UPI / Wallet Type</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divWalletNo" runat="server" visible="false" style="padding-top: 5px;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Wallet Pmnt No. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtWalletPayNo" runat="server" CssClass="form-control " placeholder="Wallet Payment No" TabIndex="42" AutoPostBack="true"></asp:TextBox>
                                                            <%--OnTextChanged="txtWalletPayNo_TextChanged"--%>
                                                            <asp:RequiredFieldValidator ID="rfvWalletePayNo" Style="color: red;" ControlToValidate="txtWalletPayNo" runat="server" ValidationGroup="Save9"
                                                                ErrorMessage="Please Enter Wallet Payment No" Display="Dynamic" Enabled="false">Please Enter Wallet Payment No</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divWalletOwner" runat="server" visible="false" style="padding-top: 5px;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Wallet Owner :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtWalletOwner" runat="server" CssClass="form-control " placeholder="Owner Name" TabIndex="43" AutoPostBack="true"></asp:TextBox> <%--OnTextChanged="txtWalletOwner_TextChanged"--%>
                                                            <asp:RequiredFieldValidator ID="rfvWalleteOwnerName" Style="color: red;" ControlToValidate="txtWalletOwner" runat="server" ValidationGroup="Save9"
                                                                ErrorMessage="Please Enter Wallet Owner Name" Display="Dynamic" Enabled="false">Please Enter Wallet Owner Name</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divNext9" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:LinkButton runat="server" ID="lnkPrev9" CssClass="btn btn-success pull-left" TabIndex="49" Text="Previous" OnClick="lnkPrev9_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                                                        <%--<asp:LinkButton runat="server" ID="lnkNext9" CssClass="btn btn-success pull-right" TabIndex="50" Text="Next" OnClientClick="return ValidateCancelledCheque();" OnClick="lnkNext9_Click" ValidationGroup="Save9"><i class="fa fa-arrow-circle-right"></i></asp:LinkButton>--%>
                                                        <asp:LinkButton runat="server" ID="lnkSave" CssClass="btn btn-success pull-right" TabIndex="52" Text="SAVE"  OnClick="lnkSave_Click" OnClientClick="return ValidateAccountNo();" ValidationGroup="Save9"><%--<i class="fa fa-arrow-circle-right"></i>--%></asp:LinkButton>

                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>

                                        <fieldset class="scheduler-border shadow" runat="server" id="dvBankDetail2" visible="false">
                                            <legend class="scheduler-border">Banking Details</legend>


                                            <div class="col-md-12" id="dviFinal" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:LinkButton runat="server" ID="lnkPrev10" CssClass="btn btn-success pull-left" TabIndex="51" Text="Previous" OnClick="lnkPrev10_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                        </fieldset>



                                        <%--</ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                    </div>
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

    <input type="hidden" id="menutabid" value="tsmMstMMVend" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />

</asp:Content>
