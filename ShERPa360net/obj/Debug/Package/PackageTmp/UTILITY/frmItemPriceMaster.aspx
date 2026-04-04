<%@ Page Title="Item Price Master" Language="C#" MasterPageFile="~/UTILITY/UtilityModule.Master" AutoEventWireup="true" CodeBehind="frmItemPriceMaster.aspx.cs" Inherits="ShERPa360net.frmItemPriceMaster" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Item Price Master</title>


    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }

        .btnAqua {
            width: 60px;
            background-color: #faa61a;
            color: white;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="pnlprimaryDetails" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddlMake" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlModel" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlRam" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlRom" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlGrade" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlColor" EventName="SelectedIndexChanged" />
            <asp:PostBackTrigger ControlID="txtSearchItemDesc" />
        </Triggers>

        <ContentTemplate>

            <div class="page-content-wrap">
                <div class="row">
                    <div class="col-md-12">

                        <div class="form-horizontal">
                            <div class="panel panel-default">




                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>Price Master</h3>
                                </div>


                                <div class="panel-body">

                                    <div class="row">

                                        <div class="col-md-12">

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Brand : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:DropDownList ID="ddlMake" runat="server" CssClass="form-control ddlMake" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Model : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:DropDownList ID="ddlModel" runat="server" CssClass="form-control ddlModel" OnSelectedIndexChanged="ddlModel_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">RAM : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:DropDownList ID="ddlRam" runat="server" CssClass="form-control ddlRam" OnSelectedIndexChanged="ddlRAM_SelectedIndexChanged" AutoPostBack="false"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">ROM : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:DropDownList ID="ddlRom" runat="server" CssClass="form-control ddlRom" OnSelectedIndexChanged="ddlROM_SelectedIndexChanged" AutoPostBack="false"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-md-12" style="margin-top: 10px;">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Color : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:DropDownList ID="ddlColor" runat="server" CssClass="form-control ddlColor" OnSelectedIndexChanged="ddlColor_SelectedIndexChanged" AutoPostBack="false"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Grade : </label>
                                                    <div class="col-md-9 col-xs-12">
                                                        <asp:DropDownList ID="ddlGrade" runat="server" CssClass="form-control ddlGrade" OnSelectedIndexChanged="ddlGrade_SelectedIndexChanged" AutoPostBack="false">
                                                            <asp:ListItem Text="-- SELECT --" Selected="True" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="GRADE A" Value="GRADE A"></asp:ListItem>
                                                            <asp:ListItem Text="GRADE B" Value="GRADE B"></asp:ListItem>
                                                            <asp:ListItem Text="GRADE C" Value="GRADE C"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="form-group">
                                                    <label class="col-md-2 control-label">Item Desc. : </label>
                                                    <div class="col-md-8 col-xs-12">
                                                        <asp:TextBox ID="txtSearchItemDesc" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <cc:AutoCompleteExtender ServiceMethod="GetItemCode" MinimumPrefixLength="1" CompletionInterval="10"
                                                            EnableCaching="false" CompletionSetCount="1" TargetControlID="txtSearchItemDesc" ID="Auto1" runat="server"
                                                            FirstRowSelected="false" ShowOnlyCurrentWordInCompletionListItem="true">
                                                        </cc:AutoCompleteExtender>
                                                    </div>
                                                    <div class="col-md-2 col-xs-12">
                                                        <asp:LinkButton runat="server" ID="lnkSerch" CssClass="btn btn-success pull-left" Text="Search" OnClick="lnkSerch_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                                    </div>
                                                </div>
                                            </div>
                                            <%-- <div class="col-md-2">
                                                <div class="form-group">--%>
                                            <%--<asp:LinkButton runat="server" ID="lnkNewPO" CssClass="btn btn-success pull-left" Text="New PO" PostBackUrl="~/MM/CreatePO.aspx"><i class="fa fa-file-text"></i></asp:LinkButton>--%>
                                            <%--<asp:LinkButton runat="server" ID="lnkSerch" CssClass="btn btn-success pull-left" Text="Search" OnClick="lnkSerch_Click"><i class="fa fa-search"></i></asp:LinkButton>--%>
                                            <%--<asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export PO" OnClick="lnkExport_Click"><i class="fa fa-file"></i></asp:LinkButton>--%>
                                            <%--</div>
                                            </div>--%>
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
                                        <div class="box">
                                            <asp:Button ID="btnSaveAll" runat="server" Text="Save All" CssClass="form-control btn btnAqua" Width="100" OnClick="btnSaveAll_Click" />
                                            <div class="box-body divhorizontal" style="margin-top: 10px; overflow-x: scroll;">


                                                <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                    CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                                    <EmptyDataTemplate>
                                                        No Record Found!
                                                    </EmptyDataTemplate>
                                                    <Columns>
                                                        <asp:BoundField DataField="ITEMID" HeaderText="Item Id" />
                                                        <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                        <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
                                                        <asp:BoundField DataField="MRP" HeaderText="MRP" />
                                                        <asp:TemplateField HeaderText="New MRP">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtNewAmount" runat="server" Text='<%#Eval("MRP") %>' CssClass="form-control" Width="100"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Status" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblStatus" runat="server" Text='<%#Eval("Status") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderText="Active">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkStatus" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                        <asp:TemplateField HeaderText="Action">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="form-control btn btnAqua" OnClick="btnSave_Click" />
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
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmUtlItemPriceMst" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmUtility" runat="server" />

</asp:Content>

