<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptGRNInvoice.aspx.cs" Inherits="ShERPa360net.REPORTS.rptGRNInvoice" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }

        body .table thead th {
            position: sticky !important;
            top: -1px !important;
            /*background-color: #3D2A70 !important;
            color: white !important;*/
        }

        .header {
            position: sticky !important;
            top: -1px !important;
            /*background-color: #3D2A70 !important;
            color: white !important;*/
        }
    </style>

    <%--<style type="text/css">
        .FixedHeader {
            position: absolute;
            font-weight: bold;
        }
    </style>--%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--<asp:UpdatePanel ID="upd1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="lnkSerch" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="ddlPlantCode" EventName="SelectedIndexChanged" />
            <asp:PostBackTrigger ControlID="gvList" />
            <asp:PostBackTrigger ControlID="lnkExport" />
        </Triggers>
        <ContentTemplate>--%>
            <div class="page-content-wrap">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>GRN Invoice List</h3>
                                </div>
                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Doc Dt : </label>
                                                    <div class="col-md-8 col-xs-12">
                                                        <div class="input-group">
                                                            <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                            <asp:TextBox ID="txtFromDocDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
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
                                                            <asp:TextBox ID="txtToDocDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-2">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Doc No. : </label>
                                                    <div class="col-md-8 col-xs-12">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtDocNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">PO No. : </label>
                                                    <div class="col-md-8 col-xs-12">
                                                        <div class="input-group">
                                                            <asp:TextBox ID="txtPoNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>





                                        </div>

                                        <div class="col-md-12 TopMarg">
                                            <%--<div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">PO No : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtRefDocNo" runat="server" CssClass="form-control" Width="205"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>

                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label">Plant : </label>
                                                    <div class="col-md-8 col-xs-12">
                                                        <div class="input-group">
                                                            <asp:DropDownList ID="ddlPlantCode" runat="server" CssClass="form-control" Width="205" OnSelectedIndexChanged="ddlPlantCode_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <%--<div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Ref No : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtRefNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>--%>


                                            <div class="col-md-3">
                                                <div class="form-group">
                                                    <label class="col-md-4 control-label"></label>
                                                    <div class="col-md-8 col-xs-12">
                                                        <div class="input-group">
                                                            <%--<asp:LinkButton runat="server" ID="lnkNewPO" CssClass="btn btn-success " Text="New PO" PostBackUrl="~/MM/MaterialInwardFromPo.aspx?Mode=I"><i class="fa fa-file-text"></i></asp:LinkButton>--%>
                                                            <asp:LinkButton runat="server" ID="lnkSerch" CssClass="btn btn-success " Text="Search Invoice" OnClick="lnkSerch_Click">

<span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success" Text="Export " OnClick="lnkExport_Click">

<span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
                                                        </div>
                                                    </div>
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
                                                    <HeaderStyle CssClass="header" />
                                                    <Columns>

                                                        <%--<asp:BoundField DataField="DOCTYPE" HeaderText="DOC Type" />--%>
                                                        <asp:BoundField DataField="DOCNO" HeaderText="Doc. No." />
                                                        <asp:BoundField DataField="DOCDATE" HeaderText="Doc Date" DataFormatString="{0:d}" />
                                                        <asp:BoundField DataField="PONO" HeaderText="PO No." />
                                                        <asp:BoundField DataField="PLANTCODE" HeaderText="Plant Code" />
                                                        <asp:BoundField DataField="INVOICESTATUS" HeaderText="INVOICE" />
                                                        <asp:TemplateField HeaderText="Action" Visible="True">
                                                            <ItemTemplate>
                                                                <%--<asp:LinkButton runat="server" ID="bntView" Text="Details" OnClick="bntView_Click"></asp:LinkButton>
                                                        | 
                                                        <asp:LinkButton runat="server" ID="btnPrint" Text="PDF" OnClick="btnPrint_Click"></asp:LinkButton>
                                                        | --%>
                                                                <asp:LinkButton runat="server" ID="btnInv" Text="INVOICE" OnClick="btnInv_Click"></asp:LinkButton>
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

        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranMMMIRPOINV" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />

</asp:Content>
