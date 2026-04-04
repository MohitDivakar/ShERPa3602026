<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptVendorStatus.aspx.cs" Inherits="ShERPa360net.REPORTS.rptVendorStatus" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Vendor Report</title>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />

    <script>
        $(document).ready(function () {
            BindMakeAssociateModel();
        });
        function BindMakeAssociateModel() {
            if ($("#ContentPlaceHolder1_grVendorList tr").length > 2) {
                $("#ContentPlaceHolder1_grVendorList").DataTable({
                    dom: 'Bfrtip',
                    buttons: [
                        {
                            extend: 'collection',
                            text: 'Export',
                            buttons: [
                                'copy',
                                'excel',
                                'csv',
                                'pdf',
                                'print'
                            ]
                        }
                    ]
                });

            }
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>Vendor Report</h3>
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
                                                    <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                        ErrorMessage="Enter From Date" ValidationGroup="valSearch" Display="Dynamic">Enter From Date</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">To Date : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtTodate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtTodate"
                                                        ErrorMessage="Enter To Date" ValidationGroup="valSearch" Display="Dynamic">Enter To Date</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Vend. Code : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtVendCode" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label"></label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    &nbsp;
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>


                                <div class="col-md-12" style="margin-top: 10px !important;">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Vend. Cat. : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlVendCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Vend. Type : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlVendType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Rejected : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <asp:CheckBox ID="chkRejected" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label"></label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <%--<asp:LinkButton runat="server" ID="lnkSummary" CssClass="btn btn-success pull-right" Text="Report Summary" PostBackUrl="~/REPORTS/rptComplianceReportSummary.aspx"></asp:LinkButton>--%>
                                                    <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-right" Text="Export Report" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="lnkSearhSR" CssClass="btn btn-success pull-right" Text="Search Report" OnClick="lnkSearhSR_Click" ValidationGroup="valSearch">
<span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
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
                                        <asp:GridView ID="grVendorList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="true" OnRowDataBound="grVendorList_RowDataBound">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" OnClick="lnkEdit_Click" Visible="false"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="VENDCODE" HeaderText="Vendor Code" />
                                                <asp:BoundField DataField="VENDTYPE" HeaderText="Vendor Type" />
                                                <asp:BoundField DataField="VENDORCATEGORY" HeaderText="Vendor Cat." />
                                                <asp:BoundField DataField="DEALERNAME" HeaderText="Dealer Name" />
                                                <asp:BoundField DataField="NAME" HeaderText="Vendor Name" />
                                                <asp:BoundField DataField="MOBILENO" HeaderText="Mobile No." />
                                                <asp:BoundField DataField="CONTACTPERSON" HeaderText="Contact Person" />
                                                <asp:BoundField DataField="CONTACTNO" HeaderText="Contact No." />
                                                <asp:BoundField DataField="EMAILID" HeaderText="Email ID" />
                                                <asp:BoundField DataField="ADDR1" HeaderText="Address 1" />
                                                <asp:BoundField DataField="ADDR2" HeaderText="Address 2" />
                                                <asp:BoundField DataField="ADDR3" HeaderText="Address 3" />
                                                <asp:BoundField DataField="CITY" HeaderText="City" />
                                                <asp:BoundField DataField="STATE" HeaderText="State" />
                                                <asp:BoundField DataField="POSTALCODE" HeaderText="Postal Code" />
                                                <asp:BoundField DataField="COUNTRY" HeaderText="Country" />
                                                <asp:BoundField DataField="PANNO" HeaderText="PAN" />
                                                <asp:BoundField DataField="GSTNO" HeaderText="GST No." />
                                                <asp:BoundField DataField="IFSCCODE" HeaderText="IFSC Code" />
                                                <asp:BoundField DataField="ACCOUNTNO" HeaderText="Account No" />
                                                <asp:BoundField DataField="BANKNAME" HeaderText="Bank Name" />
                                                <asp:BoundField DataField="CREATEBY" HeaderText="Create By" />
                                                <asp:BoundField DataField="CREATEDATE" HeaderText="Create Date" />
                                                <asp:BoundField DataField="UPDATEBY" HeaderText="Update By" />
                                                <asp:BoundField DataField="UPDATEDATE" HeaderText="Update Date" />
                                                <asp:BoundField DataField="APRVBY" HeaderText="Aprv By" />
                                                <asp:BoundField DataField="APRVDATE" HeaderText="Aprv. Date" />
                                                <asp:BoundField DataField="REJREASON" HeaderText="Reject Reason" />
                                                <asp:BoundField DataField="STATUS" HeaderText="IsActive" />
                                                <asp:BoundField DataField="MARGINESCHEME" HeaderText="Under Margine Scheme" />
                                                <asp:BoundField DataField="AADHARNO" HeaderText="Aadhar No." />
                                                <%--  <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkEdit" runat="server" CssClass="form-control" Text="Edit" OnClick="lnkEdit_Click" Visible="false"></asp:LinkButton>
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




    <div class="modal fade" id="modal-edit" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Vendor</strong> Data</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">



                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Vendor Code :</label>
                                        <asp:Label ID="lblVendCode" runat="server" CssClass="form-control"></asp:Label>
                                        <asp:HiddenField ID="hflblVendCode" runat="server" />
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Vendor Name :</label>
                                        <asp:Label ID="lblVendName" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Dealer Name :</label>
                                        <asp:Label ID="lblDealerName" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>PAN :</label>
                                        <asp:TextBox ID="txtPAN" runat="server" CssClass="form-control " placeholder="PAN" TabIndex="25" MaxLength="10"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="revPAN" runat="server" ControlToValidate="txtPAN" ValidationGroup="Save9"
                                            Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid PAN Card" ValidationExpression="[A-Z]{5}\d{4}[A-Z]{1}">Invalid PAN Card</asp:RegularExpressionValidator>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>PAN Image :</label>
                                        <asp:FileUpload ID="fuPAN" runat="server" CssClass="form-control" />
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Aadhar No. :</label>
                                        <asp:TextBox ID="txtAadharNo" runat="server" CssClass="form-control" placeholder="Aadhar No" TabIndex="27" MaxLength="12"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator ID="rfvAadhar" Style="color: red;" ControlToValidate="txtAadharNo" runat="server" ValidationGroup="Save9"
                                            ErrorMessage="Please Enter Aadhar No.">Please Enter Aadhar No.</asp:RequiredFieldValidator>--%>
                                        <asp:RegularExpressionValidator ID="revAadhar" runat="server" ControlToValidate="txtAadharNo" ValidationGroup="Save9"
                                            Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid Aadhar No." ValidationExpression="[0-9]{12}$">Invalid Aadhar No.</asp:RegularExpressionValidator>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>ID Proof :</label>
                                        <asp:FileUpload ID="fuIDProof" runat="server" CssClass="form-control" />
                                        <%--<asp:RequiredFieldValidator ID="rfvFUIDProof" Style="color: red;" ControlToValidate="fuIDProof" runat="server" ValidationGroup="Save9"
                                            ErrorMessage="Please Upload ID Proof Image" Display="Dynamic">Please Upload ID Proof Image</asp:RequiredFieldValidator>--%>
                                        <label id="lblIdproofalert" style="display: none!important; font-weight: bold; color: red!important;">Please Select the only JPEG,JPG,TIFF,PNG.</label>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>GST :</label>
                                        <asp:TextBox ID="txtGST" runat="server" CssClass="form-control" placeholder="GST" TabIndex="26" MaxLength="15"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvGST" Style="color: red;" ControlToValidate="txtGST" runat="server" ValidationGroup="Save9" Enabled="false"
                                            ErrorMessage="Please Enter GST No.">Please Enter GST No.</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="rgvGSTNo" runat="server" ControlToValidate="txtGST" ValidationGroup="Save8" Enabled="false"
                                            Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid GST No." ValidationExpression="[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9A-Z]{1}Z[0-9A-Z]{1}$">Invalid GST No.</asp:RegularExpressionValidator>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>GST Certi :</label>
                                        <asp:FileUpload ID="fuGSTCerti" runat="server" CssClass="form-control" />

                                        <asp:RequiredFieldValidator ID="rfvFUGST" Style="color: red;" ControlToValidate="fuGSTCerti" runat="server" ValidationGroup="Save9" Enabled="false"
                                            ErrorMessage="Please Upload GST Certificate Image">Please Upload GST Certificate Image</asp:RequiredFieldValidator>
                                        <label id="lblGStCeralert" style="display: none!important; font-weight: bold; color: red!important;">Please Select the only JPEG,JPG,TIFF,PNG.</label>
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="updMSMSE" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>MSME :</label><br />
                                                <asp:RadioButtonList ID="rblMSME" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow"
                                                    OnSelectedIndexChanged="rblMSME_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem Value="0" Text="NO" Selected="True" class="radio-inline"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="YES" class="radio-inline"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>


                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>MSME Certi :</label>
                                        <asp:FileUpload ID="fuMSMECerti" runat="server" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="rfvMSMECerti" Style="color: red;" ControlToValidate="fuMSMECerti" runat="server" ValidationGroup="Save9" Enabled="false"
                                            ErrorMessage="Please Upload MSME Certificate Image">Please Upload MSME Certificate Image</asp:RequiredFieldValidator>
                                        <label id="lblMSMECertialert" style="display: none!important; font-weight: bold; color: red!important;">Please Select the only JPEG,JPG,TIFF,PNG.</label>
                                    </div>
                                </div>


                            </div>

                            <div class="col-md-12">
                                <asp:UpdatePanel ID="updIfsc" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>IFSC Code :</label>
                                                <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="form-control" placeholder="IFSC Code" TabIndex="37"
                                                    OnTextChanged="txtIFSCCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvIFSCCode" Style="color: red;" ControlToValidate="txtIFSCCode" runat="server" ValidationGroup="Save9"
                                                    ErrorMessage="Please Enter IFSC Code" Enabled="false" Display="Dynamic">Please Enter IFSC Code</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>


                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Bank Name :</label>
                                        <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control " placeholder="Bank Name" TabIndex="35"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvBankName" Style="color: red;" ControlToValidate="txtBankName" runat="server" ValidationGroup="Save9"
                                            ErrorMessage="Please Enter Bank Name" Enabled="false" Display="Dynamic">Please Enter Bank Name</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Account No. :</label>
                                        <asp:TextBox ID="txtACNo" runat="server" CssClass="form-control" placeholder="Account No." TabIndex="36"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvAccountNo" Style="color: red;" ControlToValidate="txtACNo" runat="server" ValidationGroup="Save9"
                                            ErrorMessage="Please Enter Account No" Display="Dynamic" Enabled="false">Please Enter Account No</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Account Type :</label>
                                        <asp:DropDownList ID="ddlAccountType" runat="server" CssClass="form-control" TabIndex="38"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvAccountType" Style="color: red;" ControlToValidate="ddlAccountType" runat="server" ValidationGroup="Save9"
                                            ErrorMessage="Select Account Type" Display="Dynamic" Enabled="false" InitialValue="0">Select Account Type</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Cancelled Cheque / Passbook :</label>
                                        <asp:FileUpload ID="fuCheque" runat="server" CssClass="form-control" />
                                        <asp:RequiredFieldValidator ID="rfvFUCheque" Style="color: red;" ControlToValidate="fuCheque" runat="server" ValidationGroup="Save9"
                                            ErrorMessage="Please Upload Cancelled Cheque / Passbook Image" Enabled="false">Please Upload Cancelled Cheque / Passbook Image</asp:RequiredFieldValidator>
                                        <label id="lblChequealert" style="display: none!important; font-weight: bold; color: red!important;">Please Select the only JPEG,JPG,TIFF,PNG.</label>
                                    </div>
                                </div>
                                <asp:UpdatePanel ID="updWallet" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>UPI / Wallet Type :</label>
                                                <asp:DropDownList ID="ddlUPIWalletType" runat="server" CssClass="form-control" TabIndex="41" OnSelectedIndexChanged="ddlUPIWalletType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvWalletType" Style="color: red;" ControlToValidate="ddlUPIWalletType" runat="server" ValidationGroup="Save9"
                                                    ErrorMessage="Select UPI / Wallet Type" Display="Dynamic" Enabled="false" InitialValue="0">Select UPI / Wallet Type</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>


                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Wallet Pmnt No. :</label>
                                        <asp:TextBox ID="txtWalletPayNo" runat="server" CssClass="form-control " placeholder="Wallet Payment No" TabIndex="42"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvWalletePayNo" Style="color: red;" ControlToValidate="txtWalletPayNo" runat="server" ValidationGroup="Save9"
                                            ErrorMessage="Please Enter Wallet Payment No" Display="Dynamic" Enabled="false">Please Enter Wallet Payment No</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Wallet Owner :</label>
                                        <asp:TextBox ID="txtWalletOwner" runat="server" CssClass="form-control " placeholder="Owner Name" TabIndex="43"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvWalleteOwnerName" Style="color: red;" ControlToValidate="txtWalletOwner" runat="server" ValidationGroup="Save9"
                                            ErrorMessage="Please Enter Wallet Owner Name" Display="Dynamic" Enabled="false">Please Enter Wallet Owner Name</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>



                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Reject By :</label>
                                        <asp:Label ID="lblRejectBy" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Reject Reason :</label>
                                        <asp:Label ID="lblRejectReason" runat="server" CssClass="form-control"></asp:Label>
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
                                                    <asp:Label ID="lblImageType" runat="server" Text='<%# Eval("IMGTYPE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Image" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblImageData" runat="server" Text='<%# Eval("IMAGE") %>' Visible="false"></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Image Type">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgImage" runat="server" Height="50" Width="50" CssClass="zoom" AlternateText='<%# Eval("IMGTYPE") %>' Visible="true" />
                                                    <asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkDownload_Click" Text="Download" Visible="true"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblImageID" runat="server" Text='<%# Eval("IMGID") %>'></asp:Label>
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
                            <div class="col-md-12">
                                <div class="col-md-3">&nbsp;</div>
                                <div class="col-md-3">&nbsp;</div>
                                <div class="col-md-3">&nbsp;</div>
                                <div class="col-md-3">
                                    <asp:Button ID="btnSave" runat="server" Text="Update" OnClick="btnSave_Click" CssClass="btn btn-success pull-right" ValidationGroup="Save9" />
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

    <%--<input type="hidden" id="menutabid" value="tsmMstMMVend" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />--%>

    <input type="hidden" id="menutabid" value="tsmVendRpt" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptDocs" runat="server" />

</asp:Content>
