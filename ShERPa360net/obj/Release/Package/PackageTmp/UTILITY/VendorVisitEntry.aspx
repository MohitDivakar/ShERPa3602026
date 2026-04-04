<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="VendorVisitEntry.aspx.cs" Inherits="ShERPa360net.UTILITY.VendorVisitEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <asp:UpdatePanel ID="pnlprimaryDetails" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnReset" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                            </Triggers>

                            <ContentTemplate>
                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Vendor Visit Entry</strong></h3>
                                    <%--<asp:LinkButton ID="imgReset" runat="server" CssClass="btn btn-success pull-right" OnClick="imgReset_Click" Text="Reset"><i class="fa fa-undo"></i></asp:LinkButton>
                                    <asp:LinkButton ID="imgSaveAll" runat="server" CssClass="btn btn-success pull-right" OnClick="imgSaveAll_Click" Text="Save All" ValidationGroup="SaveAll"><i class="fa fa-save"></i></asp:LinkButton>--%>
                                </div>

                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="row">
                                                <fieldset class="scheduler-border">
                                                    <legend class="scheduler-border">Vendor Visit Entry</legend>
                                                    <div class="col-md-12">
                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Vendor Name. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:DropDownList TabIndex="2" ID="ddlVendor" ClientIDMode="Static"   runat="server" CssClass="form-control ddlVendor required_text_box"></asp:DropDownList>    
                                                                    <asp:Label ID="lblvendoralert" runat="server" ClientIDMode="Static" Style="color: red!important; font-weight: bold!important; display: none!important;">Please Select Vendor Name.</asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Feedback. :</label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:TextBox TabIndex="3" ID="txtFeedback" ClientIDMode="Static" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Feedback" CssClass="form-control required_text_box"></asp:TextBox>
                                                                    <asp:Label ID="lblfeedbackalert" runat="server" ClientIDMode="Static" Style="color: red!important; font-weight: bold!important; display: none!important;">Please Enter FeedBack if Not Add Stock.</asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <asp:Button TabIndex="3" ID="btnAdd" OnClick="imgSaveAll_Click" OnClientClick="return ValidateVendorVisit('AddStock');" ValidationGroup="SaveAll" runat="server" Text="Add Stock" CssClass="btn btn-primary"></asp:Button>
                                                            <asp:Button TabIndex="3" ID="btnReset" OnClick="imgReset_Click" OnClientClick="return ValidateVendorVisit('JustVisit');" Style="margin-left: 40px!important" runat="server" Text="Just Visit" CssClass="btn btn-success"></asp:Button>
                                                        </div>
                                                    </div>
                                                </fieldset>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmTranMobexVendorVisit" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />
</asp:Content>

