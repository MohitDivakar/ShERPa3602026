<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="ViewNewItemReq.aspx.cs" Inherits="ShERPa360net.MM.ViewNewItemReq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>New Item Request</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">From Date : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
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
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group TopMarg">
                                            <label class="col-md-5 control-label">Pending Only : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <asp:CheckBox ID="chkPendingOnly" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group TopMarg">
                                            <label class="col-md-5 control-label"></label>
                                            <div class="col-md-7 col-xs-12">
                                                <%--<asp:LinkButton runat="server" ID="lnkNewPR" CssClass="btn btn-success pull-left" Text="New PR" PostBackUrl="~/MM/CreatePartRequest.aspx"><i class="fa fa-file-text"></i></asp:LinkButton>--%>
                                                <asp:LinkButton runat="server" ID="lnkSearhPR" CssClass="btn btn-success pull-left" Text="Search PR" OnClick="lnkSearhPR_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                                <%--<asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export PR" OnClick="lnkExport_Click"><i class="fa fa-file"></i></asp:LinkButton>--%>
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
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" OnRowCommand="gvList_RowCommand">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="Req. No." />
                                                <asp:BoundField DataField="ITEMNAME" HeaderText="Item Name" />
                                                <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
                                                <asp:BoundField DataField="ITEMCAT" HeaderText="Item Category" />
                                                <asp:BoundField DataField="ITEMGRP" HeaderText="Item Group" />
                                                <asp:BoundField DataField="ITEMSUBGRP" HeaderText="ITEM Sub Group" />
                                                <asp:BoundField DataField="STATUSDESC" HeaderText="Status" />
                                                <asp:BoundField DataField="USERNAME" HeaderText="Req. By" />
                                                <asp:BoundField DataField="CREATEDATE" HeaderText="Create Date" />
                                                <asp:BoundField DataField="UPDATEBYNAME" HeaderText="Update By" />
                                                <asp:BoundField DataField="UPDATEDATE" HeaderText="UUpdate Date" />
                                                <asp:TemplateField HeaderText="Action" Visible="True">
                                                    <ItemTemplate>
                                                        <%-- <asp:LinkButton runat="server" ID="btnDetails" Text="Details" OnClick="btnDetails_Click"></asp:LinkButton>
                                                        | --%>
                                                        <asp:LinkButton runat="server" ID="btnApprove" Text="Approve" OnClick="btnApprove_Click"></asp:LinkButton>
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




    <div class="message-box animated fadeIn" data-sound="alert" id="mb-aprv">

        <div class="mb-container">

            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: #ffffff;">
                <span aria-hidden="true">&times;</span></button>
            <h4 class="modal-title" style="color: #faa61a;"><strong>New Item Request</strong> Approval</h4>
            <div class="mb-middle">
                <div class="mb-title" style="color: #faa61a"><span class="fa fa-check"></span>Approve <strong>New Item Request</strong> ?</div>
                <div class="mb-content">
                    <h4 style="color: #ffffff;">New item Request No. : <strong>
                        <asp:Label runat="server" ID="lblPopupPRNO"></asp:Label></strong></h4>
                    <h4 style="color: #ffffff;">Are you sure you want to Approve this New Item Request?</h4>
                </div>
                <div class="mb-footer">
                    <div class="pull-right">
                        <%--<a id="btnApprove" class="btn btn-success btn-lg">Yes</a>
                        <button class="btn btn-default btn-lg mb-control-close">No</button>--%>

                        <asp:LinkButton runat="server" ID="lnkPopReject" CssClass="btn btn-success pull-left" Text="Reject PR" OnClick="lnkPopReject_Click"><i class="fa fa-times-circle"></i> Cancel</asp:LinkButton>
                        <asp:LinkButton runat="server" ID="lnkPopApprove" CssClass="btn btn-success pull-left" Text="Approve PR" OnClick="lnkPopApprove_Click"><i class="fa fa-check-circle"></i> Approve</asp:LinkButton>
                        <asp:Label runat="server" ID="lblRightsMessage" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmTranPPPartReq" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />
</asp:Content>
