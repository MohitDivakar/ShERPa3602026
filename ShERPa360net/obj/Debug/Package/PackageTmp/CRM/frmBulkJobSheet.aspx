<%@ Page Title="Job Sheet" Language="C#" MasterPageFile="~/CRM/MasterCustomerRltionShipMng.Master" AutoEventWireup="true" CodeBehind="frmBulkJobSheet.aspx.cs" Inherits="ShERPa360net.CRM.frmBulkJobSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Job Sheet</title>

    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            debugger;
            var fu = $("#ContentPlaceHolder1_fuInvDoc").val();
            var challan = $("#ContentPlaceHolder1_txtChalanNo").val();
            var pono = $("#ContentPlaceHolder1_txtPoNo").val();

            if (fu != "" && challan != "" && pono != "") {
                document.getElementById("busy-holder1").style.display = "";
                /*document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";*/
                document.getElementById("ContentPlaceHolder1_lnkSave").style.display = "none";

            }
        }
    </script>

    <script lang="javascript" type='text/javascript'>
        function ValidateBulkSoCreation() {
            var Isvalidate = true;
            //$("#lblddlplant").css("display", "none");
            //$("#lblddllocation").css("display", "none");
            //$("#lblCostCenter").css("display", "none");
            //$("#lblfilealert").css("display", "none");
            //$("#lblPlantLocationUploadFrom").css("display", "none");

            //if ($(".ddlPlantLocationUploadFrom option:selected").val() == "0") {
            //    Isvalidate = false;
            //    $("#lblPlantLocationUploadFrom").css("display", "block");
            //}

            //if ($(".ddlPlantLocationUploadFrom option:selected").val() == "1") {

            //    if ($(".ddlPLant option:selected").val() == "0" || $(".ddlPLant option:selected").val() == "") {
            //        Isvalidate = false;
            //        $("#lblddlplant").css("display", "block");
            //    }
            //    if ($(".ddlLocation option:selected").val() == "0" || $(".ddlLocation option:selected").val() == "") {
            //        Isvalidate = false;
            //        $("#lblddllocation").css("display", "block");
            //    }

            //    if ($(".ddlCostCenter option:selected").val() == "0" || $(".ddlCostCenter option:selected").val() == "") {
            //        Isvalidate = false;
            //        $("#lblCostCenter").css("display", "block");
            //    }
            //}

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

         //function ShowSaveLoading() {
         //    $("#progress").show();
         //    return true;
         //}
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Upload Jobsheet</strong></h3>
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

    <div class="panel-heading" runat="server" visible="false">
        <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server" TargetControlID="pnlSpec"
            ExpandControlID="pnlSpecTitle" CollapseControlID="pnlSpecTitle" TextLabelID="lblColSpecMsg"
            CollapsedText="Add Manually" ExpandedText="Hide"
            ImageControlID="imgSpecCollapsible" ExpandedImage="~/img/collapse_blue.png"
            CollapsedImage="~/img/expand_blue.png" Collapsed="true">
            <%--ExpandedSize="350" ScrollContents="true"--%>
        </cc1:CollapsiblePanelExtender>

        <asp:Panel ID="pnlSpecTitle" runat="server" class="blockheader2">
            <div>
                <asp:Image ID="imgSpecCollapsible" runat="server" Width="22px" />
                <asp:Label ID="lblColSpecMsg" runat="server" Text=""></asp:Label>
            </div>
        </asp:Panel>
    </div>

    <asp:Panel ID="pnlSpec" runat="server" Visible="false">
        <div class="page-content-wrap">
            <div class="row">
                <div class="col-md-12">
                    <div class="form-horizontal">
                        <div class="panel panel-default">

                            <div class="panel-heading">
                                <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Create Job Sheet</strong></h3>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12">

                                        <div class="col-md-3">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Segment : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:DropDownList ID="ddlSegment" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvSegment" runat="server" ControlToValidate="ddlSegment" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Select Segment" InitialValue="0">Please Select Segment</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label">Dist. Chnl. : </label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:DropDownList ID="ddlDistChnl" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDistChnl_SelectedIndexChanged"></asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvDistchnl" runat="server" ControlToValidate="ddlDistChnl" ValidationGroup="SaveAll" Style="color: red;"
                                                            Display="Dynamic" ErrorMessage="Please Select Dist. Chnl." InitialValue="0">Please Select Dist. Chnl.</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label"></label>
                                                    <div class="col-md-7 col-xs-12">
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="col-md-10">
                                                <div class="form-group">
                                                    <label class="col-md-5 control-label"></label>
                                                    <div class="col-md-7 col-xs-12">
                                                        <asp:LinkButton ID="imgSaveAll" runat="server" CssClass="btn btn-success pull-right" OnClientClick="ShowLoading()" Text="Save All" OnClick="imgSaveAll_Click" ValidationGroup="SaveAll"><i class="fa fa-save"></i></asp:LinkButton>
                                                        <%--<div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                                            <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                                            <label>Please wait...</label>
                                                        </div>--%>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                    </div>
                                </div>
                            </div>

                            <div class="form-horizontal">
                                <div class="panel panel-default">
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-md-12">

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Product : </label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control required_text_box" AutoPostBack="true" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvProduct" runat="server" ControlToValidate="ddlProduct" ValidationGroup="SaveDet" Style="color: red;"
                                                                Display="Dynamic" ErrorMessage="Please Select Product" InitialValue="0">Please Select Product</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Item Code : </label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtItemCode" runat="server" CssClass="form-control required_text_box" placeholder="Item Code" AutoPostBack="true" OnTextChanged="txtItemCode_TextChanged"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvItemCode" runat="server" ControlToValidate="txtItemCode" ValidationGroup="SaveDet" Style="color: red;"
                                                                Display="Dynamic" ErrorMessage="Please Enter Item Code">Please Enter Item Code</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Make : </label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList ID="ddlMake" runat="server" CssClass="form-control required_text_box" AutoPostBack="true" OnSelectedIndexChanged="ddlMake_SelectedIndexChanged"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvMake" runat="server" ControlToValidate="ddlMake" ValidationGroup="SaveDet" Style="color: red;"
                                                                Display="Dynamic" ErrorMessage="Please Select Make" InitialValue="0">Please Select Make</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Model : </label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList ID="ddlModel" runat="server" CssClass="form-control required_text_box"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvModel" runat="server" ControlToValidate="ddlModel" ValidationGroup="SaveDet" Style="color: red;"
                                                                Display="Dynamic" ErrorMessage="Please Select Model" InitialValue="0">Please Select Model</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>



                                            </div>

                                            <div class="col-md-12">

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">IMEI No. 1 : </label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtImei1" runat="server" CssClass="form-control required_text_box" placeholder="IMEI 1"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvImei" runat="server" ControlToValidate="txtImei1" ValidationGroup="SaveDet" Style="color: red;"
                                                                Display="Dynamic" ErrorMessage="Please Enter IMEI">Please Enter IMEI</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">IMEI No. 2 : </label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtImei2" runat="server" CssClass="form-control required_text_box" placeholder="IMEI 2"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ID="rfvImei2" runat="server" ControlToValidate="txtImei2" ValidationGroup="SaveDet" Style="color: red;"
                                                        Display="Dynamic" ErrorMessage="Please Enter IMEI 2">Please Enter IMEI 2</asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Reverse Courier : </label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtReverseCourier" runat="server" CssClass="form-control required_text_box" placeholder="Reverse Courier"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvReverseCourier" runat="server" ControlToValidate="txtReverseCourier" ValidationGroup="SaveDet" Style="color: red;"
                                                                Display="Dynamic" ErrorMessage="Please Enter Reverse Courier Name">Please Enter Reverse Courier Name</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Reverse Waybill : </label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:TextBox ID="txtReverseWaybill" runat="server" CssClass="form-control required_text_box" placeholder="Reverse Waybill"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvReverseWaybill" runat="server" ControlToValidate="txtReverseWaybill" ValidationGroup="SaveDet" Style="color: red;"
                                                                Display="Dynamic" ErrorMessage="Please Enter Reverse Waybill">Please Enter Reverse Waybill</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="col-md-12">

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Plant : </label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList ID="ddlPLant" runat="server" CssClass="form-control required_text_box" AutoPostBack="true" OnSelectedIndexChanged="ddlPLant_SelectedIndexChanged"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvPlant" runat="server" ControlToValidate="ddlPLant" ValidationGroup="SaveDet" Style="color: red;"
                                                                Display="Dynamic" ErrorMessage="Please Select Plant" InitialValue="0">Please Select Plant</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Location : </label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control required_text_box"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvlocation" runat="server" ControlToValidate="ddlLocation" ValidationGroup="SaveDet" Style="color: red;"
                                                                Display="Dynamic" ErrorMessage="Please Select Location" InitialValue="0">Please Select Location</asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label">Color : </label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:DropDownList ID="ddlColor" runat="server" CssClass="form-control required_text_box"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvColor" runat="server" ControlToValidate="ddlColor" ValidationGroup="SaveDet" Style="color: red;"
                                                                Display="Dynamic" ErrorMessage="Please Select Color" InitialValue="0">Please Select Color</asp:RequiredFieldValidator>

                                                            <asp:HiddenField ID="hfNewJobID" runat="server" />
                                                            <asp:HiddenField ID="hfJCNO" runat="server" />
                                                            <asp:HiddenField ID="hfEstiNo" runat="server" />
                                                            <asp:HiddenField ID="hfOldJobID" runat="server" />
                                                            <asp:HiddenField ID="hfItemDesc" runat="server" />
                                                            <asp:HiddenField ID="hfColor" runat="server" />
                                                            <%--<asp:HiddenField ID="hfCostCenter" runat="server" />
                                                    <asp:HiddenField ID="hfGLCODE" runat="server" />
                                                    <asp:HiddenField ID="hfPrfcntr" runat="server" />--%>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-5 control-label"></label>
                                                        <div class="col-md-7 col-xs-12">
                                                            <asp:LinkButton ID="imgAdd" runat="server" CssClass="btn btn-success pull-right" OnClick="imgAdd_Click" Text="Add" ValidationGroup="SaveDet" TabIndex="15"><i class="fa fa-plus"></i> Add</asp:LinkButton>
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
    </asp:Panel>

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="form-horizontal">
                            <div class="panel panel-default">
                                <div class="panel-body">

                                    <asp:Label runat="server" ClientIDMode="Static" Style="margin-left: 11px!important; font-weight: bold; font-size: 14px;" Visible="false" ID="lblRecoretxt">Record:</asp:Label>
                                    <asp:Label runat="server" ClientIDMode="Static" Style="font-weight: bold; font-size: 12px;" ID="lblRecord" Visible="false">0</asp:Label>

                                    <asp:LinkButton runat="server" ID="lnkSave" CssClass="btn btn-success pull-right" OnClick="imgSaveAll_Click" Visible="false" OnClientClick="ShowLoading()">Import</asp:LinkButton>
                                    <div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                        <img id="img1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                        <label>Please wait...</label>
                                    </div>

                                    <div class="row" style="margin-top: 20px !important;">
                                        <div class="col-md-12">
                                            <asp:GridView ID="gvData" runat="server" CssClass="table table-hover table-striped table-bordered nowrap" AutoGenerateColumns="false"
                                                EmptyDataText="No records has been added." OnRowDeleting="gvData_RowDeleting">
                                                <Columns>


                                                    <%--                                                    <asp:TemplateField HeaderText="Product Id">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductID" runat="server" Text='<%# Eval("ProductID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Product Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProductName" runat="server" Text='<%# Eval("ProductName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>



                                                    <asp:TemplateField HeaderText="Segment">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSegment" runat="server" Text='<%# Eval("Segment") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Dist. Chnl.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDistChnl" runat="server" Text='<%# Eval("DistChnl") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Item Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblItemCcde" runat="server" Text='<%# Eval("ItemCcde") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Make">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblMake" runat="server" Text='<%# Eval("Make") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Model">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblModel" runat="server" Text='<%# Eval("Model") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="IMEI 1">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIMEI1" runat="server" Text='<%# Eval("IMEI1") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="IMEI 2">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIMEI2" runat="server" Text='<%# Eval("IMEI2") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Reverse Courier">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReverseCourier" runat="server" Text='<%# Eval("ReverseCourier") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Reverse Waybill">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblReverseWaybill" runat="server" Text='<%# Eval("ReverseWaybill") %>'></asp:Label>
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

                                                    <asp:TemplateField HeaderText="Color">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblColor" runat="server" Text='<%# Eval("Color") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Grade">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGrade" runat="server" Text='<%# Eval("Grade") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Price">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPrice" runat="server" Text='<%# Eval("Price") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--<asp:TemplateField HeaderText="Old Job ID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOldJobId" runat="server" Text='<%# Eval("OldJobId") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Cost center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCostCenter" runat="server" Text='<%# Eval("CostCenter") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="GL Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGLCODE" runat="server" Text='<%# Eval("GLCODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Prf Cnt">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPRFCNT" runat="server" Text='<%# Eval("PRFCNT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                    <%--<asp:CommandField ShowDeleteButton="True" ButtonType="Button" />--%>
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

    <input type="hidden" id="menutabid" value="tsmTranBulkJobSheet" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" runat="server" />

</asp:Content>
