<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptMRStatus.aspx.cs" Inherits="ShERPa360net.REPORTS.rptMRStatus" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">

        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>Material Requisition</h3>
                        </div>


                        <div class="panel-body">

                            <div class="row">

                                <div class="col-md-12">

                                    <div class="col-md-3">
                                        <div class="form-group">



                                            <label class="col-md-3 control-label">MR Date : </label>
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



                                            <label class="col-md-3 control-label">MR No. : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtMRno" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>

                                                </div>
                                            </div>



                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSearhMR" CssClass="btn btn-success pull-left" Text="Search MR" OnClick="lnkSearhMR_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export MR" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
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
                                        <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                            CssClass="table table-hover table-striped table-bordered nowrap">
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="Sr. No." />
                                                <asp:BoundField DataField="MRNO" HeaderText="MR No." />
                                                <asp:BoundField DataField="CREATEDATE" HeaderText="MR Date" />
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
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnView" runat="server" CssClass="form-control" Text="View" OnClick="btnView_Click" BackColor="#faa61a" ForeColor="White" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="PRISSUENO" HeaderText="PR / PO / Issue No." />
                                                <asp:BoundField DataField="ISSUEQTY" HeaderText="Issue Qty" />
                                                <asp:BoundField DataField="PRNO" HeaderText="PR No." />
                                                <asp:BoundField DataField="PONO" HeaderText="PO No." />
                                                <asp:BoundField DataField="ISSUENO" HeaderText="Issue No." />
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


    <div class="modal fade" id="modal-Aprvdetail" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>MR Approver</strong> Details</h4>
                </div>



                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>MR No. : </label>
                                    <asp:Label ID="lblAprvMRNO" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>MR Date : </label>
                                    <asp:Label ID="lblAprvMRDate" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>MR Create By :</label>
                                    <asp:Label ID="lblAprvCreateBy" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Approval Req. From :</label>
                                    <asp:Label ID="lblAprvReqBy" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-Aprveddetail" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>MR Approver</strong> Details</h4>
                </div>



                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>MR No. : </label>
                                    <asp:Label ID="lblAprvedMRno" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>MR Date : </label>
                                    <asp:Label ID="lblAprvedMRdate" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>MR Create By :</label>
                                    <asp:Label ID="lblAprvedCreateBy" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Approved By :</label>
                                    <asp:Label ID="lblAprvedBY" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Approved Date :</label>
                                    <asp:Label ID="lblAprvedByDate" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-Rjcktddetail" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>MR Approver</strong> Details</h4>
                </div>



                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>MR No. : </label>
                                    <asp:Label ID="lblRjcktdMRno" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>MR Date : </label>
                                    <asp:Label ID="lblRjcktdMRdate" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>MR Create By :</label>
                                    <asp:Label ID="lblRjcktdCreateBy" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Rejected By :</label>
                                    <asp:Label ID="lblRjcktdBY" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Rejected Date :</label>
                                    <asp:Label ID="lblRjcktdByDate" runat="server" CssClass="form-control"></asp:Label>
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

