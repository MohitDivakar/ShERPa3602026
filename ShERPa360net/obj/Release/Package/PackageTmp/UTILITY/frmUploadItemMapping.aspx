<%@ Page Title="" Language="C#" MasterPageFile="~/UTILITY/Utility.Master" AutoEventWireup="true" CodeBehind="frmUploadItemMapping.aspx.cs" Inherits="ShERPa360net.UTILITY.frmUploadItemMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Upload Item Mapping</title>

    <script lang="javascript" type='text/javascript'>
        function ValidateBulkSoCreation() {
            debugger
            var Isvalidate = true;
            $("#lblddlplant").css("display", "none");
            $("#lblddllocation").css("display", "none");
            $("#lblfilealert").css("display", "none");
            if ($(".ddlPLant option:selected").val() == "0" || $(".ddlPLant option:selected").val() == "") {
                Isvalidate = false;
                $("#lblddlplant").css("display", "block");
            }
            if ($(".ddlLocation option:selected").val() == "0" || $(".ddlLocation option:selected").val() == "") {
                Isvalidate = false;
                $("#lblddllocation").css("display", "block");
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
                    $("#lblfilealert").text("Please Select the only Excel.");
                    $("#lblfilealert").css("display", "block");
                    Isvalidate = false;
                }
            }
            return Isvalidate;
        }
    </script>

    <style>

        img.file-preview-image
        {
            width:100% !important   
        }

        .file-preview-frame{
            width:135px !important;
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Bulk  </strong>Item Mapping</h3>
                        </div>
                        <div class="panel-body pt-5">
                            <div class="row ">
                                  <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">File :</label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:FileUpload ID="fuImage" ClientIDMode="Static" runat="server"  CssClass="file-simple" />
                                            <asp:Label Style="color: red!important; font-weight: bold!important; display: none!important; padding: 6px;"
                                                runat="server" ClientIDMode="Static" ID="lblfilealert">
                                                 Please Select Excel File to Upload</asp:Label>
                                        </div>
                                    </div>  
                                </div>

                                <div class="col-md-1">
                                    <asp:LinkButton ID="btnUpload" runat="server" CssClass="btn btn-success pull-right" Text="Save All" OnClick="btnUpload_Click">Upload</asp:LinkButton>
                                </div>
                                <div class="col-md-12 text-center">
                                    <asp:LinkButton ID="btnSave" Visible="false" runat="server" CssClass="btn btn-success " Text="Save" OnClick="btnSave_Click">Save</asp:LinkButton>
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
                                        <%--<legend class="scheduler-border" id="lgrecordcount" runat="server"></legend>--%>
                                        <div class="col-md-12 divhorizontal" style="overflow-x: scroll!important;">
                                            <asp:GridView ID="gvDetail" ClientIDMode="Static" runat="server"></asp:GridView>
                                            <asp:BoundField DataField="Status"  HeaderText="Status" />
                                            <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                            <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc" />
                                            <asp:BoundField DataField="SKU" HeaderText="SKU" />
                                            <asp:BoundField DataField="FLIPKART" HeaderText="Flip kart" />
                                            <asp:BoundField DataField="AMAZON" HeaderText="Amazon" />
                                            <asp:BoundField DataField="NEWAMAZON" HeaderText="New Amazon" />
                                            <asp:BoundField DataField="WEBSITE" HeaderText="Website" />
                                            <asp:BoundField DataField="SCWEBSITE" HeaderText="Sc Website" />
                                            <asp:BoundField DataField="CFURL" HeaderText="CFurl" />

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

    <input type="hidden" id="menutabid" value="tsmMstMMItemMapped" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmMstMM" />

</asp:Content>
