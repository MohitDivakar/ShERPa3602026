<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="frmUploadMobexListing.aspx.cs" Inherits="ShERPa360net.UTILITY.frmUploadMobexListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Upload Listing On Mobex</title>

    <script type="text/javascript">
        function ValidateSalesImport() {
            var IsvalidateSalesImport = true;
            $("#lbluploadimportalert").css("display", "none");
            $("#lbluploadimportalert").text("Pls Select the File to Upload");

            if ($("#flUpload").get(0).files.length == 0) {
                $("#lbluploadimportalert").css("display", "block");
                IsvalidateSalesImport = false;
            }

            if ($("#flUpload").get(0).files.length != 0) {
                var isExcelorcsvfielfile = false;
                var filename = $("#flUpload").get(0).files[0].name;
                var fileextension = filename.split(".")[1];
                if (fileextension.includes("xlsx") || fileextension.includes("xls") || fileextension.includes("csv")) {
                    var isExcelorcsvfielfile = true;
                }

                if (isExcelorcsvfielfile == false) {
                    $("#lbluploadimportalert").text("Pls Select the only xlsx,xls,csv.");
                    $("#lbluploadimportalert").css("display", "block");
                    IsvalidateSalesImport = false;
                }
            }

            return IsvalidateSalesImport;
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Upload Listing on Mobex Website </strong></h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12" align="left">
                                    <div class="form-group">
                                        <label class="col-md-1 control-label">Select File : </label>
                                        <div class="col-md-9 col-xs-12">
                                            <asp:FileUpload runat="server" ClientIDMode="Static" ID="flUpload" multiple="false" />
                                            <asp:Label Style="color: red!important; font-weight: bold!important; display: none!important;" runat="server" ClientIDMode="Static" ID="lbluploadimportalert">Pls Select the File to Upload</asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12" style="margin-top: 10px;">
                                    <div class="form-group">
                                        <label align="left" class="col-md-1 control-label"></label>
                                        <div class="col-md-11 col-xs-12">
                                            <asp:LinkButton runat="server" ID="lnkUpload" CssClass="btn btn-primary pull-left" OnClientClick="return ValidateSalesImport();" OnClick="lnkUpload_Click" ValidationGroup="Upload">Upload</asp:LinkButton>
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
                        <asp:Label runat="server" ClientIDMode="Static" Style="margin-left: 11px!important; font-weight: bold; font-size: 14px;" Visible="false" ID="lblRecoretxt">Record:</asp:Label>
                        <asp:Label runat="server" ClientIDMode="Static" Style="font-weight: bold; font-size: 12px;" ID="lblRecord" Visible="false">0</asp:Label>


                        <asp:LinkButton runat="server" ID="lnkSave" CssClass="btn btn-success pull-right" OnClick="lnkSave_Click" Visible="false">Upload to Website</asp:LinkButton>


                        <div class="row" style="margin-top: 20px !important;">
                            <div class="col-md-12">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important; overflow-y: scroll;">
                                        <asp:GridView ID="grvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="ItemCode" HeaderText="SKU" />
                                                <asp:BoundField DataField="Qty" HeaderText="Stock Qty" />
                                                <asp:BoundField DataField="UploadQty" HeaderText="Upload Qty" />
                                                <asp:BoundField DataField="Price" HeaderText="Price" />
                                                <asp:BoundField DataField="Mrp" HeaderText="MRP" />
                                                <asp:BoundField DataField="Status" HeaderText="Status" />
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

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmUploadMobexListing" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmUtility" runat="server" />

</asp:Content>
