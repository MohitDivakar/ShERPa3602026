<%@ Page Title="" Language="C#" MasterPageFile="~/FI/MasterFI.Master" AutoEventWireup="true" CodeBehind="frmBankPayment.aspx.cs" Inherits="ShERPa360net.FI.frmBankPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Bank Payment</title>
    <%--<script src="../js/jquery-1.4.1.min.js"></script>
    <script src="../js/ScrollableGridPlugin.js"></script>--%>

    <%--    <script type="text/javascript">
        $(document).ready(function () {
            $('#<%=gvPBAdjData.ClientID %>').Scrollable({
                ScrollHeight: 300,
                Width: 467
            });
        });
    </script>--%>


    <style type="text/css">
        ul, li, body {
            margin: 0;
            padding: 0;
        }
        /* MultiView Tab Using Menu Control */
        .tabs {
            position: relative;
            top: 1px;
            z-index: 2;
        }

        #ContentPlaceHolder1_Menu1 a.static {
            text-decoration: none;
            border-style: solid !important;
            padding-left: 0em !important;
            padding-right: 0em !important;
        }

        .tab {
            border: 4px solid white;
            background-image: url(images/navigation.jpg);
            background-repeat: repeat-x;
            color: black;
            background-color: aqua !important;
            padding: 3px 10px;
        }

        .selectedtab {
            background: none;
            background-color: blue;
            background-repeat: repeat-x;
            color: black;
        }

        .tabcontents {
            border: 1px solid black;
            padding: 10px;
            width: 600px;
            height: 500px;
            background-color: white;
        }
    </style>

    <style type="text/css">
        body .table tbody tr th {
            position: sticky;
            top: 0px;
        }
    </style>

    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            //debugger;
            //var SO = $("#ContentPlaceHolder1_txtSONO").val();
            //var CUST = $("#ContentPlaceHolder1_txtCustomer").val();
            //var SHIP = $("#ContentPlaceHolder1_txtShipment").val();
            //var CUST = $("#ContentPlaceHolder1_txtSODATE").val();
            //var refno = $("#ContentPlaceHolder1_txtRefNo").val();
            //var refdat = $("#ContentPlaceHolder1_txtRefDate").val();
            //var comm = $("#ContentPlaceHolder1_ddlCommAgent").val();
            //var custn = $("#ContentPlaceHolder1_txtCustomerName").val();
            //var shipn = $("#ContentPlaceHolder1_txtShipmentName").val();
            //var pay = $("#ContentPlaceHolder1_ddlPaymentTerms").val();
            //var scheme = $("#ContentPlaceHolder1_ddlSalesScheme").val();
            //var remarks = $("#ContentPlaceHolder1_txtRemarks").val();
            //var salesch = $("#ContentPlaceHolder1_ddlSalesChannel").val();
            //var custna = $("#ContentPlaceHolder1_txtCustName").val();
            //var addr1 = $("#ContentPlaceHolder1_txtAddress1").val();
            //var addr2 = $("#ContentPlaceHolder1_txtAddress2").val();
            //var addr3 = $("#ContentPlaceHolder1_txtAddress3").val();
            //var pin = $("#ContentPlaceHolder1_txtPincode").val();
            //var state = $("#ContentPlaceHolder1_ddlState").val();
            //var city = $("#ContentPlaceHolder1_ddlCity").val();
            //var country = $("#ContentPlaceHolder1_ddlCountry").val();
            //var mbile = $("#ContentPlaceHolder1_txtMobileNo").val();
            //var email = $("#ContentPlaceHolder1_txtEmail").val();

            //if (SO != "" && CUST != "" && SHIP != "" && refno != "" && refdat != "" && comm != "" && custn != "" && shipn != "" && pay != "" && scheme != "" && remarks != "" &&
            //    salesch != "" && custna != "" && addr1 != "" && addr2 != "" && addr3 != "" && pin != "" && state != "" && city != "" && country != "" && mbile != "" && email != "") {
            document.getElementById("busy-holder1").style.display = "";
            document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";
            //}
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="updChange" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="page-content-wrap">
                <div class="row">
                    <div class="col-md-12">

                        <div class="form-horizontal">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Bank Payment  </strong>Entry</h3>


                                    <asp:LinkButton ID="imgCancel" runat="server" CssClass="btn btn-success pull-right" PostBackUrl="~/FI/frmViewBankPayment.aspx" Text="Cancel" TabIndex="16"><i class="fa fa-times"></i></asp:LinkButton>
                                    <asp:LinkButton ID="imgSaveAll" OnClientClick="ShowLoading()" runat="server" CssClass="btn btn-success pull-right" OnClick="imgSaveAll_Click" Text="Save All" ValidationGroup="SaveAll" TabIndex="15"><i class="fa fa-save"></i></asp:LinkButton>
                                    <div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                        <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                        <label>Please wait...</label>
                                    </div>
                                </div>

                                <div class="panel-body">

                                    <div class="row">


                                        <div class="col-md-4">
                                            <h4 style="color: #f05423">Bank Payment</h4>
                                            <div class="row">

                                                <div class="col-md-12">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Doc. Type : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList ID="ddlDoctype" runat="server" CssClass="form-control required_text_box" Enabled="false" placeholder="Doc. type" TabIndex="1"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvDocType" runat="server" ControlToValidate="ddlDoctype" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Select Doc Type" InitialValue="0">Please Select Doc Type</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-12" style="margin-top: 10px !important;">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Doc. No. : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtDocNo" runat="server" CssClass="form-control required_text_box" placeholder="Doc. No." Enabled="false" TabIndex="2"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDocNo" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Doc No">Please Enter Doc No</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-12" style="margin-top: 10px !important;">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Doc. Date : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <div class="input-group">
                                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                    <asp:TextBox ID="txtDocDate" runat="server" placeholder="Doc. Date" class="form-control datepicker required_text_box" Enabled="true" TabIndex="3"></asp:TextBox>
                                                                </div>
                                                                <asp:RequiredFieldValidator ID="rfvDocDate" runat="server" ControlToValidate="txtDocDate" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Doc Date">Please Enter Doc Date</asp:RequiredFieldValidator>
                                                            </div>

                                                        </div>
                                                    </div>
                                                </div>

                                                <%--<div class="col-md-12" style="margin-top: 10px !important;">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Bank A/C : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList ID="ddlBankAccount" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvBankAc" runat="server" ControlToValidate="ddlBankAccount" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Select Bank A/C" InitialValue="0">Please Select Bank A/C</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>--%>


                                                <div class="col-md-12" style="margin-top: 10px !important;">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Advance : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:CheckBox ID="chkAdvance" runat="server" CssClass="form-control" TabIndex="5" Enabled="true" OnCheckedChanged="chkAdvance_CheckedChanged" AutoPostBack="true" />
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBankAC" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Bank A/C">Please Enter Bank A/C</asp:RequiredFieldValidator>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <asp:UpdatePanel ID="updRbl" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="col-md-12" style="margin-top: 10px !important;">
                                                            <div class="col-md-10">
                                                                <div class="form-group">
                                                                    <label class="col-md-5 control-label">PO/PB : </label>
                                                                    <div class="col-md-7 col-xs-12">
                                                                        <asp:RadioButtonList ID="rblPOPB" runat="server" TabIndex="47" RepeatDirection="Horizontal" RepeatLayout="Flow" OnSelectedIndexChanged="rblPOPB_SelectedIndexChanged" AutoPostBack="true">
                                                                            <asp:ListItem Value="PO" Text="PO" class="radio-inline"></asp:ListItem>
                                                                            <asp:ListItem Value="PB" Text="PB" class="radio-inline" Selected="True"></asp:ListItem>
                                                                            <asp:ListItem Value="OA" Text="On A/C" class="radio-inline"></asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                        <asp:RequiredFieldValidator ID="rfvPOSOPBNo" runat="server" ControlToValidate="rblPOPB" ValidationGroup="SaveAll" Style="color: red;"
                                                                            Display="Dynamic" ErrorMessage="Please Select PO SO / PB / On Account">Please Select PO SO / PB / On Account</asp:RequiredFieldValidator>
                                                                    </div>

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="rblPOPB" />
                                                    </Triggers>
                                                </asp:UpdatePanel>

                                                <div class="col-md-12" style="margin-top: 10px !important;">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Vend. Code : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtVendCode" runat="server" CssClass="form-control required_text_box" placeholder="Vendor Code" Enabled="true" TabIndex="7" OnTextChanged="txtVendCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtVendCode" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Vendor Code">Please Enter Vendor Code</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>



                                                <div class="col-md-12" style="margin-top: 10px !important;">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Disc. A/C : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtDIscAC" runat="server" CssClass="form-control required_text_box" placeholder="Disc. A/C" Enabled="true" TabIndex="8" Text="0"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvDIscAC" runat="server" ControlToValidate="txtDIscAC" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Discount Bank A/C">Please Enter Discount A/C</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-12" style="margin-top: 10px !important;">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Remarks : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control required_text_box" placeholder="Remarks" Enabled="true" TabIndex="9"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="rfvRemarks" runat="server" ControlToValidate="txtRemarks" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Bank A/C">Please Enter Bank A/C</asp:RequiredFieldValidator>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-12" style="margin-top: 10px !important;">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Disc. Amt. : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtDiscountAmt" runat="server" CssClass="form-control required_text_box" placeholder="Discount Amount" Text="0" Enabled="true" TabIndex="10" OnTextChanged="txtDiscountAmt_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtDiscountAmt" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Enter Bank A/C">Please Enter Bank A/C</asp:RequiredFieldValidator>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-12" style="margin-top: 10px !important;">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Payable Amt. : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtDocAmt" runat="server" CssClass="form-control required_text_box" placeholder="Doc. Amt." Enabled="true" Text="0" TabIndex="11"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvDocAmt" runat="server" ControlToValidate="txtDocAmt" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Doc. Amt.">Please Enter Doc. Amt.</asp:RequiredFieldValidator>
                                                                <%--<asp:RangeValidator ID="rvDocAmt" runat="server" ControlToValidate="txtDocAmt" ErrorMessage="Payable amt. should be grater than 0" Display="Dynamic"
                                                                    Type="Double" MinimumValue="1" MaximumValue="1000000000000" ForeColor="Red" ValidationGroup="SaveAll">Payable amt. should be grater than 0</asp:RangeValidator>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-12" style="margin-top: 10px !important;" runat="server" id="divAdj">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Adj. Amt. : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtAdjAmt" runat="server" CssClass="form-control required_text_box" placeholder="Adjustment Amount" Text="0" Enabled="true" TabIndex="12"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvAdjAmt" runat="server" ControlToValidate="txtAdjAmt" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Adjustment Amount">Please Enter Adjustment Amount</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-12" style="margin-top: 10px !important;">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Payment Mode : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:DropDownList ID="ddlPaymentMode" runat="server" CssClass="form-control required_text_box" TabIndex="13"></asp:DropDownList>
                                                                <asp:RequiredFieldValidator ID="rfvPaymode" runat="server" ControlToValidate="ddlPaymentMode" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Select Payment Mode" InitialValue="0">Select Payment Mode</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-12" style="margin-top: 10px !important;">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Transaction ID : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtTXNID" runat="server" CssClass="form-control required_text_box" placeholder="Transaction ID" Enabled="true" TabIndex="14"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator ID="rfvTXNID" runat="server" ControlToValidate="txtTXNID" ValidationGroup="SaveAll" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Transaction Id">Please Enter Transaction Id</asp:RequiredFieldValidator>--%>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>

                                        </div>

                                        <div class="col-md-8">
                                            <div class="row">
                                                <div class="col-md-12" style="margin-top: 10px !important; margin-left: -60px;" id="divVendDetails" runat="server" visible="false">
                                                    <div class="col-md-12">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Vend. Nnme : </label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:TextBox ID="txtVendName" runat="server" CssClass="form-control " placeholder="Vendor Name" Enabled="false" TabIndex="14" TextMode="MultiLine"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Bank : </label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control " placeholder="Bank Name" Enabled="false" TabIndex="14"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-12" style="margin-top: 10px;">
                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">Vend. A/C No. : </label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:TextBox ID="txtACNO" runat="server" CssClass="form-control " placeholder="Vendor Account No." Enabled="false" TabIndex="14"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-md-6">
                                                            <div class="form-group">
                                                                <label class="col-md-5 control-label">IFSC Code : </label>
                                                                <div class="col-md-7 col-xs-12">
                                                                    <asp:TextBox ID="txtIFSCCode" runat="server" CssClass="form-control " placeholder="Vendor IFSC Code" Enabled="false" TabIndex="14"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-md-12" style="margin-top: 10px !important;" id="divGrid" runat="server" visible="false">
                                                    <div style="margin-top: 10px; height: 450px; overflow: scroll; margin-left: -50px;">
                                                        <asp:GridView ID="gvPOList" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                                            CssClass="table table-hover table-striped table-bordered nowrap">
                                                            <EmptyDataTemplate>
                                                                No Record Found!
                                                            </EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Select">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelect" runat="server" OnCheckedChanged="chkSelect_CheckedChanged" AutoPostBack="true" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="PO No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPONO" runat="server" Text='<%# Eval("PONO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PO Dt.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPODT" runat="server" Text='<%# Eval("PODT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Vend. Code">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVENDCODE" runat="server" Text='<%# Eval("VENDCODE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Vend. Name">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNAME1" runat="server" Text='<%# Eval("NAME1") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Pay Terms">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPMTTERMS" runat="server" Text='<%# Eval("PMTTERMS") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Pay Term Desc.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPMTTERMSDESC" runat="server" Text='<%# Eval("PMTTERMSDESC") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Material Value">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNETMATVALUE" runat="server" Text='<%# Eval("NETMATVALUE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TAX Amt.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNETTAXAMT" runat="server" Text='<%# Eval("NETTAXAMT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Disc.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDISCOUNT" runat="server" Text='<%# Eval("DISCOUNT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Net PO Amt.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNETPOAMT" runat="server" Text='<%# Eval("NETPOAMT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Adv. Amt.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblADVAMT" runat="server" Text='<%# Eval("ADVAMT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="PO Pending Amt.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPENDINGAMT" runat="server" Text='<%# Eval("PENDINGAMT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%-- <asp:TemplateField HeaderText="MRN Amt.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMRNAMT" runat="server" Text='<%# Eval("MRNAMT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MRN TAX Amt.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMRNTAXAMT" runat="server" Text='<%# Eval("MRNTAXAMT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MRN Other Chg.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMRNCHGAMT" runat="server" Text='<%# Eval("MRNCHGAMT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="Payable Amt.">
                                                                    <ItemTemplate>
                                                                        <%--<asp:Label ID="lblPayableAmt" runat="server" Text='<%# Convert.ToDecimal(Eval("PENDINGAMT"))  - Convert.ToDecimal(Eval("MRNAMT")) - Convert.ToDecimal(Eval("MRNTAXAMT")) - Convert.ToDecimal(Eval("MRNCHGAMT"))   %>'></asp:Label>--%>
                                                                        <asp:Label ID="lblPayableAmt" runat="server" Text='<%# Convert.ToDecimal(Eval("PENDINGAMT"))   %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Payable Amt.">
                                                                    <ItemTemplate>
                                                                        <%--<asp:TextBox ID="txtPayableAmt" runat="server" Text='<%# Convert.ToDecimal(Eval("PENDINGAMT"))  - Convert.ToDecimal(Eval("MRNAMT")) - Convert.ToDecimal(Eval("MRNTAXAMT")) - Convert.ToDecimal(Eval("MRNCHGAMT"))   %>'></asp:TextBox>--%>
                                                                        <%--<asp:TextBox ID="txtPayableAmt" runat="server" Text='<%# Convert.ToDecimal(Eval("PENDINGAMT")) %>'></asp:TextBox>--%>
                                                                        <asp:TextBox ID="txtPayableAmt" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>



                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <%-- </div>
                                        </div>

                                        <div class="col-md-8">
                                            <div class="row">--%>
                                                <div class="col-md-12" style="margin-top: 10px !important;" id="divPB" runat="server" visible="false">
                                                    <div style="margin-top: 10px; height: 450px; overflow: scroll">
                                                        <asp:GridView ID="gvPBList" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                                            CssClass="table table-hover table-striped table-bordered nowrap">
                                                            <EmptyDataTemplate>
                                                                No Record Found!
                                                            </EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Select">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelectPB" runat="server" OnCheckedChanged="chkSelectPB_CheckedChanged" AutoPostBack="true" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="PB Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPBTYPE" runat="server" Text='<%# Eval("PBTYPE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PB No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPBNO" runat="server" Text='<%# Eval("PBNO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Bill No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBILLNO" runat="server" Text='<%# Eval("BILLNO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PB Dt.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPBDT" runat="server" Text='<%# Eval("PBDT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Vend. Code">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVENDCODE" runat="server" Text='<%# Eval("VENDCODE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%--<asp:TemplateField HeaderText="Vend. Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNAME1" runat="server" Text='<%# Eval("NAME1") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>

                                                                <%--<asp:TemplateField HeaderText="Pay Terms">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPMTTERMS" runat="server" Text='<%# Eval("PMTTERMS") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="Pay Term Desc.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPMTTERMSDESC" runat="server" Text='<%# Eval("PMTTERMSDESC") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Material Value">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNETMATVALUE" runat="server" Text='<%# Eval("NETMATVALUE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TAX Amt.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNETTAXAMT" runat="server" Text='<%# Eval("NETTAXAMT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Disc.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDISCOUNT" runat="server" Text='<%# Eval("DISCOUNT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Net PB Amt.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNETPBAMT" runat="server" Text='<%# Eval("NETPBAMT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Adjusted Amt.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblADJAMT" runat="server" Text='<%# Eval("ADJAMT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PO Amt.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPOAMT" runat="server" Text='<%# Eval("POAMT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PO Pending">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPOPENDINGAMT" runat="server" Text='<%# Eval("POPENDINGAMT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PO Adv.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPOADVAMT" runat="server" Text='<%# Eval("POADVAMT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <%--<asp:TemplateField HeaderText="Adv. Amt.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblADVAMT" runat="server" Text='<%# Eval("ADVAMT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>

                                                                <%-- <asp:TemplateField HeaderText="PO Pending Amt.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPENDINGAMT" runat="server" Text='<%# Eval("PENDINGAMT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MRN Amt.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMRNAMT" runat="server" Text='<%# Eval("MRNAMT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MRN TAX Amt.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMRNTAXAMT" runat="server" Text='<%# Eval("MRNTAXAMT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="MRN Other Chg.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMRNCHGAMT" runat="server" Text='<%# Eval("MRNCHGAMT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="Payable Amt.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPayableAmt" runat="server" Text='<%# Convert.ToDecimal(Eval("NETMATVALUE"))  + Convert.ToDecimal(Eval("NETTAXAMT")) - Convert.ToDecimal(Eval("DISCOUNT")) - Convert.ToDecimal(Eval("ADJAMT")) - Convert.ToDecimal(Eval("POADVAMT"))   %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Payable Amt.">
                                                                    <ItemTemplate>
                                                                        <%--<asp:TextBox ID="txtPayableAmt" runat="server" Text='<%# Convert.ToDecimal(Eval("NETMATVALUE"))  - Convert.ToDecimal(Eval("NETTAXAMT")) - Convert.ToDecimal(Eval("DISCOUNT")) - Convert.ToDecimal(Eval("ADJAMT")) - Convert.ToDecimal(Eval("POADVAMT"))  %>'></asp:TextBox>--%>
                                                                        <asp:TextBox ID="txtPayableAmt" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                                <%-- </div>
                                        </div>

                                        <div class="col-md-8">
                                            <div class="row">--%>

                                                <div class="col-md-12" style="margin-top: 10px !important;" id="divPBAdjust" runat="server" visible="false">
                                                    <asp:Label ID="lblPBAdjustHeader" runat="server" Text="PB Data"></asp:Label>
                                                    <div class="box-body" style="margin-top: 10px; height: 400px; overflow: scroll; margin-left: -50px;">

                                                        <asp:GridView ID="gvPBAdjData" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                                            CssClass="table table-hover table-striped table-bordered nowrap">
                                                            <EmptyDataTemplate>
                                                                No Record Found!
                                                            </EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Select">
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkSelectAllPB" runat="server" OnCheckedChanged="chkSelectAllPB_CheckedChanged" AutoPostBack="true" />
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelectPBAdj" runat="server" OnCheckedChanged="chkSelectPBAdj_CheckedChanged" AutoPostBack="true" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PB Type">
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
                                                                <asp:TemplateField HeaderText="Net PB Amt.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNETPBAMT" runat="server" Text='<%# Eval("NETPBAMT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Vend. Code">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVENDCODE" runat="server" Text='<%# Eval("VENDCODE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Vendor">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVENDNAME" runat="server" Text='<%# Eval("VENDNAME") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PO Amt." HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPOAMT" runat="server" Text='<%# Eval("POAMT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PO Pending" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPOPENDINGAMT" runat="server" Text='<%# Eval("POPENDINGAMT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PO Adv." HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPOADVAMT" runat="server" Text='<%# Eval("POADVAMT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PB Pending">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPBPENDINGAMT" runat="server" Text='<%# Eval("PBPENDINGAMT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="PB Paid Amt.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPBPAIDAMT" runat="server" Text='<%# Eval("PBPAIDAMT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Payable Amt.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPayableAmt" runat="server" Text='<%# Convert.ToDecimal(Eval("PBPENDINGAMT"))  %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Payable Amt.">
                                                                    <ItemTemplate>
                                                                        <%--<asp:TextBox ID="txtPayableAmt" runat="server" Text='<%# Convert.ToDecimal(Eval("PBPENDINGAMT"))  - Convert.ToDecimal(Eval("POADVAMT"))   %>'></asp:TextBox>--%>
                                                                        <asp:TextBox ID="txtPayableAmt" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                            </Columns>
                                                        </asp:GridView>

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-md-12" style="margin-top: 10px;">

                                                            <div class="col-md-12">
                                                                <%--<div class="col-md-3">&nbsp;</div>
                                                                <div class="col-md-3">&nbsp;</div>--%>
                                                                <div class="col-md-4">
                                                                    <div class="form-group">
                                                                        <label class="col-md-5 control-label">CR Amt. : </label>
                                                                        <div class="col-md-7 col-xs-12">
                                                                            <asp:TextBox ID="txtCRAmt" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <%--</div>


                                                            <div class="col-md-12">--%>
                                                                <%--<div class="col-md-3">&nbsp;</div>
                                                                <div class="col-md-3">&nbsp;</div>--%>
                                                                <div class="col-md-4">
                                                                    <div class="form-group">
                                                                        <label class="col-md-5 control-label">DR Amt. : </label>
                                                                        <div class="col-md-7 col-xs-12">
                                                                            <asp:TextBox ID="txtDRAmt" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <%--     </div>



                                                            <div class="col-md-12">--%>
                                                                <%--<div class="col-md-3">&nbsp;</div>
                                                                <div class="col-md-3">&nbsp;</div>--%>
                                                                <div class="col-md-4">
                                                                    <div class="form-group">
                                                                        <label class="col-md-5 control-label">Adj. Amt. : </label>
                                                                        <div class="col-md-7 col-xs-12">
                                                                            <asp:TextBox ID="txtTAdjAmt" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-12" style="margin-top: 10px !important;" id="divDNAdjust" runat="server" visible="false">
                                                    <asp:Label ID="lblDebitNoteHeader" runat="server" Text="Debit Note"></asp:Label>
                                                    <div style="margin-top: 10px; height: 300px; overflow: scroll">

                                                        <asp:GridView ID="gvDNData" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                                            CssClass="table table-hover table-striped table-bordered nowrap">
                                                            <EmptyDataTemplate>
                                                                No Record Found!
                                                            </EmptyDataTemplate>
                                                            <Columns>
                                                                <%--<asp:TemplateField HeaderText="PB Type">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPBTYPE" runat="server" Text='<%# Eval("PBTYPE") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>

                                                                <asp:TemplateField HeaderText="Select">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelectDNAdj" runat="server" OnCheckedChanged="chkSelectDNAdj_CheckedChanged" AutoPostBack="true" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="DN No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDNNO" runat="server" Text='<%# Eval("DNNO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Vend. Code">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVENDCODE" runat="server" Text='<%# Eval("VENDCODE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Vendor">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNAME1" runat="server" Text='<%# Eval("NAME1") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Net Tax Amt.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNETMATVALUE" runat="server" Text='<%# Eval("NETMATVALUE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Disc.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDISCOUNT" runat="server" Text='<%# Eval("DISCOUNT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Other Charge">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblOTHRCHG" runat="server" Text='<%# Eval("OTHRCHG") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Net DN Amt.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNETDNAMT" runat="server" Text='<%# Eval("NETDNAMT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Adj. Amt.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPayableAmt" runat="server" Text='<%# Convert.ToDecimal(Eval("NETDNAMT")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Payable Amt.">
                                                                    <ItemTemplate>
                                                                        <%--<asp:TextBox ID="txtPayableAmt" runat="server" Text='<%# Convert.ToDecimal(Eval("NETDNAMT")) %>' Enabled="false"></asp:TextBox>--%>
                                                                        <asp:TextBox ID="txtPayableAmt" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                            </Columns>
                                                        </asp:GridView>


                                                    </div>
                                                </div>

                                                <div class="col-md-12" style="margin-top: 10px !important;" id="divOACAdjust" runat="server" visible="false">
                                                    <asp:Label ID="lblOACHeader" runat="server" Text="On Account Paid"></asp:Label>
                                                    <div style="margin-top: 10px; height: 300px; overflow: scroll">

                                                        <asp:GridView ID="gvOACData" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                                            CssClass="table table-hover table-striped table-bordered nowrap">
                                                            <EmptyDataTemplate>
                                                                No Record Found!
                                                            </EmptyDataTemplate>
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Select">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelectOACAdj" runat="server" OnCheckedChanged="chkSelectOACAdj_CheckedChanged" AutoPostBack="true" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Doc. Type">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDOCTYPE" runat="server" Text='<%# Eval("DOCTYPE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Doc. No.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDOCNO" runat="server" Text='<%# Eval("DOCNO") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Vend. Code">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVENDCODE" runat="server" Text='<%# Eval("VENDCODE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Vendor">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblNAME1" runat="server" Text='<%# Eval("NAME1") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Doc. Amt.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDOCAMT" runat="server" Text='<%# Eval("DOCAMT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Adjusted Amt.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblADJAMT" runat="server" Text='<%# Eval("ADJAMT") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <%--<asp:TemplateField HeaderText="Other Charge">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblOTHRCHG" runat="server" Text='<%# Eval("OTHRCHG") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                                <%--<asp:TemplateField HeaderText="Net DN Amt.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNETDNAMT" runat="server" Text='<%# Eval("NETDNAMT") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                                <asp:TemplateField HeaderText="Adj. Amt.">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPayableAmt" runat="server" Text='<%# Convert.ToDecimal(Eval("DOCAMT")) %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Payable Amt.">
                                                                    <ItemTemplate>
                                                                        <%--<asp:TextBox ID="txtPayableAmt" runat="server" Text='<%# Convert.ToDecimal(Eval("DOCAMT"))  %>' Enabled="false"></asp:TextBox>--%>
                                                                        <asp:TextBox ID="txtPayableAmt" runat="server"></asp:TextBox>
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
                </div>

            </div>

            <div class="modal fade" id="modal-YesNo" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" style="color: #337ab7">Alert Message</h4>
                        </div>


                        <div class="modal-body">
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label ID="lblAlertMessageAmt" runat="server"></asp:Label>
                                    </div>

                                    <div class="col-md-12" style="margin-top: 10px !important;">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Button ID="btnYes" runat="server" OnClick="btnYes_Click" CssClass="btn btn-success pull-right" Text="YES" />
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Button ID="btnNo" runat="server" OnClick="btnNo_Click" CssClass="btn btn-danger pull-left" Text="NO" />
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
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="chkSelect" EventName="CheckedChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="chkSelectPB" EventName="CheckedChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="chkSelectPBAdj" EventName="CheckedChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="chkSelectDNAdj" EventName="CheckedChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="chkSelectOACAdj" EventName="CheckedChanged" />--%>
            <%--<asp:PostBackTrigger ControlID="chkSelect" />
                                        <asp:PostBackTrigger ControlID="chkSelectPB" />
                                        <asp:PostBackTrigger ControlID="chkSelectPBAdj" />
                                        <asp:PostBackTrigger ControlID="chkSelectDNAdj" />
                                        <asp:PostBackTrigger ControlID="chkSelectOACAdj" />--%>
        </Triggers>
    </asp:UpdatePanel>
    <%--<asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="modal fade" id="modal-POData" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content" style="margin-left: -150px; width: 1200px;">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" style="color: #337ab7">Get Lead </h4>
                        </div>
                        <div class="modal-body">
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div style="width: 1150px !important; height: 600px; overflow: scroll">
                                            <asp:GridView ID="gvPOList" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                                CssClass="table table-hover table-striped table-bordered nowrap">
                                                <EmptyDataTemplate>
                                                    No Record Found!
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="PO No.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPONO" runat="server" Text='<%# Eval("PONO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PO Dt.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPODT" runat="server" Text='<%# Eval("PODT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vend. Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVENDCODE" runat="server" Text='<%# Eval("VENDCODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Vend. Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNAME1" runat="server" Text='<%# Eval("NAME1") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Pay Terms">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPMTTERMS" runat="server" Text='<%# Eval("PMTTERMS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Pay Term Desc.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPMTTERMSDESC" runat="server" Text='<%# Eval("PMTTERMSDESC") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Material Value">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNETMATVALUE" runat="server" Text='<%# Eval("NETMATVALUE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TAX Amt.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNETTAXAMT" runat="server" Text='<%# Eval("NETTAXAMT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Disc.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDISCOUNT" runat="server" Text='<%# Eval("DISCOUNT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Net PO Amt.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNETPOAMT" runat="server" Text='<%# Eval("NETPOAMT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MRN Amt.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMRNAMT" runat="server" Text='<%# Eval("MRNAMT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MRN TAX Amt.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMRNTAXAMT" runat="server" Text='<%# Eval("MRNTAXAMT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="MRN Other Chg.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMRNCHGAMT" runat="server" Text='<%# Eval("MRNCHGAMT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Payable Amt.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPayableAmt" runat="server" Text='<%# Convert.ToDecimal(Eval("NETPOAMT"))  - Convert.ToDecimal(Eval("MRNAMT")) - Convert.ToDecimal(Eval("MRNTAXAMT")) - Convert.ToDecimal(Eval("MRNCHGAMT"))   %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Select">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" />
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
        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranFIBP" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranFIBP" runat="server" />

</asp:Content>
