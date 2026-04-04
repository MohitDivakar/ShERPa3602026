<%@ Page Title="Croma Job ID Search" Language="C#" MasterPageFile="~/UTILITY/UtilityModule.Master" AutoEventWireup="true" CodeBehind="frmCromaJobID.aspx.cs" Inherits="ShERPa360net.UTILITY.frmCromaJobID" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Croma Job ID Search</title>

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
                if ((fileextension.toUpperCase()).includes("JPG") || (fileextension.toUpperCase()).includes("jpg") || (fileextension.toUpperCase()).includes("JPEG") || (fileextension.toUpperCase()).includes("jpeg")
                    || (fileextension.toUpperCase()).includes("PNG") || (fileextension.toUpperCase()).includes("png")) {
                    var isimglfile = true;
                }

                if (isimglfile == false) {
                    $("#lblfilealert").text("Please Select Image file only.");
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

    <style type="text/css">
        .templateTable {
            border-collapse: collapse;
            width: 100%;
        }

            .templateTable td {
                border: solid 1px #C2D4DA;
                padding: 6px;
            }

                .templateTable td.value {
                    font-weight: bold;
                }

        .imageCell {
            width: 160px;
        }

        .dxTheme-Office365Dark .templateTable td {
            border-color: black;
        }

        table, th, td {
            border: 1px solid black;
        }
    </style>

    <style>
        img.file-preview-image {
            width: 100% !important
        }
    </style>

    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            document.getElementById("busy-holder1").style.display = "";
            document.getElementById("ContentPlaceHolder1_imgSaveAll").style.display = "none";
        }

        function ShowLoading2() {
            var txtCustName = $("#ContentPlaceHolder1_txtCustName").val();
            var txtCustMobileNo = $("#ContentPlaceHolder1_txtCustMobileNo").val();
            var txtAddress1 = $("#ContentPlaceHolder1_txtAddress1").val();
            var txtAddress2 = $("#ContentPlaceHolder1_txtAddress2").val();
            var txtPinCode = $("#ContentPlaceHolder1_txtPinCode").val();
            var ddlState = $("#ContentPlaceHolder1_ddlState").val();
            var ddlCity = $("#ContentPlaceHolder1_ddlCity").val();
            if (txtCustName != "" && txtCustMobileNo != "" && txtAddress1 != "" && txtAddress2 != "" && txtPinCode != "" && ddlState != "" && ddlCity != "") {
                document.getElementById("busy-holder2").style.display = "";
                document.getElementById("ContentPlaceHolder1_btnCreateDoc").style.display = "none";
            }
        }
    </script>


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

    <asp:UpdatePanel ID="upd1" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="chkPartnerPrice" EventName="CheckedChanged" />
        </Triggers>
        <ContentTemplate>

            <div class="page-content-wrap">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="panel panel-default">

                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Search Product</strong></h3>
                                </div>

                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="row">

                                                <div class="col-md-3">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label"></label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:FileUpload ID="fuImage" ClientIDMode="Static" runat="server" CssClass="file-simple" />
                                                                <asp:Label Style="color: red!important; font-weight: bold!important; display: none!important; padding: 6px;"
                                                                    runat="server" ClientIDMode="Static" ID="lblfilealert">Please Select the File to Upload.</asp:Label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-2">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <asp:LinkButton ID="btnUpload" runat="server" CssClass="btn btn-success" Text="Save All" OnClick="btnUpload_Click" OnClientClick="return ValidateBulkSoCreation();">Upload</asp:LinkButton>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Job ID : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtJobid" runat="server" CssClass="form-control" ValidationGroup="Search"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvJobID" runat="server" ControlToValidate="txtJobid" ValidationGroup="Search" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Job ID">Please Enter Job ID</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-2">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label"></label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success" OnClick="btnSearch_Click" ValidationGroup="Search" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-2">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label"></label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="btn btn-success fa fa-refresh" OnClick="btnRefresh_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="row">

                                                <div class="col-md-9">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Item Desc. : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtItemDesc" runat="server" CssClass="form-control" OnTextChanged="txtItemDesc_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                <cc:AutoCompleteExtender ServiceMethod="GetItemCode" MinimumPrefixLength="1" CompletionInterval="10"
                                                                    EnableCaching="false" CompletionSetCount="1" TargetControlID="txtItemDesc" ID="Auto1" runat="server"
                                                                    FirstRowSelected="false" ShowOnlyCurrentWordInCompletionListItem="true">
                                                                </cc:AutoCompleteExtender>
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

            <div class="page-content-wrap">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-default" style="width: auto !important">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <%--<asp:LinkButton ID="imgSaveAll" OnClientClick="ShowLoading()" runat="server" CssClass="btn btn-success pull-left" OnClick="imgSaveAll_Click" Text="Create SO" Style="margin-top: 10px !important;" Visible="false"><i class="fa fa-save"></i>   Create SO</asp:LinkButton>--%>
                                            <asp:LinkButton ID="imgSaveAll" runat="server" CssClass="btn btn-success pull-left" OnClick="imgSaveAll_Click" Text="Create SO" Style="margin-top: 10px !important;" Visible="false"><i class="fa fa-save"></i>   Create SO</asp:LinkButton>
                                            <%--<div id="busy-holder1" style="display: none" class="clearfix inline pull-left">
                                        <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                        <label>Please wait...</label>
                                    </div>--%>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <asp:CheckBox ID="chkPartnerPrice" runat="server" Text="View Partner Price" CssClass="chclass" OnCheckedChanged="chkPartnerPrice_CheckedChanged" AutoPostBack="true" Visible="false" />
                                        </div>
                                    </div>
                                    <div class="col-md-12" style="margin-top: 30px !important;">
                                        <div class="box">
                                            <%--<asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                        CellSpacing="0" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" Width="100%">
                                        <EmptyDataTemplate>
                                            No Record Found!
                                        </EmptyDataTemplate>
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />
                                            <asp:BoundField DataField="CMPID" HeaderText="CMP ID" Visible="false" />
                                            <asp:BoundField DataField="CATEGORY" HeaderText="Category" />
                                            <asp:BoundField DataField="BRAND" HeaderText="Brand" />
                                            <asp:BoundField DataField="ITEMCODE" HeaderText="Article Code" />
                                            <asp:BoundField DataField="IETMDESC" HeaderText="Item Desc" />
                                            <asp:BoundField DataField="JOBID" HeaderText="Job ID" />
                                            <asp:BoundField DataField="SERIALNO" HeaderText="Serial No." />
                                            <asp:BoundField DataField="MRP" HeaderText="MRP" />
                                            <asp:BoundField DataField="CUSTOMERPRCE" HeaderText="Price" />
                                            <asp:BoundField DataField="DEALERRPRICE" HeaderText="Dealer Price" Visible="false" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <table class="templateTable">
                                                        <tr>
                                                            <td>Category : 
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblCATEGORY" runat="server" Text='<%# Eval("CATEGORY") %>'></asp:Label>
                                                            </td>
                                                            <td>Brand : 
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblBRAND" runat="server" Text='<%# Eval("BRAND") %>'></asp:Label>
                                                            </td>
                                                            <td>Item Code : 
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblITEMCODE" runat="server" Text='<%# Eval("ITEMCODE") %>'></asp:Label>
                                                            </td>
                                                            <td>Item Desc. : 
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblIETMDESC" runat="server" Text='<%# Eval("IETMDESC") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>

                                    </asp:GridView>--%>

                                            <asp:DataList ID="dlListSaved" runat="server" RepeatDirection="Horizontal" Width="100%" RepeatColumns="2" OnItemDataBound="dlListSaved_ItemDataBound">
                                                <ItemTemplate>
                                                    <div class="timeline-item" style="border-bottom: 1px solid #f4f4f4">
                                                        <div class="box-body pad table-responsive" style="margin: 0px">
                                                            <table class="table text-center col-sm-12 no-border">
                                                                <tr>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkSelect" runat="server" CssClass="form-control" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Category : 
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblCATEGORY" runat="server" Text='<%# Eval("CATEGORY") %>'></asp:Label>
                                                                    </td>
                                                                    <td colspan="2" rowspan="9">
                                                                        <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-striped table-bordered nowrap">
                                                                            <Columns>
                                                                                <asp:BoundField DataField="SPECNAME" HeaderText="Spec." />
                                                                                <asp:BoundField DataField="SPECVALUE" HeaderText="Value" />
                                                                            </Columns>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Brand : 
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblBRAND" runat="server" Text='<%# Eval("BRAND") %>'></asp:Label>
                                                                        <asp:Label ID="lblSEGMENT" runat="server" Text='<%# Eval("SEGMENT") %>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblDISTCHNL" runat="server" Text='<%# Eval("DISTCHNL") %>' Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Item Code : 
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblITEMCODE" runat="server" Text='<%# Eval("ITEMCODE") %>'></asp:Label>
                                                                        <asp:Label ID="lblITEMID" runat="server" Text='<%# Eval("ITEMID") %>' Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Item Desc. : 
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblIETMDESC" runat="server" Text='<%# Eval("IETMDESC") %>'></asp:Label>
                                                                        <asp:Label ID="lblITEMGRP" runat="server" Text='<%# Eval("ITEMGRP") %>' Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Job ID : 
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblJOBID" runat="server" Text='<%# Eval("JOBID") %>'></asp:Label>
                                                                        <asp:Label ID="lblPLANTCD" runat="server" Text='<%# Eval("PLANTCD") %>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblLOCCD" runat="server" Text='<%# Eval("LOCCD") %>' Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Serial No. : 
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblSERIALNO" runat="server" Text='<%# Eval("SERIALNO") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>MRP : 
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblMRP" runat="server" Text='<%# Eval("MRP") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Cust. Price : 
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblCUSTOMERPRCE" runat="server" Text='<%# Eval("CUSTOMERPRCE") %>'></asp:Label>
                                                                        <asp:Label ID="lblCONDID" runat="server" Text='<%# Eval("CONDID") %>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblCONDTYPE" runat="server" Text='<%# Eval("CONDTYPE") %>' Visible="false"></asp:Label>
                                                                        <asp:Label ID="lblRATE" runat="server" Text='<%# Eval("RATE") %>' Visible="false"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Location :
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblLOCATION" runat="server" Text='<%# Eval("LOCATION") %>'></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Enter Final Amount :
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFinalAmount" runat="server" CssClass="form-control" Text='<%# Eval("CUSTOMERPRCE") %>'></asp:TextBox>
                                                                    </td>
                                                                    <td>Partner Price :
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblDEALERPRICE" runat="server" Text='<%# Eval("DEALERPRICE") %>' Visible="true"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:DataList>
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
                            <h4 class="modal-title" style="color: #337ab7"><strong>Purchase</strong> Details</h4>
                        </div>
                        <div class="modal-body">
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Cust. Name : </label>
                                                <asp:TextBox ID="txtCustName" runat="server" CssClass="form-control" PlaceHolder="Customer Name"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvCustName" runat="server" ControlToValidate="txtCustName" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Customer Name" ValidationGroup="Check">Enter Customer Name</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Mobile No. : </label>
                                                <asp:TextBox ID="txtCustMobileNo" runat="server" CssClass="form-control" PlaceHolder="Customer Mobile No." MaxLength="10"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvCustMobileNo" runat="server" ControlToValidate="txtCustMobileNo" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Customer Mobile No." ValidationGroup="Check">Enter Customer Mobile No.</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Addr 1 : </label>
                                                <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control" MaxLength="50" PlaceHolder="Address 1 (50 Character)"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvAddress1" runat="server" ControlToValidate="txtAddress1" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Address 1" ValidationGroup="Check">Enter Address 1</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Addr 2 : </label>
                                                <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control" MaxLength="50" PlaceHolder="Address 2 (50 Character)"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvAddress2" runat="server" ControlToValidate="txtAddress2" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Address 2" ValidationGroup="Check">Enter Address 2</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Addr 3 : </label>
                                                <asp:TextBox ID="txtAddress3" runat="server" CssClass="form-control" MaxLength="50" PlaceHolder="Address 3 (50 Character)"></asp:TextBox>
                                                <%--<asp:RequiredFieldValidator ID="rfvAddress3" runat="server" ControlToValidate="txtAddress3" Display="Dynamic" ForeColor="Red"
                                            ErrorMessage="Enter Address 3" ValidationGroup="Check">Enter Address 3</asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-md-12">
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>Pin Code : </label>
                                                <asp:TextBox ID="txtPinCode" runat="server" CssClass="form-control" MaxLength="6" PlaceHolder="Pin Code"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvPinCode" runat="server" ControlToValidate="txtPinCode" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Enter Pin Code" ValidationGroup="Check">Enter Pin Code</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>State : </label>
                                                <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlState_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="ddlState" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Select State" ValidationGroup="Check" InitialValue="0">Select State</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <label>City : </label>
                                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="ddlCity" Display="Dynamic" ForeColor="Red"
                                                    ErrorMessage="Select City" ValidationGroup="Check" InitialValue="0">Select City</asp:RequiredFieldValidator>
                                            </div>
                                        </div>

                                    </div>

                                    <div class="col-md-12">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <asp:Button ID="btnCreateDoc" runat="server" OnClientClick="ShowLoading2()" CssClass="btn btn-success" Text="Create SO" OnClick="btnCreateDoc_Click" ValidationGroup="Check" />
                                                <div id="busy-holder2" style="display: none" class="clearfix inline pull-left">
                                                    <img id="img2" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                                    <label>Please wait...</label>
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

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmCromaJobID" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmUtility" runat="server" />

</asp:Content>
