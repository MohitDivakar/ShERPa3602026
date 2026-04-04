<%@ Page Title="Bulk PO Creation" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="frmBulkPOCreate.aspx.cs" Inherits="ShERPa360net.MM.frmBulkPOCreate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Bulk PO Creation</title>

    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            /*debugger;*/
            document.getElementById("busy-holder1").style.display = "";
            document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";
        }

        function ShowLoading2() {
            /*debugger;*/
            document.getElementById("busy-holder2").style.display = "";
            document.getElementById("ContentPlaceHolder1_btnCreateDoc").style.display = "none";
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Bulk PO Creation</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">From Date : </label>
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
                                            <label class="col-md-4 control-label">Segment : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlSegment" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSegment_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Job ID : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtJobID" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <div class="col-md-12">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Plant : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlPlant" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPlant_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Stage : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlStage" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="col-md-10">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label"></label>
                                                <div class="col-md-8 col-xs-12">
                                                    <asp:LinkButton runat="server" ID="lnkSerch" CssClass="btn btn-success" Text="Search Invoice" OnClick="lnkSerch_Click" ValidationGroup="SaveAll"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
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
                        <div class="row" style="margin-top: 20px !important;">
                            <asp:LinkButton ID="imgSaveAll" OnClientClick="ShowLoading()" runat="server" CssClass="btn btn-success pull-left" OnClick="imgSaveAll_Click" Text="Create PO" ValidationGroup="SaveAll" TabIndex="15" Style="margin-top: 10px !important;"><i class="fa fa-save"></i>  Create PO</asp:LinkButton>
                            <div id="busy-holder1" style="display: none" class="clearfix inline pull-left">
                                <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                <label>Please wait...</label>
                            </div>

                            <div class="col-md-12">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                        <asp:GridView ID="grvData" runat="server" CssClass="table table-hover table-striped table-bordered nowrap" OnRowDataBound="grvData_RowDataBound"
                                            CellSpacing="0" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%">

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

                                                <asp:TemplateField HeaderText="Tax">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlGVTax" runat="server"></asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtGVRate" runat="server"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Job ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJOBID" runat="server" Text='<%# Eval("JOBID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Job Status Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJOBSTATUS" runat="server" Text='<%# Eval("JOBSTATUS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Job Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJOBSTATDESC" runat="server" Text='<%# Eval("JOBSTATDESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Stage ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTAGEID" runat="server" Text='<%# Eval("STAGEID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Segment">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSEGMENT" runat="server" Text='<%# Eval("SEGMENT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Dist. Chnl.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDISTCHNL" runat="server" Text='<%# Eval("DISTCHNL") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Make">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPRODMAKE" runat="server" Text='<%# Eval("PRODMAKE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Model">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPRODMODEL" runat="server" Text='<%# Eval("PRODMODEL") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="IMEI No. 1">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIMEINO" runat="server" Text='<%# Eval("IMEINO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="IMEI No. 2">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIMEINO2" runat="server" Text='<%# Eval("IMEINO2") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Plant">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPLANTCD" runat="server" Text='<%# Eval("PLANTCD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Location">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLOCCD" runat="server" Text='<%# Eval("LOCCD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Item Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblITEMCODE" runat="server" Text='<%# Eval("ITEMCODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Item Desc.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPRODUCT" runat="server" Text='<%# Eval("PRODUCT") %>'></asp:Label>
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


    <div class="modal fade" id="modal-detail" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Purchase</strong> Details</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <%--Sender Detail Start--%>
                            <div class="col-md-12">
                                <%--<div class="col-md-3">
                                            <div class="form-group">
                                                <label>IMEI No. 1 : </label>
                                                <asp:TextBox ID="txtIMEI" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvIMEI" runat="server" ControlToValidate="txtIMEI" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter IMEI No. 1" ValidationGroup="Check">Enter IMEI No 1</asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="revIMEI" runat="server" ControlToValidate="txtIMEI"
                                                    ValidationGroup="Check" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ErrorMessage="IMEI No. must between 15 to 20 digits"
                                                    ValidationExpression="^[0-9A-Za-z]{15,20}$"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>--%>

                                <%--<div class="col-md-3">
                                            <div class="form-group">
                                                <label>IMEI No. 2 : </label>
                                                <asp:TextBox ID="txtIMEI2" runat="server" CssClass="form-control"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="rfvIMEI2" runat="server" ControlToValidate="txtIMEI2"
                                                    ValidationGroup="Check" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ErrorMessage="IMEI No. must between 15 to 20 digits"
                                                    ValidationExpression="^[0-9A-Za-z]{15,20}$"></asp:RegularExpressionValidator>
                                            </div>
                                        </div>--%>
                            </div>

                            <%--<div class="col-md-12">

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
                                    </div>--%>

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
                                        <label>Department : </label>
                                        <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvDept" runat="server" ControlToValidate="ddlDepartment" ValidationGroup="Check" Style="color: red;"
                                            ErrorMessage="Please Select Department" InitialValue="0">Please Select Department</asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <%--<div class="col-md-3">
                                            <div class="form-group">
                                                <label>With GST : </label>
                                                <asp:CheckBox ID="chkGST" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>--%>
                            </div>
                            <%--<div class="col-md-12">

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Bill Date : </label>
                                                <div class="input-group">
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

                                    </div>--%>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Other Charges : </label>
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

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Tax on Otehr Charges : </label>
                                        <asp:DropDownList ID="ddlChargeTax" runat="server" CssClass="form-control"></asp:DropDownList>
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
                                        <%--  <asp:HiddenField ID="hfNewJobID" runat="server" />
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
                                                
                                                
                                                <asp:HiddenField ID="hfGRNNo" runat="server" />
                                                <asp:HiddenField ID="hfPBNo" runat="server" />--%>
                                        <asp:HiddenField ID="hfPONo" runat="server" />
                                        <asp:HiddenField ID="hfPRNo" runat="server" />
                                        <asp:Button ID="btnCreateDoc" runat="server" OnClientClick="ShowLoading2()" CssClass="btn btn-success" Text="Create PO" OnClick="btnCreateDoc_Click" ValidationGroup="Check" />

                                        <div id="busy-holder2" style="display: none" class="clearfix inline pull-left">
                                            <img id="img2" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                            <label>Please wait...</label>
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

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranMMBulkPO" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />

</asp:Content>
