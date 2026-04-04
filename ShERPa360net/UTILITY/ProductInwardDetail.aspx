<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="ProductInwardDetail.aspx.cs" Inherits="ShERPa360net.UTILITY.ProductInwardDetail" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

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
            margin-top: -64px !important;
            -ms-transform: scale(15); /* IE 9 */
            -webkit-transform: scale(15); /* Safari 3-8 */
            transform: scale(15);
            transform-origin: 2px;
        }

        h1 {
            position: relative;
            padding: 0;
            margin: 0;
            font-family: "Raleway", sans-serif;
            font-weight: 300;
            font-size: 40px;
            color: #fff;
            -webkit-transition: all 0.4s ease 0s;
            -o-transition: all 0.4s ease 0s;
            transition: all 0.4s ease 0s;
        }




        .twelve h1 {
            font-size: 16px;
            font-weight: 700;
            letter-spacing: 1px;
            /* text-transform: uppercase; */
            width: 160px;
            text-align: center;
            margin: auto;
            white-space: nowrap;
            padding-bottom: 13px;
        }

            .twelve h1:before {
                background-color: #fff;
                content: '';
                display: block;
                height: 3px;
                width: 60px;
                margin-bottom: 5px;
            }

            .twelve h1:after {
                background-color: #fff;
                content: '';
                display: block;
                position: absolute;
                right: 0;
                bottom: 0;
                height: 3px;
                width: 60px;
                margin-bottom: 0.25em;
            }



        .form_wrapper .input_field {
            position: relative;
            margin-bottom: 30px;
        }

            .form_wrapper .input_field > a {
                position: absolute;
                left: 0;
                top: 0;
                color: #fff;
                height: 100%;
                border-right: 1px solid #cccccc;
                text-align: center;
                width: 29px;
                background: #1f4f8ec7;
            }

                .form_wrapper .input_field > a > i {
                    padding-top: 7px;
                    font-size: 17px;
                }

        .form_wrapper .textarea_field > a > i {
            padding-top: 10px;
        }

        .form_wrapper input[type=text], .form_wrapper input[type=email], .form_wrapper input[type=password] {
            width: 100%;
            padding: 8px 10px 9px 35px;
            height: 35px;
            border: 1px solid #cccccc;
            box-sizing: border-box;
            outline: none;
        }

        .pd-35 {
            padding-left: 35px
        }

        .wdth-750 {
            width: 750px;
        }

        input#fuSerialNo {
            display: none;
        }

        .modal-header {
            border-bottom: 1px solid #1f4f8e;
        }

        .modal-body.form_wrapper {
            border: 2px solid #1f4f8e;
        }

        .modal-content {
            border-radius: 10px;
        }



        @media only screen and (max-width: 600px) {
            .modal-dialog.modal-lg.wdth-750 {
                padding: 10px 19px 0px 0px !important;
            }

            .wdth-750 {
                width: 100% !important;
            }
        }

        .btn-1 {
            padding: 10px 25px;
            font-size: 14px;
        }

        .btn.btn-danger.btn-file {
            padding: 0px 0px 0px 0px;
            background: #ff98006e;
            border: none;
            font-weight: bold;
            color: #1f4f8e;
            height: 40px;
            width: 100%;
            line-height: 3pc;
        }

        .lbl-style {
            font-size: 12px;
            letter-spacing: 1px;
            color: #1f4f8e;
        }
    </style>
    <script>
        function ValidateInwardDetail() {
            debugger
            var Isvalidate = true;
            $("#lblIMEINoalert").css("display", "none");
            if ($("#txtIMEINo").val().length > 0 && $("#txtIMEINo").val().length != 15) {
                $("#lblIMEINoalert").css("display", "block");
                Isvalidate = false;
            }
            return Isvalidate;
        }
        function IsAllowonlyNumericKeyIMEINO() {
            debugger
            if ($("#txtIMEINo").val().length > 0) {
                if (!$.isNumeric($("#txtIMEINo").val())) {
                    $("#txtIMEINo").val("");
                }
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
                            <h3 class="panel-title"><strong runat="server" id="stProductDetail"><span class="fa fa-file"></span>&nbsp;Product Inward Detail</strong></h3>
                            <asp:LinkButton ID="imgReset" runat="server" CssClass="btn btn-success pull-right" OnClick="imgReset_Click" Text="Reset"><span tooltip="Detail" flow="down"><i class="fa fa-undo"></i> </span></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkSearh" CssClass="btn btn-success pull-right" Text="Search" OnClick="lnkSearh_Click" OnClientClick="return ValidateInwardDetail();" ValidationGroup="SearchDetail"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <fieldset class="scheduler-border">
                                            <legend class="scheduler-border">Filter Detail</legend>
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">From Date</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                            </div>
                                                            <asp:RequiredFieldValidator ID="rfvfromdate" Style="color: red!important;" runat="server" ControlToValidate="txtFromDate" ValidationGroup="SearchDetail"
                                                                ErrorMessage="Please Enter From Date To Search">Please Enter From Date To Search</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">To Date</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                            </div>
                                                            <asp:RequiredFieldValidator ID="rfvToDate" Style="color: red!important;" runat="server" ControlToValidate="txtToDate" ValidationGroup="SearchDetail"
                                                                ErrorMessage="Please Enter To Date To Search">Please Enter To Date To Search</asp:RequiredFieldValidator>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Vendor Name. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList TabIndex="2" ID="ddlVendor" runat="server" CssClass="form-control ddlVendor"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label" style="padding-top: 10px">IMEI No :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox TabIndex="3" onkeyup="IsAllowonlyNumericKeyIMEINO();" ClientIDMode="Static" ID="txtIMEINo" MaxLength="15" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="IMEI No" CssClass="form-control"></asp:TextBox>
                                                            <asp:Label ID="lblIMEINoalert" Style="color: red!important; display: none; position: absolute; padding: 6px;"
                                                                runat="server" ClientIDMode="Static">Please Enter the IMEI No and should be 15 Digit</asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Listing ID. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox runat="server" ClientIDMode="Static" ID="txtListingID" CssClass="form-control" placeholder="Listing ID"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>


                                            </div>
                                        </fieldset>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="page-content-wrap">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="panel panel-default tabs">
                                        <ul class="nav nav-tabs" role="tablist">
                                            <li class="active"><a href="#tab-first" role="tab" data-toggle="tab">Details</a></li>
                                        </ul>
                                        <div class="panel-body tab-content">
                                            <div class="tab-pane active" id="tab-first">
                                                <div class="panel-body">
                                                    <div class="row">
                                                        <fieldset class="scheduler-border">
                                                            <legend class="scheduler-border" id="lgrecordcount" runat="server"></legend>
                                                            <div class="col-md-12 divhorizontal" style="overflow-x: scroll!important;">
                                                                <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
                                                                    <EmptyDataTemplate>
                                                                        No Record Found!
                                                                    </EmptyDataTemplate>
                                                                    <Columns>
                                                                        <%--Action Column --%>
                                                                        <asp:TemplateField HeaderText="Final Qc">
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblImageData" runat="server" Text='<%# Eval("IMEIIMAGE") %>' Visible="false"></asp:Label>
                                                                                <asp:Label ID="lblInvoiceImageData" runat="server" Text='<%# Eval("INVOICEIMAGE") %>' Visible="false"></asp:Label>
                                                                                <asp:LinkButton Style="margin-top: 5px!important; margin-left: 64px!important;" runat="server" ID="btnQc" Text="Inward" CssClass="btn btn-primary" OnClick="btnQc_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--Action Column --%>

                                                                        <%--Image Column --%>
                                                                        <asp:TemplateField HeaderText="IMEI Image" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Image ID="imgImage" runat="server" Height="50" Width="50" CssClass="zoom" AlternateText='IMEI Image' Visible="true" />
                                                                                <%--<asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkDownload_Click" Text="Download" Visible="true"></asp:LinkButton>--%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--Image Column --%>

                                                                        <%--INVOICE IMAGE Column --%>
                                                                        <asp:TemplateField HeaderText="INVOICE Image" Visible="false">
                                                                            <ItemTemplate>
                                                                                <asp:Image ID="imgInvoice" runat="server" Height="50" Width="50" CssClass="zoom" AlternateText='INVOICE Image' Visible="true" />
                                                                                <%--<asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkDownload_Click" Text="Download" Visible="true"></asp:LinkButton>--%>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--Image Column --%>

                                                                        <%--Regular Column --%>
                                                                        <asp:TemplateField HeaderText="Entry Date">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField runat="server" ID="hdID" Value='<%# Bind("ID") %>'></asp:HiddenField>
                                                                                <asp:Label runat="server" ID="lblDate" Text='<%# Bind("CREATEDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="ID" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblID" Text='<%# Bind("ID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Make">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblMake" Text='<%# Bind("MAKE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Model">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblModel" Text='<%# Bind("MODEL") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Rom">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblRom" Text='<%# Bind("ROM") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Ram">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblRam" Text='<%# Bind("RAM") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Color">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblColor" Text='<%# Bind("COLOR") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Vendor Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVendorName" Text='<%# Bind("VENDORNAME") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Vendor Grade">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVendorGrade" Text='<%# Bind("VENDORGRADE") %>'></asp:Label>
                                                                                <asp:Label runat="server" ID="lblVendorID" Visible="false" Text='<%# Bind("VENDORID") %>'></asp:Label>
                                                                                <asp:Label runat="server" ID="lblOrderNo" Visible="false" Text='<%# Bind("ORDERNO") %>'></asp:Label>
                                                                                <asp:Label runat="server" ID="lblSoNo" Visible="false" Text='<%# Bind("SONO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Vendor Rate">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVendorRate" Text='<%# Bind("VENDORPRICE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Mobex Rate">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblMobexRate" Text='<%# Bind("MOBEXPRICE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Blancco Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblBlanccoDate" Text='<%# Bind("BLANCCOQCDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblStatus" Text='<%# Bind("STATUS") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="IMEINo" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblIMEINo" Text='<%# Bind("IMEINO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                       <asp:TemplateField HeaderText="List Type" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblListingType" Text='<%# Bind("LISTINGTYPE") %>'></asp:Label>
                                                                                <asp:HiddenField runat="server" ID="hdgrdLISTINGTYPE" Value='<%# Bind("LISTINGTYPEID") %>'></asp:HiddenField>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Listing Is KRO" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblLISTVENDORISKRO" Text='<%# Bind("LISTVENDORISKRO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <%--Regular Column --%>
                                                                    </Columns>
                                                                </asp:GridView>
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
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-detail" data-backdrop="static">
        <div class="modal-dialog modal-lg  wdth-750">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <div class="twelve">
                        <h1>Inward Details</h1>
                    </div>

                </div>
                <div class="modal-body form_wrapper">
                    <div class="box-body">
                        <div class="row">

                            <div class="col-md-6">
                                <label>Inward Result :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:HiddenField runat="server" ID="hdMake" />
                                    <asp:HiddenField runat="server" ID="hdModel" />
                                    <asp:HiddenField runat="server" ID="hdRam" />
                                    <asp:HiddenField runat="server" ID="hdRom" />
                                    <asp:HiddenField runat="server" ID="hdcolor" />
                                    <asp:HiddenField runat="server" ID="hdGrade" />
                                    <asp:HiddenField runat="server" ID="hdVendorID" />
                                    <asp:HiddenField ClientIDMode="Static" runat="server" ID="hdlistingtype" />
                                    <asp:DropDownList ID="ddlInwardResult" ClientIDMode="Static" runat="server" CssClass="form-control pd-35">
                                        <asp:ListItem Text="SELECT" Selected="True" Value="SELECT"></asp:ListItem>
                                        <asp:ListItem Text="PASS" Value="PASS"></asp:ListItem>
                                        <asp:ListItem Text="FAIL" Value="FAIL"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblInwardResultalert" Style="color: red!important; display: none; position: absolute;"
                                        runat="server" ClientIDMode="Static">Please Select the Inward Result</asp:Label>
                                </div>
                            </div>

                            <div class="col-md-6">

                                <label>Reason :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:TextBox TabIndex="3" Rows="1" ClientIDMode="Static" ID="txtInwardReason" Style="font-weight: bold!important; color: black!important;" runat="server" TextMode="MultiLine" placeholder="Fail Reason" CssClass="form-control pd-35"></asp:TextBox>
                                    <asp:Label ID="lblInwardReasonalert" Style="color: red!important; display: none; position: absolute" runat="server" ClientIDMode="Static">Please Enter the Reason</asp:Label>
                                </div>
                            </div>


                        </div>


                        <div class="row">
                            <div class="col-md-6">

                                <label>Inward Grade :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:DropDownList ID="ddlInwardGrade" ClientIDMode="Static" runat="server" CssClass="form-control pd-35">
                                        <asp:ListItem Text="SELECT" Selected="True" Value="SELECT"></asp:ListItem>
                                        <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                        <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                        <asp:ListItem Text="C" Value="C"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblInwardGradealert" Style="color: red!important; display: none; position: absolute" runat="server" ClientIDMode="Static">Please Select the Inward Grade</asp:Label>
                                </div>
                            </div>

                            <div class="col-md-6">

                                <label>Order No :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:TextBox TabIndex="3" ClientIDMode="Static" ID="txtOrderNo" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Order No" CssClass="form-control "></asp:TextBox>
                                    <asp:Label ID="lblOrderNoalert" Style="color: red!important; display: none; position: absolute" runat="server" ClientIDMode="Static">Please Enter Order No</asp:Label>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-6">

                                <label>Ref No :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:TextBox TabIndex="3" ClientIDMode="Static" ID="txtRefNo" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Ref No" CssClass="form-control"></asp:TextBox>
                                    <asp:Label ID="lblRefNoalert" Style="color: red!important; display: none; position: absolute" runat="server" ClientIDMode="Static">Please Enter Ref No</asp:Label>
                                </div>
                            </div>

                            <div class="col-md-6">

                                <label>So No :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:TextBox TabIndex="3" ClientIDMode="Static" ID="txtSoNo" onfocusout="SONOFound();" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="So No" MaxLength="10" CssClass="form-control"></asp:TextBox>
                                    <asp:Label ID="lblSoNoalert" Style="color: red!important; display: none; position: absolute" runat="server" ClientIDMode="Static">Please Enter So No</asp:Label>
                                    <asp:Label ID="lblSONoNotFound" Style="color: red!important; display: none; position: absolute" runat="server" ClientIDMode="Static">SO noumber not found.</asp:Label>
                                    <asp:Label ID="lblvalidSONoalert" Style="color: red!important; display: none; position: absolute" runat="server" ClientIDMode="Static">Please Enter Valid SO No</asp:Label>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-6">

                                <label>IMEI Image :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:Image ID="imgImage" runat="server" Style="position: relative !important; z-index: 1" Height="43" Width="50" CssClass="zoom" AlternateText='IMEI Image' Visible="true" />
                                </div>
                            </div>

                            <div class="col-md-6">

                                <label>Invoice :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:DropDownList ID="ddlInvoice" runat="server" CssClass="form-control pd-35 ddlInvoice required_text_box">
                                        <asp:ListItem Text="SELECT" Selected="True" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="NO" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblInvoicealert" Style="color: red!important; display: none; position: absolute" runat="server" ClientIDMode="Static">Please Select the Value</asp:Label>
                                </div>
                            </div>

                        </div>

                        <div class="row">
                            <div class="col-md-6 disply-none">

                                <label>Box :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:DropDownList ID="ddlBox" runat="server" CssClass="form-control pd-35 ddlBox required_text_box">
                                        <asp:ListItem Text="SELECT" Selected="True" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="NO" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblBoxalert" Style="color: red!important; display: none; position: absolute" runat="server" ClientIDMode="Static">Please Select the Value</asp:Label>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <label>Charger and Cable :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:DropDownList ID="ddlCharger" runat="server" CssClass="form-control pd-35 ddlCharger required_text_box">
                                        <asp:ListItem Text="SELECT" Selected="True" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="NO" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Charger" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Cable" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="ChargerCable" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblChargeralert" Style="color: red!important; display: none; position: absolute" runat="server" ClientIDMode="Static">Please Select the Value</asp:Label>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-6">

                                <label>Orignal:</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:DropDownList ID="ddlOrignal" runat="server" CssClass="form-control pd-35 ddlOrignal required_text_box">
                                        <asp:ListItem Text="SELECT" Selected="True" Value="-1"></asp:ListItem>
                                        <asp:ListItem Text="NO" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblorignalalert" Style="color: red!important; display: none; position: absolute" runat="server" ClientIDMode="Static">Please Select the Value</asp:Label>
                                </div>
                            </div>


                            <div class="col-md-6">

                                <label>Charger Walt :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:TextBox TabIndex="3" ClientIDMode="Static" ID="txtChargerWalt" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Charger Walt" CssClass="form-control"></asp:TextBox>
                                    <asp:Label ID="lblchargerwaltalert" Style="color: red!important; display: none; position: absolute" runat="server" ClientIDMode="Static">Please Enter Charger Walt</asp:Label>
                                </div>
                            </div>


                        </div>
                        <%--Manan 02-02-2023--%>
                        <div class="row">
                            <div class="col-md-6">
                                <label>COLOR :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:DropDownList ID="ddlColor" ClientIDMode="Static" runat="server" CssClass="form-control pd-35">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblColoralert" Style="color: red!important; display: none; position: absolute" runat="server" ClientIDMode="Static">Please Select the Color.</asp:Label>
                                </div>
                            </div>
                            <div class="col-md-6">

                                <label>Invoice Image :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:Image ID="invoiceImage" runat="server" Style="position: relative !important; z-index: 1" Height="43" Width="50" CssClass="zoom" Visible="true" />
                                </div>
                            </div>
                        </div>
                        <div class="row">

                            <div class="col-md-12 " style="text-align: center">
                                <asp:HiddenField runat="server" ID="hdprimarykey" />
                                <asp:Button TabIndex="3" ID="btnSaveQc" OnClick="btnSaveQc_Click" OnClientClick="return ValidateProductInwardDetail();" runat="server" Text="Update" CssClass="btn btn-primary"></asp:Button>
                                <asp:Button TabIndex="3" ID="btnResetQc" OnClick="btnResetQc_Click" Style="margin-left: 40px!important" runat="server" Text="Reset" CssClass="btn btn-danger"></asp:Button>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabCheckerid" value="tsmMobexSellerInwardDetail" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />
</asp:Content>
