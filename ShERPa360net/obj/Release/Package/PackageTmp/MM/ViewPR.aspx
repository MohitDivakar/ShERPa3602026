<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="ViewPR.aspx.cs" Inherits="ShERPa360net.ViewPR" EnableEventValidation="false" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rpt" %>--%>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:ScriptManager ID="Scrmgr" runat="server">
    </asp:ScriptManager>--%>
    <%--<asp:ToolkitScriptManager ID="ToolKitScriptManager1" runat="server"></asp:ToolkitScriptManager>--%>
    <%--<rpt:ReportViewer ID="ReportViewer1" runat="server" Width="500" SizeToReportContent="true" Visible="false"></rpt:ReportViewer>--%>
    <%--<rpt:ReportViewer ID="rpt1" runat="server"></rpt:ReportViewer>--%>

    <div class="page-content-wrap">

        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>Requisition</h3>
                        </div>


                        <div class="panel-body">

                            <div class="row">

                                <div class="col-md-12">

                                    <div class="col-md-3">
                                        <div class="form-group">



                                            <label class="col-md-3 control-label">PR Date : </label>
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

                                    <div class="col-md-3">
                                        <div class="form-group">



                                            <label class="col-md-3 control-label">PR No. : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtPrno" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>

                                                </div>
                                            </div>



                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkNewPR" CssClass="btn btn-success pull-left" Text="New PR" PostBackUrl="~/MM/CreatePR.aspx"><span tooltip="Add New" flow="down"><i class="fa fa-plus"  aria-hidden="true"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkSearhPR" CssClass="btn btn-success pull-left" Text="Search PR" OnClick="imgFind_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export PR" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
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
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" OnRowDataBound="gvList_RowDataBound">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="PRTYPE" HeaderText="PR Type" />
                                                <asp:BoundField DataField="PRNO" HeaderText="PR No." />
                                                <asp:BoundField DataField="PRDTD" HeaderText="PR Date" />
                                                <asp:BoundField DataField="DEPTNAME" HeaderText="Department Name" />
                                                <asp:BoundField DataField="PLANTCD" HeaderText="Plant Code" />
                                                <asp:BoundField DataField="REMARK" HeaderText="Remark" />
                                                <asp:BoundField DataField="LISTDESC" HeaderText="Status" />
                                                <asp:BoundField DataField="ENTEREDBY" HeaderText="Entered By" />
                                                <asp:BoundField DataField="UPDATEBY" HeaderText="Updated By" />
                                                <asp:TemplateField HeaderText="Action" Visible="True">
                                                    <ItemTemplate>

                                                        <asp:LinkButton runat="server" ID="btnEdit" Text="Edit" OnClick="btnEdit_Click"></asp:LinkButton>
                                                        <asp:Label ID="lblStick" runat="server" Text="|"></asp:Label>
                                                        <asp:LinkButton runat="server" ID="bntView" Text="Details" OnClick="bntView_Click"></asp:LinkButton>
                                                        | 
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






    <%--<div class="modal modal-success fade" id="modal-detail" data-backdrop="static">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="alert alert-success alert-dismissible">
                    <button type="button" class="close" aria-hidden="true" data-dismiss="modal">&times;</button>
                    <h4><i class="icon fa fa-check"></i>Success</h4>
                    <asp:Label ID="lblSuccessMsg" runat="server"></asp:Label>


                    <div class="row">
                        <div class="col-md-6">

                            <div class="col-md-12 margin-top-10">
                                <div class="col-md-6">

                                    <div class="form-group">
                                        <label class="col-md-4 control-label">Doc. Type : </label>
                                        <div class="col-md-8">
                                            <div class="input-group">
                                                <asp:Label ID="lblDoctype" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-6">

                                    <div class="form-group">
                                        <label class="col-md-4 control-label">PR No. : </label>
                                        <div class="col-md-8">
                                            <div class="input-group">
                                                <asp:Label ID="lblPRNo" runat="server" CssClass="form-control"></asp:Label>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="col-md-12 margin-top-10">

                                <div class="col-md-6">

                                    <div class="form-group">
                                        <label class="col-md-4 control-label">PR Date : </label>
                                        <div class="col-md-8">
                                            <asp:Label ID="lblPRDate" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">

                                    <div class="form-group">
                                        <label class="col-md-4 control-label">Remark : </label>
                                        <div class="col-md-8">
                                            <asp:Label ID="lblRemark" runat="server" CssClass="form-control"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="box">
                                <div class="box-body" style="overflow-x: scroll">
                                    <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                        CssClass="table table-hover table-striped table-bordered nowrap">
                                        <Columns>
                                            <asp:BoundField DataField="SRNO" HeaderText="Sr. No." />
                                            <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                            <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
                                            <asp:BoundField DataField="ITEMGRP" HeaderText="Item Group" />
                                            <asp:BoundField DataField="UOMDESC" HeaderText="UOM" />
                                            <asp:BoundField DataField="PRQTY" HeaderText="Qty" />
                                            <asp:BoundField DataField="RATE" HeaderText="Rate" />
                                            <asp:BoundField DataField="CAMOUNT" HeaderText="Amount" />
                                            <asp:BoundField DataField="DELIDT" HeaderText="Delivery Date" />
                                            <asp:BoundField DataField="GLCD" HeaderText="GL Code" />
                                            <asp:BoundField DataField="CSTCENTCD" HeaderText="Cost Center" />
                                            <asp:BoundField DataField="PLANTCD" HeaderText="Plant Code" />
                                            <asp:BoundField DataField="LOCCD" HeaderText="Location Code" />
                                            <asp:BoundField DataField="PRFCNT" HeaderText="Profit Center" />
                                            <asp:BoundField DataField="ASSETCD" HeaderText="Asset Code" />
                                            <asp:BoundField DataField="PRBY" HeaderText="Requisitioner" />
                                            <asp:BoundField DataField="TRNUM" HeaderText="Tracking No." />
                                            <asp:BoundField DataField="ITEMTEXT" HeaderText="Item Text" />
                                            <asp:BoundField DataField="PARTREQNO" HeaderText="Part Req. No." />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                    </div>


                </div>
            </div>
        </div>
    </div>--%>

    <div class="modal fade" id="modal-detail" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>PR</strong> Details</h4>
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
                                    <label>PR No. :</label>
                                    <asp:Label ID="lblPRNo" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>PR Date :</label>
                                    <asp:Label ID="lblPRDate" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Remark :</label>
                                    <asp:Label ID="lblRemark" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll">
                                        <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                            CssClass="table table-hover table-striped table-bordered nowrap">
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="Sr. No." />
                                                <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
                                                <asp:BoundField DataField="ITEMGROUP" HeaderText="Item Group" />
                                                <asp:BoundField DataField="ITEMUOM" HeaderText="UOM" />
                                                <asp:BoundField DataField="ITEMQTY" HeaderText="Qty" />
                                                <asp:BoundField DataField="ITEMRATE" HeaderText="Rate" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="ITEMAMOUNT" HeaderText="Amount" />
                                                <asp:BoundField DataField="DELIVERYDATE" HeaderText="Delivery Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                <asp:BoundField DataField="GLCODE" HeaderText="GL Code" />
                                                <asp:BoundField DataField="COSTCENTER" HeaderText="Cost Center" />
                                                <asp:BoundField DataField="ITEMPLANTCD" HeaderText="Plant Code" />
                                                <asp:BoundField DataField="ITEMLOCCD" HeaderText="Location Code" />
                                                <asp:BoundField DataField="PROFITCENTER" HeaderText="Profit Center" />
                                                <asp:BoundField DataField="ASSETCODE" HeaderText="Asset Code" />
                                                <asp:BoundField DataField="REQUISITIONER" HeaderText="Requisitioner" />
                                                <asp:BoundField DataField="TRACKNO" HeaderText="Tracking No." />
                                                <asp:BoundField DataField="ITEMTEXT" HeaderText="Item Text" />
                                                <asp:BoundField DataField="PARTREQNO" HeaderText="Part Req. No." />
                                                <asp:BoundField DataField="STATUS" HeaderText="Status" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-5" style="margin-top: 5px;">
                                <div class="form-group">
                                    <asp:Label ID="lblAPRV1" runat="server" Visible="false"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblAPRV2" runat="server" Visible="false"></asp:Label>
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
    <input type="hidden" id="menutabid" value="tsmTranMMPR" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />
</asp:Content>
