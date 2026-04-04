<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="PickedUpProductReturn.aspx.cs" Inherits="ShERPa360net.UTILITY.PickedUpProductReturn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong runat="server" id="stProductDetail"><span class="fa fa-file"></span>&nbsp;Picked Up Product Return</strong></h3>
                            <asp:LinkButton ID="imgReset" runat="server" CssClass="btn btn-success pull-right" Text="Reset" OnClick="imgReset_Click"><span tooltip="Detail" flow="down"><i class="fa fa-undo"></i> </span></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkSearh" CssClass="btn btn-success pull-right" Text="Search" OnClientClick="return ProductReturnDetail()" ValidationGroup="SearchDetail" OnClick="lnkSearh_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
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
                                                        <label class="col-md-5 control-label">Status. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList ID="ddlStatus" ClientIDMode="Static" runat="server" Width="170" CssClass="form-control" Enabled="false">
                                                                <asp:ListItem Text="ORDERRECEIVED" Selected="True" Value="11301"></asp:ListItem>

                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>


                                            </div>

                                            <div class="col-md-12">

                                                <div class="col-md-3">

                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label" style="padding-top: 10px">IMEI No :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox TabIndex="3" onkeyup="IsAllowonlyNumericKeyIMEINO();" ClientIDMode="Static" ID="txtIMEINumber" MaxLength="15" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="IMEI No" CssClass="form-control"></asp:TextBox>
                                                            <asp:Label ID="lblIMEINoalert" Style="color: red!important; display: none; padding: inherit; position: absolute; padding: 6px;"
                                                                runat="server" ClientIDMode="Static">Please Enter the IMEI No and should be 15 Digit</asp:Label>
                                                        </div>
                                                    </div>
                                                </div>


                                                <div class="col-md-3">

                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label" style="padding-top: 10px">List Type :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList TabIndex="2" ID="ddllistype" runat="server" CssClass="form-control"></asp:DropDownList>
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
                                                                        <asp:TemplateField HeaderText="Print">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton Style="margin-top: 5px!important; margin-left: 64px!important;" runat="server" ID="btnPrint" Text="Print" CssClass="btn btn-primary"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--Action Column --%>


                                                                        <%--Action Column --%>
                                                                        <asp:TemplateField HeaderText="Return">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton Style="margin-top: 5px!important; margin-left: 64px!important;" runat="server" ID="btnReturn" Text="Return" CssClass="btn btn-primary" OnClick="btnReturn_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--Action Column --%>

                                                                        <%--Action Column --%>
                                                                        <asp:TemplateField HeaderText="HandOver To BDO">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton Style="margin-top: 5px!important; margin-left: 64px!important;" runat="server" ID="btnHandoverToBDO" Text="BDO Handover" CssClass="btn btn-primary"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--Action Column --%>

                                                                        <%--Regular Column --%>

                                                                        <%--Regular Column --%>
                                                                        <asp:TemplateField HeaderText="Entry Date">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField runat="server" ID="hdID" Value='<%# Bind("ID") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="hdVendorId" Value='<%# Bind("VENDORID") %>'></asp:HiddenField>
                                                                                <asp:Label runat="server" ID="lblDate" Text='<%# Bind("CREATEDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="ID">
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
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Vendor Rate">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVendorRate" Text='<%# Bind("VENDORPRICE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="IMEINo" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblIMEINo" Text='<%# Bind("IMEINO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Status">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblStatus" Text='<%# Bind("STATUS") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Return Reason" Visible="false">
                                                                            <ItemTemplate>

                                                                                <asp:Label runat="server" ID="lblBikerContactNo" Visible="false" Text='<%# Bind("BIKERCONTACT") %>'></asp:Label>
                                                                                <asp:Label runat="server" ID="lblAsmContactNo" Visible="false" Text='<%# Bind("ASMCONTACT") %>'></asp:Label>
                                                                                <asp:Label runat="server" ID="lblJpBhaiContactNo" Visible="false" Text='<%# Bind("JPBHAICONTACT") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Contact No">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDealerContactNo" Text='<%# Bind("DEALERCONTACTNO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Contact No2">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDealerContactNo2" Text='<%# Bind("DEALERCONTACTNO2") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Contact No3">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDealerContactNo3" Text='<%# Bind("DEALERCONTACTNO3") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="LISTINGTYPE">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblListingType" Text='<%# Bind("LISTINGTYPE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>


                                                                        <asp:TemplateField HeaderText="LISTVENDORISKRO">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblListVendorIsKro" Text='<%# Bind("LISTVENDORISKRO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <%--Regular Column --%>

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
    <style>
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
            margin-bottom: 15px;
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
            width: 650px;
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
    <div class="modal fade" id="modal-detail" data-backdrop="static">
        <div class="modal-dialog modal-lg wdth-750">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <div class="twelve">
                        <h1>Return Details</h1>
                    </div>

                </div>
                <div class="modal-body form_wrapper">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-6">

                                <label>Make :</label>
                                <div class="input_field">
                                    <a><i class='bx bx-edit form-bx'></i></a>
                                    <asp:TextBox TabIndex="1" Enabled="false" ClientIDMode="Static" ID="txtMake" Style="font-weight: bold!important; color: black!important;" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6">

                                <label>Model :</label>
                                <div class="input_field">
                                    <a><i class='bx bx-cube-alt form-bx'></i></a>
                                    <asp:TextBox TabIndex="2" Enabled="false" ClientIDMode="Static" ID="txtModel" Style="font-weight: bold!important; color: black!important;" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-6">

                                <label>Ram :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-memory-card form-bx'></i></a>
                                    <asp:TextBox TabIndex="3" Enabled="false" ClientIDMode="Static" ID="txtRam" Style="font-weight: bold!important; color: black!important;" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6">

                                <label>Rom :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-memory-card form-bx'></i></a>
                                    <asp:TextBox TabIndex="4" Enabled="false" ClientIDMode="Static" ID="txtRom" Style="font-weight: bold!important; color: black!important;" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">

                                <label>Color :</label>
                                <div class="input_field">
                                    <a><i class='bx bx-palette form-bx'></i></a>
                                    <asp:TextBox TabIndex="5" Enabled="false" ClientIDMode="Static" ID="txtColor" Style="font-weight: bold!important; color: black!important;" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6">

                                <label>Vendor :</label>
                                <div class="input_field">
                                    <a><i class='bx bx-user form-bx'></i></a>
                                    <asp:TextBox TabIndex="5" Enabled="false" ClientIDMode="Static" ID="txtVendorDetail" Style="font-weight: bold!important; color: black!important;" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                        </div>
                        <div class="row"></div>
                        <div class="row">
                            <div class="col-md-6">

                                <label>IMEI No :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-report'></i></a>
                                    <asp:TextBox TabIndex="7" Enabled="false" ClientIDMode="Static" ID="txtIMEINo" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="IMEI No" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>OTP :</label>
                                    <asp:TextBox TabIndex="3" ClientIDMode="Static" ID="txtHandoverReturnBDOOTP" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="OTP" CssClass="form-control"></asp:TextBox>
                                    <asp:Label ID="lblalertotp" Style="color: red!important; display: none;" runat="server" ClientIDMode="Static">Please Enter the OTP</asp:Label>
                                    <asp:Label ID="lblvalidotpalert" Style="color: red!important;" Visible="false" runat="server" ClientIDMode="Static">Invalid OTP Please enter valid OTP.</asp:Label>
                                </div>
                            </div>

                            <div class="col-md-6">

                                <label>Return Reason :</label>
                                <div class="input_field">
                                    <a><i class='bx bx-bulb'></i></a>
                                    <asp:TextBox TabIndex="3" Rows="1" ClientIDMode="Static" ID="txtReturnReason" Style="font-weight: bold!important; color: black!important;" runat="server" TextMode="MultiLine" placeholder="Fail Reason" CssClass="form-control pd-35"></asp:TextBox>
                                    <asp:Label ID="lblReturnReasonalert" Style="color: red!important; display: none; position: absolute" runat="server" ClientIDMode="Static">Please Enter the Reason</asp:Label>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-12 text-center">
                                <div class="col-md-12" style="margin-top: 10px!important;">
                                    <asp:HiddenField runat="server" ID="hdprimarykey" />
                                    <asp:HiddenField runat="server" ID="hdBikerContactNo" />
                                    <asp:HiddenField runat="server" ID="hdAsmContactNo" />
                                    <asp:HiddenField runat="server" ID="hdJpBhaiContactNo" />
                                    <asp:HiddenField runat="server" ID="hdVendorID" />
                                    <asp:HiddenField runat="server" ID="hdDealerContactNo" />
                                    <asp:HiddenField runat="server" ID="hdDealerContactNo2" />
                                    <asp:HiddenField runat="server" ID="hdDealerContactNo3" />
                                    <asp:Button TabIndex="3" ID="btnSaveReturn" OnClientClick="return ValidateProductPickedupReturn();" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="btnSaveReturn_Click"></asp:Button>
                                    <asp:Button TabIndex="3" ID="btnResetReturn" Style="margin-left: 40px!important" runat="server" Text="Reset" CssClass="btn btn-danger"></asp:Button>
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
    <input type="hidden" id="menutabCheckerid" value="tsmMobexSellerBDOVendorProductReturn" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />
</asp:Content>
