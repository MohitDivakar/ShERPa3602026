<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="ViewMaterialIssue.aspx.cs" Inherits="ShERPa360net.MM.ViewMaterialIssue" EnableEventValidation="false" %>


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
                            <asp:Literal runat="server" ID="ltTitle"></asp:Literal>
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

                                    <div class="col-md-2">
                                        <div class="form-group">



                                            <label class="col-md-4 control-label">Doc. No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtDocNo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>

                                                </div>
                                            </div>



                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Plant : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlPlantCode" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPlantCode_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkNewIST" CssClass="btn btn-success pull-left" Text="New IST" OnClick="lnkNewIST_Click"><span tooltip="Add New" flow="down"><i class="fa fa-plus"  aria-hidden="true"></i> </span></asp:LinkButton>
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
                                                <asp:BoundField HeaderText="Doc. Type" DataField="DOCTYPE" />
                                                <asp:BoundField HeaderText="Doc. No." DataField="DOCNO" />
                                                <asp:BoundField HeaderText="Doc. Date" DataField="DOCDATE" />
                                                <asp:BoundField HeaderText="Ref. No." DataField="REFNO" />
                                                <asp:BoundField HeaderText="Remark" DataField="REMARK" />
                                                <asp:BoundField HeaderText="Issue Dept" DataField="DEPARTMENT" />
                                                <asp:BoundField HeaderText="Emp Name" DataField="EMPLOYEENAME" />
                                                <asp:BoundField HeaderText="PO No." DataField="PONO" />
                                                <asp:TemplateField HeaderText="Action" Visible="True">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="bntView" Text="Details" OnClick="bntView_Click"></asp:LinkButton>
                                                        <asp:Label ID="lblLine" runat="server" Text=" | "></asp:Label>
                                                        <asp:LinkButton runat="server" ID="btnPrint" Text="PDF" OnClick="btnPrint_Click"></asp:LinkButton>
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

    <div class="modal fade" id="modal-detail" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>IST</strong> Details</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Doc Type :</label>
                                    <asp:Label ID="lblDoctype" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Doc. No. :</label>
                                    <asp:Label ID="lblDocNo" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Date :</label>
                                    <asp:Label ID="lblDate" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Remark :</label>
                                    <asp:Label ID="lblRemark" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Ref. No. :</label>
                                    <asp:Label ID="lblRefNo" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll">
                                        <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                            CssClass="table table-hover table-striped table-bordered nowrap">
                                            <Columns>
                                                <asp:BoundField DataField="SRNO" HeaderText="Sr. No." />
                                                <asp:BoundField DataField="DOCTYPE" HeaderText="Doc. Type" />
                                                <asp:BoundField DataField="DOCNO" HeaderText="Doc. No." />
                                                <asp:BoundField DataField="DOCKDT" HeaderText="Doc. Date" />
                                                <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
                                                <asp:BoundField DataField="QTY" HeaderText="Quantity" />
                                                <%-- HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />--%>
                                                <asp:BoundField DataField="UNIT" HeaderText="Unit" />
                                                <%--<asp:BoundField DataField="VENDOR" HeaderText="Delivery Date" DataFormatString="{0:MM/dd/yyyy}" />--%>
                                                <asp:BoundField DataField="FROMPLANT" HeaderText="From Plant" />
                                                <asp:BoundField DataField="LOCDESC" HeaderText="From Location" />
                                                <asp:BoundField DataField="TOPLANT" HeaderText="To Plant" />
                                                <asp:BoundField DataField="TOLOCDESC" HeaderText="To Location" />
                                                <%--<asp:BoundField DataField="PROFITCENTER" HeaderText="Profit Center" />--%>
                                                <%--<asp:BoundField DataField="ASSETCODE" HeaderText="Asset Code" />--%>
                                                <%--<asp:BoundField DataField="REQUISITIONER" HeaderText="Requisitioner" />--%>
                                                <%--<asp:BoundField DataField="TRACKNO" HeaderText="Tracking No." />--%>
                                                <%--<asp:BoundField DataField="ITEMTEXT" HeaderText="Item Text" />--%>
                                                <%--<asp:BoundField DataField="PARTREQNO" HeaderText="Part Req. No." />--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>


                            <%--<div class="col-md-5" style="margin-top: 5px;">
                                <div class="form-group">
                                    <asp:Label ID="lblAPRV1" runat="server" Visible="false"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblAPRV2" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>--%>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-materialissue" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Material Issue</strong> Details</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Doc Type :</label>
                                    <asp:Label ID="lblmiDoctType" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Doc. No. :</label>
                                    <asp:Label ID="lblmiDocNo" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Date :</label>
                                    <asp:Label ID="lblmiDocDate" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Issue Dept :</label>
                                    <asp:Label ID="lblmiIssueDept" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Emp Name :</label>
                                    <asp:Label ID="lblmiEmpName" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Remark :</label>
                                    <asp:Label ID="lblmiRemark" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Ref. No. :</label>
                                    <asp:Label ID="lblmiRefNo" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll">
                                        <asp:GridView ID="gvMaterialIssue" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                            CssClass="table table-hover table-striped table-bordered nowrap">
                                            <Columns>
                                                <asp:BoundField DataField="SRNO" HeaderText="Sr. No." />
                                                <asp:BoundField DataField="DOCTYPE" HeaderText="Doc. Type" />
                                                <asp:BoundField DataField="DOCNO" HeaderText="Doc. No." />
                                                <asp:BoundField DataField="DOCKDT" HeaderText="Doc. Date" />
                                                <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
                                                <asp:BoundField DataField="QTY" HeaderText="Quantity" />
                                                <asp:BoundField DataField="UNIT" HeaderText="Unit" />
                                                <asp:BoundField DataField="FROMPLANT" HeaderText="Plant" />
                                                <asp:BoundField DataField="LOCDESC" HeaderText="Location" />
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

    <div class="modal fade" id="modal-Consume" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Comsumption</strong> Details</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Doc Type :</label>
                                    <asp:Label ID="lblCMDoctype" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Doc. No. :</label>
                                    <asp:Label ID="lblCMDocNo" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Date :</label>
                                    <asp:Label ID="lblCMDate" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Ref. No. :</label>
                                    <asp:Label ID="lblCMRefNo" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Remark :</label>
                                    <asp:Label ID="lblCMRemark" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll">
                                        <asp:GridView ID="grvConsume" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                            CssClass="table table-hover table-striped table-bordered nowrap">
                                            <Columns>
                                                <asp:BoundField DataField="SRNO" HeaderText="Sr. No." />
                                                <asp:BoundField DataField="DOCTYPE" HeaderText="Doc. Type" />
                                                <asp:BoundField DataField="DOCNO" HeaderText="Doc. No." />
                                                <asp:BoundField DataField="DOCKDT" HeaderText="Doc. Date" />
                                                <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
                                                <asp:BoundField DataField="UNIT" HeaderText="Unit" />
                                                <asp:BoundField DataField="QTY" HeaderText="Quantity" />
                                                <asp:BoundField DataField="RATE" HeaderText="Rate" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="CAMOUNT" HeaderText="Total Amount" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                                                <%-- HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />--%>
                                                <%--<asp:BoundField DataField="VENDOR" HeaderText="Delivery Date" DataFormatString="{0:MM/dd/yyyy}" />--%>
                                                <asp:BoundField DataField="PLANTCD" HeaderText="Plant" />
                                                <asp:BoundField DataField="LOCCD" HeaderText="Location" />

                                                <%--<asp:BoundField DataField="CHLNQTY" HeaderText="Challan Qty" />
                                                <asp:BoundField DataField="ACPTQTY" HeaderText="Accept Qty" />
                                                <asp:BoundField DataField="RTNQTY" HeaderText="Return Qty" />--%>

                                                <asp:BoundField DataField="TRACKNO" HeaderText="Tracking No." />
                                                <asp:BoundField DataField="ITEMTEXT" HeaderText="Item Remark" />
                                                <%--<asp:BoundField DataField="TOPLANT" HeaderText="To Plant" />--%>
                                                <%--<asp:BoundField DataField="TOLOCDESC" HeaderText="To Location" />--%>
                                                <%--<asp:BoundField DataField="PROFITCENTER" HeaderText="Profit Center" />--%>
                                                <%--<asp:BoundField DataField="ASSETCODE" HeaderText="Asset Code" />--%>
                                                <%--<asp:BoundField DataField="REQUISITIONER" HeaderText="Requisitioner" />--%>
                                                <%--<asp:BoundField DataField="TRACKNO" HeaderText="Tracking No." />--%>
                                                <%--<asp:BoundField DataField="ITEMTEXT" HeaderText="Item Text" />--%>
                                                <%--<asp:BoundField DataField="PARTREQNO" HeaderText="Part Req. No." />--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>


                            <%--<div class="col-md-5" style="margin-top: 5px;">
                                <div class="form-group">
                                    <asp:Label ID="lblAPRV1" runat="server" Visible="false"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblAPRV2" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>--%>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-STO" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Delivery Challan (STO)</strong> Details</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Doc Type :</label>
                                        <asp:Label ID="lblSTODocType" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Doc. No. :</label>
                                        <asp:Label ID="lblSTODocNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Date :</label>
                                        <asp:Label ID="lblSTODate" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Ref. No. :</label>
                                        <asp:Label ID="lblSTORefNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Remark :</label>
                                        <asp:Label ID="lblSTORemark" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>PO No. :</label>
                                        <asp:Label ID="lblSTOPONO" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Docket No. :</label>
                                        <asp:Label ID="lblSTODocketNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>No. of Boxes : </label>
                                        <asp:Label ID="lblSTONoOfBoxes" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Transporter : </label>
                                        <asp:Label ID="lblSTOTranCode" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Transporter Name : </label>
                                        <asp:Label ID="lblSTOTranName" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll">
                                        <asp:GridView ID="grvSTO" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                            CssClass="table table-hover table-striped table-bordered nowrap">
                                            <Columns>
                                                <asp:BoundField DataField="SRNO" HeaderText="Sr. No." />
                                                <asp:BoundField DataField="DOCTYPE" HeaderText="Doc. Type" />
                                                <asp:BoundField DataField="DOCNO" HeaderText="Doc. No." />
                                                <asp:BoundField DataField="DOCKDT" HeaderText="Doc. Date" />
                                                <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
                                                <asp:BoundField DataField="UNIT" HeaderText="Unit" />
                                                <asp:BoundField DataField="QTY" HeaderText="Quantity" />
                                                <asp:BoundField DataField="RATE" HeaderText="Rate" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="CAMOUNT" HeaderText="Total Amount" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                                                <%-- HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />--%>
                                                <%--<asp:BoundField DataField="VENDOR" HeaderText="Delivery Date" DataFormatString="{0:MM/dd/yyyy}" />--%>
                                                <asp:BoundField DataField="PLANTCD" HeaderText="From Plant" />
                                                <asp:BoundField DataField="LOCCD" HeaderText="From Location" />

                                                <asp:BoundField DataField="TOPLANTCD" HeaderText="To Plant" />
                                                <asp:BoundField DataField="TOLOCCD" HeaderText="To Location" />

                                                <%--<asp:BoundField DataField="CHLNQTY" HeaderText="Challan Qty" />
                                                <asp:BoundField DataField="ACPTQTY" HeaderText="Accept Qty" />
                                                <asp:BoundField DataField="RTNQTY" HeaderText="Return Qty" />--%>

                                                <asp:BoundField DataField="TRACKNO" HeaderText="Tracking No." />
                                                <asp:BoundField DataField="ITEMTEXT" HeaderText="Item Remark" />
                                                <%--<asp:BoundField DataField="TOPLANT" HeaderText="To Plant" />--%>
                                                <%--<asp:BoundField DataField="TOLOCDESC" HeaderText="To Location" />--%>
                                                <%--<asp:BoundField DataField="PROFITCENTER" HeaderText="Profit Center" />--%>
                                                <%--<asp:BoundField DataField="ASSETCODE" HeaderText="Asset Code" />--%>
                                                <%--<asp:BoundField DataField="REQUISITIONER" HeaderText="Requisitioner" />--%>
                                                <%--<asp:BoundField DataField="TRACKNO" HeaderText="Tracking No." />--%>
                                                <%--<asp:BoundField DataField="ITEMTEXT" HeaderText="Item Text" />--%>
                                                <%--<asp:BoundField DataField="PARTREQNO" HeaderText="Part Req. No." />--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>


                            <%--<div class="col-md-5" style="margin-top: 5px;">
                                <div class="form-group">
                                    <asp:Label ID="lblAPRV1" runat="server" Visible="false"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblAPRV2" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>--%>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-STOIN" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Material Inward (STO)</strong> Details</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Doc Type :</label>
                                        <asp:Label ID="lblSTOINDocType" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Doc. No. :</label>
                                        <asp:Label ID="lblSTOINDocNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Date :</label>
                                        <asp:Label ID="lblSTOINDocDt" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Ref. No. :</label>
                                        <asp:Label ID="lblSTOINRefNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Remark :</label>
                                        <asp:Label ID="lblSTOINRemark" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>PO No. :</label>
                                        <asp:Label ID="lblSTOINPoNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Challan No. :</label>
                                        <asp:Label ID="lblSTOINChallanNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Challan Dt : </label>
                                        <asp:Label ID="lblSTOINChallanDt" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Transporter : </label>
                                        <asp:Label ID="lblSTOINTranspoerter" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Transporter Name : </label>
                                        <asp:Label ID="lblSTOINTranspoerterName" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll">
                                        <asp:GridView ID="grvSTOINList" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                            CssClass="table table-hover table-striped table-bordered nowrap">
                                            <Columns>
                                                <asp:BoundField DataField="SRNO" HeaderText="Sr. No." />
                                                <asp:BoundField DataField="DOCTYPE" HeaderText="Doc. Type" />
                                                <asp:BoundField DataField="DOCNO" HeaderText="Doc. No." />
                                                <asp:BoundField DataField="DOCKDT" HeaderText="Doc. Date" />
                                                <asp:BoundField DataField="PONO" HeaderText="PO NO" />
                                                <asp:BoundField DataField="POSRNO" HeaderText="PO Sr. No." />
                                                <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
                                                <asp:BoundField DataField="UNIT" HeaderText="Unit" />
                                                <asp:BoundField DataField="QTY" HeaderText="Quantity" />
                                                <asp:BoundField DataField="RATE" HeaderText="Rate" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="CAMOUNT" HeaderText="Total Amount" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                                                <asp:BoundField DataField="PLANTCD" HeaderText="Plant" />
                                                <asp:BoundField DataField="LOCCD" HeaderText="Location" />

                                                <%-- <asp:BoundField DataField="TOPLANTCD" HeaderText="To Plant" />
                                                <asp:BoundField DataField="TOLOCCD" HeaderText="To Location" />--%>


                                                <%--<asp:BoundField DataField="TRACKNO" HeaderText="Tracking No." />--%>
                                                <asp:BoundField DataField="ITEMTEXT" HeaderText="Item Remark" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>


                            <%--<div class="col-md-5" style="margin-top: 5px;">
                                <div class="form-group">
                                    <asp:Label ID="lblAPRV1" runat="server" Visible="false"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblAPRV2" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>--%>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-materialreturndetail" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Material Return</strong> Details</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Doc Type :</label>
                                    <asp:Label ID="lblMrDocType" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Doc. No. :</label>
                                    <asp:Label ID="lblMrDocNo" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Date :</label>
                                    <asp:Label ID="lblMrDocDate" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Remark :</label>
                                    <asp:Label ID="lblMrRemark" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Ref. No. :</label>
                                    <asp:Label ID="lblMrRefNo" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>Vendor Name. :</label>
                                    <asp:Label ID="lblVendorName" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll">
                                        <asp:GridView ID="gvMaterialReturn" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                            CssClass="table table-hover table-striped table-bordered nowrap">
                                            <Columns>
                                                <asp:BoundField DataField="SRNO" HeaderText="Sr. No." />
                                                <asp:BoundField DataField="PONO" HeaderText="Po No" />
                                                <asp:BoundField DataField="POSRNO" HeaderText="Po Sr No" />
                                                <asp:BoundField DataField="QTY" HeaderText="Qty" />
                                                <asp:BoundField DataField="CHLNQTY" HeaderText="Chalan Qty" />
                                                <asp:BoundField DataField="ACPTQTY" HeaderText="Accepted Qty" />
                                                <asp:BoundField DataField="MMDOCNO" HeaderText="MM Doc No" />
                                                <asp:BoundField DataField="MMFINYEAR" HeaderText="MM Year" />
                                                <asp:BoundField DataField="MMSRNO" HeaderText="MM Sr No" />
                                                <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                <asp:BoundField DataField="ITEMDESC" HeaderText="Item Description" />
                                                <asp:BoundField DataField="UNIT" HeaderText="UOM" />
                                                <asp:BoundField DataField="FROMPLANT" HeaderText="Plant" />
                                                <asp:BoundField DataField="LOCDESC" HeaderText="Location" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>


                            <%--<div class="col-md-5" style="margin-top: 5px;">
                                <div class="form-group">
                                    <asp:Label ID="lblAPRV1" runat="server" Visible="false"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblAPRV2" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>--%>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-IS" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Material Inward (STO)</strong> Details</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Doc Type :</label>
                                        <asp:Label ID="lblISDocType" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Doc. No. :</label>
                                        <asp:Label ID="lblISDocNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Date :</label>
                                        <asp:Label ID="lblISDocDt" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <label>Ref. No. :</label>
                                        <asp:Label ID="lblISRefNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Remark :</label>
                                        <asp:Label ID="lblISRemark" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll">
                                        <asp:GridView ID="grvISList" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                            CssClass="table table-hover table-striped table-bordered nowrap">
                                            <Columns>
                                                <asp:BoundField DataField="SRNO" HeaderText="Sr. No." />
                                                <asp:BoundField DataField="DOCTYPE" HeaderText="Doc. Type" />
                                                <asp:BoundField DataField="DOCNO" HeaderText="Doc. No." />
                                                <asp:BoundField DataField="DOCKDT" HeaderText="Doc. Date" />
                                                <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />

                                                <asp:BoundField DataField="TOITEMCODE" HeaderText="To Item Code" />
                                                <asp:BoundField DataField="TOITEMDESC" HeaderText="To Item Desc." />

                                                <asp:BoundField DataField="UNIT" HeaderText="Unit" />
                                                <asp:BoundField DataField="QTY" HeaderText="Quantity" />
                                                <asp:BoundField DataField="RATE" HeaderText="Rate" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="CAMOUNT" HeaderText="Total Amount" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />

                                                <asp:BoundField DataField="PLANTCD" HeaderText="Plant" />
                                                <asp:BoundField DataField="LOCCD" HeaderText="Location" />

                                                <asp:BoundField DataField="TOPLANTCD" HeaderText="To Plant" />
                                                <asp:BoundField DataField="TOLOCCD" HeaderText="To Location" />

                                                <asp:BoundField DataField="CRDR" HeaderText="CRDR" />


                                                <%--<asp:BoundField DataField="TRACKNO" HeaderText="Tracking No." />--%>
                                                <asp:BoundField DataField="ITEMTEXT" HeaderText="Item Remark" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>


                            <%--<div class="col-md-5" style="margin-top: 5px;">
                                <div class="form-group">
                                    <asp:Label ID="lblAPRV1" runat="server" Visible="false"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblAPRV2" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>--%>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranMMIST" runat="server" />
    <input type="hidden" id="menuMIid" value="tsmTranMMMatIss" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />
    <input type="hidden" id="menuCMID" value="tsmTranPPDirectCons" runat="server" />
    <input type="hidden" id="menuSTO" value="tsmTranMMSTODc" runat="server" />
    <input type="hidden" id="menuSTOIN" value="tsmTranMMSTOIn" runat="server" />
    <input type="hidden" id="menuIS" value="tsmTranMMMatSpl" runat="server" />

</asp:Content>


