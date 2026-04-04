<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptOpenMR.aspx.cs" Inherits="ShERPa360net.REPORTS.rptOpenMR" EnableEventValidation="false" %>

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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>Open MR</h3>
                        </div>


                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">MR Date : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtMRFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">To : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtMRToDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group TopMarg">
                                            <label class="col-md-5 control-label">MR No. : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtMRNo" runat="server" CssClass="form-control" placeholder="MR No."></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-2">
                                        <div class="form-group TopMarg">
                                            <label class="col-md-7 control-label">Approved MR : </label>
                                            <div class="col-md-5 col-xs-12">
                                                <div class="input-group">

                                                    <asp:CheckBox ID="chkApprovedMR" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-2">
                                        <div class="form-group TopMarg">
                                            <label class="col-md-5 control-label"></label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:LinkButton runat="server" ID="lnkSearchMR" CssClass="btn btn-success pull-left" Text="Search MR" OnClick="lnkSearchMR_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export MR" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>

                                    <%--<div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Job Id : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtJobId" runat="server" CssClass="form-control" MaxLength="10" placeholder="Job Id"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
                                </div>
                                <div class="col-md-12">
                                    <%--<div class="col-md-3">
                                        <div class="form-group TopMarg">
                                            <label class="col-md-5 control-label">Segment : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlSegment" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>




                                    <%--<div class="col-md-3">
                                        <div class="form-group TopMarg">
                                            <label class="col-md-5 control-label">Summery Report : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <asp:CheckBox ID="chkSummeryReport" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>
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
                                                <asp:BoundField DataField="MRTYPE" HeaderText="MR Type" />
                                                <asp:BoundField DataField="MRNO" HeaderText="MR No." />
                                                <asp:BoundField DataField="CREATEDATE" HeaderText="MR Date" />
                                                <asp:BoundField DataField="DEPTNAME" HeaderText="Department Name" />
                                                <asp:BoundField DataField="REMARK" HeaderText="Remark" />
                                                <asp:BoundField DataField="LISTDESC" HeaderText="Status" />
                                                <asp:BoundField DataField="ID" HeaderText="Sr. No." />
                                                <asp:BoundField DataField="ITEMSPEC" HeaderText="Item Spec." />
                                                <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
                                                <asp:BoundField DataField="ITEMQTY" HeaderText="Qty" />
                                                <asp:BoundField DataField="ITEMPLANTCD" HeaderText="Plant Code" />
                                                <asp:BoundField DataField="ITEMLOCCD" HeaderText="Location Code" />
                                                <asp:BoundField DataField="REQUISITIONER" HeaderText="Requisitioner" />
                                                <asp:BoundField DataField="ENTEREDBY" HeaderText="Entered By" />

                                                <%--<asp:TemplateField HeaderText="Action" Visible="True">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="bntView" Text="Details" OnClick="bntView_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
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
                    <h4 class="modal-title" style="color: #337ab7"><strong>MR</strong> Details</h4>
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
                                    <label>MR No. :</label>
                                    <asp:Label ID="lblMRNo" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>MR Date :</label>
                                    <asp:Label ID="lblMRDate" runat="server" CssClass="form-control"></asp:Label>
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
                                                <asp:BoundField DataField="ITEMSPEC" HeaderText="Item Spec." />
                                                <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
                                                <asp:BoundField DataField="ITEMGROUP" HeaderText="Item Group" />
                                                <asp:BoundField DataField="ITEMUOM" HeaderText="UOM" />
                                                <asp:BoundField DataField="ITEMQTY" HeaderText="Qty" />
                                                <asp:BoundField DataField="ITEMRATE" HeaderText="Rate" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="ITEMAMOUNT" HeaderText="Amount" />
                                                <asp:BoundField DataField="COSTCENTER" HeaderText="Cost Center" />
                                                <asp:BoundField DataField="ITEMPLANTCD" HeaderText="Plant Code" />
                                                <asp:BoundField DataField="ITEMLOCCD" HeaderText="Location Code" />
                                                <asp:BoundField DataField="REQUISITIONER" HeaderText="Requisitioner" />
                                                <asp:BoundField DataField="TRACKNO" HeaderText="Tracking No." />
                                                <asp:BoundField DataField="ITEMTEXT" HeaderText="Item Remark" />
                                                <asp:BoundField DataField="STATUS" HeaderText="Status" />
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
    <input type="hidden" id="menutabid" value="tsmRptMMMRList" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptMM" runat="server" />
</asp:Content>
