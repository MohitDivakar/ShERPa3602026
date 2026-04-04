<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="SkuUpload.aspx.cs" Inherits="ShERPa360net.REPORTS.SkuUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script lang="javascript" type='text/javascript'>
        function ValidateAsinCreation()
        {
            debugger
            var Isvalidate = true;
            $("#lblfilealert").css("display", "none");
            if ($("#skufile").get(0).files.length == 0) {
                $("#lblfilealert").css("display", "block");
                Isvalidate = false;
            }
            if ($("#skufile").get(0).files.length > 0) {
                var isimglfile = false;
                var filename = $("#skufile").get(0).files[0].name;
                var fileextensionarray = filename.split(".");
                var fileextension = fileextensionarray[(fileextensionarray.length - 1)];
                if ((fileextension.toUpperCase()).includes("XLSX") || (fileextension.toUpperCase()).includes("XLS")) {
                    var isimglfile = true;
                }

                if (isimglfile == false) {
                    $("#lblfilealert").text("Please Select the only Excel.");
                    $("#lblfilealert").css("display", "block");
                    Isvalidate = false;
                }
            }
            return Isvalidate;
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; SKU  </strong>Upload</h3>
                        </div>
                        <div class="panel-body pt-5">
                            <div class="row ">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">File. :</label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:FileUpload ID="skufile" ClientIDMode="Static" runat="server" CssClass="file-simple" />
                                            <asp:Label Style="color: red!important; font-weight: bold!important; display: none!important; padding: 6px;"
                                                runat="server" ClientIDMode="Static" ID="lblfilealert">Please Select the File to Upload.</asp:Label>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-12 text-center">
                                    <asp:LinkButton ID="btnUpload" runat="server" CssClass="btn btn-success" Text="Save All" OnClick="btnUpload_Click" OnClientClick="return ValidateAsinCreation();">Upload</asp:LinkButton>
                                    <asp:LinkButton ID="btnDelete" runat="server" CssClass="btn btn-danger" Text="Delete All" OnClientClick="return confirm('Are you sure you want to Delete?');" OnClick="btnDelete_Click">Delete</asp:LinkButton>
                                    <asp:LinkButton ID="btnSave" Visible="false" runat="server" CssClass="btn btn-success " Text="Save" OnClick="btnSave_Click">Save</asp:LinkButton>
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
                <div class="panel panel-default tabs">
                    <ul class="nav nav-tabs" role="tablist">
                        <li class="active">Detail</li>
                    </ul>
                    <div class="panel-body tab-content">
                        <div class="tab-pane active" id="tab-first">
                            <div class="panel-body">
                                <div class="row">
                                    <fieldset class="scheduler-border">
                                        <legend class="scheduler-border" id="lgrecordcount" runat="server"></legend>
                                        <div class="col-md-12 divhorizontal" style="overflow-x: scroll!important;">
                                            <asp:GridView ID="gvasindetail" ClientIDMode="Static" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
                                                <EmptyDataTemplate>
                                                    No Record Found!
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:TemplateField HeaderText="ASIN">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblasin" Text='<%# Bind("SKUASIN_NO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ITEMNAME">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblitemname" Text='<%# Bind("SKUASIN_ITEMNAME") %>'></asp:Label>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmTranMobexVendorVisit" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranSD" runat="server" />
</asp:Content>
