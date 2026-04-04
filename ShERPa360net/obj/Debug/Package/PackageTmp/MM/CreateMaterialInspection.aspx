<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="CreateMaterialInspection.aspx.cs" Inherits="ShERPa360net.MM.CreateMaterialInspection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--<script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            document.getElementById("busy-holder1").style.display = "";
            document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";
        }
    </script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Material Inspection</strong></h3>
                            <asp:LinkButton ID="lnkAcceptAll" runat="server" CssClass="btn btn-success pull-right" OnClick="lnkAcceptAll_Click" Text="Accept All" ValidationGroup="SaveAll"></asp:LinkButton>
                            <asp:LinkButton ID="imgSaveAll" runat="server" CssClass="btn btn-success pull-right" OnClick="imgSaveAll_Click" OnClientClick="return AllowtoRejectAllQty();" Text="Save All" ValidationGroup="SaveAll"></asp:LinkButton>
                            <div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                <label>Please wait...</label>
                            </div>

                        </div>

                        <asp:UpdatePanel ID="pnlprimaryDetails" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtDocNo" EventName="TextChanged" />
                            </Triggers>

                            <ContentTemplate>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="col-md-2">
                                                        <div class="form-group">
                                                            <label class="col-md-4 control-label">Doc No. :</label>
                                                            <div class="col-md-8 col-xs-12">
                                                                <asp:TextBox ID="txtDocNo" runat="server" AutoPostBack="true" OnTextChanged="txtDocNo_TextChanged" CssClass="form-control" placeholder="Doc No."></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvDocNo" Style="color: red!important;" runat="server" ControlToValidate="txtDocNo" ValidationGroup="SaveAll"
                                                                    ErrorMessage="Please Enter Doc No">Please Enter Doc No</asp:RequiredFieldValidator>
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
                                            <div class="panel panel-default tabs">
                                                <ul class="nav nav-tabs" role="tablist">
                                                    <li class="active"><a href="#tab-first" role="tab" data-toggle="tab">Material Details</a></li>
                                                </ul>
                                                <div class="panel-body tab-content">
                                                    <div class="tab-pane active" id="tab-first">
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <asp:GridView ID="grvListItem" runat="server" AutoGenerateColumns="false" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Document No">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDocumentNo" Text='<%# Bind("DOCNO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Sr No">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblSrNo" Text='<%# Bind("SRNO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="TR No.">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblTrNo" Text='<%# Bind("TRNUM") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Item Code">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblItemCode" Text='<%# Bind("ITEMCODE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Item">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblItem" Text='<%# Bind("ITEMDESC") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="PO No">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblPoNo" Text='<%# Bind("PONO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="PO Sr No">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblPoSrNo" Text='<%# Bind("POSRNO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Qty">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblQty" Text='<%# Bind("QTY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Challan Qty">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblChalanQty" Text='<%# Bind("CHLNQTY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Accepted Qty">
                                                                            <ItemTemplate>
                                                                                <asp:TextBox runat="server" ID="txtAcceptedQty" Text='<%# Bind("ACPTQTY") %>'></asp:TextBox>
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmTranMMMatIns" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />

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
                    <button type="button" class="btn btn-success" id="btnYes" runat="server" data-dismiss="modal">Yes</button>
                    <button type="button" class="btn btn-danger" data-dismiss="modal">No</button>
                </div>
            </div>
            <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
        </div>
    </div>
</asp:Content>
