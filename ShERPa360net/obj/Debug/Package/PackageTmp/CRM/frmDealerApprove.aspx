<%@ Page Title="Dealer Approval" Language="C#" MasterPageFile="~/CRM/MasterCustomerRltionShipMng.Master" AutoEventWireup="true" CodeBehind="frmDealerApprove.aspx.cs" Inherits="ShERPa360net.CRM.frmDealerApprove" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Dealer Approval</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Dealer Approval</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <fieldset class="scheduler-border">

                                            <div class="col-md-12" id="divName" runat="server" visible="true">
                                                <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                    <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                        CellSpacing="0" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="true">
                                                        <EmptyDataTemplate>
                                                            No Record Found!
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <asp:BoundField DataField="CUSTCODE" HeaderText="Dealer Code" />
                                                            <asp:BoundField DataField="NAME1" HeaderText="Dealer Name" />
                                                            <asp:BoundField DataField="PANNO" HeaderText="PAN No." />
                                                            <asp:BoundField DataField="AADHARNO" HeaderText="Aadhar No." />
                                                            <asp:BoundField DataField="GSTNO" HeaderText="GST No." />
                                                            <asp:BoundField DataField="MOBILENO" HeaderText="Mobile No." />
                                                            <asp:BoundField DataField="EMAILID" HeaderText="Email ID" />
                                                            <asp:BoundField DataField="ADDR1" HeaderText="Address 1" />
                                                            <asp:BoundField DataField="ADDR2" HeaderText="Address 2" />
                                                            <asp:BoundField DataField="ADDR3" HeaderText="Address 3" />
                                                            <asp:BoundField DataField="CITY" HeaderText="City" />
                                                            <asp:BoundField DataField="POSTALCODE" HeaderText="Pincode" />
                                                            <asp:BoundField DataField="STATE" HeaderText="State" />
                                                            <asp:TemplateField HeaderText="Action" Visible="True">
                                                                <ItemTemplate>
                                                                    <%--  <asp:LinkButton runat="server" ID="btnDetails" Text="Details" OnClick="btnDetails_Click"></asp:LinkButton>
                                                                    | --%>
                                                                    <asp:LinkButton runat="server" ID="btnApprove" Text="Approve" OnClick="btnApprove_Click"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </fieldset>
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
            <h4 class="modal-title" style="color: #faa61a;"><strong>Dealer</strong> Approval</h4>
            <div class="mb-middle">
                <div class="mb-title" style="color: #faa61a"><span class="fa fa-check"></span>Approve <strong>Dealer</strong> ?</div>
                <div class="mb-content">
                    <h4 style="color: #ffffff;">Dealer Code : <strong>
                        <asp:Label runat="server" ID="lblPopupVendorCode"></asp:Label></strong></h4>
                    <h4 style="color: #ffffff;">Dealer Name : <strong>
                        <asp:Label runat="server" ID="lblPopupVendorName"></asp:Label></strong></h4>
                    <h4 style="color: #ffffff;">Are you sure you want to Approve this Dealer?</h4>
                </div>

                <div class="mb-footer">
                    <div class="pull-right">
                        <asp:TextBox ID="txtAPREJReason" runat="server" CssClass="form-control" placeholder="Approve / Reject Reason" Visible="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvVal" runat="server" ControlToValidate="txtAPREJReason" ValidationGroup="ValRej">Enter Reject Reason</asp:RequiredFieldValidator>
                        <br />
                        <%--<asp:LinkButton runat="server" ID="lnkPopReject" CssClass="btn btn-success pull-left" Text="Reject MR" OnClick="lnkPopReject_Click" ValidationGroup="ValRej"><i class="fa fa-times-circle"></i> Reject</asp:LinkButton>--%>
                        <asp:LinkButton runat="server" ID="lnkPopApprove" CssClass="btn btn-success pull-left" Text="Approve MR" OnClick="lnkPopApprove_Click"><i class="fa fa-check-circle"></i> Approve</asp:LinkButton>
                        <asp:Label runat="server" ID="lblRightsMessage" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmMMCustAprv" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />

</asp:Content>
