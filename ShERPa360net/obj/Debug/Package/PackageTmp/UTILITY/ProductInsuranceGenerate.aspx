<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="ProductInsuranceGenerate.aspx.cs" Inherits="ShERPa360net.UTILITY.ProductInsuranceGenerate" EnableEventValidation="false" %>

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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong runat="server" id="stProductDetail"><span class="fa fa-file"></span>&nbsp;Product Insurance Generate</strong></h3>
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
                                                <div class="col-md-3" runat="server" visible="false">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">From Date</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                            </div>
                                                            <%--<asp:RequiredFieldValidator ID="rfvfromdate" Style="color: red!important;" runat="server" ControlToValidate="txtFromDate" ValidationGroup="SearchDetail"
                                                                ErrorMessage="Please Enter From Date To Search">Please Enter From Date To Search</asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3" runat="server" visible="false">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">To Date</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                            </div>
                                                            <%--                  <asp:RequiredFieldValidator ID="rfvToDate" Style="color: red!important;" runat="server" ControlToValidate="txtToDate" ValidationGroup="SearchDetail"
                                                                ErrorMessage="Please Enter To Date To Search">Please Enter To Date To Search</asp:RequiredFieldValidator>--%>
                                                        </div>

                                                    </div>
                                                </div>


                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label" style="padding-top: 10px">Invoice No :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox TabIndex="3" ClientIDMode="Static" ID="txtSINO" MaxLength="10" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Invoice No" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label" style="padding-top: 10px">Status :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                        <asp:DropDownList TabIndex="2" ID="ddlstatus" runat="server" CssClass="form-control"></asp:DropDownList>
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
                                                                        <asp:TemplateField HeaderText="Action">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton Style="margin-top: 5px!important; margin-left: 64px!important;" runat="server" ID="btnGenerateInsurance" Text="Generate Insurance" CssClass="btn btn-primary" OnClick="btnGenerateInsurance_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Invoice No">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblSRNO" Visible="false" Text='<%# Bind("SRNO") %>'></asp:Label>
                                                                                <asp:Label runat="server" ID="lblInvoiceNo" Text='<%# Bind("SINO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Invoice Date" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblInvoiceDate" Text='<%# Bind("SIDT") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Order No">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblOrderNo" Text='<%# Bind("REFNO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="First Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblfirstname" Text='<%# Bind("CUSTNAME") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="last Name">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lbllastname" Text='<%# Bind("CUSTNAME") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Address 1">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblAddress1" Text='<%# Bind("CUSTADD1") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Address 2">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblAddress2" Text='<%# Bind("CUSTADD2") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Address 3">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblAddress3" Text='<%# Bind("CUSTADD3") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="City">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblCity" Text='<%# Bind("CITY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="State">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblState" Text='<%# Bind("STATENAME") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Pincode">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblPincode" Text='<%# Bind("PINCODE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Mobile Number">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblMobieNumber" Text='<%# Bind("CUSTMOBILENO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Email">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblEmail" Text='<%# Bind("CUSTEMAILID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Item Detail">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblItemDetail" Text='<%# Bind("ITEMDESC") %>'></asp:Label>
                                                                                <asp:Label runat="server" ID="lblSoNo" Visible="false" Text='<%# Bind("SONO") %>'></asp:Label>
                                                                                <asp:Label runat="server" ID="lblDeviceID" Visible="false" Text='<%# Bind("INSURANCEDEVICEID") %>'></asp:Label>
                                                                                <asp:Label runat="server" ID="lblPlanID" Visible="false" Text='<%# Bind("INSURANCEPLANID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                         <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblStatus" Text='<%# Bind("INSURANCEALLOSTATUSVALUE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Alloted Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblAllotedDate" Text='<%# Bind("INSURANCEALLOTEDDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                         <asp:TemplateField HeaderText="Alloted By">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblAllotedBY" Text='<%# Bind("INSURANCEALLOTEDBY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Invoice URL">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton runat="server" ID="lblInvoiceURL" Text='<%# Bind("INSURANCEINVOICEURL") %>'></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                          <asp:TemplateField HeaderText="IMEI Image URL">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton runat="server" ID="lblIMEIURL" Text='<%# Bind("INSURANCEIMEIIMAGEURL") %>'></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

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
                        <h1>Generate Insurance</h1>
                    </div>

                </div>
                <div class="modal-body form_wrapper">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-3">
                                <label>Invoice No :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:HiddenField runat="server" ID="hdDeviceID" />
                                    <asp:HiddenField runat="server" ID="hdPlantVariantID" />
                                    <asp:HiddenField runat="server" ID="hdSoNo" />
                                    <asp:HiddenField runat="server" ID="hdSrNo" />
                                    <asp:TextBox TabIndex="3" Rows="1" ClientIDMode="Static" ID="txtInvoiceNo" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Invoice No" CssClass="form-control pd-35"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <label>Invoice Date :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:TextBox TabIndex="3" Rows="1" ClientIDMode="Static" ID="txtInvoiceDate" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Invoice Date" CssClass="form-control pd-35"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <label>Order No :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:TextBox TabIndex="3" Rows="1" ClientIDMode="Static" ID="txtOrderNo" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Order No" CssClass="form-control pd-35"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <label>First Name :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:TextBox TabIndex="3" Rows="1" ClientIDMode="Static" ID="txtFirstName" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="First Name" CssClass="form-control pd-35"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <label>last Name :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:TextBox TabIndex="3" Rows="1" ClientIDMode="Static" ID="txtLastName" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Last Name" CssClass="form-control pd-35"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <label>Address 1 :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:TextBox TabIndex="3" Rows="1" ClientIDMode="Static" ID="txtAddress1" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Address 1" CssClass="form-control pd-35"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <label>Address 2 :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:TextBox TabIndex="3" Rows="1" ClientIDMode="Static" ID="txtAddress2" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Address 2" CssClass="form-control pd-35"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <label>Address 3 :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:TextBox TabIndex="3" Rows="1" ClientIDMode="Static" ID="txtAddress3" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Address 3" CssClass="form-control pd-35"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <label>City :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:TextBox TabIndex="3" Rows="1" ClientIDMode="Static" ID="txtCity" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="City" CssClass="form-control pd-35"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <label>State :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:TextBox TabIndex="3" Rows="1" ClientIDMode="Static" ID="txtState" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="State" CssClass="form-control pd-35"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <label>Picode :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:TextBox TabIndex="3" Rows="1" ClientIDMode="Static" ID="txtpincode" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Pincode" CssClass="form-control pd-35"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <label>Mobile Number :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:TextBox TabIndex="3" Rows="1" ClientIDMode="Static" ID="txtMobileNumber" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Mobile Number" CssClass="form-control pd-35"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <label>Email :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:TextBox TabIndex="3" Rows="1" ClientIDMode="Static" ID="txtEmail" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Email" CssClass="form-control pd-35"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <label>Make :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:TextBox TabIndex="3" Rows="1" ClientIDMode="Static" ID="txtMake" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Make" CssClass="form-control pd-35"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <label>Model :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:TextBox TabIndex="3" Rows="1" ClientIDMode="Static" ID="txtModel" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Model" CssClass="form-control pd-35"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <label>Ram :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:TextBox TabIndex="3" Rows="1" ClientIDMode="Static" ID="txtRam" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Ram" CssClass="form-control pd-35"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <label>Rom :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:TextBox TabIndex="3" Rows="1" ClientIDMode="Static" ID="txtRom" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Rom" CssClass="form-control pd-35"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <label>Color :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:TextBox TabIndex="3" Rows="1" ClientIDMode="Static" ID="txtColor" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Color" CssClass="form-control pd-35"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <label>IMEI No :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:TextBox TabIndex="3" Rows="1" ClientIDMode="Static" ID="txtIMEINo" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="IMEI No" CssClass="form-control pd-35"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <label>Price :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:TextBox TabIndex="3" Rows="1" ClientIDMode="Static" ID="txtPrice" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Price" CssClass="form-control pd-35"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-3">
                                <label>Invoice upload :</label>
                                <div class="input_field">
                                <asp:FileUpload ID="fuinvoiceimage" ClientIDMode="Static" runat="server" CssClass="file-simple" accept="image/*" />
                                <asp:Label Style="color: red!important; font-weight: bold!important; display: none!important; padding: 6px;"
                                    runat="server" ClientIDMode="Static" ID="lblInvoicealert">Please Select the File to Upload.</asp:Label>
                                </div>
                            </div>

                             <div class="col-md-3">
                                <label>IMEI Image :</label>
                                <div class="input_field">
                                <asp:FileUpload ID="fuImeiImage" ClientIDMode="Static" runat="server" CssClass="file-simple" accept="image/*" />
                                <asp:Label Style="color: red!important; font-weight: bold!important; display: none!important; padding: 6px;"
                                    runat="server" ClientIDMode="Static" ID="lblimeialert">Please Select the File to Upload.</asp:Label>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <label>Insurance Plan :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>
                                    <asp:TextBox TabIndex="3" Rows="1" ClientIDMode="Static" ID="txtInsurancePlan" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Insurance Plan" CssClass="form-control pd-35"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12 " style="text-align: center">
                                <asp:Button TabIndex="3" ID="btnClaimInsurance" OnClick="btnClaimInsurance_Click" OnClientClick="return ValidateGenerateInsurance();" runat="server" Text="Generate" CssClass="btn btn-primary"></asp:Button>
                                <asp:Button TabIndex="3" ID="btnResetInsurance" OnClick="btnResetInsurance_Click" Style="margin-left: 40px!important" runat="server" Text="Reset" CssClass="btn btn-danger"></asp:Button>
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
