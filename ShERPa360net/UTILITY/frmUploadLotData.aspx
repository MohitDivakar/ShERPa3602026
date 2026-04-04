<%@ Page Title="Upload Croma Lot Data" Language="C#" MasterPageFile="~/UTILITY/UtilityModule.Master" AutoEventWireup="true" CodeBehind="frmUploadLotData.aspx.cs" Inherits="ShERPa360net.UTILITY.frmUploadLotData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Upload Croma Lot Data</title>


    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />


    <%--<link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.7.1.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.9.0/js/bootstrap-datepicker.min.js"></script>--%>


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

   <%--     function ValidateAMT() {
            debugger;

            var lblSalePricesum = document.getElementById('<%= lblSalePricesum.ClientID %>');
            var txtBidStartAmt = document.getElementById('<%= txtBidStartAmt.ClientID %>');
            var txtMinimumBidAmt = document.getElementById('<%= txtMinimumBidAmt.ClientID %>');

            if (txtBidStartAmt < lblSalePricesum) {
                alert('Bid Start Amount should be greater than or equal to Total Sales Price..!!');
            }
            else {
                if (txtMinimumBidAmt % 1000 == 0) {

                }
                else {
                    alert('Minimum bid increament amount should be multiple of 1000 ..!!');
                }
            }

        }--%>

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

            .chclass label {
                margin: 10px 10px 0px;
            }
        /*  .chclass input {
                position: absolute;
                top: 10px;
                left: 10px;
            }*/

        .rowGreen {
            background-color: green !important;
            /*background: #00FF00 !important;*/
        }
    </style>




    <%--<script>
        $(function () {
            debugger;
            $(".datepicker").datepicker({
                dateFormat: "dd-mm-yy",
                changeMonth: false,
                changeYear: false
            });
        });
    </script>--%>
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


    <asp:UpdatePanel ID="upd1" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="chkBidSetting" EventName="CheckedChanged" />
            <%--<asp:PostBackTrigger ControlID="chkBidSetting" />--%>
        </Triggers>
        <ContentTemplate>

            <div class="page-content-wrap">
                <div class="row">
                    <div class="col-md-12">
                        <div class="form-horizontal">
                            <div class="panel panel-default">

                                <div class="panel-heading">
                                    <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Lot & Bid Setting</strong></h3>
                                </div>

                                <div class="panel-body">
                                    <div class="row">
                                        <div class="col-md-12">
                                            <div class="row">

                                                <div class="col-md-6">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label"></label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:CheckBox ID="chkShowLot" runat="server" CssClass="chclass" Text="Show Lot to Dealers" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-6">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label"></label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:CheckBox ID="chkBidSetting" runat="server" CssClass="chclass" Text="Start Bid" OnCheckedChanged="chkBidSetting_CheckedChanged" AutoPostBack="true" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                        </div>

                                        <div class="col-md-12" id="divBidSetting" runat="server" visible="false">
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Bid Start Dt. : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <div class="input-group">
                                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                    <asp:TextBox ID="txtBidStartDt" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvBidStartDt" runat="server" ControlToValidate="txtBidStartDt" ValidationGroup="SaveLot" Style="color: red;"
                                                                        Display="Dynamic" ErrorMessage="Please Enter Start Date">Please Enter Start Date</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Bid End Dt. : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <div class="input-group">
                                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                    <asp:TextBox ID="txtBidEndDt" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvBidEndDate" runat="server" ControlToValidate="txtBidEndDt" ValidationGroup="SaveLot" Style="color: red;"
                                                                        Display="Dynamic" ErrorMessage="Please Enter End Date">Please Enter End Date</asp:RequiredFieldValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Bid Start Amt. : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtBidStartAmt" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvBidStartAmt" runat="server" ControlToValidate="txtBidStartAmt" ValidationGroup="SaveLot" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Bid Start Amt">Please Enter Bid Start Amt</asp:RequiredFieldValidator>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="col-md-10">
                                                        <div class="form-group">
                                                            <label class="col-md-5 control-label">Minimum Bid Amt. : </label>
                                                            <div class="col-md-7 col-xs-12">
                                                                <asp:TextBox ID="txtMinimumBidAmt" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                <asp:RequiredFieldValidator ID="rfvMinimumBidAmt" runat="server" ControlToValidate="txtMinimumBidAmt" ValidationGroup="SaveLot" Style="color: red;"
                                                                    Display="Dynamic" ErrorMessage="Please Enter Minimum Bid Amt">Please Enter Minimum Bid Amt</asp:RequiredFieldValidator>
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
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">
                    <div class="panel-body">

                        <div class="row">
                            <div class="col-md-12">

                                <div class="col-md-3">
                                    <div class="col-md-10">
                                        <div class="form-group">
                                            <asp:Label runat="server" ClientIDMode="Static" Style="margin-left: 11px!important; font-weight: bold; font-size: 14px;" Visible="false" ID="lblRecoretxt">Record:</asp:Label>
                                            <asp:Label runat="server" ClientIDMode="Static" Style="font-weight: bold; font-size: 12px;" ID="lblRecord" Visible="false">0</asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="col-md-10">
                                        <div class="form-group">
                                            <asp:Label runat="server" ClientIDMode="Static" Style="margin-left: 11px!important; font-weight: bold; font-size: 14px;" Visible="false" ID="txtQtekPriceSum">QTEK Price Sum:</asp:Label>
                                            <asp:Label runat="server" ClientIDMode="Static" Style="font-weight: bold; font-size: 12px;" ID="lblQtekPriceSum" Visible="false">0</asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="col-md-10">
                                        <div class="form-group">
                                            <asp:Label runat="server" ClientIDMode="Static" Style="margin-left: 11px!important; font-weight: bold; font-size: 14px;" Visible="false" ID="txtSalePriceSum">Sale Price Sum:</asp:Label>
                                            <asp:Label runat="server" ClientIDMode="Static" Style="font-weight: bold; font-size: 12px;" ID="lblSalePricesum" Visible="false">0</asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="col-md-10">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSave" CssClass="btn btn-success pull-right" OnClick="lnkSave_Click" Visible="false" ValidationGroup="SaveLot">Import</asp:LinkButton>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>


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
                                                <asp:BoundField DataField="RPA Site Code" HeaderText="RPA Site Code" />
                                                <asp:BoundField DataField="Croma Lot No" HeaderText="Croma Lot No" />
                                                <asp:BoundField DataField="QTEK Lot No" HeaderText="QTEK Lot No" />
                                                <asp:BoundField DataField="Inward Scan ID" HeaderText="Inward Scan ID" />
                                                <asp:BoundField DataField="SR No" HeaderText="SR No" />
                                                <asp:BoundField DataField="Article No" HeaderText="Article No" />
                                                <asp:BoundField DataField="Item Code" HeaderText="Item Code" />
                                                <asp:BoundField DataField="Item Desc" HeaderText="Item Desc" />
                                                <asp:BoundField DataField="Brand" HeaderText="Brand" />
                                                <asp:BoundField DataField="Product" HeaderText="Product" />
                                                <asp:BoundField DataField="Serial No" HeaderText="Serial No" />
                                                <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                                <asp:BoundField DataField="Grade" HeaderText="Grade" />
                                                <asp:BoundField DataField="MRP" HeaderText="MRP" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="MAP" HeaderText="MAP" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="ASP" HeaderText="ASP" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="ASP with GST" HeaderText="ASP with GST" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="PO Amt" HeaderText="PO Amt" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="Inventory Value" HeaderText="Inventory Value" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="Average Recovery (%)" HeaderText="Average Recovery (%)" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="Recovery without Mark Up" HeaderText="Recovery without Mark Up" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="Mark Up Brand (%)" HeaderText="Mark Up Brand (%)" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="Final Recovery without GST" HeaderText="Final Recovery without GST" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="GST (%)" HeaderText="GST (%)" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="GST Amt" HeaderText="GST Amt" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="QTEK Price" HeaderText="QTEK Price" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="Old Price" HeaderText="Old Price" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="Sales Price" HeaderText="Sales Price" DataFormatString="{0:N2}" />
                                                <asp:BoundField DataField="Location" HeaderText="Location" />
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

    <input type="hidden" id="menutabid" value="tsmCromaLotUpload" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmUtility" runat="server" />

</asp:Content>
