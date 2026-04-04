<%@ Page Title="Payment Link" Language="C#" MasterPageFile="~/UTILITY/UtilityModule.Master" AutoEventWireup="true" CodeBehind="frmPaymentLink.aspx.cs" Inherits="ShERPa360net.UTILITY.frmPaymentLink" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Payment Link</title>

    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            var txtCustName = $("#ContentPlaceHolder1_txtCustName").val();
            var txtContactNo = $("#ContentPlaceHolder1_txtContactNo").val();
            var txtAmount = $("#ContentPlaceHolder1_txtAmount").val();
            var txtDescription = $("#ContentPlaceHolder1_txtDescription").val();
            var txtrefno = $("#ContentPlaceHolder1_txtrefno").val();
            if (txtCustName != "" && txtContactNo != "" && txtAmount != "" && txtDescription != "" && txtrefno != "") {
                document.getElementById("busy-holder1").style.display = "";
                document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";
            }
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Generate Payment Link</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="row">

                                        <div class="col-md-12">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Cust. Name : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtCustName" runat="server" CssClass="form-control required_text_box" placeholder="Customer Name" Enabled="true" TabIndex="1" MaxLength="100"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvCustName" runat="server" ControlToValidate="txtCustName" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Customer Name">Please Enter Customer Name</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Contact No. : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control required_text_box" placeholder="Customer Contact No." Enabled="true" MaxLength="10" TextMode="Number" TabIndex="2"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvContactNo" runat="server" ControlToValidate="txtContactNo" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Customer Contact Number">Please Enter Customer Contact Number</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Email ID : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtEmailID" runat="server" CssClass="form-control" placeholder="Customer Email ID" Enabled="true" TabIndex="3" MaxLength="300"></asp:TextBox>
                                                        <asp:RegularExpressionValidator ID="revEmailID" runat="server" ControlToValidate="txtEmailID" Display="Dynamic" ForeColor="Red"
                                                            ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid Email</asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Amount : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control required_text_box" placeholder="Amount" Enabled="true" TextMode="Number" TabIndex="4"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvAmount" runat="server" ControlToValidate="txtAmount" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Amount">Please Enter Amount</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Reference No. : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtrefno" runat="server" CssClass="form-control required_text_box" placeholder="Reference No." Enabled="true" TabIndex="5" MaxLength="100"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvRefno" runat="server" ControlToValidate="txtrefno" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Reference No">Please Enter Reference No</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Description : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control required_text_box" placeholder="Description" Enabled="true" TabIndex="6" MaxLength="300"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvDescription" runat="server" ControlToValidate="txtDescription" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Description">Please Enter Description</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Remarks (if any) : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" placeholder="Remarks" Enabled="true" TabIndex="7" MaxLength="300"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="col-md-10">
                                                <asp:LinkButton ID="imgCancel" runat="server" CssClass="btn btn-success pull-right" Text="Refresh" TabIndex="16" OnClick="imgCancel_Click"><i class="fa fa-refresh"></i> Refresh</asp:LinkButton>
                                                <asp:LinkButton ID="imgSaveAll" OnClientClick="ShowLoading()" runat="server" CssClass="btn btn-success pull-right" OnClick="imgSaveAll_Click" Text="Generate Link" ValidationGroup="SaveAll" TabIndex="15"><i class="fa fa-link"></i> Generate Link</asp:LinkButton>
                                                <div id="busy-holder1" style="display: none" class="clearfix inline pull-left">
                                                    <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                                    <label>Please wait...</label>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12" id="divLink" runat="server" visible="false" style="margin-top: 10px !important;">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Payment Link : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:Label ID="lblLink" runat="server" CssClass="form-control"></asp:Label>
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
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmPaymentLink" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmUtility" runat="server" />

</asp:Content>
