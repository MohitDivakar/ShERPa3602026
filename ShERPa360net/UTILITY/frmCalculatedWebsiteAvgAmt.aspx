<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="frmCalculatedWebsiteAvgAmt.aspx.cs" Inherits="ShERPa360net.UTILITY.frmCalculatedWebsiteAvgAmt" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Calculated Website Upload Avg Amount</title>
    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }

        body .table thead th {
            position: sticky !important;
            top: -1px !important;
            /*background-color: #3D2A70 !important;
            color: white !important;*/
        }

        .header {
            position: sticky !important;
            top: -1px !important;
            /*background-color: #3D2A70 !important;
            color: white !important;*/
        }
    </style>

    <style type="text/css">
        .margin-top {
            margin-top: 25px;
        }

        .new {
            height: 100px;
            width: 100px;
        }

        .col-md-12 .margin-bottom img {
            margin: 20px;
        }

        .red {
            background: none;
            color: red;
            border: none;
        }

        .zoom:hover {
            margin-top: -50px !important;
            -ms-transform: scale(15); /* IE 9 */
            -webkit-transform: scale(15); /* Safari 3-8 */
            transform: scale(15);
            transform-origin: -1px;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Calculated Website   </strong>Upload Avg Amount</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <%--<div class="col-md-4">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Itemcode : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtItemcode" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">SKU : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtSKU" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Make. :</label>
                                            <div class="col-md-7 col-xs-12">
                                                <asp:DropDownList TabIndex="1" ID="ddlMake" ClientIDMode="Static" AutoPostBack="false"  runat="server" CssClass="form-control ddlMake"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Search Operator. :</label>
                                            <div class="col-md-7 col-xs-12">
                                                <asp:DropDownList TabIndex="2" ID="ddlSearchOperator" ClientIDMode="Static" AutoPostBack="false"  runat="server" CssClass="form-control ddlSearchOperator">
                                                    <asp:ListItem Value="ALL" Text ="ALL" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Value="=" Text ="="></asp:ListItem>
                                                    <asp:ListItem Value="!=" Text ="!="></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkTotalUnmappedItem" CssClass="btn btn-success pull-right" Visible="false" Text="Total Unmapped" OnClick="lnkTotalUnmappedItem_Click"></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkSerchCalcWebAmt" CssClass="btn btn-success pull-right" Text="Search Mapping" OnClick="lnkSerchCalcWebAmt_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-right" Text="Export Mapping" OnClick="lnkExport_Click">
                                            <span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
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
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Listed Product</strong> List</h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <fieldset class="scheduler-border">
                                            <br />
                                            <div class="col-md-12" id="divName" runat="server" visible="true">
                                                <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                    <div class="col-md-12" runat="server" id="dvlisted" visible="true">
                                                        <div class="col-md-2">
                                                            <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkSelectAll" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" Text="SelectAll" Style="font-weight: bold!important;" />
                                                        </div>

                                                        <div class="col-md-8 text-center">
                                                            <asp:Button runat="server" OnClientClick="ShowProgressBaar();" Style="margin-bottom: 10px!important;" ID="lnkUpdateSelectedAllStock" OnClick="lnkUpdateSelectedAllStock_Click" ClientIDMode="Static" Text="Update All" CssClass="btn btn-success" />
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12">
                                                        <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                                            <EmptyDataTemplate>
                                                                <div style="text-align: center; color: red; font-size: 18px;">
                                                                    No Record Found !
                                                                </div>
                                                            </EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Select">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelection" runat="server"></asp:CheckBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Make">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMake" runat="server" Text='<%# Eval("MAKE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Model">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblModel" runat="server" Text='<%# Eval("MODEL") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Color">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblColor" runat="server" Text='<%# Eval("COLOR") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Ram">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRam" runat="server" Text='<%# Eval("RAM") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Rom">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRom" runat="server" Text='<%# Eval("ROM") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Website Code">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblWEBSITE" runat="server" Text='<%# Eval("WEBSITE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Average Amt">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblAverageAmt" runat="server" Text='<%# Eval("AVERAGEAMT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Total Qty">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalQty" runat="server" Text='<%# Eval("TOTALQTY") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Action" Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" CssClass="btn btn-primary" ClientIDMode="Static" OnClick="btnUpdateStock_Click" ID="btnUpdateStock" Text="Update"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
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
    <input type="hidden" id="menutabid" value="tsmTranMobexSellerCalWebAvgAmt" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmMstMM" />
</asp:Content>
