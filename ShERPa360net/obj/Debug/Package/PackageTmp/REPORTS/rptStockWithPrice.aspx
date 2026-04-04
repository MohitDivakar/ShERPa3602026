<%@ Page Title="Stock Report  with Purchase Price" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptStockWithPrice.aspx.cs" Inherits="ShERPa360net.REPORTS.rptStockWithPrice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Stock Report  with Purchase Price</title>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <%--<link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />--%>

    <script>
        $(document).ready(function () {
            BindMakeAssociateModel();
        });

        function BindMakeAssociateModel() {
            $("#ContentPlaceHolder1_grvData").DataTable({
                paging: true,
                dom: 'Bfrtip',
                destroy: true,
                buttons: [
                    {
                        extend: 'collection',
                        text: 'Export',
                        buttons: [
                            'copy',
                            'excel',
                            'csv',
                            'pdf',
                            'print'
                        ]
                    }
                ]
            });
        }

    </script>

    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            /*debugger;*/
            document.getElementById("busy-holder1").style.display = "";
            document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Stock report with Purchase Price</strong></h3>
                        </div>

                        <div class="panel-body">

                            <div class="col-md-12">
                                <div class="row">


                                    <div class="col-md-3">
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <label class="col-md-5 control-label"></label>
                                                <div class="col-md-7 col-xs-12">
                                                    <asp:LinkButton runat="server" ID="lnkSerch" CssClass="btn btn-success" Text="Search Rate" OnClick="lnkSerch_Click" ValidationGroup="SaveAll"><span tooltip="Search" flow="down"><i class="fa fa-search"> View Stock</i></span></asp:LinkButton>
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

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-body">
                        <div class="row" style="margin-top: 20px !important;">

                            <div class="col-md-12">
                                <div class="box-body divhorizontal" style="overflow-x: scroll; overflow-y: scroll; max-height: 500px !important;">
                                    <asp:GridView ID="grvData" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                        CellSpacing="0" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%">
                                        <EmptyDataTemplate>
                                            No Record Found!
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Inward Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPODT" runat="server" Text='<%# Eval("PODT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Lot No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblREMARK" runat="server" Text='<%# Eval("REMARK") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Article Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblARTICLENO" runat="server" Text='<%# Eval("ARTICLENO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item-Desctiption">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblITEMDESC" runat="server" Text='<%# Eval("ITEMDESC") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Brand">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMAKE" runat="server" Text='<%# Eval("MAKE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item Group">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblITEMGRPDESC" runat="server" Text='<%# Eval("ITEMGRPDESC") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Category">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblITEMSUBGRPDESC" runat="server" Text='<%# Eval("ITEMSUBGRPDESC") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Serial No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIMEINO" runat="server" Text='<%# Eval("IMEINO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Purchase Price">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRATE" runat="server" Text='<%# Eval("RATE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Job Id">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTRNUM" runat="server" Text='<%# Eval("TRNUM") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Plant">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPLANTCD" runat="server" Text='<%# Eval("PLANTCD") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Job Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblJOBSTATDESC" runat="server" Text='<%# Eval("JOBSTATDESC") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="MRP">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMRP" runat="server" Text='<%# Eval("MRP") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Online Price">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblONLINEPRICE" runat="server" Text='<%# Eval("ONLINEPRICE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Dealer Price">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDEALERPRICE" runat="server" Text='<%# Eval("DEALERPRICE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cust. Price">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCUSTOMERPRCE" runat="server" Text='<%# Eval("CUSTOMERPRCE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Approx. Estimate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAPRXESTIAMT" runat="server" Text='<%# Eval("APRXESTIAMT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Actual Estimate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblESTIMATE" runat="server" Text='<%# Eval("ESTIMATE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Date Diff.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblJDATED" runat="server" Text='<%# Eval("JDATED") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="URL" Visible="false">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lblURL" runat="server" Target="_blank" Text="Open Product" NavigateUrl='<%# Eval("URL") %>'></asp:HyperLink>
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

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmRptStockwithPrice" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptMIS" runat="server" />

</asp:Content>
