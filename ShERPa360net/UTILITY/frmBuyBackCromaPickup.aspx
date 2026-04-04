<%@ Page Title="Croma Exchange Pickup" Language="C#" MasterPageFile="~/UTILITY/UtilityModule.Master" AutoEventWireup="true" CodeBehind="frmBuyBackCromaPickup.aspx.cs" Inherits="ShERPa360net.UTILITY.frmBuyBackCromaPickup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Croma Exchange Pickup</title>

    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            document.getElementById("busy-holder1").style.display = "";
            document.getElementById("ContentPlaceHolder1_lnkOTPSave").style.display = "none";
        }
        function ShowLoadingReverse() {
            document.getElementById("busy-holder1").style.display = "none";
            document.getElementById("ContentPlaceHolder1_lnkOTPSave").style.display = "";
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Exchange  </strong>Detials</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Date From : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">To : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Store Name : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlStore" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSearh" CssClass="btn btn-success pull-left" Text="Search" OnClick="lnkSearh_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                            <%--<asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export Quotation" OnClick="lnkExport_Click"><i class="fa fa-file"></i></asp:LinkButton>--%>
                                        </div>
                                    </div>
                                </div>
                                <div id="mobile_View">
                                    <div class="col-md-1" style="margin-top: 5px;">&nbsp;</div>
                                    <div class="col-md-3" style="margin-top: 5px;">
                                        <div class="form-group">
                                            <div class="col-md-9 col-xs-12">
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

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <asp:LinkButton ID="imgSaveAll" OnClientClick="ShowLoading()" runat="server" CssClass="btn btn-success pull-left" OnClick="imgSaveAll_Click" Text="Pickup Selected" Style="margin-top: 10px !important;"><i class="fa fa-save"></i>   Pickup Selected</asp:LinkButton>
                                    <%--<div id="busy-holder1" style="display: none" class="clearfix inline pull-left">
                                        <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                        <label>Please wait...</label>
                                    </div>--%>
                                </div>
                            </div>
                            <div class="col-md-12" style="margin-top: 20px !important;">
                                <div class="box">
                                    <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                        CellSpacing="0" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%">
                                        <EmptyDataTemplate>
                                            No Record Found!
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Select">
                                                <HeaderTemplate>

                                                    <asp:CheckBox ID="chkSelectAll" runat="server" OnCheckedChanged="chkSelectAll_CheckedChanged" AutoPostBack="true" Enabled="true" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:UpdatePanel ID="upd1" runat="server">
                                                        <Triggers>
                                                            <%--<asp:AsyncPostBackTrigger ControlID="chkSelect" EventName="CheckedChanged" />--%>
                                                            <asp:PostBackTrigger ControlID="chkSelect" />
                                                        </Triggers>
                                                        <ContentTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                            <%--<asp:CheckBox ID="chkSelect" runat="server" OnCheckedChanged="chkSelect_CheckedChanged" AutoPostBack="true" Enabled="true" />--%>
                                                            <%--<asp:LinkButton runat="server" ID="chkSelect" CssClass="btn btn-success pull-left" Text="Select" OnClick="chkSelect_CheckedChanged" Visible="true" Enabled="false">Select</asp:LinkButton>--%>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ID" Visible="true">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="CMPID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCMPID" runat="server" Text='<%# Eval("CMPID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Brand">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBRAND" runat="server" Text='<%# Eval("BRAND") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Model">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMODEL" runat="server" Text='<%# Eval("MODEL") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="RAM">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRAM" runat="server" Text='<%# Eval("RAM") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ROM">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblROM" runat="server" Text='<%# Eval("ROM") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Grade">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGRADE" runat="server" Text='<%# Eval("GRADE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cust. Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblFULLNAME" runat="server" Text='<%# Eval("FULLNAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Mobile No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMOBILENO" runat="server" Text='<%# Eval("MOBILENO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblESTIAMT" runat="server" Text='<%# Eval("ESTIAMT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Create By ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCREATEBY" runat="server" Text='<%# Eval("CREATEBY") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Create By">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUSERNAME" runat="server" Text='<%# Eval("USERNAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Create Dt.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCREATEDATE" runat="server" Text='<%# Eval("CREATEDATE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                              <asp:TemplateField HeaderText="IMAGE" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblINVIMAGE" runat="server" Text='<%# Eval("INVIMAGE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                             <asp:TemplateField HeaderText="Action" Visible="True">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="lnkInvoice" Text="Invoice" OnClick="lnkInvoice_Click"></asp:LinkButton>
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

    <div class="modal fade" id="modal-detail" data-backdrop="static" style="margin-top: 200px;">
        <center>
            <div class="modal-dialog modal-lg">
                <div class="modal-content" style="height: 120px !important; width: 50% !important;">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" style="color: #337ab7"><strong>OTP Verification</strong></h4>
                    </div>
                    <div class="modal-body">
                        <div class="box-body">
                            <div class="col-md-12">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:TextBox ID="txtOTP" runat="server" CssClass="form-control" placeholder="Enter OTP"></asp:TextBox>
                                        <asp:HiddenField ID="matchotp" runat="server" />
                                        <asp:RequiredFieldValidator ID="rfvVendorCode" runat="server" ControlToValidate="txtOTP" Display="Dynamic" ForeColor="Red"
                                            ErrorMessage="Enter OTP" ValidationGroup="Check">Enter OTP</asp:RequiredFieldValidator>
                                        <asp:Label ID="lblErrormsg" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lnkOTPSave" OnClientClick="ShowLoading()" runat="server" CssClass="btn btn-success pull-left" ValidationGroup="Check" OnClick="lnkOTPSave_Click" Text="Submit"><i class="fa fa-save"></i>   Submit</asp:LinkButton>
                                        <div id="busy-holder1" style="display: none" class="clearfix inline pull-left">
                                            <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                            <label>Please wait...</label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </center>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmCromaBuybackPickup" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmUtility" runat="server" />

</asp:Content>
