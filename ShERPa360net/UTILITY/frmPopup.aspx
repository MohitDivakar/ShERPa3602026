<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/UtilityModule.Master" AutoEventWireup="true" CodeBehind="frmPopup.aspx.cs" Inherits="ShERPa360net.UTILITY.frmPopup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Allow Lead Popup</title>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Lead Popup  </strong>Show / Hide</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-12">
                                        <asp:LinkButton ID="imgSaveAll" runat="server" CssClass="btn btn-success pull-right" OnClick="imgSaveAll_Click" Text="Save"><i class="fa fa-save"></i>&nbsp;&nbsp;&nbsp;&nbsp; Save </asp:LinkButton>
                                    </div>
                                </div>

                                <div class="col-md-12" style="margin-top: 10px !important;">
                                    <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                        CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" OnRowDataBound="gvList_RowDataBound">
                                        <emptydatatemplate>
                                            No Record Found!
                                        </emptydatatemplate>
                                        <headerstyle cssclass="header" />
                                        <columns>
                                            <asp:TemplateField HeaderText="ID">
                                                <itemtemplate>
                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                </itemtemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="User Name">
                                                <itemtemplate>
                                                    <asp:Label ID="lblUSERNAME" runat="server" Text='<%# Eval("USERNAME") %>'></asp:Label>
                                                </itemtemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Agent ID">
                                                <itemtemplate>
                                                    <asp:Label ID="lblAGENTID" runat="server" Text='<%# Eval("AGENTID") %>'></asp:Label>
                                                </itemtemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Popup ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <itemtemplate>
                                                    <asp:Label ID="lblPOPUP" runat="server" Text='<%# Eval("POPUP") %>'></asp:Label>
                                                </itemtemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Popup">
                                                <itemtemplate>
                                                    <asp:CheckBox ID="chkPopup" runat="server" />
                                                </itemtemplate>
                                            </asp:TemplateField>
                                        </columns>
                                    </asp:GridView>
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

    <input type="hidden" id="menutabid" value="tsmUtlPopupAllow" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmUtility" />

</asp:Content>
