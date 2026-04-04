<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="ProductBlanccoQcApproveDetail.aspx.cs" Inherits="ShERPa360net.UTILITY.ProductBlanccoQcApproveDetail" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .Lstbtn {
            /*width: 70px;*/
            background-color: #3d1971;
            color: white;
        }

        .custom-file-input::-webkit-file-upload-button {
            visibility: hidden;
        }

        .custom-file-input::before {
            content: ' Scan ';
            display: inline-block;
            background: linear-gradient(top, #3d1971, #3d1971);
            background-color: #3d1971;
            color: white;
            border: 1px solid #999;
            border-radius: 3px;
            /*padding: 5px 8px;*/
            outline: none;
            /*white-space: nowrap;*/
            -webkit-user-select: none;
            cursor: pointer;
            /*text-shadow: 1px 1px #fff;*/
            font-weight: 700;
            font-size: 10pt;
            height: 34px;
            width: 40px;
        }

        .custom-file-input:hover::before {
            border-color: black;
        }

        .custom-file-input:active::before {
            background: -webkit-linear-gradient(top, #3d1971, #3d1971);
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
                            <h3 class="panel-title"><strong runat="server" id="stProductDetail"><span class="fa fa-file"></span>&nbsp;Product Qc Approve Detail</strong></h3>
                            <%--                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-right" Text="Export Report" OnClick="lnkExport_Click"><i class="fa fa-file"></i></asp:LinkButton>--%>
                            <asp:LinkButton ID="imgReset" runat="server" CssClass="btn btn-success pull-right" OnClick="imgReset_Click" Text="Reset"><span tooltip="Detail" flow="down"><i class="fa fa-undo"></i> </span></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkSearh" CssClass="btn btn-success pull-right" Text="Search" OnClick="lnkSearh_Click" ValidationGroup="SearchDetail"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
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
                                                                <%--OnRowCreated="gvProduct_RowCreated"--%>
                                                                <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
                                                                    <EmptyDataTemplate>
                                                                        No Record Found!
                                                                    </EmptyDataTemplate>
                                                                    <Columns>
                                                                        <%--Action Column --%>
                                                                        <asp:TemplateField HeaderText="Final Qc">
                                                                            <ItemTemplate>
                                                                                <%--<asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%#Eval("ID") %>'
                                                                                                                CommandName="eEdit">
                                                                                                                Edit</asp:LinkButton>--%>
                                                                                <asp:LinkButton Style="margin-top: 5px!important; margin-left: 64px!important;" runat="server" ID="btnQc" Text="Blancco Qc" CssClass="btn btn-primary" OnClick="btnQc_Click"></asp:LinkButton>
                                                                                <asp:LinkButton Style="margin-top: 5px!important; margin-left: 10px!important;" runat="server" ID="btnUnlist" Text="Unlist" OnClientClick="return confirm('Are you sure you want to unlist this Model?');" CssClass="btn btn-primary" OnClick="btnUnlist_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--Action Column --%>

                                                                        <%--Regular Column --%>

                                                                        <%--Regular Column --%>
                                                                        <asp:TemplateField HeaderText="Entry Date">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField runat="server" ID="hdID" Value='<%# Bind("ID") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="hdVendorId" Value='<%# Bind("VENDORID") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="hdListingType" Value='<%# Bind("LISTINGTYPEID") %>'></asp:HiddenField>
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

                                                                        <asp:TemplateField HeaderText="Mobex Rate">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblMobexRate" Text='<%# Bind("MOBEXPRICE") %>'></asp:Label>
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

                                                                        <asp:TemplateField HeaderText="Reserved By" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblReservedBy" Text='<%# Bind("RESERVEDBY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Reserved Date" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblReservedDate" Text='<%# Bind("RESERVEDDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                       <asp:TemplateField HeaderText="LISTINGTYPE" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblListingType" Text='<%# Bind("LISTINGTYPE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="LISTVENDORISKRO" Visible="true">
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
    <div class="modal fade" id="modal-detail" data-backdrop="static">
        <div class="modal-dialog modal-lg wdth-750">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>

                    <div class="twelve">
                        <h1>Final QC Details</h1>
                    </div>
                </div>

                <div class="modal-body form_wrapper">
                    <div class="box-body">
                        <div class="row">

                            <div class="col-md-6">
                                <label class="lbl-style">Blancco Result :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-report'></i></a>
                                    <asp:DropDownList ID="ddlBlanccoQcResult" onchange="EnableDisableCtrl();" ClientIDMode="Static" runat="server" CssClass="form-control" Style="padding-left: 35px;">
                                        <asp:ListItem Text="SELECT" Selected="True" Value="SELECT"></asp:ListItem>
                                        <asp:ListItem Text="PASS" Value="PASS"></asp:ListItem>
                                        <asp:ListItem Text="FAIL" Value="FAIL"></asp:ListItem>
                                        <asp:ListItem Text="NOTDONE" Value="NOTDONE"></asp:ListItem>
                                        <asp:ListItem Text="NOTAVAILABLE" Value="NOTAVAILABLE"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblBlanccoQcResultalert" Style="color: red!important; display: none; position: absolute; padding: 6px;"
                                        runat="server" ClientIDMode="Static">Please Select the Blancco Result</asp:Label>

                                </div>
                            </div>



                            <div class="col-md-6">

                                <label class="lbl-style">Reason :</label>
                                <div class="input_field">
                                    <a><i class='bx bx-bulb'></i></a>

                                    <asp:TextBox TabIndex="3" Rows="1" ClientIDMode="Static" ID="txtBlanccoReason" Style="font-weight: bold!important; color: black!important;" runat="server" TextMode="MultiLine" placeholder="Fail Reason" CssClass="form-control pd-35"></asp:TextBox>
                                    <asp:Label ID="lblBlanccoReasonalert" Style="color: red!important; display: none; position: absolute; padding: 6px;"
                                        runat="server" ClientIDMode="Static">Please Enter the Reason</asp:Label>
                                </div>

                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label class="lbl-style">Final Result :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-book-content'></i></a>

                                    <asp:DropDownList ID="ddlQcResult" ClientIDMode="Static" runat="server" CssClass="form-control pd-35">
                                        <asp:ListItem Text="SELECT" Selected="True" Value="SELECT"></asp:ListItem>
                                        <asp:ListItem Text="PASS" Value="PASS"></asp:ListItem>
                                        <asp:ListItem Text="FAIL" Value="FAIL"></asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lblQcResultalert" Style="color: red!important; display: none; position: absolute; padding: 6px;"
                                        runat="server" ClientIDMode="Static">Please Select the Final Result</asp:Label>
                                </div>
                            </div>

                            <div class="col-md-6">
                                <label class="lbl-style">Reason :</label>
                                <div class="input_field">
                                    <a><i class='bx bx-book-open'></i></a>

                                    <asp:TextBox TabIndex="3" Rows="1" ClientIDMode="Static" ID="txtReason" Style="font-weight: bold!important; color: black!important;" runat="server" TextMode="MultiLine" placeholder="Fail Reason" CssClass="form-control pd-35"></asp:TextBox>
                                    <asp:Label ID="lblReasonalert" Style="color: red!important; display: none; position: absolute; padding: 6px;"
                                        runat="server" ClientIDMode="Static">Please Enter the Reason</asp:Label>
                                </div>
                            </div>

                        </div>
                        <div class="row">

                            <div class="col-md-6">
                                <label class="lbl-style" style="padding-top: 10px">IMEI No :</label>
                                <div class="input_field">
                                    <a><i class='bx bx-list-ol'></i></a>
                                    <asp:TextBox TabIndex="3" onkeyup="IsAllowonlyNumericIMEINO();" ClientIDMode="Static" ID="txtIMEINo" MaxLength="15" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="IMEI No" CssClass="form-control" onfocusout="ValidateBlanccoIMEINo()"></asp:TextBox>
                                    <asp:Label ID="lblIMEINoalert" Style="color: red!important; display: none; position: absolute; padding: 6px;"
                                        runat="server" ClientIDMode="Static">Please Enter the IMEI No and should be 15 Digit</asp:Label>
                                    <asp:Label Style="color: red!important; font-weight: bold!important; display: none!important;position: absolute; padding: 6px;" runat="server" ClientIDMode="Static" ID="lblimei">IMEI NO. already exists.</asp:Label>

                                </div>
                            </div>

                            <div class="col-md-6">

                                <label class="lbl-style">Purchase Rate :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-purchase-tag-alt'></i></a>
                                    <asp:TextBox TabIndex="3" ClientIDMode="Static" ID="txtPurchaseRate" MaxLength="15" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="Purchase Rate" CssClass="form-control"></asp:TextBox>
                                    <asp:Label ID="lblPurchasealert" Style="color: red!important; display: none; position: absolute; padding: 6px;"
                                        runat="server" ClientIDMode="Static">Please Enter the Purchase Rate</asp:Label>
                                    <asp:FileUpload ID="fuSerialNo" runat="server" CssClass="custom-file-input" Height="25" Width="40" ClientIDMode="Static" Style="display: none!important;" />
                                    <asp:Button runat="server" ClientIDMode="Static" ID="btnUploadScan" OnClick="btnUploadScan_Click" Style="display: none!important;" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <label class="lbl-style">Final Grade :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-cube-alt'></i></a>
                                    <asp:DropDownList ID="ddlBlanccoGrade" ClientIDMode="Static" runat="server" CssClass="form-control pd-35">
                                        <asp:ListItem Text="SELECT" Selected="True" Value="SELECT"></asp:ListItem>
                                        <asp:ListItem Text="A" Value="A"></asp:ListItem>
                                        <asp:ListItem Text="B" Value="B"></asp:ListItem>
                                        <asp:ListItem Text="C" Value="C"></asp:ListItem>

                                    </asp:DropDownList>
                                    <asp:Label ID="lblBlanccoGradealert" Style="color: red!important; display: none; position: absolute; padding: 6px;"
                                        runat="server" ClientIDMode="Static">Please Select the Final Grade</asp:Label>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <label class="lbl-style">Vendor :</label>
                                <div class="input_field">
                                    <a><i class='bx bxs-user-pin'></i></a>
                                    <asp:DropDownList ID="ddlDealerVendor" ClientIDMode="Static" runat="server" CssClass="form-control pd-35">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblDealerVendoralert" Style="color: red!important; display: none; position: absolute; padding: 6px;"
                                        runat="server" ClientIDMode="Static">Please Select the Vendor</asp:Label>
                                </div>
                            </div>

                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="input_field">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label class="lbl-style pt-4" style="padding-top: 10px">IMEI Image:</label>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:FileUpload ID="fuImeiImage" ClientIDMode="Static" runat="server" CssClass="file-simple" />
                                            <asp:Label Style="color: red!important; font-weight: bold!important; display: none!important; padding: 6px;"
                                                runat="server" ClientIDMode="Static" ID="lblimeialert">Please Select the File to Upload.</asp:Label>


                                        </div>
                                    </div>

                                </div>
                            </div>

                            <div class="col-md-6">
                                <div class="input_field">
                                    <div class="row">
                                        <div class="col-md-4">
                                            <label class="lbl-style pt-4" style="padding-top: 10px">Invoice Image:</label>
                                        </div>
                                        <div class="col-md-7">
                                            <asp:FileUpload ID="fuinvoiceimage" ClientIDMode="Static" runat="server" CssClass="file-simple" accept="image/*" />
                                            <asp:Label Style="color: red!important; font-weight: bold!important; display: none!important; padding: 6px;"
                                                runat="server" ClientIDMode="Static" ID="lblInvoicealert">Please Select the File to Upload.</asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>
                        <div class="row">

                            <div class="col-md-12 text-center">

                                <div class="col-md-12 button-style" style="margin-top: 10px!important;">
                                    <asp:HiddenField runat="server" ID="hdprimarykey" />
                                    <asp:HiddenField runat="server" ID="hddelaerid" />
                                    <asp:HiddenField runat="server" ID="hdlisttype" />

                                    <asp:Button TabIndex="3" ID="btnSaveQc" OnClick="btnSaveQc_Click" OnClientClick="return ValidateBlanccoQcResult();" runat="server" Text="Update" CssClass="btn btn-primary btn-1"></asp:Button>
                                    <asp:Button TabIndex="3" ID="btnResetQc" OnClick="btnResetQc_Click" Style="margin-left: 40px!important" runat="server" Text="Reset" CssClass="btn btn-danger btn-1"></asp:Button>
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
    <input type="hidden" id="menutabCheckerid" value="tsmMobexSellerBlanccoQc" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />
</asp:Content>
