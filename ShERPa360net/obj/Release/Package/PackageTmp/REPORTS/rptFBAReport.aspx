<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptFBAReport.aspx.cs" Inherits="ShERPa360net.REPORTS.rptFBAReport" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>FBM - FBA Report</title>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />


    <script>
        $(document).ready(function () {
            BindMakeAssociateModel();
            //$("#myInput").on("keyup", function () {
            //    var value = $(this).val().toLowerCase();
            //    $("#ContentPlaceHolder1_gvList tr ").filter(function () {
            //        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            //    });
            //});
        });
        function BindMakeAssociateModel() {

            $("#ContentPlaceHolder1_gvList").DataTable({
                dom: 'Bfrtip',
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

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">

        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>FBM - FBA Report</h3>
                        </div>

                        <div class="panel-body">

                            <div class="row">

                                <div class="col-md-12">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Date From : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">To : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker" MaxLength="10" Enabled="false"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <%--<asp:LinkButton runat="server" ID="lnkNewVend" CssClass="btn btn-success pull-left" Text="New Vend" PostBackUrl="~/CRM/frmVendorMaster.aspx"><i class="fa fa-file-text"></i></asp:LinkButton>--%>
                                            <asp:LinkButton runat="server" ID="lnkSearh" CssClass="btn btn-success pull-left" OnClick="lnkSearh_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span>   </asp:LinkButton>
                                            <%--<asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export List" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>--%>
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;FBM - FBA Report Summary</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <fieldset class="scheduler-border">

                                            <div class="col-md-12" id="divName" runat="server" visible="true">
                                                <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                    <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                        CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" OnRowCommand="gvList_RowCommand">
                                                        <EmptyDataTemplate>
                                                            <div style="text-align: center; color: red; font-size: 18px;">
                                                                List is empty !
                                                            </div>
                                                        </EmptyDataTemplate>

                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Listing Type" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLISTINGTYPEID" runat="server" Text='<%# Eval("LISTINGTYPEID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Branch">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLISTINGTYPE" runat="server" Text='<%# Eval("LISTINGTYPE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Inventory Inward FTD">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblFTD" runat="server" Text='<%# Eval("FTD") %>' CommandName="FTD"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Inventory Inward MTD">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblMTD" runat="server" Text='<%# Eval("MTD") %>' CommandName="MTD"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Avg TAT from Receipt to Inward">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblPICKUPTOINWARDAVGHRs" runat="server" Text='<%# Eval("PICKUPTOINWARDAVGHRs") %>' CommandName="PICKUPTOINWARDAVGHRs"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Avg TAT from Pre-Inward to Job Create">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblINWARDTOJOBSHEETAVGHRs" runat="server" Text='<%# Eval("INWARDTOJOBSHEETAVGHRs") %>' CommandName="INWARDTOJOBSHEETAVGHRs"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Avg TAT from Inward to Ready for FBA Dispatch">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblINWARDTOPHYAVGHRs" runat="server" Text='<%# Eval("INWARDTOPHYAVGHRs") %>' CommandName="INWARDTOPHYAVGHRs"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Avg TAT from Inward to FBA Dispatch">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblINWARDTOFBAAVGHRs" runat="server" Text='<%# Eval("INWARDTOFBAAVGHRs") %>' CommandName="INWARDTOFBAAVGHRs"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Avg TAT from Procurement to FBA Dispatch">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblPICKUPTOFBAAVGHRs" runat="server" Text='<%# Eval("PICKUPTOFBAAVGHRs") %>' CommandName="PICKUPTOFBAAVGHRs"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
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

    <input type="hidden" id="menutabid" value="tsmRptFBM" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmReports" runat="server" />

</asp:Content>
