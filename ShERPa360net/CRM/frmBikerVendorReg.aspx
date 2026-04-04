<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/MasterCustomerRltionShipMng.Master" AutoEventWireup="true" CodeBehind="frmBikerVendorReg.aspx.cs" Inherits="ShERPa360net.CRM.frmBikerVendorReg" EnableEventValidation="false" %>

<%--<%@ Register Assembly="AJAX" TagPrefix="ajax" Namespace="Ajax" Src="~/bin/AjaxControlToolkit.dll" TagName="AJAX" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Biker Vendor Reg.</title>

    <%-- <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.1/jquery.min.js" type="text/javascript"></script>


    <script type="text/javascript">
        $(document).ready(function () {

            $(".ddlDealer").select2();
            //$(".ddlModel").select2();
            //$(".ddlRom").select2();
            //$(".ddlRam").select2();
            //$(".ddlColor").select2();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_endRequest(ApplyAutoSuggestfunctionality);
        });

        function ApplyAutoSuggestfunctionality() {
            $(".ddlDealer").select2();
            //$(".ddlModel").select2();
            //$(".ddlRom").select2();
            //$(".ddlRam").select2();
            //$(".ddlColor").select2();
            //$(".ddlVendor").select2();
        }
    </script>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"> </asp:ScriptManager>--%>

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Vendor Registration</strong></h3>
                            <%--<asp:LinkButton ID="imgReset" runat="server" CssClass="btn btn-success pull-right" OnClick="imgReset_Click" Text="Reset"><i class="fa fa-undo"></i></asp:LinkButton>
                                    <asp:LinkButton ID="imgSaveAll" runat="server" CssClass="btn btn-success pull-right" OnClick="imgSaveAll_Click" Text="Save All" ValidationGroup="SaveAll"><i class="fa fa-save"></i></asp:LinkButton>--%>
                        </div>
                        <%--  <asp:UpdatePanel ID="upd1" runat="server" UpdateMode="Conditional" ClientIDMode="Static">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtName" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="txtShortName" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="lnkNext1" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnkPrevious2" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnkNext2" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="txtPincode" EventName="TextChanged" />
                                <asp:AsyncPostBackTrigger ControlID="lnkPrevious3" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnkNext3" EventName="Click" />

                                <asp:AsyncPostBackTrigger ControlID="lnkPrevious4" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnkNext4" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnkPrevious5" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnkNext5" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnkPrevious6" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnkNext6" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="lnkPrevious7" EventName="Click" />
                                <asp:PostBackTrigger ControlID="lnkSave" />
                            </Triggers>
                            <ContentTemplate>--%>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <fieldset class="scheduler-border">
                                            <%--<legend class="scheduler-border">Product Entry</legend>--%>

                                            <div class="col-md-12" id="divDealer" runat="server">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Dealer Name :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control required_text_box" TabIndex="0"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvDealer" Style="color: red;" ControlToValidate="ddlDealer" runat="server" ValidationGroup="Save1"
                                                                ErrorMessage="Select Dealer Name" Display="Dynamic" InitialValue="0">Select Dealer Name</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divContactPerson" runat="server" visible="true" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Person Name :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control" placeholder="Person Name" TabIndex="15" OnTextChanged="txtContactPerson_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvPersonName" Style="color: red;" ControlToValidate="txtContactPerson" runat="server" ValidationGroup="Save1"
                                                                ErrorMessage="Please Enter Person Name">Please Enter Person Name</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divContactNo" runat="server" visible="true">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Contact No :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control" placeholder="Contact No" TabIndex="16"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvContactNo" Style="color: red;" ControlToValidate="txtContactNo" runat="server" ValidationGroup="Save1"
                                                                ErrorMessage="Please Enter Contact No.">Please Enter Contact No.</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divSuggestedName" runat="server" visible="true">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Name :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtSuggestedName" runat="server" CssClass="form-control" placeholder="Name" TabIndex="16"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvSuggestedName" Style="color: red;" ControlToValidate="txtSuggestedName" runat="server" ValidationGroup="Save1"
                                                                ErrorMessage="Please Enter Enter Name">Please Enter Name</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divNext1" runat="server" visible="true" style="margin-top: 5px !important;">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:LinkButton runat="server" TabIndex="3" ID="lnkNext1" CssClass="btn btn-success pull-right" Text="Next" OnClick="lnkNext1_Click" ValidationGroup="Save1"><i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divPAN" runat="server" visible="false">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">PAN :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtPAN" runat="server" CssClass="form-control " placeholder="PAN" TabIndex="19"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="color: red;" ControlToValidate="txtMobileNo" runat="server" ValidationGroup="Save4"
                                                                        ErrorMessage="Please Enter Mobile No" Display="Dynamic">Please Enter Mobile No</asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divGST" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">GST :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtGST" runat="server" CssClass="form-control" placeholder="GST" TabIndex="20"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" ControlToValidate="txtShortName" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Enter Vendor Short Name">Please Enter Vendor Short Name</asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divMaginScheme" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Is Under Margin Scheme :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:CheckBox ID="chkUnderMarginScheme" runat="server" CssClass="form-control" TabIndex="21" Checked="true" />
                                                            <%--<asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" placeholder="City" TabIndex="16"></asp:TextBox>--%>
                                                            <%--<asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" ControlToValidate="txtCity" runat="server" ValidationGroup="Save3"
                                                                        ErrorMessage="Please Enter City Name">Please Enter City Name</asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divNext5" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:LinkButton runat="server" ID="lnkPrevious5" CssClass="btn btn-success pull-left" TabIndex="22" Text="Previous" OnClick="lnkPrevious5_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lnkNext5" CssClass="btn btn-success pull-right" TabIndex="23" Text="SKIP" OnClick="lnkNext5_Click"><i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divBank" runat="server" visible="false">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Bank Name :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control " placeholder="Bank Name" TabIndex="24"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="color: red;" ControlToValidate="txtMobileNo" runat="server" ValidationGroup="Save4"
                                                                        ErrorMessage="Please Enter Mobile No" Display="Dynamic">Please Enter Mobile No</asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divACNo" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Account No. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtACNo" runat="server" CssClass="form-control" placeholder="Account No." TabIndex="25"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" ControlToValidate="txtShortName" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Enter Vendor Short Name">Please Enter Vendor Short Name</asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divIFSC" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">IFSC Code :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="form-control" placeholder="IFSC Code" TabIndex="26"></asp:TextBox>
                                                            <%--<asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>--%>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" ControlToValidate="txtCity" runat="server" ValidationGroup="Save3"
                                                                        ErrorMessage="Please Enter City Name">Please Enter City Name</asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divNext6" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:LinkButton runat="server" ID="lnkPrevious6" CssClass="btn btn-success pull-left" TabIndex="27" Text="Previous" OnClick="lnkPrevious6_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lnkNext6" CssClass="btn btn-success pull-right" TabIndex="28" Text="SKIP" OnClick="lnkNext6_Click"><i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
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
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="color: red;" ControlToValidate="txtMobileNo" runat="server" ValidationGroup="Save4"
                                                                        ErrorMessage="Please Enter Mobile No" Display="Dynamic">Please Enter Mobile No</asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divGSTCerti" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">PAN :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:FileUpload ID="fuGSTCerti" runat="server" CssClass="form-control" />
                                                            <%--<asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="Account No." TabIndex="25"></asp:TextBox>--%>
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" ControlToValidate="txtShortName" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Enter Vendor Short Name">Please Enter Vendor Short Name</asp:RequiredFieldValidator>--%>
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
                                                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" ControlToValidate="txtCity" runat="server" ValidationGroup="Save3"
                                                                        ErrorMessage="Please Enter City Name">Please Enter City Name</asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="dviFinal" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:LinkButton runat="server" ID="lnkPrevious7" CssClass="btn btn-success pull-left" TabIndex="29" Text="Previous" OnClick="lnkPrevious7_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lnkSave" CssClass="btn btn-success pull-right" TabIndex="30" Text="SAVE" OnClick="lnkSave_Click"><%--<i class="fa fa-arrow-circle-right"></i>--%></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>

                                            <%--<div class="col-md-12" id="divName" runat="server" visible="true" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Shop Name :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control required_text_box" placeholder="Shop Name" OnTextChanged="txtName_TextChanged" TabIndex="1" AutoPostBack="true"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvName" Style="color: red;" ControlToValidate="txtName" runat="server" ValidationGroup="Save1"
                                                                ErrorMessage="Please Enter Shop Name" Display="Dynamic">Please Enter Shop Name</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>--%>

                                            <%--<div class="col-md-12" id="divShortname" runat="server" visible="true" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Short Name :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtShortName" runat="server" CssClass="form-control" placeholder="Shop Short Name" TabIndex="2" OnTextChanged="txtShortName_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvShortName" Style="color: red;" ControlToValidate="txtShortName" runat="server" ValidationGroup="Save1"
                                                                        ErrorMessage="Please Enter Vendor Short Name">Please Enter Vendor Short Name</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>--%>

                                            <%--<div class="col-md-12" id="divAddress1" runat="server" visible="false">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Address 1 :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control required_text_box" TabIndex="4" placeholder="Address 1"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvAddress1" Style="color: red;" ControlToValidate="txtAddress1" runat="server" ValidationGroup="Save2"
                                                                ErrorMessage="Please Enter Vendor Address" Display="Dynamic">Please Enter Vendor Address</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>--%>

                                            <%--<div class="col-md-12" id="divAddress2" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Address 2 :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control" placeholder="Address 2" TabIndex="5"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" ControlToValidate="txtShortName" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Enter Vendor Short Name">Please Enter Vendor Short Name</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>--%>

                                            <%--<div class="col-md-12" id="divAddress3" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Address 3 :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtAddress3" runat="server" CssClass="form-control" placeholder="Address 2" TabIndex="6"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" Style="color: red;" ControlToValidate="txtShortName" runat="server" ValidationGroup="SaveAll"
                                                                        ErrorMessage="Please Enter Vendor Short Name">Please Enter Vendor Short Name</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>--%>

                                            <%--<div class="col-md-12" id="divNext2" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:LinkButton runat="server" ID="lnkPrevious2" CssClass="btn btn-success pull-left" Text="Previous" TabIndex="7" OnClick="lnkPrevious2_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lnkNext2" CssClass="btn btn-success pull-right" Text="Next" OnClick="lnkNext2_Click" ValidationGroup="Save2"><i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>--%>

                                            <%--<div class="col-md-12" id="divPincode" runat="server" visible="false">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Pin Code :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtPincode" runat="server" CssClass="form-control required_text_box" placeholder="Pincode" TabIndex="8" OnTextChanged="txtPincode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvPincode" Style="color: red;" ControlToValidate="txtPincode" runat="server" ValidationGroup="Save3"
                                                                ErrorMessage="Please Enter Pincode" Display="Dynamic">Please Enter Pincode</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>--%>

                                            <%--<div class="col-md-12" id="divState" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">State :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" TabIndex="9"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>--%>

                                            <%--<div class="col-md-12" id="divCity" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">City :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtCity" runat="server" CssClass="form-control required_text_box" placeholder="City" TabIndex="10"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvCity" Style="color: red;" ControlToValidate="txtCity" runat="server" ValidationGroup="Save3"
                                                                ErrorMessage="Please Enter City Name" Display="Dynamic">Please Enter City Name</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>--%>

                                            <%--<div class="col-md-12" id="divCountry" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Country :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="form-control" TabIndex="11"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>--%>

                                            <%--<div class="col-md-12" id="divNext3" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:LinkButton runat="server" ID="lnkPrevious3" CssClass="btn btn-success pull-left" TabIndex="12" Text="Previous" OnClick="lnkPrevious3_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lnkNext3" CssClass="btn btn-success pull-right" TabIndex="13" Text="Next" OnClick="lnkNext3_Click" ValidationGroup="Save3"><i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>--%>

                                            <%--<div class="col-md-12" id="divMobile" runat="server" visible="false">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Mobile No. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control required_text_box" placeholder="Contact No" TabIndex="14"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvContactNo" Style="color: red;" ControlToValidate="txtMobileNo" runat="server" ValidationGroup="Save4"
                                                                ErrorMessage="Please Enter Mobile No" Display="Dynamic">Please Enter Mobile No</asp:RequiredFieldValidator>
                                                            <asp:RegularExpressionValidator ID="revContactNo" runat="server" Style="color: red;" ValidationGroup="Save4"
                                                                ControlToValidate="txtMobileNo" ErrorMessage="Please Enter Numeric 10 Digit Contact No." Display="Dynamic"
                                                                ValidationExpression="[0-9]{10}">Please Enter Numeric 10 Digit Contact No.</asp:RegularExpressionValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>--%>

                                            <%--<div class="col-md-12" id="divNext4" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:LinkButton runat="server" ID="lnkPrevious4" CssClass="btn btn-success pull-left" TabIndex="17" Text="Previous" OnClick="lnkPrevious4_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                                                        <asp:LinkButton runat="server" ID="lnkNext4" CssClass="btn btn-success pull-right" TabIndex="18" Text="Next" OnClick="lnkNext4_Click" ValidationGroup="Save4"><i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>--%>

                                            <%--<div class="col-md-12" id="divShop" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Shop Photo :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:FileUpload ID="fuShopImage" runat="server" CssClass="form-control" />
                                                            <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" placeholder="IFSC Code" TabIndex="26"></asp:TextBox>
                                                            <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" Style="color: red;" ControlToValidate="txtCity" runat="server" ValidationGroup="Save3"
                                                                        ErrorMessage="Please Enter City Name">Please Enter City Name</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>--%>

                                            <%--<div class="col-md-12" id="divNext7" runat="server" visible="false" style="margin-top: 5px !important;">
                                                        <div class="col-md-12">
                                                            <div class="form-group">
                                                                <asp:LinkButton runat="server" ID="lnkPrevious8" CssClass="btn btn-success pull-left" TabIndex="27" Text="Previous" OnClick="lnkPrevious8_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>
                                                                <asp:LinkButton runat="server" ID="lnkNext8" CssClass="btn btn-success pull-right" TabIndex="28" Text="SKIP" OnClick="lnkNext8_Click"><i class="fa fa-arrow-circle-right"></i></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>--%>

                                            <div class="col-md-12" id="div1" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-12 control-label">
                                                            Vendor Name : &nbsp;
                                                                    <asp:Label ID="lblVendorName" runat="server"></asp:Label></label>
                                                        <%--<div class="col-md-7 col-xs-12">
                                                                    
                                                                </div>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="div2" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-12 control-label">
                                                            Short Name : &nbsp;
                                                                    <asp:Label ID="lblShortName" runat="server"></asp:Label></label>
                                                        <%--<div class="col-md-7 col-xs-12">
                                                                    
                                                                </div>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="div3" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-12 control-label">
                                                            Address 1 : &nbsp;
                                                                    <asp:Label ID="lblAddress1" runat="server"></asp:Label></label>
                                                        <%--<div class="col-md-7 col-xs-12">
                                                                    
                                                                </div>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="div4" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-12 control-label">
                                                            Address 2 : &nbsp;
                                                                    <asp:Label ID="lblAddress2" runat="server"></asp:Label></label>
                                                        <%--<div class="col-md-7 col-xs-12">
                                                                    
                                                                </div>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="div5" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-12 control-label">
                                                            Address 3 : &nbsp;
                                                                    <asp:Label ID="lblAddress3" runat="server"></asp:Label></label>
                                                        <%--<div class="col-md-7 col-xs-12">
                                                                    
                                                                </div>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="div6" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-12 control-label">
                                                            Pin Code : &nbsp;
                                                                    <asp:Label ID="lblPincode" runat="server"></asp:Label></label>
                                                        <%--<div class="col-md-7 col-xs-12">
                                                                    
                                                                </div>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="div7" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-12 control-label">
                                                            City : &nbsp;
                                                                    <asp:Label ID="lblCity" runat="server"></asp:Label></label>
                                                        <%--<div class="col-md-7 col-xs-12">
                                                                    
                                                                </div>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="div8" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-12 control-label">
                                                            State : &nbsp;
                                                                    <asp:Label ID="lblState" runat="server"></asp:Label></label>
                                                        <%--<div class="col-md-7 col-xs-12">
                                                                    
                                                                </div>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="div9" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-12 control-label">
                                                            Country : &nbsp;
                                                                    <asp:Label ID="lblCountry" runat="server"></asp:Label></label>
                                                        <%--<div class="col-md-7 col-xs-12">
                                                                    
                                                                </div>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="div10" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-12 control-label">
                                                            Mobile No. : &nbsp;
                                                                    <asp:Label ID="lblMobileNo" runat="server"></asp:Label></label>
                                                        <%--<div class="col-md-7 col-xs-12">
                                                                    
                                                                </div>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="div11" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-12 control-label">
                                                            Contact Person Name : &nbsp;
                                                                    <asp:Label ID="lblContactPerson" runat="server"></asp:Label></label>
                                                        <%--<div class="col-md-7 col-xs-12">
                                                                    
                                                                </div>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="div12" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-12 control-label">
                                                            Contact No : &nbsp;
                                                                    <asp:Label ID="lblContactNo" runat="server"></asp:Label></label>
                                                        <%--<div class="col-md-7 col-xs-12">
                                                                    
                                                                </div>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="div13" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-12 control-label">
                                                            PAN : &nbsp;
                                                                    <asp:Label ID="lblPAN" runat="server"></asp:Label></label>
                                                        <%--<div class="col-md-7 col-xs-12">
                                                                    
                                                                </div>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="div14" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-12 control-label">
                                                            GST : &nbsp;
                                                                    <asp:Label ID="lblGST" runat="server"></asp:Label></label>
                                                        <%--<div class="col-md-7 col-xs-12">
                                                                    
                                                                </div>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="div15" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-12 control-label">
                                                            Inder Margin Scheme : &nbsp;
                                                                    <asp:Label ID="lblUnderMargin" runat="server"></asp:Label></label>
                                                        <%--<div class="col-md-7 col-xs-12">
                                                                    
                                                                </div>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="div16" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-12 control-label">
                                                            Bank Name : &nbsp;
                                                                    <asp:Label ID="lblBankName" runat="server"></asp:Label></label>
                                                        <%--<div class="col-md-7 col-xs-12">
                                                                    
                                                                </div>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="div17" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-12 control-label">
                                                            Account No. : &nbsp;
                                                                    <asp:Label ID="lblAccountNo" runat="server"></asp:Label></label>
                                                        <%--<div class="col-md-7 col-xs-12">
                                                                    
                                                                </div>--%>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="div18" runat="server" visible="false" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-12 control-label">
                                                            IFSC Code : &nbsp;
                                                                    <asp:Label ID="lblIFSCCode" runat="server"></asp:Label></label>
                                                        <%--<div class="col-md-7 col-xs-12">
                                                                    
                                                                </div>--%>
                                                    </div>
                                                </div>
                                            </div>









                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%-- </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmMstMMUnRegVend" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />

</asp:Content>
