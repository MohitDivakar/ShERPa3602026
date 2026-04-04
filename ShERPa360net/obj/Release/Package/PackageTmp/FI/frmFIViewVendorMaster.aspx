<%@ Page Title="" Language="C#" MasterPageFile="~/FI/MasterFI.Master" AutoEventWireup="true" CodeBehind="frmFIViewVendorMaster.aspx.cs" Inherits="ShERPa360net.FI.frmFIViewVendorMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <title>Vendor Master </title>


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
            top: -1px !important;
            /*background-color: #3D2A70 !important;
            color: white !important;*/
        }
    </style>

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


    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />


    <script>
        $(document).ready(function () {
           BindMakeAssociateModel();
            //$("#myInput").on("keyup", function () {
            //    var value = $(this).val().toLowerCase();
            //    $("#ContentPlaceHolder1_gvList tr ").filter(function () {
            //        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            //    });
            //});
        });

        function BindMakeAssociateModel() {
            if ($("#contentplaceholder1_gvlist tr").length > 2) {
                $("#contentplaceholder1_gvlist").datatable({
                    dom: 'bfrtip',
                    buttons: [
                        {
                            extend: 'collection',
                            text: 'export',
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>Registered Vendor</h3>
                        </div>

                        <div class="panel-body">

                            <div class="row">

                                <div class="col-md-12">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Date From : </label>
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
                                </div>

                                <div class="col-md-12" style="padding-top: 10px !important;">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Vend. Code : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtVendCode" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Vend. Name : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtVendName" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Dealer Name : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlDealer" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Approved : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:CheckBox ID="chkApproved" runat="server" Checked="true" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                </div>

                                <div class="col-md-12" style="padding-top: 10px !important;">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">City : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Vend. Type : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlVendType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Vend. Cat. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlVendCat" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkNewVend" CssClass="btn btn-success pull-left" Text="New Vend" PostBackUrl="~/FI/frmFIVendMaster.aspx"><span tooltip="Add New" flow="down"><i class="fa fa-plus"  aria-hidden="true"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkSearhVend" CssClass="btn btn-success pull-left" Text="Search Vendor" OnClick="lnkSearhVend_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export Vendor List" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
                                        </div>
                                    </div>


                                </div>
                                <div id="mobile_View">
                                    <div class="col-md-1" style="margin-top: 5px;">&nbsp;</div>
                                    <div class="col-md-3" style="margin-top: 5px;">
                                        <div class="form-group">
                                            <div class="col-md-9 col-xs-12">
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
                <div class="form-horizontal">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Vendor List</strong></h3>
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
                                                            <asp:BoundField DataField="TALLYVENDOR" HeaderText="Tally Vend. Created" />
                                                            <asp:TemplateField HeaderText="Action" Visible="True">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="btnDetails" Text="Details" OnClick="btnDetails_Click"></asp:LinkButton>
                                                                    | 
                                                        <asp:LinkButton runat="server" ID="btnImage" Text="Image" OnClick="btnImage_Click"></asp:LinkButton>
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
                                        <asp:HiddenField ID="hflblVendCode" runat="server" />
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



                            </div>

                            <div class="col-md-12">


                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Title :</label>
                                        <asp:Label ID="lblTitle" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-9">
                                    <div class="form-group">
                                        <label>Vendor Name :</label>
                                        <asp:Label ID="lblVendorName" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>



                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Contact Person :</label>
                                        <asp:Label ID="lblContactPerson" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Contact No. :</label>
                                        <asp:Label ID="lblContactNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label>Dealer :</label>
                                        <asp:Label ID="lblDealer" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Mobile No. :</label>
                                        <asp:Label ID="lblMobileNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Email ID :</label>
                                        <asp:Label ID="lblEmailID" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Website :</label>
                                        <asp:Label ID="lblWebsite" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Status :</label>
                                        <asp:Label ID="lblStatus" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>


                            </div>

                            <div class="col-md-12">
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label>Address :</label>
                                        <asp:Label ID="lblAddress1" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <%--<div class="col-md-3">
                                    <div class="form-group">
                                        <label>Address 2 :</label>
                                        <asp:Label ID="lblAddress2" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>--%>

                                <%--<div class="col-md-3">
                                    <div class="form-group">
                                        <label>Address 3 :</label>
                                        <asp:Label ID="lblAddress3" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>--%>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>City :</label>
                                        <asp:Label ID="lblCity" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

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
                                        <label>GSTIN :</label>
                                        <asp:Label ID="lblGSTIN" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>PAN :</label>
                                        <asp:Label ID="lblPAN" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Aadhar :</label>
                                        <asp:Label ID="lblAadharno" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>


                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Bank Name :</label>
                                        <asp:Label ID="lblBankName" runat="server" CssClass="form-control"></asp:Label>
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
                                        <label>Tally Vendor :</label>
                                        <asp:DropDownList ID="ddlTally" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlTally_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Text="No" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>


                            </div>

                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Aprv. By :</label>
                                        <asp:Label ID="lblAprvBy" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Aprv. Date :</label>
                                        <asp:Label ID="lblAprvDate" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Reject Reason :</label>
                                        <asp:Label ID="lblRejectReason" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12" style="margin-top: 10px !important;">
                                <div class="col-md-12">
                                    <asp:GridView ID="grvCommunication" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                        CssClass="table table-hover table-striped table-bordered nowrap">
                                        <EmptyDataTemplate>
                                            No Record Found!
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Designation">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDesignation" runat="server" Text='<%# Eval("DESIGNATION") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblContName" runat="server" Text='<%# Eval("CONTNAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Contact No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblContNo" runat="server" Text='<%# Eval("CONTNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Email">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblContEmail" runat="server" Text='<%# Eval("CONTEMAIL") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>




                            <div id="dialog" style="display: none"></div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>


    <div class="modal fade" id="modal-image" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Vendor</strong> Document Images</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">

                            <div class="col-md-12" style="margin-top: 10px !important;">
                                <div class="col-md-6">
                                      <asp:HiddenField ID="hfVendCode" runat="server" />
                                    <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                        CssClass="table table-hover table-striped table-bordered nowrap" OnRowDataBound="gvDetail_RowDataBound">
                                        <EmptyDataTemplate>
                                            No Record Found!
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:TemplateField HeaderText="Image Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblImageType" runat="server" Text='<%# Eval("IMAGETYPE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Image" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblImageData" runat="server" Text='<%# Eval("IMAGE") %>' Visible="false"></asp:Label>

                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Image Type">
                                                <ItemTemplate>
                                                    <asp:Image ID="imgImage" runat="server" Height="50" Width="50" CssClass="zoom" AlternateText='<%# Eval("IMAGETYPE") %>' Visible="true" />
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
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmMstMMVend" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />

</asp:Content>