<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="MRToPBDetail.aspx.cs" Inherits="ShERPa360net.REPORTS.MRToPBDetail" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }

        /*table {
    width: 100%;
}

/*thead, tbody, tr, td, th { display: block; }*/

/*tr:after {
    content: ' ';
    display: block;
    visibility: hidden;
    clear: both;
}*/

/*thead th {
    height: 30px;
}

tbody {
    height: 120px;
}*/

/*tbody td, thead th {
    width: 19.2%;
    float: left;
}*/

 /*.dataTables_scrollHead {
  position: sticky !important;
  top: 119px;
  z-index: 99;
  background-color: white;
  box-shadow: 0px 5px 5px 0px rgba(82, 63, 105, 0.08);
}*/

 body .table thead th {
    position: sticky;
    top: 0px;
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>MR To Purchase Bill</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">From : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDate" Style="width: 146px!important" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
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
                                                    <asp:TextBox ID="txtToDate" Style="width: 146px!important" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3" runat="server" visible="false">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">MR No. : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtMrNo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">PR No. : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtPRNo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group pull-right">
                                            <asp:LinkButton runat="server" Style="margin-left: 2px!important;" ID="lnkSearhMRToPB" CssClass="btn btn-success pull-left" Text="Search MR To PB" OnClick="lnkSearhMRToPB_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" Style="margin-left: 4px!important;" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export MR" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
                                        </div>
                                    </div>

                                </div>

                                <div class="col-md-12" style="margin-top: 5px!important;">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Po No. : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtPoNo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">GRN No. : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtGRNNo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">PB No. : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtPBNo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-12" style="margin-top: 5px!important;">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Plant. : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList style="width:181px!important;" ID="ddlPlantCode" runat="server" CssClass="form-control" ></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">ItemGroup.:</label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList style="width:181px!important;" ID="ddlItemGroup" runat="server" CssClass="form-control"></asp:DropDownList>
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
                                        <asp:GridView ID="gvMrToPurchaseBill" runat="server" CssClass="table table-hover table-striped  table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sr No">
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex + 1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="MRNO" HeaderText="Mr No" />

                                                <asp:TemplateField HeaderText="MR Date">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%# Eval("MRDT").ToString() != "01/01/1900" ? Eval("MRDT").ToString() : "" %>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="MR Apprd Date">
                                                    <ItemTemplate>
                                                        <asp:Label Text='<%# Eval("APRVDATE").ToString() != "01/01/1900" ? Eval("APRVDATE").ToString() : "" %>'
                                                            runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PLANT" HeaderText="Plant" />
                                                <asp:BoundField DataField="LOCATION" HeaderText="Location" />
                                                <asp:BoundField DataField="DEPARTMENT" HeaderText="Department" />
                                                <asp:BoundField DataField="ITEMGROUP" HeaderText="Item Group" />
                                                <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                <asp:BoundField DataField="ITEMDESC" HeaderText="Item" />
                                                <asp:BoundField DataField="MRQTY" HeaderText="Qty" />
                                                <asp:BoundField DataField="ISSUEDQTY" HeaderText="Issue Qty" />
                                                <asp:BoundField DataField="PENDINGQTY" HeaderText="Pending qty" />
                                                <asp:BoundField DataField="LISTSTATUS" HeaderText="MR Status" />
                                                <asp:BoundField DataField="ISSUENO" HeaderText="Issue Number" />
                                                <asp:BoundField DataField="PRNO" HeaderText="PR Number" />
                                                <asp:BoundField DataField="PRDATE" HeaderText="PR DATE" />
                                                <asp:BoundField DataField="PRQTY" HeaderText="PR QTY" />
                                                <asp:BoundField DataField="PONO" HeaderText="PO Number" />
                                                <asp:BoundField DataField="PODT" HeaderText="PO DATE" />
                                                <asp:BoundField DataField="POQTY" HeaderText="PO Qty" />
                                                <asp:BoundField DataField="PORATE" HeaderText="PO Price" />
                                                <asp:BoundField DataField="VENDNAME" HeaderText="Vendor" />
                                                <asp:BoundField DataField="GRNTOTALQTY" HeaderText="PO Rec Qty" />
                                                <asp:BoundField DataField="POPNDQTY" HeaderText="Pending PO qty" />
                                                <asp:BoundField DataField="POSTATUS" HeaderText="PO Status" />
                                                <asp:BoundField DataField="BILLNO" HeaderText="INV Number" />
                                                <asp:BoundField DataField="BILLDT" HeaderText="INV Date" />
                                                <asp:BoundField DataField="GRNNo" HeaderText="GRN Number" />
                                                <asp:BoundField DataField="GRNDATE" HeaderText="GRN DATE" />
                                                <asp:BoundField DataField="GRNQTY" HeaderText="GRN QTY" />
                                                <asp:BoundField DataField="PBNO" HeaderText="Purchase Bill Number" />
                                                <asp:BoundField DataField="PBDT" HeaderText="Purchase Bill Date" />
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
    <input type="hidden" id="menutabid" value="tsmMRToPurchaseBill" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />
</asp:Content>
