<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="frmRTVData.aspx.cs" Inherits="ShERPa360net.MM.frmRTVData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Return To Vendor Data</title>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />

    <script>
        $(document).ready(function () {
            BindGrid();
        });
        function BindGrid() {
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Return to Vendor  </strong>Data</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Date : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
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
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Plant : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlPlantCode" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPlantCode_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Item Group : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlItemGroup" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSearh" CssClass="btn btn-success pull-left" Text="Search" OnClick="lnkSearh_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export IST" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
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
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; overflow-y: scroll; max-height: 500px !important;">
                                        <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Action" Visible="True">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="btnRTV" Text="Return To Vendor" OnClick="btnRTV_Click" OnClientClick="return confirm('Are you sure you want to Return this Device to Vendor?');"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Vendor Code">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblVENDCODE" Text='<%# Bind("VENDCODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Vendor Code" DataField="VENDCODE" />--%>

                                                <asp:TemplateField HeaderText="Vendor Name">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblVENDNAME" Text='<%# Bind("VENDNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Vendor Name" DataField="VENDNAME" />--%>

                                                <asp:TemplateField HeaderText="DN No.">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDNNO" Text='<%# Bind("DNNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="DN No." DataField="DNNO" />--%>

                                                <asp:TemplateField HeaderText="DN Sr. No.">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblSRNO" Text='<%# Bind("SRNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="DN Sr. No." DataField="SRNO" />--%>

                                                <asp:TemplateField HeaderText="PO No.">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPONO" Text='<%# Bind("PONO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="PO No." DataField="PONO" />--%>

                                                <asp:TemplateField HeaderText="PO Sr. No.">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPOSRNO" Text='<%# Bind("POSRNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="PO Sr. No." DataField="POSRNO" />--%>

                                                <asp:TemplateField HeaderText="Item Code">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblITEMCODE" Text='<%# Bind("ITEMCODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Item Code" DataField="ITEMCODE" />--%>

                                                <asp:TemplateField HeaderText="Item Desc">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblITEMDESC" Text='<%# Bind("ITEMDESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Item Desc" DataField="ITEMDESC" />--%>

                                                <asp:TemplateField HeaderText="Qty">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDNQTY" Text='<%# Bind("DNQTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Qty" DataField="DNQTY" />--%>


                                                <asp:TemplateField HeaderText="IMEI No.">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblIMEINO" Text='<%# Bind("IMEINO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Plant">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPLANTCD" Text='<%# Bind("PLANTCD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Plant" DataField="PLANTCD" />--%>

                                                <asp:TemplateField HeaderText="Loc.">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblLOCCD" Text='<%# Bind("LOCCD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Loc." DataField="LOCCD" />--%>

                                                <asp:TemplateField HeaderText="Job ID">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblTRNUM" Text='<%# Bind("TRNUM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Job ID" DataField="TRNUM" />--%>

                                                <asp:TemplateField HeaderText="Return By">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblENTRYBY" Text='<%# Bind("ENTRYBY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Return By" DataField="ENTRYBY" />--%>

                                                <asp:TemplateField HeaderText="Return Date">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblENTRYDT" Text='<%# Bind("ENTRYDT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField HeaderText="Return Date" DataField="ENTRYDT" />--%>
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

    <input type="hidden" id="menutabid" value="tsmTranRTV" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />

</asp:Content>
