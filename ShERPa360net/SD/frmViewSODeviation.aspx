<%@ Page Title="" Language="C#" MasterPageFile="~/SD/SDMaster.Master" AutoEventWireup="true" CodeBehind="frmViewSODeviation.aspx.cs" Inherits="ShERPa360net.SD.frmViewSODeviation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>View SO Deviation</title>
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>SO Deviation</h3>
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

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">SO No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtSONO" runat="server" CssClass="form-control" MaxLength="10" TextMode="Number"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-12" style="margin-top: 10px !important;">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Status : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-right" Text="Export SO Deviation" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkSerchSO" CssClass="btn btn-success pull-right" Text="Search SO Deviation" OnClick="lnkSerchSO_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkNewSODeviation" CssClass="btn btn-success pull-right" Text="New SO Deviation" PostBackUrl="~/SD/frmSODeviation.aspx"><span tooltip="Add New" flow="down"><i class="fa fa-plus"  aria-hidden="true"></i> </span></asp:LinkButton>
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
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <%-- OnRowDataBound="gvList_RowDataBound">--%>
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGVID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="SONO" HeaderText="SO No." />
                                                <asp:BoundField DataField="SRNO" HeaderText="SO Sr. No." />
                                                <%--<asp:BoundField DataField="OLDITEMID" HeaderText="Old Item ID" />--%>
                                                <asp:TemplateField HeaderText="Old Item ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGVOldItemID" runat="server" Text='<%# Eval("OLDITEMID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="OLDITEMCODE" HeaderText="Old Item Code" />
                                                <%--<asp:TemplateField HeaderText="Old Item Desc.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGVOldItemDesc" runat="server" Text='<%# Eval("OLDITEMDESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:BoundField DataField="OLDITEMDESC" HeaderText="Old Item Desc." />
                                                <asp:BoundField DataField="OLDITEMGRADE" HeaderText="Old Item Grade" />
                                                <%--<asp:BoundField DataField="NEWITEMID" HeaderText="New Item ID" />--%>
                                                <asp:TemplateField HeaderText="New Item ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGVNewItemID" runat="server" Text='<%# Eval("NEWITEMID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="NEWITEMCODE" HeaderText="New Item Code" />
                                                <asp:BoundField DataField="NEWITEMDESC" HeaderText="New Item Desc." />
                                                <asp:BoundField DataField="NEWITEMGRADE" HeaderText="New Item Grade" />
                                                <asp:BoundField DataField="REMARKS" HeaderText="Reason to change" />
                                                <asp:BoundField DataField="STATUSDESC" HeaderText="Status" />
                                                <asp:BoundField DataField="ENTRYBY" HeaderText="Entered By" />
                                                <asp:BoundField DataField="ENTRYDATE" HeaderText="Entry Dt." />
                                                <asp:BoundField DataField="REASONTOAPRVCANCEL" HeaderText="Aprv/Rej. Reason" />
                                                <asp:BoundField DataField="APRVBY" HeaderText="Aprv/Rej. By" />
                                                <asp:BoundField DataField="APRVDATE" HeaderText="Aprv/Rej. Dt." />
                                                <%-- <asp:TemplateField HeaderText="Action" Visible="true">
                                                    <ItemTemplate>

                                                        <asp:LinkButton runat="server" ID="bntView" Text="Details" OnClick="bntView_Click"></asp:LinkButton>
                                                        <asp:Label ID="lblLine" runat="server" Text=" | "></asp:Label>
                                                        <asp:LinkButton runat="server" ID="btnEdit" Text="Edit" OnClick="btnEdit_Click"></asp:LinkButton>
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




</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranSDSO" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranSD" runat="server" />

</asp:Content>
