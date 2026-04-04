<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="frmViewProdSpec.aspx.cs" Inherits="ShERPa360net.UTILITY.frmViewProdSpec" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>Product Specification </h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3" runat="server" id="dvItemGroup" visible="true">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Item Group. :</label>
                                            <div class="col-md-7 col-xs-12">
                                                <asp:DropDownList TabIndex="2" ID="ddlItemGroup" ClientIDMode="Static" AutoPostBack="false" runat="server" CssClass="form-control ddlItemGroup required_text_box"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3" runat="server" id="dvItemSubGroup" visible="true">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Item Sub Group. :</label>
                                            <div class="col-md-7 col-xs-12">
                                                <asp:DropDownList TabIndex="2" ID="ddlItemSubGroup" ClientIDMode="Static"  AutoPostBack="true" runat="server" CssClass="form-control ddlItemSubGroup required_text_box"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3" runat="server" visible="false">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Item Type. :</label>
                                            <div class="col-md-7 col-xs-12">
                                                <asp:DropDownList TabIndex="2" ID="ddlItemType" ClientIDMode="Static" AutoPostBack="true" runat="server" CssClass="form-control ddlItemType required_text_box"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Make. :</label>
                                            <div class="col-md-7 col-xs-12">
                                                <asp:DropDownList TabIndex="2" ID="ddlMake" ClientIDMode="Static" AutoPostBack="true" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged" runat="server" CssClass="form-control ddlMake required_text_box"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Model. :</label>
                                            <div class="col-md-7 col-xs-12">
                                                <asp:DropDownList TabIndex="2" ID="ddlModel" runat="server" CssClass="form-control ddlModel required_text_box"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                   

                                </div>

                                <div class="col-md-12" style="margin-top: 10px!important;">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Mnt Top Model. :</label>
                                            <div class="col-md-7 col-xs-12">
                                                <asp:DropDownList TabIndex="2" ID="ddlMnthTop10" runat="server" CssClass="form-control">
                                                    <asp:ListItem Selected="True" Text="ALL" Value="ALL">
                                                    </asp:ListItem>
                                                    <asp:ListItem Text="MnthTop10" Value="MnthTop10">
                                                    </asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">ASIN. :</label>
                                            <div class="col-md-7 col-xs-12">
                                                <asp:TextBox runat="server" ClientIDMode="Static" ID="txtAsin" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                     <div class="col-md-6 text-center">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkNewMR" CssClass="btn btn-success" Text="New Spec" PostBackUrl="~/UTILITY/frmAddProdSpec.aspx"><span tooltip="Add New" flow="down"><i class="fa fa-plus"  aria-hidden="true"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkSerchSpec" CssClass="btn btn-success" Text="Search Spec" OnClick="lnkSerchSpec_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success " Text="Export Spec" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
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
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                        <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:TemplateField HeaderText="Action" Visible="True">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="btnEdit" Text="Edit" OnClick="btnEdit_Click" OnClientClick="SetTarget();"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ITEMGRPNAME" HeaderText="Item Group" />
                                                <asp:BoundField DataField="ITEMSUBGRPNAME" HeaderText="Item Sub Group" />
                                                <asp:BoundField DataField="BRAND_ID" HeaderText="BRAND ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="BRAND_DESC" HeaderText="Make" />
                                                <asp:BoundField DataField="MODEL_ID" HeaderText="MODEL ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="MODEL_NAME" HeaderText="Model" />
                                                <asp:BoundField DataField="RAMSIZE" HeaderText="RAM" />
                                                <asp:BoundField DataField="ROMSIZE" HeaderText="ROM" />
                                                <asp:BoundField DataField="COLOR" HeaderText="Color" />
                                                <asp:BoundField DataField="ISACTIVE" HeaderText="Active" />
                                                <asp:BoundField DataField="MNTTOPSALESSRNO" HeaderText="Monthly Top Sr No" />
                                                <asp:BoundField DataField="NEWRATE" HeaderText="New Rate" />
                                                <asp:BoundField DataField="NEWRATEUPDATEDDATE" HeaderText="New Rate Update Date" />
                                                <asp:BoundField DataField="BASICPURRATE" HeaderText="Pur Rate For A Grade" />
                                                <asp:BoundField DataField="BASICPURRATEFORBGRADE" HeaderText="Pur Rate For B Grade" />
                                                <asp:BoundField DataField="BASICPURRATEFORCGRADE" HeaderText="Pur Rate For C Grade" />
                                                <asp:BoundField DataField="LAUNCHYEAR" HeaderText="Launch Year" />
                                                <asp:BoundField DataField="CREATEDBY" HeaderText="Create By" />
                                                <asp:BoundField DataField="CREATEDATE" HeaderText="Create Date" />
                                                <asp:BoundField DataField="UPDATEBY" HeaderText="Update By" />
                                                <asp:BoundField DataField="UPDATEDATE" HeaderText="Update Date" />
                                                <asp:BoundField DataField="ASIN" HeaderText="ASIN" />
                                                <asp:BoundField DataField="FinalApproveListingAmount" HeaderText="Final Price Amount" />
                                                <asp:BoundField DataField="ISINSTANTSELLING" HeaderText="Is Instant Selling" />
                                                <asp:BoundField DataField="INSTANTSELLINGAMOUNT" HeaderText="Instant Selling Amount" />
                                                <asp:BoundField DataField="FinalStockApproveAmount" HeaderText="Final Stock Price Amount" />
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


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmTranMobexNewModel" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />
</asp:Content>
