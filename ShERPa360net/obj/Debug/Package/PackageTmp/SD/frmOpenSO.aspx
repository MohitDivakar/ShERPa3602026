<%@ Page Title="" Language="C#" MasterPageFile="~/SD/SDMaster.Master" AutoEventWireup="true" CodeBehind="frmOpenSO.aspx.cs" Inherits="ShERPa360net.SD.frmOpenSO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Pending SO Data</title>
    <%-- <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />--%>
    <style>
        .icon1 {
            text-align: center;
            vertical-align: middle;
            height: 116px;
            width: 116px;
            border-radius: 50%;
            color: #999999;
            margin: 0 auto 20px;
            border: 4px solid #fe970a;
            transition: all 0.2s;
            -webkit-transition: all 0.2s;
        }

        .imgcircle {
            height: 65px !important;
            width: 65px !important;
            margin-top: 20px !important;
        }

        .shadow {
            -moz-box-shadow: 3px 3px 5px 6px #fe970a !important;
            -webkit-box-shadow: 0 0 5px #cccccc87 !important;
            border: 5px solid #cccccc87 !important;
            box-shadow: 0 0 5px #cccccc87 !important;
            background: #fff;
        }
    </style>

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
            top: -14px !important;
            /*background-color: #3D2A70 !important;
            color: white !important;*/
            background-color: #ffffff;
        }
    </style>

    <style type="text/css">
        .chclass {
        }

            .chclass label {
                position: relative;
                top: -2px;
                left: -5px;
            }

            .chclass input[type="checkbox"] {
                margin: 10px 10px 0px;
            }

            .chclass label {
                margin: 10px 10px 0px;
            }
        /*  .chclass input {
                position: absolute;
                top: 10px;
                left: 10px;
            }*/
    </style>

    <script>
        $(document).ready(function () {
            BindMakeAssociateModel();
        });
        function BindMakeAssociateModel() {
            if ($("#ContentPlaceHolder1_gvAllList tr").length > 2) {
                $("#ContentPlaceHolder1_gvAllList").DataTable({
                    paging: false,
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
                //$('#ContentPlaceHolder1_gvAllList_info').hide();
                //$('#ContentPlaceHolder1_gvAllList_paginate').hide();
                //$('#ContentPlaceHolder1_gvAllList_length').hide();
            }
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="pnlPODetails" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="txtVendorCode" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtTranCode" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlPaymentTerms" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="txtBillNo" EventName="TextChanged" />
        </Triggers>
        <ContentTemplate>


            <div class="page-content-wrap">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="panel panel-default">

                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Pending SO Data</strong></h3>
                                </div>

                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="row">
                                                <fieldset class="scheduler-border">

                                                    <div class="col-md-12" id="divAll" runat="server" visible="true">
                                                        <div class="box-body divhorizontal" style="overflow-x: scroll; height: 800px;">
                                                            <asp:GridView ID="gvAllList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                                CellSpacing="0" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="true">
                                                                <EmptyDataTemplate>
                                                                    No Record Found!
                                                                </EmptyDataTemplate>
                                                                <HeaderStyle CssClass="header" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Create JS">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lblCreate" runat="server" Text="Create JS" OnClick="lblCreate_Click"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="AGEING" HeaderText="Ageing" />
                                                                    <asp:TemplateField HeaderText="Plant">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPlant" runat="server" Text='<%# Eval("PLANTCDDESC") %>'></asp:Label>
                                                                        </ItemTemplate>

                                                                    </asp:TemplateField>
                                                                    <%--<asp:BoundField DataField="LOCCD" HeaderText="Location" />--%>
                                                                    <asp:TemplateField HeaderText="Location">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("LOCCD") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <%--<asp:BoundField DataField="SODT" HeaderText="SO Dt." />--%>
                                                                    <asp:TemplateField HeaderText="SO Dt.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSODt" runat="server" Text='<%# Eval("SODT") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <%--<asp:BoundField DataField="DELIDT" HeaderText="Deli. Dt." />--%>
                                                                    <asp:TemplateField HeaderText="Deli. Dt.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblDeliDate" runat="server" Text='<%# Eval("DELIDT") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <%--<asp:BoundField DataField="SONO" HeaderText="SO No." />--%>
                                                                    <asp:TemplateField HeaderText="SO No.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSONO" runat="server" Text='<%# Eval("SONO") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <%--<asp:TemplateField HeaderText="SO No.">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="lnkSONO" Text='<%# Eval("SONO") %>' OnClick="lnkSONO_Click"></asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                                    <asp:BoundField DataField="REFNO" HeaderText="Order ID" />
                                                                    <%--<asp:BoundField DataField="SALESCHANNEL" HeaderText="Sales From" />--%>
                                                                    <asp:TemplateField HeaderText="Sales From">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSALESCHANNEL" runat="server" Text='<%# Eval("SALESCHANNEL") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--<asp:BoundField DataField="COMMAGENT" HeaderText="Agent" />--%>
                                                                    <%--<asp:BoundField DataField="LISTINGID" HeaderText="Listing ID" />--%>
                                                                    <%--<asp:BoundField DataField="LISTINGSTATUS" HeaderText="Listing Status" />--%>
                                                                    <%--<asp:BoundField DataField="JOBID" HeaderText="Job Id" />--%>
                                                                    <%--<asp:BoundField DataField="JOBSTATDESC" HeaderText="Job Status" />--%>
                                                                    <%--<asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />--%>
                                                                    <asp:TemplateField HeaderText="Item Code">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("ITEMCODE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <%--<asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc" />--%>
                                                                    <asp:TemplateField HeaderText="Item Desc">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblItemDesc" runat="server" Text='<%# Eval("ITEMDESC") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <%--<asp:BoundField DataField="SOQTY" HeaderText="SO Qty" />--%>
                                                                    <asp:TemplateField HeaderText="SO Qty">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSOQty" runat="server" Text='<%# Eval("SOQTY") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <%--<asp:BoundField DataField="RATE" HeaderText="Rate" />--%>
                                                                    <asp:TemplateField HeaderText="Rate">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblRate" runat="server" Text='<%# Eval("RATE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <%--<asp:BoundField DataField="CAMOUNT" HeaderText="Total Amount" />--%>
                                                                    <asp:TemplateField HeaderText="Total Amount">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblCAmount" runat="server" Text='<%# Eval("CAMOUNT") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Make">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMake" runat="server" Text='<%# Eval("MAKE") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Model">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblModel" runat="server" Text='<%# Eval("MODEL") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Lock Amt.">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLOCKAMT" runat="server" Text='<%# Eval("LOCKAMT") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>

                                                                    <%--<asp:BoundField DataField="STATUSDESC" HeaderText="SO Status" />--%>
                                                                    <asp:BoundField DataField="ENTRYBY" HeaderText="Create By" />
                                                                    <asp:BoundField DataField="CREATEDATE" HeaderText="Create Date" />

                                                                    <asp:TemplateField HeaderText="Plant Code" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPlantCode" runat="server" Text='<%# Eval("PLANTCD") %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Loc. Code" Visible="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLocCode" runat="server" Text='<%# Eval("LOCCD") %>'></asp:Label>
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
                            <h4 class="modal-title" style="color: #337ab7"><strong>JS</strong> Details</h4>
                        </div>
                        <div class="modal-body">
                            <div class="box-body">
                                <div class="row">
                                    <%--Sender Detail Start--%>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>IMEI No. 1 : </label>
                                                <asp:TextBox ID="txtIMEI" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvIMEI" runat="server" ControlToValidate="txtIMEI" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter IMEI No. 1" ValidationGroup="Check">Enter IMEI No 1</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revIMEI" runat="server" ControlToValidate="txtIMEI"
                                                    ValidationGroup="Check" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ErrorMessage="IMEI No. must between 15 to 20 digits"
                                                    ValidationExpression="^[0-9A-Za-z]{15,20}$"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>IMEI No. 2 : </label>
                                                <asp:TextBox ID="txtIMEI2" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="rfvIMEI2" runat="server" ControlToValidate="txtIMEI2"
                                                    ValidationGroup="Check" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ErrorMessage="IMEI No. must between 15 to 20 digits"
                                                    ValidationExpression="^[0-9A-Za-z]{15,20}$"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Reverse Courier : </label>
                                                <asp:TextBox ID="txtReveCourier" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvRevCourier" runat="server" ControlToValidate="txtReveCourier" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Reverse Courier Name" ValidationGroup="Check">Enter Reverse Courier Name</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Reverse Waybill No. : </label>
                                                <asp:TextBox ID="txtRevWaybill" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvRevWaybill" runat="server" ControlToValidate="txtRevWaybill" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Reverse Waybill Number" ValidationGroup="Check">Enter Reverse Waybill Number</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Vendor Code : </label>
                                                <asp:TextBox ID="txtVendorCode" runat="server" CssClass="form-control" OnTextChanged="txtVendorCode_TextChanged" MaxLength="10" AutoPostBack="true"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvVendorCode" runat="server" ControlToValidate="txtVendorCode" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Vendor Code" ValidationGroup="Check">Enter Vendor Code</asp:RequiredFieldValidator>
                                                <asp:Label ID="lblVendorError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Vendor Name : </label>
                                                <asp:TextBox ID="txtVendorName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvVrndorName" runat="server" ControlToValidate="txtVendorName" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Vendor Name" ValidationGroup="Check">Enter Vendor Name</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Transaporter Code : </label>
                                                <asp:TextBox ID="txtTranCode" runat="server" CssClass="form-control" OnTextChanged="txtTranCode_TextChanged" MaxLength="10" AutoPostBack="true"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvTranCode" runat="server" ControlToValidate="txtTranCode" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Transporter Code" ValidationGroup="Check">Enter Transporter Code</asp:RequiredFieldValidator>
                                                <asp:Label ID="lblTransporterError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Transaporter Name : </label>
                                                <asp:TextBox ID="txtTranName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvTranName" runat="server" ControlToValidate="txtTranName" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Transporter Name" ValidationGroup="Check">Enter Transporter Name</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Payment Terms : </label>
                                                <asp:DropDownList ID="ddlPaymentTerms" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPaymentTerms_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvPaymentTerms" runat="server" ControlToValidate="ddlPaymentTerms" ValidationGroup="Check" Style="color: red;"
                                                    ErrorMessage="Please Select Payment Terms" InitialValue="0">Please Select Payment Terms</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Payment Terms Desc. : </label>
                                                <asp:TextBox ID="txtPaymentTermsDesc" runat="server" CssClass="form-control" placeholder="Payment Terms Desc." ReadOnly="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvPaymentTermsDesc" runat="server" ControlToValidate="txtPaymentTermsDesc" ValidationGroup="Check" Style="color: red;"
                                                    ErrorMessage="Payment Terms Desc.">Payment Terms Desc.</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Pur. Type : </label>
                                                <asp:DropDownList ID="ddlPurType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvPurType" runat="server" ControlToValidate="ddlPurType" ValidationGroup="Check" Style="color: red;"
                                                    ErrorMessage="Please Select Purchase Type" InitialValue="0">Please Select Purchase Type</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>With GST : </label>
                                                <asp:CheckBox ID="chkGST" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Bill Date : </label>
                                                <div class="input-group">
                                                    <%--<span class="input-group-addon"><span class="fa fa-calendar"></span></span>--%>
                                                    <asp:TextBox ID="txtBillDate" runat="server" placeholder="SO Date" class="form-control datepicker" Enabled="true" TextMode="Date" MaxLength="10"></asp:TextBox>
                                                </div>
                                                <asp:RequiredFieldValidator ID="rfvBilldate" runat="server" ControlToValidate="txtBillDate" ValidationGroup="Check" Style="color: red;"
                                                    Display="Dynamic" ErrorMessage="Please Enter Bill Date">Please Enter Bill Date</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Bill No. : </label>
                                                <asp:TextBox ID="txtBillNo" runat="server" CssClass="form-control" placeholder="Bill No." ReadOnly="false" AutoPostBack="true" OnTextChanged="txtBillNo_TextChanged"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvBillNo" runat="server" ControlToValidate="txtBillNo" ValidationGroup="Check" Style="color: red;"
                                                    ErrorMessage="Please Enter Bill No.">Please Enter Bill No.</asp:RequiredFieldValidator><br />
                                                <asp:Label ID="BillError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Bill No. is same as PO No </label>
                                                <asp:CheckBox ID="chkINVPO" runat="server" Checked="true" CssClass="form-control" OnCheckedChanged="chkINVPO_CheckedChanged" />

                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Charges : </label>
                                                <asp:DropDownList ID="ddlCharges" runat="server" CssClass="form-control"></asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Amount : </label>
                                                <asp:TextBox ID="txtChgAmt" runat="server" CssClass="form-control" placeholder="Charges Amount"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="rfvChargeAmount" runat="server" ControlToValidate="txtChgAmt" Style="color: red;"
                                                        ErrorMessage="Enter Charges Amount" ValidationGroup="SaveCharge">Enter Charges Amount</asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <br />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <br />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <br />
                                            </div>
                                        </div>



                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:HiddenField ID="hfNewJobID" runat="server" />
                                                <asp:HiddenField ID="hfJCNO" runat="server" />
                                                <asp:HiddenField ID="hfEstiNo" runat="server" />
                                                <asp:HiddenField ID="hfOldJobID" runat="server" />
                                                <asp:HiddenField ID="hfMake" runat="server" />
                                                <asp:HiddenField ID="hfModel" runat="server" />
                                                <asp:HiddenField ID="hfColor" runat="server" />
                                                <asp:HiddenField ID="hfItemCode" runat="server" />
                                                <asp:HiddenField ID="hfItemDesc" runat="server" />
                                                <asp:HiddenField ID="hfProdItemID" runat="server" />
                                                <asp:HiddenField ID="hfProdItemDesc" runat="server" />
                                                <asp:HiddenField ID="hfSOQty" runat="server" />
                                                <asp:HiddenField ID="hfPlantCd" runat="server" />
                                                <asp:HiddenField ID="hfLocCd" runat="server" />
                                                <asp:HiddenField ID="hfSONO" runat="server" />
                                                <asp:HiddenField ID="hfSalesFrom" runat="server" />
                                                <asp:HiddenField ID="hfRAM" runat="server" />
                                                <asp:HiddenField ID="hfROM" runat="server" />
                                                <asp:HiddenField ID="hfPrice" runat="server" />
                                                <asp:HiddenField ID="hfLockAmt" runat="server" />
                                                <asp:HiddenField ID="hfPRNo" runat="server" />
                                                <asp:HiddenField ID="hfPONo" runat="server" />
                                                <asp:HiddenField ID="hfGRNNo" runat="server" />
                                                <asp:HiddenField ID="hfPBNo" runat="server" />
                                                <asp:Button ID="btnCreateDoc" runat="server" CssClass="btn btn-success" Text="Create JS" OnClick="btnCreateDoc_Click" ValidationGroup="Check" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="modal-Specs" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" style="color: #337ab7"><strong>Job</strong> Specification</h4>
                        </div>
                        <div class="modal-body">
                            <div class="box-body">
                                <div class="row">

                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Job ID : </label>
                                                <asp:TextBox ID="txtJobID" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvJobid" runat="server" ControlToValidate="txtJobID" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Job ID" ValidationGroup="CheckSpec">Enter Job ID</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Pur. Grade : </label>
                                                <asp:DropDownList ID="ddlPurGrade" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvPurGrade" runat="server" ControlToValidate="ddlPurGrade" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Select Pur. Grade" ValidationGroup="CheckSpec" InitialValue="0">Select Pur. Grade</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Make : </label>
                                                <asp:TextBox ID="txtMake" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvmake" runat="server" ControlToValidate="txtMake" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Product Make" ValidationGroup="CheckSpec">Enter Product Make</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Model : </label>
                                                <asp:TextBox ID="txtModel" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvModel" runat="server" ControlToValidate="txtModel" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Product Model" ValidationGroup="CheckSpec">Enter Product Model</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>RAM : </label>
                                                <asp:DropDownList ID="ddlRAM" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvRAM" runat="server" ControlToValidate="ddlRAM" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Select RAM" ValidationGroup="CheckSpec" InitialValue="0">Select RAM</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>ROM : </label>
                                                <asp:DropDownList ID="ddlROM" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvROM" runat="server" ControlToValidate="ddlROM" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Select ROM" ValidationGroup="CheckSpec" InitialValue="0">Select ROM</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Color : </label>
                                                <asp:DropDownList ID="ddlColor" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvColor" runat="server" ControlToValidate="ddlColor" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Select Color" ValidationGroup="CheckSpec" InitialValue="0">Select Color</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Grade : </label>
                                                <asp:DropDownList ID="ddlGrade" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvGrade" runat="server" ControlToValidate="ddlGrade" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Select Grade" ValidationGroup="CheckSpec" InitialValue="0">Select Grades</asp:RequiredFieldValidator>
                                            </div>
                                        </div>



                                    </div>

                                    <div class="col-md-12">

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Disp. Size :</label>
                                                <asp:DropDownList ID="ddlDispSize" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvDispSize" runat="server" ControlToValidate="ddlDispSize" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Select Disp. Size" ValidationGroup="CheckSpec" InitialValue="0">Select Disp. Size</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Front Camera :</label>
                                                <asp:DropDownList ID="ddlFrontCam" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvFrontCam" runat="server" ControlToValidate="ddlFrontCam" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Select Front Camera" ValidationGroup="CheckSpec" InitialValue="0">Select Front Camera</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Back Camera :</label>
                                                <asp:DropDownList ID="ddlBackCam" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvBackCam" runat="server" ControlToValidate="ddlBackCam" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Select Back Camera" ValidationGroup="CheckSpec">Enter Back Camera</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>LCD Color : </label>
                                                <asp:DropDownList ID="ddlLCDColor" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvLCDColor" runat="server" ControlToValidate="ddlLCDColor" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Select LCD Color" ValidationGroup="CheckSpec" InitialValue="0">Select LCD Color</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>MRP : </label>
                                                <asp:TextBox ID="txtMRP" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvMRP" runat="server" ControlToValidate="txtMRP" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter MRP" ValidationGroup="CheckSpec">Enter MRP</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>IMEI : </label>
                                                <asp:TextBox ID="txtSpecIMEI" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvIMEIE" runat="server" ControlToValidate="txtSpecIMEI" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter IMEI" ValidationGroup="CheckSpec">Enter IMEI</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>VoltE 4G : </label>
                                                <asp:CheckBox ID="chkVolte" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Cable Type : </label>
                                                <asp:DropDownList ID="ddlCableType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvCabletype" runat="server" ControlToValidate="ddlCableType" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Select Cable Type" ValidationGroup="CheckSpec" InitialValue="0">Select Cable Type</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>JDA Ref. Dt. : </label>
                                                <asp:TextBox ID="txtJDARefDt" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvJDARefDt" runat="server" ControlToValidate="txtJDARefDt" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter JDA Ref. Dt." ValidationGroup="CheckSpec">Enter JDA Ref. Dt.</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Serial No. : </label>
                                                <asp:TextBox ID="txtSerialNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvSerialNo" runat="server" ControlToValidate="txtSerialNo" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Serial No." ValidationGroup="CheckSpec">Enter Serial No.</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>SKU : </label>
                                                <asp:TextBox ID="txtSKU" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvSKU" runat="server" ControlToValidate="txtSKU" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter SKU" ValidationGroup="CheckSpec">Enter SKU</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Item Code : </label>
                                                <asp:TextBox ID="txtSpeItemCode" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvItemCode" runat="server" ControlToValidate="txtSpeItemCode" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Item Code" ValidationGroup="CheckSpec">Enter Item Code</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <br />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <br />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <br />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <%--<asp:HiddenField ID="HiddenField1" runat="server" />
                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                        <asp:HiddenField ID="HiddenField3" runat="server" />
                                        <asp:HiddenField ID="HiddenField4" runat="server" />
                                        <asp:HiddenField ID="HiddenField5" runat="server" />
                                        <asp:HiddenField ID="HiddenField6" runat="server" />
                                        <asp:HiddenField ID="HiddenField7" runat="server" />
                                        <asp:HiddenField ID="HiddenField8" runat="server" />
                                        <asp:HiddenField ID="HiddenField9" runat="server" />
                                        <asp:HiddenField ID="HiddenField10" runat="server" />
                                        <asp:HiddenField ID="HiddenField11" runat="server" />
                                        <asp:HiddenField ID="HiddenField12" runat="server" />
                                        <asp:HiddenField ID="HiddenField13" runat="server" />
                                        <asp:HiddenField ID="HiddenField14" runat="server" />
                                        <asp:HiddenField ID="HiddenField15" runat="server" />
                                        <asp:HiddenField ID="HiddenField16" runat="server" />--%>
                                                <asp:Button ID="btnSpecs" runat="server" CssClass="btn btn-success" Text="Create Job Specs" OnClick="btnSpecs_Click" ValidationGroup="CheckSpec" />
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </div>

                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="modal-Qty" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" style="color: #337ab7">Warning Message</h4>
                        </div>


                        <div class="modal-body">
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label ID="lblQtyError" runat="server"></asp:Label>
                                    </div>

                                    <div class="col-md-12" style="margin-top: 10px !important;">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Button ID="btnQtyYes" runat="server" OnClick="btnQtyYes_Click" CssClass="btn btn-success pull-right" Text="YES" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Button ID="btnQtyNo" runat="server" OnClick="btnQtyNo_Click" CssClass="btn btn-danger pull-left" Text="NO" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranSDSO" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranSD" runat="server" />

</asp:Content>
