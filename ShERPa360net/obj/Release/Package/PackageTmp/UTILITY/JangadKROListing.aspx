<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="JangadKROListing.aspx.cs" Inherits="ShERPa360net.UTILITY.JangadKROListing" EnableEventValidation="false" %>

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
            margin-top: -50px !important;
            -ms-transform: scale(15); /* IE 9 */
            -webkit-transform: scale(15); /* Safari 3-8 */
            transform: scale(15);
            transform-origin: -1px;
        }

        .modal-h1 {
            margin-top: 5px !important;
            margin-bottom: 5px !important;
            font-size: 27px !important;
            font-weight: 400;
            color: #fff;
        }

        .pd-40 {
            padding-top: 40px;
            padding-bottom: 40px;
        }

        .pd-5 {
            padding-top: 5px;
        }

        .wdth-50 {
            width: 50%;
        }

        .btn-center {
            display: flex;
            justify-content: center;
        }
    </style>
    <script>
        function PoandPRFromInward() {
            var Isvalidate = true;
            $("#lblIMEINoalert").css("display", "none");
            if ($("#txtIMEINo").val().length > 0 && $("#txtIMEINo").val().length != 15) {
                $("#lblIMEINoalert").css("display", "block");
                Isvalidate = false;
            }
            return Isvalidate;
        }
        function IsAllowonlyNumericKeyIMEINo() {
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
                            <h3 class="panel-title"><strong runat="server" id="stProductDetail"><span class="fa fa-file"></span>&nbsp;Jangad Listing Detail</strong></h3>
                            <asp:LinkButton ID="imgReset" runat="server" CssClass="btn btn-success pull-right" OnClick="imgReset_Click" Text="Reset">
                            <span tooltip="Detail" flow="down"><i class="fa fa-undo"></i> </span></asp:LinkButton>
                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-right" Text="Export MR" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
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
                                                            <asp:TextBox TabIndex="3" onkeyup="IsAllowonlyNumericKeyIMEINo();" ClientIDMode="Static" ID="txtIMEINo" MaxLength="15" Style="font-weight: bold!important; color: black!important;" runat="server" placeholder="IMEI No" CssClass="form-control"></asp:TextBox>
                                                            <asp:Label ID="lblIMEINoalert" Style="color: red!important; display: none; padding: inherit; position: absolute; padding: 6px;"
                                                                runat="server" ClientIDMode="Static">Please Enter the IMEI No and should be 15 Digit</asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Listing. :</label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList TabIndex="2" ID="ddlListingType" runat="server" CssClass="form-control">
                                                                <asp:ListItem Text="ALL" Value="ALL" Selected="True"></asp:ListItem>
                                                                <asp:ListItem Text="RELIST" Value="RELIST"></asp:ListItem>
                                                                <asp:ListItem Text="RETURN" Value="RETURN"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>

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
                                                        <div class="col-md-12">
                                                            <div class="col-md-2">
                                                                <asp:CheckBox runat="server" ClientIDMode="Static" ID="chkSelectAll" AutoPostBack="true" OnCheckedChanged="chkSelectAll_CheckedChanged" Text="SelectAll" Style="font-weight: bold!important;" />
                                                            </div>

                                                            <div class="col-md-2">
                                                                <asp:DropDownList runat="server" Visible="false" ClientIDMode="Static" ID="ddlSelectFBALocation"></asp:DropDownList>
                                                            </div>

                                                            <div class="col-md-2">
                                                                <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlDownloadColumn" AutoPostBack="true" OnSelectedIndexChanged="ddlDownloadColumn_SelectedIndexChanged">
                                                                    <asp:ListItem Text="All Column" Value="1" Selected="True"></asp:ListItem>
                                                                    <asp:ListItem Text="Download Column" Value="2"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="col-md-3 text-center">
                                                                <asp:Button runat="server" OnClientClick="ShowProgressBaar();" Style="margin-bottom: 10px!important;" ID="lnkUpdateSelectedinFBA" OnClick="lnkUpdateSelectedinFBA_Click" ClientIDMode="Static" Text="Update All" CssClass="btn btn-success" />
                                                            </div>

                                                            <div class="col-md-3 text-center">
                                                                <asp:Button runat="server" OnClientClick="ShowProgressBaar();" Style="margin-bottom: 10px!important;" ID="lnkRemoveUpdateFromFBA" OnClick="lnkRemoveUpdateFromFBA_Click" ClientIDMode="Static" Text="Remove All FBA" CssClass="btn btn-success" />
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="row">
                                                        <fieldset class="scheduler-border">
                                                            <legend class="scheduler-border" id="lgrecordcount" runat="server"></legend>
                                                            <div class="col-md-12 divhorizontal" style="overflow-x: scroll!important;">
                                                                <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvProduct_RowDataBound" ShowHeaderWhenEmpty="true" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
                                                                    <EmptyDataTemplate>
                                                                        No Record Found!
                                                                    </EmptyDataTemplate>
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="Select" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:CheckBox runat="server" ID="chkSelection"></asp:CheckBox>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Action" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" Visible="false" ID="lblDCNO" Text='<%# Bind("DCNO") %>'></asp:Label>
                                                                                <asp:Label runat="server" Visible="false" ID="lblDcStatus" Text='<%# Bind("DCSTATUS") %>'></asp:Label>
                                                                                <asp:Label runat="server" Visible="false" ID="lblSchemeID" Text='<%# Bind("SCHEMEID") %>'></asp:Label>
                                                                                <asp:LinkButton Style="margin-top: 5px!important; margin-left: 2px!important;" runat="server" Visible="false" ID="btRtnDc" Text="Create RDC" CssClass="btn btn-primary" OnClick="btRtnDc_Click"></asp:LinkButton>
                                                                                <asp:LinkButton Style="margin-top: 5px!important; margin-left: 2px!important;" runat="server" ID="btnRtnDcprint" Visible="false" Text="Print" CssClass="btn btn-primary" OnClick="btnRtnDcprint_Click"></asp:LinkButton>
                                                                                <asp:LinkButton Style="margin-top: 5px!important; margin-left: 2px!important;" runat="server" ID="btnRtnDcView" Visible="false" Text="Print" CssClass="btn btn-primary" OnClick="btnRtnDcView_Click"></asp:LinkButton>
                                                                                <asp:LinkButton Style="margin-top: 5px!important; margin-left: 2px!important;" runat="server" ID="btReturn" Visible="false" Text="Return" CssClass="btn btn-primary" OnClick="btReturn_Click"></asp:LinkButton>
                                                                                <asp:LinkButton Style="margin-top: 5px!important; margin-left: 2px!important;" runat="server" ID="btnRemoveFromFBA" Visible="false" Text="Return FBA" CssClass="btn btn-primary" OnClick="btnRemoveFromFBA_Click"></asp:LinkButton>
                                                                                <asp:LinkButton Style="margin-top: 5px!important; margin-left: 2px!important;" runat="server" ID="btRelist" Visible="false" Text="Renewed" CssClass="btn btn-primary" OnClick="btRelist_Click"></asp:LinkButton>
                                                                                <asp:LinkButton Style="margin-top: 5px!important; margin-left: 2px!important;" runat="server" ID="btnQc" Visible="false" Text="Create PR & PO" CssClass="btn btn-primary" OnClick="btnQc_Click"></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="ID" Visible="true">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblID" Text='<%# Bind("ID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Job Id">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblJobId" Text='<%# Bind("JOBID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="FBA Location" Visible="false" ItemStyle-Width="200">
                                                                            <ItemTemplate>
                                                                                <asp:DropDownList runat="server" ClientIDMode="Static" ID="ddlFBALocation"></asp:DropDownList>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="FBA Location" Visible="true" ItemStyle-Width="200">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblFBALocation" Text='<%# Bind("FBALOCATION") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="FBA BY" Visible="true" ItemStyle-Width="200">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblFBABY" Text='<%# Bind("FBABY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="FBA Date" Visible="true" ItemStyle-Width="200">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblFBADate" Text='<%# Bind("FBADATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                       <asp:TemplateField HeaderText="FBA Deallot Date" Visible="true" ItemStyle-Width="200">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblDeAllotFBADate" Text='<%# Bind("FBAREALLOCATEDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="List Type">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblListingType" Text='<%# Bind("LISTINGTYPE") %>'></asp:Label>
                                                                                <asp:Label runat="server" ID="lblLISTINGTYPEID" Visible="false" Text='<%# Bind("LISTINGTYPEID") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <%--Regular Column --%>
                                                                        <asp:TemplateField HeaderText="Entry Date">
                                                                            <ItemTemplate>
                                                                                <asp:HiddenField runat="server" ID="hdFBLOCATIONID" Value='<%# Bind("FBALOCATIONID") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="hdID" Value='<%# Bind("ID") %>'></asp:HiddenField>
                                                                                <asp:HiddenField runat="server" ID="hdinwardechange" Value='<%# Bind("INWARDEDCHARGER") %>'></asp:HiddenField>
                                                                                <asp:Label runat="server" ID="lblDate" Text='<%# Bind("CREATEDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Picked up Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblPickedupdate" Text='<%# Bind("BLANCCOQCDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Inward Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblInwardDate" Text='<%# Bind("INWARDDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Due Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblActualDate" Text='<%# Bind("ACTUALCREATEDDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Jangad Remaining Days">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblJangadOldDays" Text='<%# Bind("JANGADOLDDAYS") %>'></asp:Label>
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
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Vendor Rate">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblVendorRate" Text='<%# Bind("VENDORPRICE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Blancco Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblBlanccoDate" Text='<%# Bind("BLANCCOQCDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Inward Result">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblInwardResult" Text='<%# Bind("INWARDRESULT") %>'></asp:Label>
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

                                                                        <asp:TemplateField HeaderText="Inward By">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblInwardBy" Text='<%# Bind("INWARDBY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Vendor Is KRO">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblLISTVENDORISKRO" Text='<%# Bind("LISTVENDORISKRO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Jangad Days Limit">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblJANGADMAXDAYLIMIT" Text='<%# Bind("JANGADMAXDAYLIMIT") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="KRO Days Limit">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblKROMAXDAYLIMIT" Text='<%# Bind("KROMAXDAYLIMIT") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Sales Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblSALESDATE" Text='<%# Bind("SALESDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="So No">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblSONO" Text='<%# Bind("SONO") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Item Code">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblItemCode" Text='<%# Bind("ITEMCODE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Renewed Date">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblRenewedDate" Text='<%# Bind("RENEWEDDATE") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Renewed BY">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblRenewedBy" Text='<%# Bind("RENEWEDBY") %>'></asp:Label>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:TemplateField HeaderText="Item Description">
                                                                            <ItemTemplate>
                                                                                <asp:Label runat="server" ID="lblItemDescription" Text='<%# Bind("ITEMDESC") %>'></asp:Label>
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
        <div class="modal-dialog   wdth-50">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <div class="twelve">
                        <h1 class="modal-h1">Relist Details</h1>
                    </div>

                </div>
                <div class="modal-body form_wrapper pd-40">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-2 pd-5">
                                <label>Extend Days :</label>
                            </div>
                            <div class="col-md-2">
                                <div class="input_field">
                                    <asp:HiddenField runat="server" ID="hdMake" ClientIDMode="Static" />
                                    <asp:HiddenField runat="server" ID="hdModel" ClientIDMode="Static" />
                                    <asp:HiddenField runat="server" ID="hdRam" ClientIDMode="Static" />
                                    <asp:HiddenField runat="server" ID="hdRom" ClientIDMode="Static" />
                                    <asp:HiddenField runat="server" ID="hdcolor" ClientIDMode="Static" />
                                    <asp:HiddenField runat="server" ID="hdGrade" ClientIDMode="Static" />
                                    <asp:HiddenField runat="server" ID="hdLockAmount" ClientIDMode="Static" />
                                    <asp:HiddenField runat="server" ID="hdprimarykey" ClientIDMode="Static" />
                                    <asp:DropDownList TabIndex="2" ID="ddlmaxday" ClientIDMode="Static" runat="server" CssClass="form-control required_text_box"></asp:DropDownList>
                                    <asp:Label ID="lblExtenddaysalert" Style="color: red!important; display: none; position: absolute;"
                                        runat="server" ClientIDMode="Static">Please Select the Extend Days</asp:Label>
                                </div>
                            </div>

                            <div class="col-md-2 pd-5 ">
                                <label>Vendor Price :</label>
                            </div>

                            <div class="col-md-2">
                                <div class="input_field">
                                    <asp:TextBox runat="server" ID="txtVendorprice" ClientIDMode="Static" CssClass="form-control required_text_box"></asp:TextBox>
                                    <asp:Label ID="lblVendorpricealert" Style="color: red!important; display: none; position: absolute;"
                                        runat="server" ClientIDMode="Static">Please enter vendor price</asp:Label>
                                </div>
                            </div>

                            <div class="col-md-4 btn-center">
                                <asp:Button TabIndex="3" ID="btnExtendDays" OnClick="btnExtendDays_Click" OnClientClick="return ValidateExtendays();" runat="server" Text="Update" CssClass="btn btn-primary"></asp:Button>
                                <asp:Button TabIndex="3" ID="btnResetExtend" OnClick="btnResetExtend_Click" Style="margin-left: 40px!important" runat="server" Text="Reset" CssClass="btn btn-danger"></asp:Button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabCheckerid" value="tsmTranJangadlisting" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />
</asp:Content>
