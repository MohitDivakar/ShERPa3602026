<%@ Page Title="" Language="C#" MasterPageFile="~/SD/SDMaster.Master" AutoEventWireup="true" CodeBehind="BulkCreatedSoNameAddUpdate.aspx.cs" Inherits="ShERPa360net.SD.BulkCreatedSoNameAddUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script lang="javascript" type='text/javascript'>
        function ValidateBulkSoCreation() {
            debugger
            var Isvalidate = true;
            $("#lblfilealert").css("display", "none");
            $("#lblfilealert").text("Please Select the file.");

            if ($("#fuImage").get(0).files.length == 0) {
                $("#lblfilealert").text("Please Select the file.");
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Bulk  </strong>SO Name Address Update</h3>
                        </div>
                        <div class="panel-body pt-5">
                            <div class="row ">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-5 control-label">File. :</label>
                                        <div class="col-md-7 col-xs-12">
                                            <asp:FileUpload ID="fuImage" ClientIDMode="Static" runat="server" CssClass="file-simple" />
                                            <asp:Label Style="color: red!important; font-weight: bold!important; display: none!important; padding: 6px;"
                                                runat="server" ClientIDMode="Static" ID="lblfilealert">
                                                 Please Select the File to Upload.</asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <asp:LinkButton ID="btnUpload" runat="server" CssClass="btn btn-success" Text="Save All" OnClick="btnUpload_Click" OnClientClick="return ValidateBulkSoCreation();">Upload</asp:LinkButton>
                                    <asp:LinkButton ID="btnSave" Visible="false" runat="server" CssClass="btn btn-success " OnClientClick="return ShowSaveLoading();" Text="Save" OnClick="btnSaveDetail_Click">Save</asp:LinkButton>
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
                                            <asp:GridView ID="gvbulksoAddressupdate" ClientIDMode="Static" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
                                                <EmptyDataTemplate>
                                                    No Record Found!
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <%--Regular Column --%>
                                                    <asp:TemplateField HeaderText="SONO">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSONO" Text='<%# Bind("SONO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="REFNO">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblREFNO" Text='<%# Bind("REFNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CUSTNAME">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCUSTNAME" Text='<%# Bind("CUSTNAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CUSTADD1">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCUSTADD1" Text='<%# Bind("CUSTADD1") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CUSTADD2">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCUSTADD2" Text='<%# Bind("CUSTADD2") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CITY">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCITY" Text='<%# Bind("CITY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="STATE">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSTATE" Text='<%# Bind("STATENAME") %>'></asp:Label>
                                                            <asp:Label runat="server" Visible="false" ID="lblSTATEID" Text='<%# Bind("STATEID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="PINCODE">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblPINCODE" Text='<%# Bind("PINCODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ISVALIDREQUEST">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblISVALIDREQUEST" Text='<%# Bind("ISVALIDSO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ERROR">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblERRORMSG" Text='<%# Bind("ERRORMSG") %>'></asp:Label>
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
    <input type="hidden" id="menutabid" value="tsmTranBulkSoCreation" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranSD" runat="server" />
</asp:Content>
