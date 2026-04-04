<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="frmTRNPending.aspx.cs" Inherits="ShERPa360net.MM.frmTRNPending" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>TRN IST Pending</title>


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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; IST  </strong>In-Transit</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
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

                                    <div class="col-md-3">
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

                                    <%--<div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Doc. No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtDocNo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>

                                    <div class="col-md-3">
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
                                            <%--<asp:LinkButton runat="server" ID="lnkNewIST" CssClass="btn btn-success pull-left" Text="New IST" OnClick="lnkNewIST_Click"><span tooltip="Add New" flow="down"><i class="fa fa-plus"  aria-hidden="true"></i> </span></asp:LinkButton>--%>
                                            <asp:LinkButton runat="server" ID="lnkSearhIST" CssClass="btn btn-success pull-left" Text="Search IST" OnClick="lnkSearhIST_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
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
                                                        <asp:LinkButton runat="server" ID="btnIST" Text="IST" OnClick="btnIST_Click" OnClientClick="return confirm('Are you sure you want to do IST for this record?');"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField HeaderText="Doc. Type" DataField="TRANTYPE" />
                                                <asp:BoundField HeaderText="SI No." DataField="REFSINO" />
                                                <asp:BoundField HeaderText="Return SI No." DataField="SINO" />
                                                <asp:BoundField HeaderText="Item Id" DataField="ITEMID" Visible="false" />
                                                <asp:BoundField HeaderText="Item Code" DataField="ITEMCODE" />
                                                <asp:BoundField HeaderText="Item Desc." DataField="ITEMDESC" />
                                                <asp:BoundField HeaderText="Qty" DataField="QTY" />
                                                <asp:BoundField HeaderText="Rate" DataField="CAMOUNT" />
                                                <asp:BoundField HeaderText="Cost Cnt." DataField="CSTCENTCD" Visible="false" />
                                                <asp:BoundField HeaderText="Profit Cnt" DataField="PRFCNT" Visible="false" />
                                                <asp:BoundField HeaderText="Job Id" DataField="JOBID" />
                                                <asp:BoundField HeaderText="Plant" DataField="PLANTCD" />
                                                <asp:BoundField HeaderText="Loc." DataField="LOCCD" />
                                                <asp:BoundField HeaderText="Actual Plant" DataField="ACTUALPLANT" />
                                                <asp:BoundField HeaderText="Actual Loc." DataField="ACTUALLOCATION" />
                                                <asp:BoundField HeaderText="Return By" DataField="CREATEBY" />
                                                <asp:BoundField HeaderText="Return Date" DataField="CREATEDATE" />
                                                <asp:BoundField HeaderText="SO No." DataField="SONO" Visible="false" />
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


    <asp:GridView ID="grvListItem" runat="server" AutoGenerateColumns="false" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table"
        Width="100%" Visible="false">
        <Columns>
            <asp:TemplateField HeaderText="Sr. No.">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblID" Text='<%# Bind("ID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Item Code">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblItemCode" Text='<%# Bind("ITEMCODE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Item Desc.">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblItemDesc" Text='<%# Bind("ITEMDESC") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblItemId" Text='<%# Bind("ITEMID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Item Group">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblItemGroup" Text='<%# Bind("ITEMGROUP") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Item Group Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblGroupId" Text='<%# Bind("ITEMGROUPID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="UOM">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblUOM" Text='<%# Bind("ITEMUOM") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="UOM ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblUOMID" Text='<%# Bind("UOMID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Qty">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblQty" Text='<%# Bind("ITEMQTY") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Rate" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblRate" Text='<%# Bind("ITEMRATE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Amount" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblAmount" Text='<%# Bind("ITEMAMOUNT") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Deli. Date">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblDeliDate" Text='<%# Bind("DELIVERYDATE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="GL Code">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblGLCode" Text='<%# Bind("GLCODE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Cost Center">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblCostCenter" Text='<%# Bind("COSTCENTER") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="From Plant Code">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblFromPlantCD" Text='<%# Bind("ITEMFROMPLANTCD") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="From Plant Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblFromPlantID" Text='<%# Bind("ITEMFROMPLANTID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="From Location Code">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblFromLocationCD" Text='<%# Bind("ITEMFROMLOCCD") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="From Location Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblFromLocationCDID" Text='<%# Bind("LOCCDFROMID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="To Plant Code">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblPlantCD" Text='<%# Bind("ITEMPLANTCD") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="To Plant Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblPlantID" Text='<%# Bind("ITEMPLANTID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="To Location Code">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblLocationCD" Text='<%# Bind("ITEMLOCCD") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="To Location Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblLocationCDID" Text='<%# Bind("LOCCDID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Profit Center">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblProfitCenter" Text='<%# Bind("PROFITCENTER") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Asset Code">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblAssetCode" Text='<%# Bind("ASSETCODE") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Requisitioner">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblRequisitioner" Text='<%# Bind("REQUISITIONER") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Track No.">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblTrackNo" Text='<%# Bind("TRACKNO") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Item Text">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblItemText" Text='<%# Bind("ITEMTEXT") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Part Req. No.">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblPartReqNo" Text='<%# Bind("PARTREQNO") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Remarks">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblRemarks" Text='<%# Bind("ITEMREMARKS") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblSKU" Text='<%# Bind("SKU") %>'></asp:Label>

                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblMake" Text='<%# Bind("MAKE") %>'></asp:Label>

                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblModel" Text='<%# Bind("MODEL") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblDispName" Text='<%# Bind("DISPNAME") %>'></asp:Label>

                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblDispMRP" Text='<%# Bind("DISPMRP") %>'></asp:Label>

                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblValueLimit" Text='<%# Bind("VALUELIMIT") %>'></asp:Label>

                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblMaxStkQty" Text='<%# Bind("MAXSTKQTY") %>'></asp:Label>

                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblHSN" Text='<%# Bind("HSN") %>'></asp:Label>

                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblHSNGroup" Text='<%# Bind("HSNGROUP") %>'></asp:Label>

                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblHSNGroupDesc" Text='<%# Bind("HSNGROUPDESC") %>'></asp:Label>

                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblCondType" Text='<%# Bind("CONDTYPE") %>'></asp:Label>

                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblItemStatus" Text='<%# Bind("ITEMSTATUS") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblOnHandStock" Text='<%# Bind("ONHANDSTOCK") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Ref Sr No" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                <ItemTemplate>
                    <asp:Label runat="server" ID="lblREFSRNO" Text='<%# Bind("REFSRNO") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranMMIST" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />

</asp:Content>
