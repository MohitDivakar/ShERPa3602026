<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/MasterCustomerRltionShipMng.Master" AutoEventWireup="true" CodeBehind="Inquiry.aspx.cs" Inherits="ShERPa360net.CRM.Inquiry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Inquiry</title>
    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }
    </style>

    <%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>
    <link href="../HelpViewer/css_js_plugins/plugins/timepicker/bootstrap-timepicker.css" rel="stylesheet" />
    <link href="../HelpViewer/css_js_plugins/plugins/timepicker/bootstrap-timepicker.min.css" rel="stylesheet" />--%>
    <%--<script type="text/javascript">
        var dt = new Date();
        var time = dt.getHours() + ":" + dt.getMinutes();

        $('.timepickerpickup, .timepickerpickedup').bind("focus keydown", function (event) {

            if (event.type == "focus") {
                // Input focused/clicked
                $(this).timepicker({
                    showInputs: false,
                    showMeridian: false,
                    defaultTime: time,
                    minuteStep: 15,

                }).timepicker("showWidget")


            }
            else if (event.type == "keydown") {
                $(this).timepicker("hideWidget")
            }
        });
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong>Create</strong> Inquiry</h3>
                        </div>
                        <div class="panel-body">
                            <div class="panel panel-default">
                                <div class="panel-body">
                                    <div class="row">
                                        <asp:HiddenField ID="hfpincode" runat="server" ClientIDMode="Static" />
                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Segment</label>
                                                    <div class="col-md-9">
                                                        <asp:DropDownList ClientIDMode="Static" runat="server" CssClass="form-control select2" ID="ddlSegment" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlSegment_SelectedIndexChanged">
                                                        </asp:DropDownList>

                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlSegment"
                                                            ValidationGroup="aValidationGroup" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                                            InitialValue="0" ErrorMessage="Please select segment">
                                                        </asp:RequiredFieldValidator>

                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Dist Chnl</label>
                                                    <div class="col-md-9">
                                                        <asp:DropDownList ClientIDMode="Static" runat="server" Style="color: black!important; font-weight: bold!important;" CssClass="form-control" ID="ddlDistChnl" AutoPostBack="false" AppendDataBoundItems="true">
                                                        </asp:DropDownList>

                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="ddlDistChnl"
                                                            ValidationGroup="aValidationGroup" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                                            InitialValue="0" ErrorMessage="Please select dist. channel">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Inq No</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtInquiryNo" ClientIDMode="Static" Style="color: black!important; font-weight: bold!important;" runat="server" CssClass="form-control" placeholder="Inquiry No" Enabled="false"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtInquiryNo"
                                                            ValidationGroup="aValidationGroup" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                                            ErrorMessage="Inquiry No. is required">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Inq Date</label>
                                                    <div class="col-md-9 input-group">
                                                        <asp:TextBox ID="txtInqDate" ClientIDMode="Static" Style="color: black!important; font-weight: bold!important;" runat="server" CssClass="form-control" placeholder="DD/MM/YYYY" Enabled="false" MaxLength="10"></asp:TextBox>
                                                        <div class="input-group-addon">
                                                            <span class="glyphicon glyphicon-th"></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12" style="margin-top: 5px!important;">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Card No</label>
                                                    <div class="col-md-7">
                                                        <asp:TextBox ID="txtCardNo" MaxLength="16" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Card No" OnTextChanged="txtCardNo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvCardNo" runat="server" ControlToValidate="txtCardNo" ValidationGroup="aValidationGroup"
                                                            Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ErrorMessage="Card No. is required">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-md-2">
                                                        <asp:Button runat="server" ClientIDMode="Static" CssClass="btn btn-success" Text="Detail" Style="margin-left: 5px!important;" ID="btnShowRegDtl" OnClick="btnShowRegDtl_Click" />
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Lead</label>
                                                    <div class="col-md-9">
                                                        <asp:DropDownList ClientIDMode="Static" runat="server" CssClass="form-control" ID="ddlLeads" AutoPostBack="false" AppendDataBoundItems="true">
                                                            <asp:ListItem Text="--SELECT--" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="Inbound" Value="Inbound"></asp:ListItem>
                                                            <asp:ListItem Text="Outbound" Value="Outbound"></asp:ListItem>
                                                        </asp:DropDownList>

                                                        <asp:RequiredFieldValidator ID="rfvddlLead" runat="server" ControlToValidate="ddlLeads"
                                                            ValidationGroup="aValidationGroup" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                                            InitialValue="0" ErrorMessage="Please select Lead Type">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Reference</label>
                                                    <div class="col-md-9">
                                                        <asp:DropDownList ClientIDMode="Static" runat="server" CssClass="form-control" ID="ddlRef" AutoPostBack="false" AppendDataBoundItems="true">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlRef" runat="server" ControlToValidate="ddlRef"
                                                            ValidationGroup="aValidationGroup" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                                            InitialValue="0" ErrorMessage="Please select reference">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Comments</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtComments" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Comments" MaxLength="100"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="panel panel-default">
                                <div class="panel-heading" style="height: 30px!important;">
                                    <h4 class="panel-title" style="margin-top: -12px!important;"><strong>Device</strong> Detail</h4>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Product</label>
                                                    <div class="col-md-9">
                                                        <asp:DropDownList ClientIDMode="Static" runat="server" CssClass="form-control" ID="ddlProduct" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="ddlProduct"
                                                            ValidationGroup="aValidationGroup" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                                            InitialValue="0" ErrorMessage="Please select product">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Brand</label>
                                                    <div class="col-md-9">
                                                        <asp:DropDownList ClientIDMode="Static" runat="server" CssClass="form-control" ID="ddlBrand" OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems="true">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlBrand"
                                                            ValidationGroup="aValidationGroup" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                                            InitialValue="0" ErrorMessage="Please select brand">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Model</label>
                                                    <div class="col-md-9">
                                                        <asp:DropDownList ClientIDMode="Static" runat="server" CssClass="form-control" ID="ddlModel" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvddlModel" runat="server" ControlToValidate="ddlModel"
                                                            ValidationGroup="aValidationGroup" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                                            InitialValue="0" ErrorMessage="Please select model">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Model Name</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtModelName" ClientIDMode="Static" runat="server" MaxLength="50" CssClass="form-control" placeholder="Model Name" Enabled="false"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvtxtModelName" runat="server" ControlToValidate="txtModelName"
                                                            ValidationGroup="aValidationGroup" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" Enabled="false"
                                                            ErrorMessage="Model name is required">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12" style="margin-top: 5px!important;">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">IMEI No</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtIMEINo" MaxLength="20" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="IMEI No" onkeypress="return onlyNumber(event)" OnTextChanged="txtIMEINo_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtIMEINo"
                                                            ValidationGroup="aValidationGroup" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ErrorMessage="IMEI No. must between 15 to 20 digits"
                                                            ValidationExpression="^[0-9]{15,20}$"></asp:RegularExpressionValidator>
                                                        <asp:RequiredFieldValidator ID="rfvImei" runat="server" ControlToValidate="txtIMEINo" Display="Dynamic" ForeColor="Red"
                                                            ValidationGroup="aValidationGroup" SetFocusOnError="true" ErrorMessage="Please Enter IMEI No.">Please Enter IMEI No.</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Device Password</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtDevicePass" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Device Password" MaxLength="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Sim Carrier</label>
                                                    <div class="col-md-9">
                                                        <asp:DropDownList ClientIDMode="Static" runat="server" CssClass="form-control" ID="ddlSimCarrier" AutoPostBack="false" AppendDataBoundItems="true">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-8 control-label">Is repair by other service center</label>
                                                    <div class="col-md-4">
                                                        <asp:CheckBox ID="chkServiceCntr" runat="server" ClientIDMode="Static" Checked="false" AutoPostBack="true" OnCheckedChanged="chkServiceCntr_CheckedChanged"></asp:CheckBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <div class="col-md-3"></div>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtServiceCntrName" Enabled="false" MaxLength="50" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Service Center Name"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvServCntr" runat="server" ControlToValidate="txtServiceCntrName"
                                                            ValidationGroup="aValidationGroup" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" Enabled="false"
                                                            ErrorMessage="Please enter service center name"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12" style="margin-top: 5px!important;">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Estimate Amount</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtEstAmt" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Estimate Amount" Enabled="false" MaxLength="10" onkeypress="return onlyNumber(event)"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-9">
                                                <div class="form-group">
                                                    <label class="col-md-1 control-label">Device Problems</label>
                                                    <div class="col-md-11">
                                                        <asp:TextBox ID="txtProblems" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Device Problems" MaxLength="300"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtProblems"
                                                            ValidationGroup="aValidationGroup" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                                            ErrorMessage="Device Problems is required">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="panel panel-default">
                                <div class="panel-heading" style="height: 30px!important;">
                                    <h4 class="panel-title" style="margin-top: -12px!important;"><strong>Customer</strong> Detail</h4>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Full Name</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtName" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Full Name" MaxLength="50"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtName"
                                                            ValidationGroup="aValidationGroup" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                                            ErrorMessage="Full Name is required">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Contact No</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtMobileNo" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Contact No" MaxLength="10" OnTextChanged="txtMobileNo_TextChanged" onkeypress="return onlyNumber(event)"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtMobileNo"
                                                            ValidationGroup="aValidationGroup" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                                            ErrorMessage="Contact No. is required">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtMobileNo"
                                                            ValidationGroup="aValidationGroup" Display="Dynamic" ForeColor="red" SetFocusOnError="true" ErrorMessage="Contact no. must be 10 digits"
                                                            ValidationExpression="^[0-9]{10}$"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Contact No.2</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtAltMobNo" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Contact No.2" MaxLength="10" onkeypress="return onlyNumber(event)"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Email Id</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtEmailId" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Email Id" MaxLength="50"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="regexEmailValid" runat="server" ValidationGroup="aValidationGroup"
                                                            ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" SetFocusOnError="true" ControlToValidate="txtEmailId"
                                                            Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid email format"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12" style="margin-top: 5px!important;">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Address 1</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtAddr1" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Address 1" MaxLength="35"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtAddr1"
                                                            Display="Dynamic" ForeColor="Red" ValidationGroup="aValidationGroup" ErrorMessage="Address 1 is required"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Address 2</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtAddr2" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Address 2" MaxLength="35"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Landmark</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtLandmark" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Landmark" MaxLength="35"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Area</label>
                                                    <div class="col-md-9">
                                                        <asp:DropDownList ClientIDMode="Static" runat="server" CssClass="form-control" ID="ddlArea" AutoPostBack="false" AppendDataBoundItems="true">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvArea" runat="server" ControlToValidate="ddlArea" Enabled="false"
                                                            ValidationGroup="aValidationGroup" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                                            InitialValue="0" ErrorMessage="Please select area">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12" style="margin-top: 5px!important;">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Postal Code</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtPostCode" OnTextChanged="txtPostCode_TextChanged" ClientIDMode="Static" AutoPostBack="true" runat="server" CssClass="form-control" placeholder="Postal Code" MaxLength="6" onkeypress="return onlyNumber(event)"></asp:TextBox>
                                                        <asp:Label ID="lblFedexCap" runat="server"></asp:Label>
                                                        <asp:Label ID="lblInvalidPincode" runat="server"></asp:Label>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtPostCode" Display="Dynamic"
                                                            ForeColor="Red" ValidationGroup="aValidationGroup" ErrorMessage="Postal Code is required">
                                                        </asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPostCode"
                                                            ValidationGroup="aValidationGroup" SetFocusOnError="true" Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid postal code"
                                                            ValidationExpression="^[0-9]{6}$"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">State</label>
                                                    <div class="col-md-9">
                                                        <asp:DropDownList ClientIDMode="Static" runat="server" CssClass="form-control" ID="ddlState" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlState"
                                                            ValidationGroup="aValidationGroup" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                                            InitialValue="0" ErrorMessage="Please select state">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">City</label>
                                                    <div class="col-md-9">
                                                        <asp:DropDownList ClientIDMode="Static" runat="server" CssClass="form-control" ID="ddlCity" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCity_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="ddlCity"
                                                            ValidationGroup="aValidationGroup" Display="Dynamic" ForeColor="red" SetFocusOnError="true"
                                                            InitialValue="0" ErrorMessage="Please select city">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Transaction Id</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtTXNId" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="Transaction Id" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="panel panel-default">
                                <div class="panel-heading" style="height: 30px!important;">
                                    <h4 class="panel-title" style="margin-top: -12px!important;"><strong>Pickup</strong> Detail</h4>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Requested Pickup Date</label>
                                                    <div class="col-md-9 input-group date" data-provide="datepicker" data-date-format="dd/mm/yyyy">
                                                        <asp:TextBox ID="txtPickupDt" ClientIDMode="Static" runat="server" CssClass="form-control" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                        <div class="input-group-addon">
                                                            <span class="glyphicon glyphicon-th"></span>
                                                        </div>

                                                    </div>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPickupDt" Display="Dynamic" ForeColor="Red"
                                                        ValidationGroup="aValidationGroup" ErrorMessage="Pickup date is required">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group bootstrap-timepicker">
                                                    <label class="col-md-3 control-label">Req Pickup Time</label>
                                                    <div class="col-md-9 input-group" id="pickuptime">
                                                        <asp:TextBox ID="dtpPickupTime" ClientIDMode="Static" runat="server" CssClass="form-control timepickerpickup" TextMode="Time" placeholder="HH:MM"></asp:TextBox>

                                                        <%--<div class="input-group-addon">
                                                            <i class="fa fa-clock-o"></i>
                                                        </div>--%>
                                                    </div>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="dtpPickupTime" Display="Dynamic" ForeColor="Red"
                                                        ValidationGroup="aValidationGroup" ErrorMessage="Pickup time is required">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Payment Mode</label>
                                                    <div class="col-md-9">
                                                        <asp:DropDownList ClientIDMode="Static" runat="server" CssClass="form-control" ID="ddlPayMode" AutoPostBack="false" AppendDataBoundItems="true">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlPayMode"
                                                            ValidationGroup="aValidationGroup" Display="Dynamic" ForeColor="Red" SetFocusOnError="true"
                                                            InitialValue="0" ErrorMessage="Please Select Payment Mode">
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Loaner</label>
                                                    <div class="col-md-9">
                                                        <asp:CheckBox ID="chkLoaner" runat="server" ClientIDMode="Static"></asp:CheckBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12 text-center">
                                            <%--<asp:Button runat="server" ClientIDMode="Static" CssClass="btn btn-success" Text="Save" Style="margin-left: 200px!important;" ID="btnSave" OnClientClick="return ValidateInquiry();" OnClick="btnSave_Click" />--%>
                                            <asp:Button runat="server" ClientIDMode="Static" CssClass="btn btn-success" Text="Save" Style="margin-left: 200px!important;" ID="btnSave" ValidationGroup="aValidationGroup" OnClick="btnSave_Click" />

                                            <asp:Button runat="server" ClientIDMode="Static" CssClass="btn btn-primary" Text="Hold/Cancel" Style="margin-left: 10px!important;" ID="btnHold" Visible="false" OnClick="btnHold_Click" />
                                            <asp:Button runat="server" ClientIDMode="Static" CssClass="btn btn-alert" Text="Call History" Style="margin-left: 10px!important;" ID="btnCallHistory" Visible="false" OnClick="btnCallHistory_Click" />
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmTranInq" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />
    <div class="modal fade" id="modal-default">
        <div class="modal-dialog modal-lg">
            <%--<asp:UpdatePanel ID="upPackRegDtl" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>--%>
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7">QUIKe Pack Registration Detail</h4>
                </div>
                <div class="modal-body">
                    <asp:DataList ID="dlRegDtl" runat="server" RepeatDirection="Vertical" Width="100%" OnItemDataBound="dlRegDtl_ItemDataBound">
                        <ItemTemplate>
                            <div class="box-body no-padding">
                                <span class="badge bg-maroon margin-r-5"><%# Eval("STATUSDESC") %></span>
                                <span class="badge bg-light-blue"><%# Eval("ITEMDESC") %></span>
                                <div class="box-body">
                                    <div class="row">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Registration No.</label>
                                                <br />
                                                <asp:Label ID="lblRegId" runat="server" Text='<%# Eval("REGID") %>'></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Registration Date</label>
                                                <br />
                                                <%# Eval("REGDATE","{0:dd-MM-yyyy}") %>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Card No.</label>
                                                <br />
                                                <%# Eval("CARDNO") %>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Customer Name</label>
                                                <br />
                                                <%# Eval("FULLNAME") %>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Mobile No.</label>
                                                <br />
                                                <%# Eval("MOBILENO") %>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Email Id</label>
                                                <br />
                                                <%# Eval("EMAILID") %>
                                            </div>
                                        </div>

                                        <%--<div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Address 1</label>
                                                        <br />
                                                        <%# Eval("ADDR1") %>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Address 2</label>
                                                        <br />
                                                        <%# Eval("ADDR2") %>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label>Landmark</label>
                                                        <br />
                                                        <%# Eval("LANDMARK") %>
                                                    </div>
                                                </div>--%>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>City</label>
                                                <br />
                                                <%# Eval("CITY_NAME") %>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Handset Purchase Date</label>
                                                <br />
                                                <%# Eval("HANDSETPURDATE","{0:dd-MM-yyyy}") %>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Pack Purchase Date</label>
                                                <br />
                                                <%# Eval("PURDATE","{0:dd-MM-yyyy}") %>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Handset Price</label>
                                                <br />
                                                <i class="fa fa-rupee"></i><%# Eval("HSVALUE") %>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Handset</label>
                                                <br />
                                                <%# Eval("BRAND_NAME") %> &nbsp;-&nbsp;<%# Eval("MODELNAME") %>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>IMEI No.</label>
                                                <br />
                                                <%# Eval("IMEINO") %>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Dealer Name</label>
                                                <br />
                                                <%# Eval("DEALERNAME") %>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Promoter Name</label>
                                                <br />
                                                <%# Eval("PROMOTERNAME") %>
                                            </div>
                                        </div>


                                        <div class="col-md-12 divhorizontal" style="padding-top: 10px; overflow-x: scroll;">
                                            <asp:GridView ID="gvFeatures" runat="server" CssClass="table table-striped table-bordered nowrap" Width="100%" CellSpacing="0"
                                                AutoGenerateColumns="False" Style="margin-bottom: 10px">
                                                <Columns>
                                                    <asp:BoundField DataField="FEATURE" HeaderStyle-Width="14%" HeaderText="Feature" />
                                                    <asp:BoundField DataField="FEATUREDESC" HeaderStyle-Width="38%" HeaderText="Description" />
                                                    <asp:BoundField DataField="VALIDITY" HeaderText="Validity" />
                                                    <asp:BoundField DataField="ADDONVALUE" HeaderText="Addon Value" />
                                                    <asp:BoundField DataField="TOTVALUE" HeaderText="Toal Value" />
                                                    <asp:BoundField DataField="VALIDFROM" HeaderText="Valid From" DataFormatString="{0:dd-MM-yyyy}" />
                                                    <asp:BoundField DataField="VALIDTO" HeaderText="Valid To" DataFormatString="{0:dd-MM-yyyy}" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            <div style="text-align: center; color: red; font-size: 18px;">
                                <asp:Label Visible='<%#bool.Parse((dlRegDtl.Items.Count==0).ToString())%>' runat="server" ID="lblNoRecord" Text="List is empty !"></asp:Label>
                            </div>
                        </FooterTemplate>
                    </asp:DataList>
                </div>

                <div class="modal fade" id="modal-blynk">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true">&times;</span></button>
                                <h4 class="modal-title" style="color: #337ab7">BLYNK Activation Detail</h4>
                            </div>
                            <div class="modal-body">
                                <asp:DataList ID="dlActivation" runat="server" RepeatDirection="Vertical" Width="100%">
                                    <ItemTemplate>
                                        <div class="box-body no-padding">
                                            <span class="badge bg-maroon margin-r-5"><%# Eval("STATUS") %></span>
                                            <div class="box-body">
                                                <div class="row">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label>Full Name </label>
                                                            <br />
                                                            <asp:Label ID="lblFullName" runat="server" Text='<%# Eval("FULLNAME") %>'></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label>Mobile No. </label>
                                                            <br />
                                                            <asp:Label ID="lblMobileNo" runat="server" Text='<%# Eval("MOBILENO") %>'></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label>City </label>
                                                            <br />
                                                            <asp:Label ID="lblCity" runat="server" Text='<%# Eval("CITY") %>'></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label>State </label>
                                                            <br />
                                                            <asp:Label ID="lblState" runat="server" Text='<%# Eval("STATE") %>'></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label>Brand </label>
                                                            <br />
                                                            <asp:Label ID="lblBrand" runat="server" Text='<%# Eval("BRAND") %>'></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label>Model </label>
                                                            <br />
                                                            <asp:Label ID="lblModel" runat="server" Text='<%# Eval("MODEL") %>'></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label>Color </label>
                                                            <br />
                                                            <asp:Label ID="lblColor" runat="server" Text='<%# Eval("COLOR") %>'></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label>IMEI No. </label>
                                                            <br />
                                                            <asp:Label ID="lblIMEINO" runat="server" Text='<%# Eval("IMEINO") %>'></asp:Label>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label>Serial No. </label>
                                                            <br />
                                                            <asp:Label ID="lblSerialNo" runat="server" Text='<%# Eval("SERIALNO") %>'></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label>Warranty No. </label>
                                                            <br />
                                                            <asp:Label ID="lblWarrantyNo" runat="server" Text='<%# Eval("WARRANTYNO") %>'></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label>Activation Date </label>
                                                            <br />
                                                            <asp:Label ID="lblActivationDate" runat="server" Text='<%# Eval("ACTIVATIONDATE") %>'></asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label>Dealer Name </label>
                                                            <br />
                                                            <asp:Label ID="lblDealerName" runat="server" Text='<%# Eval("DEALERNAME") %>'></asp:Label>
                                                        </div>
                                                    </div>

                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="modal modal-primary fade" id="modal-confirm" data-backdrop="static">
                    <div class="modal-dialog">
                        <%--<asp:UpdatePanel ID="upModelconfirm" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                        <ContentTemplate>--%>
                        <div class="modal-content">
                            <div class="modal-body">
                                <h4>Confirm Message</h4>
                                <p id="lblConfirmMsg"></p>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-success" id="btnYes" runat="server" data-dismiss="modal" onserverclick="btnYes_ServerClick">Yes</button>
                                <button type="button" class="btn btn-danger" data-dismiss="modal">No</button>
                            </div>
                        </div>
                        <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
                    </div>
                </div>

            </div>
            <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
        </div>
    </div>

    <%--<script src="../js/plugins/bootstrap/bootstrap-timepicker.min.js"></script>--%>
</asp:Content>
