<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="DelearToKRO.aspx.cs" Inherits="ShERPa360net.UTILITY.DelearToKRO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <contenttemplate>
                            <div class="panel-heading">
                                <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Delear To KRO</strong></h3>
                            </div>

                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="row">
                                            <fieldset class="scheduler-border">
                                                <legend class="scheduler-border">Delear To KRO</legend>
                                                <div class="col-md-12">
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Vendor Name. :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList TabIndex="2" ID="ddlVendor" ClientIDMode="Static" runat="server" CssClass="form-control ddlVendor required_text_box" OnSelectedIndexChanged="ddlVendor_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                <asp:Label ID="lblvendoralert" runat="server" ClientIDMode="Static" Style="color: red!important; font-weight: bold!important; display: none!important;">Please Select Vendor Name.</asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                      <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Vendor Code.:</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtVendorCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Max Day. :</label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList TabIndex="2" ID="ddlmaxday" ClientIDMode="Static" runat="server" CssClass="form-control ddlVendor required_text_box"></asp:DropDownList>
                                                                <asp:Label ID="lblmaxday" runat="server" ClientIDMode="Static" Style="color: red!important; font-weight: bold!important; display: none!important;">Please Select Max Day.</asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Button TabIndex="3" ID="btnmakekro" ValidationGroup="SaveAll" runat="server" Text="Make KRO" CssClass="btn btn-primary" OnClientClick="return ShowErrorMessageKRO();" OnClick="btnmakekro_Click"></asp:Button>
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </contenttemplate>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmDealertoKRO" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />
</asp:Content>
