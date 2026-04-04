<%@ Page Title="" Language="C#" MasterPageFile="~/FI/MasterFI.Master" AutoEventWireup="true" CodeBehind="frmBankDetail.aspx.cs" Inherits="ShERPa360net.FI.frmBankDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Update Bank Details</title>

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
                                        <fieldset class="scheduler-border shadow" runat="server" id="dvPersonalDetail" visible="true">
                                            <legend class="scheduler-border">Personal Details</legend>
                                            <div class="col-md-12" id="divVendCode" runat="server" visible="true" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Vendor Code :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtVendCode" runat="server" CssClass="form-control textboxbordercolor" Enabled="true" TabIndex="2"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvVendCOde" Style="color: red;" ControlToValidate="txtVendCode" runat="server" ValidationGroup="Save1"
                                                                ErrorMessage="Enter Vendor Code" Display="Dynamic">Enter Vendor Code</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <%--<asp:LinkButton runat="server" ID="lnkNewVend" CssClass="btn btn-success pull-left" Text="New Vend" PostBackUrl="~/FI/frmFIVendMaster.aspx"><span tooltip="Add New" flow="down"><i class="fa fa-plus"  aria-hidden="true"></i> </span></asp:LinkButton>--%>
                                                        <asp:LinkButton runat="server" ID="lnkSearhVend" CssClass="btn btn-success pull-left" Text="Search Vendor" OnClick="lnkSearhVend_Click" ValidationGroup="Save1"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                                        <%--<asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export Vendor List" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>--%>
                                                    </div>
                                                </div>

                                            </div>
                                        </fieldset>

                                        <fieldset class="scheduler-border shadow" runat="server" id="dvBankDetail1" visible="true">
                                            <legend class="scheduler-border">Banking Details</legend>

                                            <div class="col-md-12" id="div1" runat="server" visible="true" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Vendor Code :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtVendorCode" runat="server" CssClass="form-control textboxbordercolor" ReadOnly="true" TabIndex="2"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Style="color: red;" ControlToValidate="txtVendorCode" runat="server" ValidationGroup="Save9"
                                                                ErrorMessage="Enter Vendor Code" Display="Dynamic">Enter Vendor Code</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divAccType" runat="server" visible="true" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Account Type :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="form-control" TabIndex="38"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvAccountType" Style="color: red;" ControlToValidate="ddlAccountType" runat="server" ValidationGroup="Save9"
                                                                ErrorMessage="Select Account Type" Display="Dynamic" Enabled="true" InitialValue="0">Select Account Type</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divIFSC" runat="server" visible="true" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">IFSC Code :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="form-control" placeholder="IFSC Code" TabIndex="37"
                                                                OnTextChanged="txtIFSCCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvIFSCCode" Style="color: red;" ControlToValidate="txtIFSCCode" runat="server" ValidationGroup="Save9"
                                                                ErrorMessage="Please Enter IFSC Code" Enabled="true" Display="Dynamic">Please Enter IFSC Code</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divBank" runat="server" visible="true">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Bank Name :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control " placeholder="Bank Name" TabIndex="35"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvBankName" Style="color: red;" ControlToValidate="txtBankName" runat="server" ValidationGroup="Save9"
                                                                ErrorMessage="Please Enter Bank Name" Enabled="true" Display="Dynamic">Please Enter Bank Name</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12" id="divACNo" runat="server" visible="true" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Account No. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtACNo" runat="server" CssClass="form-control" placeholder="Account No." TabIndex="36"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvAccountNo" Style="color: red;" ControlToValidate="txtACNo" runat="server" ValidationGroup="Save9"
                                                                ErrorMessage="Please Enter Account No" Display="Dynamic" Enabled="true">Please Enter Account No</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12" id="div2" runat="server" visible="true" style="margin-top: 5px !important;">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Cancelled Cheque / Passbook :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:FileUpload ID="fuCheque" runat="server" CssClass="form-control" />
                                                            <asp:RequiredFieldValidator ID="rfvFUCheque" Style="color: red;" ControlToValidate="fuCheque" runat="server" ValidationGroup="Save9"
                                                                ErrorMessage="Please Upload Cancelled Cheque / Passbook Image" Enabled="true">Please Upload Cancelled Cheque / Passbook Image</asp:RequiredFieldValidator>
                                                            <label id="lblChequealert" style="display: none!important; font-weight: bold; color: red!important;">Please Select the only JPEG,JPG,TIFF,PNG.</label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="col-md-12" id="divNext9" runat="server" visible="true" style="margin-top: 5px !important;">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <%--<asp:LinkButton runat="server" ID="lnkPrev9" CssClass="btn btn-success pull-left" TabIndex="49" Text="Previous" OnClick="lnkPrev9_Click"><i class="fa fa-arrow-circle-left"></i></asp:LinkButton>--%>
                                                        <asp:LinkButton runat="server" ID="lnkSave" CssClass="btn btn-success pull-right" TabIndex="52" Text="SAVE" OnClick="lnkSave_Click" ValidationGroup="Save9"><%--<i class="fa fa-arrow-circle-right"></i>--%></asp:LinkButton>

                                                    </div>
                                                </div>
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
    </div>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmMMVendAprv" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />

</asp:Content>
