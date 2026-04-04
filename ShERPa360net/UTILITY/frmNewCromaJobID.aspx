<%@ Page Title="Croma Job ID Search" Language="C#" MasterPageFile="~/UTILITY/UtilityModule.Master" AutoEventWireup="true" CodeBehind="frmNewCromaJobID.aspx.cs" Inherits="ShERPa360net.UTILITY.frmNewCromaJobID" %>

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


        function NewEvent(row) {
            var itemDiv = row.closest(".dataItem");
            var label = itemDiv.querySelector(".lblMessage");
            label.style.display = "";

            setTimeout(function () {
                label.style.display = "none";
            }, 5000);
        };

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

    <style type="text/css">
        .rowGreen {
            background-color: lightgreen !important;
            /*color: white !important;*/
            /*background: #00FF00 !important;*/
        }

        .rowYellow {
            background-color: yellow !important;
            /*background: #00FF00 !important;*/
        }

        .rowOrange {
            background-color: orange !important;
            /*background: #00FF00 !important;*/
        }

        .hidden {
            display: none;
        }
    </style>

    <style>
        img.file-preview-image {
            width: 100% !important
        }

        .pmarginbottom {
            margin-bottom: inherit !important;
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


    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);
            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "../img/UpArrow.png";
            } else {
                div.style.display = "none";
                img.src = "../img/DownArrow.png";
            }
        }
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="upd1" runat="server">
        <Triggers>
            <%--<asp:AsyncPostBackTrigger ControlID="chkPartnerPrice" EventName="CheckedChanged" />--%>
        </Triggers>
        <ContentTemplate>

            <div class="page-content-wrap">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <cc:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server" TargetControlID="pnlSpec"
                                        ExpandControlID="pnlSpecTitle" CollapseControlID="pnlSpecTitle" TextLabelID="lblColSpecMsg"
                                        CollapsedText="     Search" ExpandedText="     Search"
                                        ImageControlID="imgSpecCollapsible" ExpandedImage="~/img/search.png"
                                        CollapsedImage="~/img/search.png" Collapsed="true">
                                    </cc:CollapsiblePanelExtender>

                                    <asp:Panel ID="pnlSpecTitle" runat="server" class="blockheader2">
                                        <div>
                                            <asp:Image ID="imgSpecCollapsible" runat="server" src="../img/search.png" Style="width: 22px;" title="     Search" />
                                            <asp:Label ID="lblColSpecMsg" runat="server" Text=""></asp:Label>
                                        </div>
                                    </asp:Panel>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <asp:Panel ID="pnlSpec" runat="server">
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
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Job ID : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtJobid" runat="server" CssClass="form-control" ValidationGroup="Search"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvJobID" runat="server" ControlToValidate="txtJobid" ValidationGroup="Search" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Job ID">Please Enter Job ID</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div class="col-md-2" style="margin-top: -15px !important;">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label"></label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-success" OnClick="btnSearch_Click" ValidationGroup="Search" />
                                                                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="btn btn-success fa fa-refresh" OnClick="btnRefresh_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12" style="margin-top: -15px !important;">
                                                <div class="row">

                                                    <div class="col-md-12">
                                                        <div class="form-group">
                                                            <label class="col-md-3 control-label">Item Desc. : </label>
                                                            <div class="col-md-9 col-xs-12">
                                                                <%--<asp:TextBox ID="txtItemDesc" runat="server" CssClass="form-control"></asp:TextBox>--%>
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
            </asp:Panel>

            <div class="page-content-wrap">
                <div class="row">
                    <div class="col-md-12">
                        <div class="panel panel-default">

                            <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>--%>
                                <asp:DataList ID="dtNewList" runat="server" RepeatDirection="Vertical" Width="100%" Style="border: hidden !important;" OnItemDataBound="dtNewList_ItemDataBound">
                                    <ItemTemplate>
                                        <div class="timeline-item" style="width: inherit !important;">
                                            <div class="box-body pad table-responsive" style="margin: 0px;">
                                                <table class="table text-center col-sm-12 no-border">

                                                    <tr style="border: hidden !important;">

                                                        <td>
                                                            <div style="text-align: left !important;">
                                                                <asp:CheckBox ID="chkSelect" runat="server" OnCheckedChanged="chkSelect_CheckedChanged" AutoPostBack="true" />
                                                                <asp:Label ID="lblAVAILSTAT" runat="server" Text='<%# Eval("AVAILSTAT") %>'></asp:Label>
                                                                <asp:Label ID="lblROWCOLOR" runat="server" Text='<%# Eval("ROWCOLOR") %>' Visible="false"></asp:Label>
                                                            </div>
                                                            <div style="text-align: left !important;">
                                                                <asp:Label ID="lblITEMCODE" runat="server" Text='<%# Eval("ITEMCODE") %>'></asp:Label>
                                                                -
                                                                                        <asp:Label ID="lblIETMDESC" runat="server" Text='<%# Eval("IETMDESC") %>'></asp:Label>

                                                            </div>
                                                            <div style="text-align: left !important;">
                                                                <asp:Label ID="lblJOBID" runat="server" Text='<%# Eval("JOBID") %>'></asp:Label>
                                                                -
                                                                                        <asp:Label ID="lblSERIALNO" runat="server" Text='<%# Eval("SERIALNO") %>'></asp:Label>
                                                                -
                                                                <asp:Label ID="lblLOCATION" runat="server" Text='<%# Eval("LOCATION") %>' Visible="true"></asp:Label>
                                                            </div>
                                                            <div style="text-align: left !important;" class="dataItem" data-index="<%# Container.ItemIndex %>">
                                                                MRP :
                                                                <asp:Label ID="lblMRP" runat="server" Text='<%# Eval("MRP") %>'></asp:Label>
                                                                - Online Price :
                                                                <asp:Label ID="lblOnlinePrice" runat="server" Text='<%# Eval("ONLINEPRICE") %>'></asp:Label>
                                                                - Cust. Price : 
                                                                <asp:Label ID="lblCUSTOMERPRCE" runat="server" Text='<%# Eval("CUSTOMERPRCE") %>'></asp:Label>
                                                                - 
                                                                <%--<asp:LinkButton ID="lbkNLP" runat="server" Text="NLP : " OnClick="lbkNLP_Click"></asp:LinkButton>--%>
                                                                <asp:LinkButton ID="lbkNLP" runat="server" Text="NLP : " OnClientClick="NewEvent(this); return false;"></asp:LinkButton>
                                                                <%--<asp:Label ID="lblDEALERPRICE" runat="server" Text='<%# Eval("DEALERPRICE") %>' CssClass="lbldlr hidden"></asp:Label>--%>
                                                                <asp:Label ID="lblDEALERPRICE" runat="server" Text='<%# Eval("DEALERPRICE") %>' CssClass="lblMessage" Style="display: none;"></asp:Label>
                                                            </div>
                                                            <div style="text-align: left !important;">
                                                                Final Amount :
                                                                <asp:TextBox ID="txtFinalAmount" runat="server" Text='<%# Eval("CUSTOMERPRCE") %>' ForeColor="Black"></asp:TextBox>
                                                                <asp:Label ID="lblBRAND" runat="server" Text='<%# Eval("BRAND") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblSEGMENT" runat="server" Text='<%# Eval("SEGMENT") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblDISTCHNL" runat="server" Text='<%# Eval("DISTCHNL") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblITEMID" runat="server" Text='<%# Eval("ITEMID") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblITEMGRP" runat="server" Text='<%# Eval("ITEMGRP") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblPLANTCD" runat="server" Text='<%# Eval("PLANTCD") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblLOCCD" runat="server" Text='<%# Eval("LOCCD") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblCONDID" runat="server" Text='<%# Eval("CONDID") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblCONDTYPE" runat="server" Text='<%# Eval("CONDTYPE") %>' Visible="false"></asp:Label>
                                                                <asp:Label ID="lblRATE" runat="server" Text='<%# Eval("RATE") %>' Visible="false"></asp:Label>


                                                                <asp:HyperLink ID="lblURL" runat="server" Target="_blank" Text="Open Product" NavigateUrl='<%# Eval("URL") %>' Visible="false"></asp:HyperLink>

                                                            </div>
                                                            <div style="text-align: left !important;">
                                                                <a href="JavaScript:divexpandcollapse('div<%# Eval("JOBID") %>');">
                                                                    <img id="imgdiv<%# Eval("JOBID") %>" width="14px" border="0" src="../img/DownArrow.png" />
                                                                </a>
                                                            </div>
                                                        </td>


                                                    </tr>
                                                </table>
                                            </div>
                                        </div>
                                        <div id="div<%# Eval("JOBID") %>" style="display: none; overflow-x: scroll; max-height: 500px !important;" class="box-body divhorizontal">
                                            <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-striped table-bordered nowrap" Style="border-collapse: collapse; font-size: 12px !important; font-weight: 300 !important; white-space: pre-line;">
                                                <Columns>
                                                    <asp:BoundField DataField="SPECNAME" HeaderText="Spec." />
                                                    <asp:BoundField DataField="SPECVALUE" HeaderText="Value" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </ItemTemplate>
                                </asp:DataList>
                                <%-- </ContentTemplate>
                                </asp:UpdatePanel>--%>
                            </div>

                            <asp:LinkButton ID="imgSaveAll" runat="server" CssClass="btn btn-success pull-right" OnClick="imgSaveAll_Click" Text="Create SO" Style="margin-top: 10px !important;" Visible="false"><i class="fa fa-save"></i>   Create SO</asp:LinkButton>
                            <br />
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
