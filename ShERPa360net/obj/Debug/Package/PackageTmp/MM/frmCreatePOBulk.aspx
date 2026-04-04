<%@ Page Title="Bulk PO" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="frmCreatePOBulk.aspx.cs" Inherits="ShERPa360net.MM.frmCreatePOBulk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Bulk PO</title>

    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            var fu = $("#ContentPlaceHolder1_fuInvDoc").val();
            var challan = $("#ContentPlaceHolder1_txtChalanNo").val();
            var pono = $("#ContentPlaceHolder1_txtPoNo").val();

            if (fu != "" && challan != "" && pono != "") {
                document.getElementById("busy-holder1").style.display = "";
                document.getElementById("ContentPlaceHolder1_lnkSave").style.display = "none";
            }
        }
    </script>

    <script lang="javascript" type='text/javascript'>
        function ValidateBulkSoCreation() {
            var Isvalidate = true;

            if ($("#fuImage").get(0).files.length == 0) {
                $("#lblfilealert").text("Please Select the File.");
                $("#lblfilealert").css("display", "block");
                Isvalidate = false;
            }

            if ($("#fuImage").get(0).files.length > 0) {
                var isimglfile = false;
                var filename = $("#fuImage").get(0).files[0].name;
                var fileextensionarray = filename.split(".");
                var fileextension = fileextensionarray[(fileextensionarray.length - 1)];
                if ((fileextension.toUpperCase()).includes("XLSX") || (fileextension.toUpperCase()).includes("XLS")) {
                    var isimglfile = true;
                }

                if (isimglfile == false) {
                    $("#lblfilealert").text("Please Select only Excel.");
                    $("#lblfilealert").css("display", "block");
                    Isvalidate = false;
                }
            }
            return Isvalidate;
        }

    </script>

    <style>
        img.file-preview-image {
            width: 100% !important
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
        /*  .chclass input {
                position: absolute;
                top: 10px;
                left: 10px;
            }*/
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Upload PO</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">

                                        <div class="col-md-6">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">File : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:FileUpload ID="fuImage" ClientIDMode="Static" runat="server" CssClass="file-simple" />
                                                        <asp:Label Style="color: red!important; font-weight: bold!important; display: none!important; padding: 6px;"
                                                            runat="server" ClientIDMode="Static" ID="lblfilealert">Please Select the File to Upload.</asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <asp:LinkButton ID="btnUpload" runat="server" CssClass="btn btn-success" Text="Save All" OnClick="btnUpload_Click" OnClientClick="return ValidateBulkSoCreation();">Upload</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <asp:LinkButton ID="lnkDownload" runat="server" CssClass="btn btn-success" Text="Download Template" OnClick="lnkDownload_Click">Download Template</asp:LinkButton>
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
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="form-horizontal">
                            <div class="panel panel-default">
                                <div class="panel-body">




                                    <asp:HiddenField ID="hfPONo" runat="server" />
                                    <asp:HiddenField ID="hfPRNo" runat="server" />
                                    <asp:HiddenField ID="hfGRNNo" runat="server" />
                                    <asp:HiddenField ID="hfPBNo" runat="server" />



                                    <div class="col-md-12">
                                        <div class="row">
                                            <div class="col-md-3">
                                                <div class="col-md-10">
                                                    <div class="form-group">
                                                        <asp:Label runat="server" ClientIDMode="Static" Style="margin-left: 11px!important; font-weight: bold; font-size: 14px;" Visible="false" ID="lblRecoretxt">Record:</asp:Label>
                                                        <asp:Label runat="server" ClientIDMode="Static" Style="font-weight: bold; font-size: 12px;" ID="lblRecord" Visible="false">0</asp:Label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-7">
                                                <div class="col-md-12">
                                                    <div class="form-group">
                                                        <asp:CheckBox ID="chkOnlyPO" runat="server" CssClass="chclass pull-right" Text="Only PO" Visible="false" Checked="true" />
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-md-2">
                                                <div class="col-md-10">
                                                    <div class="form-group">
                                                        <asp:LinkButton runat="server" ID="lnkSave" CssClass="btn btn-success pull-right" OnClick="lnkSave_Click" Visible="false" OnClientClick="ShowLoading()">Import</asp:LinkButton>
                                                        <div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                                            <img id="img1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                                            <label>Please wait...</label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <div class="row" style="margin-top: 20px !important;">
                                        <div class="col-md-12">
                                            <asp:GridView ID="gvData" runat="server" CssClass="table table-hover table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                                EmptyDataText="No records has been added.">
                                                <Columns>

                                                    <asp:TemplateField HeaderText="Vendor Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVendorCode" runat="server" Text='<%# Eval("Vendor Code") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Tran Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTranCode" runat="server" Text='<%# Eval("Tran Code") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Eval("Item Code") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblQty" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRate" runat="server" Text='<%# Eval("Rate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Discount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDiscount" runat="server" Text='<%# Eval("Discount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Net Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNetAmount" runat="server" Text='<%# Eval("Net Amount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Tax Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTaxRate" runat="server" Text='<%# Eval("Tax Rate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Tax Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTaxAmount" runat="server" Text='<%# Eval("Tax Amount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Total Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalAmount" runat="server" Text='<%# Eval("Total Amount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Plant">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPlant" runat="server" Text='<%# Eval("Plant") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Location">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("Location") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Job ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblJobID" runat="server" Text='<%# Eval("Job ID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="IMEI">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIMEI" runat="server" Text='<%# Eval("IMEI") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Remarks">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Eval("Remarks") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bill No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBillNo" runat="server" Text='<%# Eval("Bill No") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bill Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBillDate" runat="server" Text='<%# Eval("Bill Date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Bill Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBillAmount" runat="server" Text='<%# Eval("Bill Amount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Pay Terms">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPayTerms" runat="server" Text='<%# Eval("Pay Terms") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Department">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDepartment" runat="server" Text='<%# Eval("Department") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Purchase Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPurchaseType" runat="server" Text='<%# Eval("Purchase Type") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Cost Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCostCenter" runat="server" Text='<%# Eval("Cost Center") %>'></asp:Label>
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

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmTranMMBulkPOUpload" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />

</asp:Content>
