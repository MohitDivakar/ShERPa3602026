<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/MasterCustomerRltionShipMng.Master" AutoEventWireup="true" CodeBehind="frmVendorAprv.aspx.cs" Inherits="ShERPa360net.CRM.frmVendorAprv" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Vendor Approval</title>


    <%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.24/themes/start/jquery-ui.css" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.24/jquery-ui.min.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#dialog").dialog({
                autoOpen: false,
                modal: true,
                height: 600,
                width: 600,
                title: "Zoomed Image"
            });
            $("[id*=gvDetail] img").click(function () {
                debugger;
                $('#dialog').html('');
                $('#dialog').append($(this).clone());
                $('#dialog').dialog('open');
            });
        });
    </script>--%>



    <style type="text/css">
        .margin-top {
            margin-top: 25px;
        }

        .new {
            height: 100px;
            width: 100px;
        }

        .col-md-12 .margin-bottom img {
            margin: 20px;
        }

        .red {
            background: none;
            color: red;
            border: none;
        }

        .zoom:hover {
            margin-top: -50px !important;
            -ms-transform: scale(15); /* IE 9 */
            -webkit-transform: scale(15); /* Safari 3-8 */
            transform: scale(15);
            transform-origin: -1px;
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Vendor Approval</strong></h3>
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
                                                            <asp:BoundField DataField="VENDCODE" HeaderText="Vendor Code" />
                                                            <asp:BoundField DataField="VENDTYPE" HeaderText="Vendor Type" />
                                                            <asp:BoundField DataField="VENDCAT" HeaderText="Vendor Cate." />
                                                            <asp:BoundField DataField="SHOPNAME" HeaderText="Vendor Name" />
                                                            <asp:BoundField DataField="DEALERNAME" HeaderText="Dealer Name" />
                                                            <asp:BoundField DataField="USERNAME" HeaderText="Regi. By" />
                                                            <asp:BoundField DataField="CREATEDATE" HeaderText="Regi. Date" />
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



    <div class="modal fade" id="modal-detail" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Vendor</strong> Details</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Vendor Code :</label>
                                        <asp:Label ID="lblVendCode" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Vendor Type :</label>
                                        <asp:Label ID="lblVendType" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Vendor Cate. :</label>
                                        <asp:Label ID="lblVendorCategory" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Dealer :</label>
                                        <asp:Label ID="lblDealer" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12">


                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Title :</label>
                                        <asp:Label ID="lblTitle" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Vendor Name :</label>
                                        <%--<asp:Label ID="lblVendorName" runat="server" CssClass="form-control"></asp:Label>--%>
                                        <asp:TextBox ID="lblVendorName" runat="server" CssClass="form-control" TextMode="MultiLine" Height="50" Enabled="false"></asp:TextBox>

                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Mobile No. :</label>
                                        <asp:Label ID="lblMobileNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>PAN :</label>
                                        <asp:Label ID="lblPAN" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Contact Perosn :</label>
                                        <asp:Label ID="lblContactPerson" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Contact No. :</label>
                                        <asp:Label ID="lblContactNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Aadhar No. :</label>
                                        <asp:Label ID="lblAadharNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>GSTIN :</label>
                                        <asp:Label ID="lblGSTIN" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Address 1 :</label>
                                        <%--<asp:Label ID="lblAddress1" runat="server" CssClass="form-control"></asp:Label>--%>
                                        <asp:TextBox ID="lblAddress1" runat="server" CssClass="form-control" TextMode="MultiLine" Height="50" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Address 2 :</label>
                                        <%--<asp:Label ID="lblAddress2" runat="server" CssClass="form-control"></asp:Label>--%>
                                        <asp:TextBox ID="lblAddress2" runat="server" CssClass="form-control" TextMode="MultiLine" Height="50" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Address 3 :</label>
                                        <%--<asp:Label ID="lblAddress3" runat="server" CssClass="form-control"></asp:Label>--%>
                                        <asp:TextBox ID="lblAddress3" runat="server" CssClass="form-control" TextMode="MultiLine" Height="50" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>City :</label>
                                        <asp:Label ID="lblCity" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Postal Code :</label>
                                        <asp:Label ID="lblPostalCode" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>State :</label>
                                        <asp:Label ID="lblState" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Country :</label>
                                        <asp:Label ID="lblCountry" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>


                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Bank Name :</label>
                                        <%--<asp:Label ID="lblBankName" runat="server" CssClass="form-control"></asp:Label>--%>
                                        <asp:TextBox ID="lblBankName" runat="server" CssClass="form-control" TextMode="MultiLine" Height="50" Enabled="false"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Account No. :</label>
                                        <asp:Label ID="lblAccountNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>IFSC Code :</label>
                                        <asp:Label ID="lblIFSCCode" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Account Type :</label>
                                        <asp:Label ID="lblAccountType" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>


                            </div>

                            <div class="col-md-12">



                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>UPI / Wallet Detailer :</label>
                                        <asp:Label ID="lblUPIWallet" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Wallet Payment Number :</label>
                                        <asp:Label ID="lblPaymentNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Owner Name of Wallet :</label>
                                        <asp:Label ID="lblOwnerName" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Under Margin Scheme :</label>
                                        <asp:Label ID="lblUnderMargin" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Agreement Received :</label>
                                        <asp:Label ID="lblAgreement" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Mobile selling authorization linked with each Sale :</label>
                                        <asp:Label ID="lblMobileSale" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Regi. By :</label>
                                        <asp:Label ID="lblRegisterBy" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Regi. Date :</label>
                                        <asp:Label ID="lblRegiDate" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>MSME :</label>
                                        <asp:Label ID="lblMSME" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Tally Vendor Created :</label>
                                        <asp:Label ID="lblTallVendor" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                            </div>





                            <div class="col-md-12" style="margin-top: 10px !important;">
                                <div class="col-md-6">
                                    <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                        CssClass="table table-hover table-striped table-bordered nowrap" OnRowDataBound="gvDetail_RowDataBound">
                                        <EmptyDataTemplate>
                                            No Record Found!
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Image Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblImageType" runat="server" Text='<%# Eval("LISTDESC") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Image" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblImageData" runat="server" Text='<%# Eval("IMAGE") %>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Image Type">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgImage" runat="server" Height="50" Width="50" CssClass="zoom" AlternateText='<%# Eval("LISTDESC") %>' Visible="true" />
                                                    <asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkDownload_Click" Text="Download" Visible="true"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblImageID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Extension" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblImageExtension" runat="server" Text='<%# Eval("EXTENSION") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>



                            </div>

                            <div class="col-md-12" style="margin-top: 10px !important;">
                                <div class="col-md-3">
                                    <center>
                                        <div class="form-group">
                                            <asp:TextBox ID="txtApRejDetReason" runat="server" CssClass="form-control" placeholder="Approve / Reject Reason"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtApRejDetReason" ValidationGroup="ValDetRej" ForeColor="Red">Enter Reject Reason</asp:RequiredFieldValidator>
                                            <br />
                                            <asp:LinkButton runat="server" ID="lnkReject" CssClass="btn btn-success pull-left" Text="Reject MR" OnClick="lnkReject_Click" Visible="true" ValidationGroup="ValDetRej"><i class="fa fa-times-circle"></i> Reject</asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkApprove" CssClass="btn btn-success pull-left" Text="Approve MR" OnClick="lnkApprove_Click" Visible="true"><i class="fa fa-check-circle"></i> Approve</asp:LinkButton>

                                        </div>
                                    </center>

                                </div>
                            </div>
                            <div id="dialog" style="display: none"></div>
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
            <h4 class="modal-title" style="color: #faa61a;"><strong>Vendor</strong> Approval</h4>
            <div class="mb-middle">
                <div class="mb-title" style="color: #faa61a"><span class="fa fa-check"></span>Approve <strong>Vendor</strong> ?</div>
                <div class="mb-content">
                    <h4 style="color: #ffffff;">Vendor Code : <strong>
                        <asp:Label runat="server" ID="lblPopupVendorCode"></asp:Label></strong></h4>
                    <h4 style="color: #ffffff;">Vendor Name : <strong>
                        <asp:Label runat="server" ID="lblPopupVendorName"></asp:Label></strong></h4>
                    <h4 style="color: #ffffff;">Are you sure you want to Approve this Vendor?</h4>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmMMVendAprv" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />

</asp:Content>
