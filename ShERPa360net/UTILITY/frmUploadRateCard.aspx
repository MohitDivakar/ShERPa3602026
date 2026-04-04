<%@ Page Title="Upload Croma Rate Card" Language="C#" MasterPageFile="~/UTILITY/UtilityModule.Master" AutoEventWireup="true" CodeBehind="frmUploadRateCard.aspx.cs" Inherits="ShERPa360net.UTILITY.frmUploadRateCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Upload Croma Rate Card</title>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />

    <script lang="javascript" type='text/javascript'>
        function ValidateBulkSoCreation() {
            var Isvalidate = true;
            $("#lblfilealert").css("display", "none");
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
                    $("#lblfilealert").text("Please Select the only Excel.");
                    $("#lblfilealert").css("display", "block");
                    Isvalidate = false;
                }
            }
            return Isvalidate;
        }

        function ShowSaveLoading() {
            $("#progress").show();
            return true;
        }
    </script>
    <style>
        img.file-preview-image {
            width: 100% !important
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Upload Rate Card</strong></h3>
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
                                                    <%--<asp:LinkButton ID="btnSave" Visible="false" runat="server" CssClass="btn btn-success " OnClientClick="return ShowSaveLoading();" Text="Save" OnClick="btnSave_Click">Save</asp:LinkButton>--%>
                                                </div>
                                            </div>
                                        </div>

                                           <div class="col-md-3">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <asp:LinkButton ID="lnkDownload" runat="server" CssClass="btn btn-success" Text="Download Format" OnClick="lnkDownload_Click">Download Format</asp:LinkButton>
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
                <div class="panel panel-default">
                    <div class="panel-body">
                        <asp:Label runat="server" ClientIDMode="Static" Style="margin-left: 11px!important; font-weight: bold; font-size: 14px;" Visible="false" ID="lblRecoretxt">Record:</asp:Label>
                        <asp:Label runat="server" ClientIDMode="Static" Style="font-weight: bold; font-size: 12px;" ID="lblRecord" Visible="false">0</asp:Label>
                        <asp:LinkButton runat="server" ID="lnkSave" CssClass="btn btn-success pull-right" OnClick="lnkSave_Click" Visible="false">Import</asp:LinkButton>
                        <div class="row" style="margin-top: 20px !important;">
                            <div class="col-md-12">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                        <asp:GridView ID="grvLead" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="Article No" HeaderText="Article Code" />
                                                <asp:BoundField DataField="Item-Description" HeaderText="Item Desc." />
                                                <asp:BoundField DataField="Brand" HeaderText="Brand" />
                                                <asp:BoundField DataField="Category" HeaderText="Category" />
                                                <asp:BoundField DataField="JOB ID" HeaderText="Job ID" />
                                                <asp:BoundField DataField="Serial No" HeaderText="Serial No." />
                                                <asp:BoundField DataField="MRP" HeaderText="MRP" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="Online Price" HeaderText="Online Price" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="Dealer Price" HeaderText="Dealer Price" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="Customer Price" HeaderText="Customer Price" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="Size" HeaderText="Size" />
                                                <asp:BoundField DataField="Grade" HeaderText="Grade" />
                                                <asp:BoundField DataField="URL" HeaderText="URL" />
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

    <input type="hidden" id="menutabid" value="tsmCromaRateCard" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmUtility" runat="server" />

</asp:Content>
