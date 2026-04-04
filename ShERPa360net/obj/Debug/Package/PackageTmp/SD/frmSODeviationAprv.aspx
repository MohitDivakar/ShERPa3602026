<%@ Page Title="" Language="C#" MasterPageFile="~/SD/SDMaster.Master" AutoEventWireup="true" CodeBehind="frmSODeviationAprv.aspx.cs" Inherits="ShERPa360net.SD.frmSODeviationAprv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Aprrove SO Deviation</title>
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Approve  </strong>SO Deviation</h3>
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
                                            <%--<asp:LinkButton runat="server" ID="lnkNewSODeviation" CssClass="btn btn-success pull-right" Text="New SO Deviation" PostBackUrl="~/SD/frmSODeviation.aspx"><i class="fa fa-file-text"></i></asp:LinkButton>--%>
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
                                                <%--<asp:BoundField DataField="NEWITEMID" HeaderText="New Item ID" /> 7--%>
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
                                                <asp:TemplateField HeaderText="Action" Visible="True">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="btnDetails" Text="Details" OnClick="btnDetails_Click"></asp:LinkButton>
                                                        | 
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
            <h4 class="modal-title" style="color: #faa61a;"><strong>SO Deviation</strong> Approval</h4>
            <div class="mb-middle">
                <div class="mb-title" style="color: #faa61a"><span class="fa fa-check"></span>Approve <strong>SO Deviation</strong> ?</div>
                <div class="mb-content">
                    <h4 style="color: #ffffff;">SO No. : <strong>
                        <asp:Label runat="server" ID="lblPopupSONO"></asp:Label></strong></h4>
                    <h4 style="color: #ffffff;">SO Sr. No. : <strong>
                        <asp:Label runat="server" ID="lblPopupSOSrNo"></asp:Label></strong></h4>
                    <asp:Label runat="server" ID="lblPopupSOID" Visible="false"></asp:Label>

                    <asp:Label runat="server" ID="lblPopupNewItemID" Visible="false"></asp:Label>
                    <asp:Label runat="server" ID="lblPopupNewItemDesc" Visible="false"></asp:Label>
                    <asp:Label runat="server" ID="lblPopupNewGrade" Visible="false"></asp:Label>
                    <asp:Label runat="server" ID="lblPopupOldItemID" Visible="false"></asp:Label>

                    <h4 style="color: #ffffff;">Reason to Change : <strong>
                        <asp:Label runat="server" ID="lblPopupSOReason"></asp:Label></strong></h4>
                    <h4 style="color: #ffffff;">Are you sure you want to Approve this SO Deviation?</h4>
                </div>

                <div class="mb-footer">
                    <div class="pull-right">
                        <asp:TextBox ID="txtAPREJReason" runat="server" CssClass="form-control" placeholder="Approve / Reject Reason"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvVal" runat="server" ControlToValidate="txtAPREJReason" ValidationGroup="ValRej">Enter Reject Reason</asp:RequiredFieldValidator>
                        <br />
                        <asp:LinkButton runat="server" ID="lnkPopReject" CssClass="btn btn-success pull-left" Text="Reject MR" OnClick="lnkPopReject_Click" ValidationGroup="ValRej"><i class="fa fa-times-circle"></i> Reject</asp:LinkButton>
                        <asp:LinkButton runat="server" ID="lnkPopApprove" CssClass="btn btn-success pull-left" Text="Approve MR" OnClick="lnkPopApprove_Click"><i class="fa fa-check-circle"></i> Approve</asp:LinkButton>
                        <asp:Label runat="server" ID="lblRightsMessage" Visible="false"></asp:Label>
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
                    <h4 class="modal-title" style="color: #337ab7"><strong>SO Deviation</strong> Details</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12" style="margin-top: 10px !important;">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>SO No. :</label>
                                        <asp:Label ID="lblSONO" runat="server" CssClass="form-control"></asp:Label>
                                        <asp:Label ID="lblSOID" runat="server" CssClass="form-control" Visible="false"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>So Sr. No. :</label>
                                        <asp:Label ID="lblSOSRNO" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Old Item Code :</label>
                                        <asp:Label ID="lblOldItemCode" runat="server" CssClass="form-control"></asp:Label>
                                        <asp:Label ID="lblOldItemID" runat="server" CssClass="form-control" Visible="false"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>New Item Code :</label>
                                        <asp:Label ID="txtItemcode" runat="server" CssClass="form-control"></asp:Label>
                                        <asp:Label ID="lblNewItemID" runat="server" CssClass="form-control" Visible="false"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Old Item Desc. :</label>
                                        <asp:TextBox ID="lblOldItemDesc" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>New Item Desc. :</label>
                                        <asp:TextBox ID="lblNewItemDesc" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Old Item Grade :</label>
                                        <asp:Label ID="lblOldItemGrade" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>New Item Grade :</label>
                                        <asp:Label ID="ddlItemGrade" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Reason to Change :</label>
                                        <asp:Label ID="txtReason" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                            </div>


                            <div class="col-md-12" style="margin-top: 10px !important;">
                                <div class="col-md-6">
                                    <center>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtApRejDetReason" runat="server" CssClass="form-control" placeholder="Approve / Reject Reason"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtApRejDetReason" ValidationGroup="ValDetRej" ForeColor="Red">Enter Reject Reason</asp:RequiredFieldValidator>
                                            <br />
                                            <asp:LinkButton runat="server" ID="lnkReject" CssClass="btn btn-success pull-left" Text="Reject SO" OnClick="lnkReject_Click" Visible="true" ValidationGroup="ValDetRej"><i class="fa fa-times-circle"></i> Reject</asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkApprove" CssClass="btn btn-success pull-left" Text="Approve SO" OnClick="lnkApprove_Click" Visible="true"><i class="fa fa-check-circle"></i> Approve</asp:LinkButton>

                                        </div>
                                    </center>

                                </div>
                            </div>
                        </div>
                        <div id="dialog" style="display: none"></div>
                    </div>
                </div>

            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranAprvSDSO" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranSD" runat="server" />

</asp:Content>
