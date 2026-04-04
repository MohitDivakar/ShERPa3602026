<%@ Page Title="PB Approval" Language="C#" MasterPageFile="~/FI/MasterFI.Master" AutoEventWireup="true" CodeBehind="frmAPRVREJPB.aspx.cs" Inherits="ShERPa360net.FI.frmAPRVREJPB" EnableEventValidation="false" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>PB Approval</title>

    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            debugger;
            document.getElementById("busy-holder1").style.display = "";
            document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <rpt:ReportViewer ID="ReportViewer1" runat="server" SizeToReportContent="true" Visible="false"></rpt:ReportViewer>

    <div class="page-content-wrap">

        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Purchase Bill </strong>Approve</h3>

                            <%--<asp:LinkButton ID="imgCancel" runat="server" CssClass="btn btn-success pull-right" PostBackUrl="~/FI/frmAPRVREJPB.aspx" Text="Cancel" TabIndex="16"><i class="fa fa-times"></i></asp:LinkButton>--%>
                        </div>

                        <div class="panel-body">

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">PB No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtPBNO" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Sent By : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlSentBy" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Rcvd. By : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:DropDownList ID="ddlRcvdBy" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSearh" CssClass="btn btn-success pull-left" Text="Search PO" OnClick="lnkSearh_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export PO Register" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
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
                <div class="form-horizontal">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Purchase Bill List</strong></h3>
                        </div>
                        <asp:LinkButton ID="imgSaveAll" OnClientClick="ShowLoading()" runat="server" CssClass="btn btn-success pull-left" OnClick="imgSaveAll_Click" Text="Approve Selected" ValidationGroup="SaveAll" TabIndex="15" Style="margin-top: 10px !important;"><i class="fa fa-save"></i>  Approve Selected</asp:LinkButton>
                        <div id="busy-holder1" style="display: none" class="clearfix inline pull-left">
                            <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                            <label>Please wait...</label>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <fieldset class="scheduler-border">

                                            <div class="col-md-12" id="divAll" runat="server" visible="true">
                                                <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                    <asp:GridView ID="gvAllList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                        CellSpacing="0" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="true">
                                                        <EmptyDataTemplate>
                                                            No Record Found!
                                                        </EmptyDataTemplate>
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="Select">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkSelectAll" runat="server" OnCheckedChanged="chkSelectAll_CheckedChanged" AutoPostBack="true" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Action" Visible="true">
                                                                <ItemTemplate>
                                                                    <%--<asp:LinkButton runat="server" ID="bntView" Text="Details" OnClick="bntView_Click"></asp:LinkButton>
                                                                        |
                                                                        <asp:LinkButton runat="server" ID="btnPrint" Text="PDF" OnClick="btnPrint_Click"></asp:LinkButton>
                                                                        |--%>
                                                                    <asp:LinkButton runat="server" ID="btnInvoice" Text="INVOICE" OnClick="btnInvoice_Click"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Approve">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnApprove" runat="server" CssClass="form-control" Text="Approve" OnClick="btnApprove_Click" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Reject">
                                                                <ItemTemplate>
                                                                    <asp:Button ID="btnReject" runat="server" CssClass="form-control" Text="Reject" OnClick="btnReject_Click" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Doc Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPBTYPE" runat="server" Text='<%# Eval("PBTYPE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="PB No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPBNO" runat="server" Text='<%# Eval("PBNO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="PB Dt.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPBDT" runat="server" Text='<%# Eval("PBDT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Sent By">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSENTBY" runat="server" Text='<%# Eval("SENTBY") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Sent Dt.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSENDDATE" runat="server" Text='<%# Eval("SENDDATE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Received By">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRCVDBY" runat="server" Text='<%# Eval("RCVDBY") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Received Dt.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRCVDDT" runat="server" Text='<%# Eval("RCVDDT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Vendor Code">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVENDCODE" runat="server" Text='<%# Eval("VENDCODE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Vendor Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVENDNAME" runat="server" Text='<%# Eval("VENDNAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Bill No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBILLNO" runat="server" Text='<%# Eval("BILLNO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Bill Dt.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblBILLDT" runat="server" Text='<%# Eval("BILLDT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="PO No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPONO" runat="server" Text='<%# Eval("PONO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="IMEI No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblIMEINO" runat="server" Text='<%# Eval("IMEINO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Material Value">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNETMATVALUE" runat="server" Text='<%# Eval("NETMATVALUE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Tax Amt.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNETTAXAMT" runat="server" Text='<%# Eval("NETTAXAMT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="PB Amt.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblNETPBAMT" runat="server" Text='<%# Eval("NETPBAMT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Pay Term">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPMTTERMS" runat="server" Text='<%# Eval("PMTTERMS") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Pay Term Desc.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPMTTERMSDESC" runat="server" Text='<%# Eval("PMTTERMSDESC") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSTATUSDESC" runat="server" Text='<%# Eval("STATUSDESC") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Reject Reason">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblREJREASON" runat="server" Text='<%# Eval("REJREASON") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Reject Reason" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEMAILID" runat="server" Text='<%# Eval("EMAILID") %>'></asp:Label>
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


    <div class="modal fade" id="modal-Approve" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7">Reject PB</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:HiddenField ID="hfUpdateID" runat="server" />
                                    <asp:HiddenField ID="hfPBNo" runat="server" />
                                    <asp:HiddenField ID="hfMailID" runat="server" />
                                    <label>Rejest Reason : </label>
                                    <asp:TextBox ID="txtRejectReason" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvRejectReason" runat="server" ControlToValidate="txtRejectReason" ForeColor="Red" Display="Dynamic"
                                        ValidationGroup="VgNameUpdate" ErrorMessage="Enter Reject Reason">Enter Reject Reason</asp:RequiredFieldValidator>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="box-footer text-center">
                        <i class="fa fa-spin fa-refresh fa-lg" id="faLoading2" style="display: none"></i>
                        <p style="margin-bottom: 0px; margin-top: 5px">
                            <asp:Button ID="btnPopReject" runat="server" CssClass="btn btn-success" Text="Submit" OnClick="btnPopReject_Click" UseSubmitBehavior="false"
                                OnClientClick="if (window.Page_ClientValidate('VgNameUpdate') == true) {this.disabled = true; $('#faLoading1').show();}" />
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranFIPBRCVDAC" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranFI" runat="server" />

</asp:Content>
