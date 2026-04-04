<%@ Page Title="" Language="C#" MasterPageFile="~/SD/SDMaster.Master" AutoEventWireup="true" CodeBehind="frmViewSO.aspx.cs" Inherits="ShERPa360net.SD.frmViewSO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>View SO</title>
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>Sales Order</h3>
                        </div>


                        <div class="panel-body">

                            <div class="row">

                                <div class="col-md-12">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">SO Type : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlSOTYPE" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">SO No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtSONO" runat="server" CssClass="form-control" MaxLength="10" TextMode="Number"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">SO Date : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
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
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12" style="margin-top: 10px !important;">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Ref. No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtRefNo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Status : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                    <asp:ListItem Text="Active" Value="57" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="Cancelled" Value="58"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Only Pending : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:CheckBox ID="chkPending" runat="server" Text="  (DC Pending)" Checked="true" CssClass="chk form-control" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkNewSO" CssClass="btn btn-success pull-left" Text="New SO" PostBackUrl="~/SD/frmSO.aspx"><span tooltip="Add New" flow="down"><i class="fa fa-plus"  aria-hidden="true"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkSerchSO" CssClass="btn btn-success pull-left" Text="Search SO" OnClick="lnkSerchSO_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export SO" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
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
                                    <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" OnRowDataBound="gvList_RowDataBound">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="SOTYPE" HeaderText="SO Type" />
                                                <asp:BoundField DataField="SONO" HeaderText="SO No." />
                                                <asp:BoundField DataField="SODTD" HeaderText="SO Date" DataFormatString="{0:dd-MM-yyyy}" />
                                                <asp:BoundField DataField="BILLTOCODE" HeaderText="Bill To Code" />
                                                <asp:BoundField DataField="CUSTNAME" HeaderText="Customer Name" />
                                                <asp:BoundField DataField="SHIPTOCODE" HeaderText="Ship To Code" />
                                                <asp:BoundField DataField="SHIPTOPARTY" HeaderText="Ship To Party" />
                                                <asp:BoundField DataField="JOBID" HeaderText="Job Id" />
                                                <asp:BoundField DataField="RETAILCUSTNAME" HeaderText="Retail Customer" />
                                                <asp:BoundField DataField="REFNO" HeaderText="Ref. No." />
                                                <asp:BoundField DataField="REFDT" HeaderText="Ref. Dt." />
                                                <asp:BoundField DataField="DELIDT" HeaderText="Delivery Dt." />
                                                <asp:BoundField DataField="ENTRYBY" HeaderText="Entered By" />
                                                <asp:BoundField DataField="ENTRYDATE" HeaderText="Entry Dt." />
                                                <asp:TemplateField HeaderText="Action" Visible="true">
                                                    <ItemTemplate>

                                                        <asp:LinkButton runat="server" ID="bntView" Text="Details" OnClick="bntView_Click"></asp:LinkButton>
                                                        <asp:Label ID="lblLine" runat="server" Text=" | "></asp:Label>
                                                        <asp:LinkButton runat="server" ID="btnEdit" Text="Edit" OnClick="btnEdit_Click"></asp:LinkButton>
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

    <div class="modal fade" id="modal-SODet" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7">SO Detail</h4>
                </div>


                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Label ID="lblSODET" runat="server"></asp:Label>
                            </div>

                            <div class="col-md-12" style="margin-top: 10px !important;">
                                <asp:Label ID="lblSONO" runat="server"></asp:Label>
                            </div>

                            <div class="col-md-12" style="margin-top: 10px !important;">
                                <asp:Label ID="lblDCNO" runat="server"></asp:Label>
                            </div>

                            <div class="col-md-12" style="margin-top: 10px !important;">
                                <asp:Label ID="lblSINO" runat="server"></asp:Label>
                            </div>


                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>


    <div class="modal fade" id="modal-SOView" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">

                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7">SO Detail</h4>
                </div>


                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>So Type :</label>
                                    <asp:Label ID="lblPopDocType" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Segment :</label>
                                    <asp:Label ID="lblPopSegment" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Dist. Chnl. :</label>
                                    <asp:Label ID="lblPopDistChnl" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>So Dt. :</label>
                                    <asp:Label ID="lblPopSODate" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>SO No. :</label>
                                    <asp:Label ID="lblPopSONO" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Ref. No. :</label>
                                    <asp:Label ID="lblPopRefNo" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Ref. Dt. :</label>

                                    <asp:Label ID="lblPopRefDate" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Comm. Agent :</label>

                                    <asp:Label ID="lblPopAgent" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Customer :</label>
                                    <asp:TextBox ID="lblPopCustomer" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Shipper :</label>
                                    <asp:TextBox ID="lblPopShipper" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Pay Terms :</label>
                                    <asp:TextBox ID="lblPopPayterms" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Retail Cust. :</label>
                                    <asp:TextBox ID="lblPopRetialCust" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Remarks :</label>

                                    <asp:Label ID="lblPopRemarks" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Net Amt. :</label>

                                    <asp:Label ID="lblPopNetAmt" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Tax Amt. :</label>

                                    <asp:Label ID="lblPopTaxAmt" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Discount :</label>

                                    <asp:Label ID="lblPopDiscount" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Total Amt. :</label>

                                    <asp:Label ID="lblPopTotalAmt" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Other Charges :</label>

                                    <asp:Label ID="lblPopOtherCharges" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Sales From :</label>

                                    <asp:Label ID="lblPopSalesChnl" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Sales Scheme :</label>

                                    <asp:Label ID="lblPopSaleChnl" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>


                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Address 1 :</label>

                                    <asp:TextBox ID="txtPopAddress1" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Address 2 :</label>

                                    <asp:TextBox ID="txtPopAddress2" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Address 3 :</label>

                                    <asp:TextBox ID="txtPopAddress3" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Pincode :</label>
                                    <asp:TextBox ID="lblPopPincode" runat="server" CssClass="form-control" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>City :</label>

                                    <asp:Label ID="lblPopCity" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>State :</label>

                                    <asp:Label ID="lblPopState" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Mobile No. :</label>

                                    <asp:Label ID="lblPopMobileNo" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Email :</label>

                                    <asp:Label ID="lblPopEmail" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>






                            <div class="col-md-12" style="margin-top: 10px;">
                                <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                    <asp:GridView ID="grvListItem" runat="server" AutoGenerateColumns="false" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%"
                                        EmptyDataText="No Record Found !">
                                        <EmptyDataTemplate>
                                            <asp:Label ID="lblGVEmpty" runat="server" Text="No Data Found"></asp:Label>
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr. No.">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVID" Text='<%# Bind("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Item Code">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVItemCode" Text='<%# Bind("ITEMCODE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item Desc.">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVItemDesc" Text='<%# Bind("ITEMDESC") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVItemId" Text='<%# Bind("ITEMID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item Group">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVItemGroup" Text='<%# Bind("ITEMGROUP") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item Group Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVGroupId" Text='<%# Bind("ITEMGROUPID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="UOM">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVUOM" Text='<%# Bind("ITEMUOM") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="UOM ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVUOMID" Text='<%# Bind("UOMID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Grade">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVGrade" Text='<%# Bind("GRADE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Grade ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVGradeID" Text='<%# Bind("GRADEID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVQty" Text='<%# Bind("ITEMQTY") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVRate" Text='<%# Bind("ITEMRATE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Base Rate">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVBaseRate" Text='<%# Bind("ITEMBRATE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVAmount" Text='<%# Bind("ITEMAMOUNT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Discount Amount">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVDiscount" Text='<%# Bind("DISCOUNTAMT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Deli. Date">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVDeliDate" Text='<%# Bind("DELIVERYDATE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="GL Code">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVGLCode" Text='<%# Bind("GLCODE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cost Center">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVCostCenter" Text='<%# Bind("COSTCENTER") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Plant Code">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVPlantCD" Text='<%# Bind("ITEMPLANTCD") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Plant Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVPlantID" Text='<%# Bind("ITEMPLANTID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Location Code">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVLocationCD" Text='<%# Bind("ITEMLOCCD") %>'></asp:Label>
                                                </ItemTemplate>

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Location Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVLocationCDID" Text='<%# Bind("LOCCDID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Profit Center">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVProfitCenter" Text='<%# Bind("PROFITCENTER") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Job Id">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVTrackNo" Text='<%# Bind("JOBID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVRemarks" Text='<%# Bind("ITEMREMARKS") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="IMEI">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVIMEI" Text='<%# Bind("IMEINO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item Text">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVITEMTEXT" Text='<%# Bind("ITEMTEXT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cust. Part No.">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVCUSTPARTNO" Text='<%# Bind("CUSTPARTNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Cust. Part Desc.">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblGVCUSTPARTDESC" Text='<%# Bind("CUSTPARTDESC") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Id" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblSKU" Text='<%# Bind("SKU") %>'></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                            <div class="col-md-12" style="margin-top: 10px;">
                                <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                    <asp:GridView ID="grvTaxation" runat="server" AutoGenerateColumns="false" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
                                        <Columns>

                                            <asp:TemplateField HeaderText="Operator">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblTaxOperator" Text='<%# Bind("OPERATOR") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Sr. No.">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblTaxSrNo" Text='<%# Bind("TAXSRNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="PO Sr. No.">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblTaxPOSrNo" Text='<%# Bind("SOSRNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Condition Type">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblTaxCondType" Text='<%# Bind("CONDTYPE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblTaxRate" Text='<%# Bind("TAXRATE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Base Amount">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblTaxBaseAmount" Text='<%# Bind("TAXBASEAMOUNT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Tax Amount">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblTaxAmount" Text='<%# Bind("TAXAMOUNT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="PID">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblPID" Text='<%# Bind("PID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="COND. ID">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblCONDID" Text='<%# Bind("CONDID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>




                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>

                            <div class="col-md-12" style="margin-top: 15px;">
                                <asp:GridView ID="grvCharges" runat="server" AutoGenerateColumns="false" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sr. No.">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblChrgSrNo" Text='<%# Bind("CHRGSRNO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Charges Type">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblChrgCondType" Text='<%# Bind("CHRGTYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Charges Amount">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblChrgAmount" Text='<%# Bind("CHRGAMOUNT") %>'></asp:Label>
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


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranSDSO" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranSD" runat="server" />

</asp:Content>
