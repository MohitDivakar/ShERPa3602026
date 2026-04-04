<%@ Page Title="" Language="C#" MasterPageFile="~/SD/SDMaster.Master" AutoEventWireup="true" CodeBehind="BulkWebsiteSoCreation.aspx.cs" Inherits="ShERPa360net.SD.BulkWebsiteSoCreation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

            if ($(".ddlCostCenter option:selected").val() == "0" || $(".ddlCostCenter option:selected").val() == "") {
                Isvalidate = false;
                $("#lblCostCenter").css("display", "block");
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Bulk  </strong>SO Creation</h3>
                        </div>
                        <div class="panel-body pt-5">
                            <div class="row ">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">Plant : </label>
                                        <div class="col-md-9 col-xs-12">
                                            <asp:DropDownList ID="ddlPLant" runat="server" CssClass="form-control ddlPLant" OnSelectedIndexChanged="ddlPLant_SelectedIndexChanged" AutoPostBack="true" TabIndex="12"></asp:DropDownList>
                                            <asp:Label ID="lblddlplant" runat="server" ClientIDMode="Static" Style="color: red!important; font-weight: bold!important; display: none">Please Select Plant</asp:Label>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">Location : </label>
                                        <div class="col-md-9 col-xs-12">
                                            <asp:DropDownList ID="ddlLocation" OnSelectedIndexChanged="ddlLocation_SelectedIndexChanged" runat="server" CssClass="form-control ddlLocation" TabIndex="13" AutoPostBack="true"></asp:DropDownList>
                                            <asp:Label ID="lblddllocation" runat="server" ClientIDMode="Static" Style="color: red!important; font-weight: bold!important; display: none">Please Select Location</asp:Label>
                                        </div>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">CostCenter: </label>
                                        <div class="col-md-9 col-xs-12">
                                            <asp:DropDownList ID="ddlCostCenter" runat="server" CssClass="form-control ddlCostCenter" TabIndex="13" AutoPostBack="false"></asp:DropDownList>
                                            <asp:Label ID="lblCostCenter" runat="server" ClientIDMode="Static" Style="color: red!important; font-weight: bold!important; display: none">Please Select Cost Center</asp:Label>
                                        </div>
                                    </div>
                                </div>

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


                            </div>


                            <div class="row">
                                <div class="col-md-12 text-center">
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
                                            <asp:GridView ID="gvbulksoProduct" ClientIDMode="Static" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" CssClass="table items table-striped table-bordered table-condensed table-hover sortable_table" Width="100%">
                                                <EmptyDataTemplate>
                                                    No Record Found!
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <%--Action Column --%>

                                                    <%--Action Column --%>

                                                    <%--Regular Column --%>
                                                    <%--Regular Column --%>
                                                    <asp:TemplateField HeaderText="CMPID">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCMPID" Text='<%# Bind("CMPID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SOTYPE">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSOTYPE" Text='<%# Bind("SOTYPE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="SONO" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSONO" Text='<%# Bind("SONO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SODT">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSODT" Text='<%# Bind("SODT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SEGMENT">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSEGMENT" Text='<%# Bind("SEGMENT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DISTCHNL">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDISTCHNL" Text='<%# Bind("DISTCHNL") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="BILLTOCODE">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblBILLTOCODE" Text='<%# Bind("BILLTOCODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SHIPTOCODE">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSHIPTOCODE" Text='<%# Bind("SHIPTOCODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PMTTERMS">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblPMTTERMS" Text='<%# Bind("PMTTERMS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="REMARK">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblREMARK" Text='<%# Bind("REMARK") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="STATUS" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSTATUS" Text='<%# Bind("STATUS") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NETMATVALUE">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblNETMATVALUE" Text='<%# Bind("NETMATVALUE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NETTAXAMT">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblNETTAXAMT" Text='<%# Bind("NETTAXAMT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DISCOUNT">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDISCOUNT" Text='<%# Bind("DISCOUNT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NETSOAMT">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblNETSOAMT" Text='<%# Bind("NETSOAMT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CNCLREASON" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCNCLREASON" Text='<%# Bind("CNCLREASON") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="REFNO">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblREFNO" Text='<%# Bind("REFNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="REFDT">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblREFDT" Text='<%# Bind("REFDT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SALESFROM">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSALESFROM" Text='<%# Bind("SALESFROM") %>'></asp:Label>
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
                                                    <asp:TemplateField HeaderText="CUSTADD3">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCUSTADD3" Text='<%# Bind("CUSTADD3") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CITY">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCITY" Text='<%# Bind("CITY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="STATEID">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSTATEID" Text='<%# Bind("STATEID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PINCODE">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblPINCODE" Text='<%# Bind("PINCODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CUSTMOBILENO">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCUSTMOBILENO" Text='<%# Bind("CUSTMOBILENO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CUSTEMAILID">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCUSTEMAILID" Text='<%# Bind("CUSTEMAILID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="JOBID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblJOBID" Text='<%# Bind("JOBID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="REFSONO" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblREFSONO" Text='<%# Bind("REFSONO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="COMMAGENT">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCOMMAGENT" Text='<%# Bind("COMMAGENT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SCHEMEID">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSCHEMEID" Text='<%# Bind("SCHEMEID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PAYMODE">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblPAYMODE" Text='<%# Bind("PAYMODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PREPAIDAMT">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblPREPAIDAMT" Text='<%# Bind("PREPAIDAMT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="REMAINAMT">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblREMAINAMT" Text='<%# Bind("REMAINAMT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SRNO">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSRNO" Text='<%# Bind("SRNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ITEMID">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblITEMID" Text='<%# Bind("ITEMID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ITEMDESC">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblITEMDESC" Text='<%# Bind("ITEMDESC") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PLANTCD">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblPLANTCD" Text='<%# Bind("PLANTCD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="LOCCD">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblLOCCD" Text='<%# Bind("LOCCD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ITEMGRPID">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblITEMGRPID" Text='<%# Bind("ITEMGRPID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SOQTY">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblSOQTY" Text='<%# Bind("SOQTY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="UOM">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblUOM" Text='<%# Bind("UOM") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="RATE">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblRATE" Text='<%# Bind("RATE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CAMOUNT">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCAMOUNT" Text='<%# Bind("CAMOUNT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DISCAMT">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDISCAMT" Text='<%# Bind("DISCAMT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DELIDT">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDELIDT" Text='<%# Bind("DELIDT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="GLCD">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblGLCD" Text='<%# Bind("GLCD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CSTCENTCD">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCSTCENTCD" Text='<%# Bind("CSTCENTCD") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PRFCNT">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblPRFCNT" Text='<%# Bind("PRFCNT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ITEMTEXT">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblITEMTEXT" Text='<%# Bind("ITEMTEXT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TAXAMT">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblTAXAMT" Text='<%# Bind("TAXAMT") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CUSTPARTNO" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCUSTPARTNO" Text='<%# Bind("CUSTPARTNO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CUSTPARTDESC" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCUSTPARTDESC" Text='<%# Bind("CUSTPARTDESC") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CUSTPARTDESC2" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCUSTPARTDESC2" Text='<%# Bind("CUSTPARTDESC2") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="OLDITEMID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblOLDITEMID" Text='<%# Bind("OLDITEMID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CHANGEREASON" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCHANGEREASON" Text='<%# Bind("CHANGEREASON") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PRODGRADE">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblPRODGRADE" Text='<%# Bind("PRODGRADE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ISSOALREADYCREATED">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblISSOALREADYCREATED" Text='<%# Bind("ISSOALREADYCREATED") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ISVALIDREQUEST">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblISVALIDREQUEST" Text='<%# Bind("ISVALIDREQUEST") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="ALREADY CREATED SONO">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblALREADYCREATEDSONO" Text='<%# Bind("ALREADYCREATEDSONO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Itemcode">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblItemCode" Text='<%# Bind("ITEMCODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="City">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCity" Text='<%# Bind("CITY") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                       <%--                             <asp:TemplateField HeaderText="State">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblStateName" Text='<%# Bind("STATENAME") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                   <%-- <asp:TemplateField HeaderText="Pincode">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblPincode" Text='<%# Bind("PINCODE") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                      <%-- <asp:TemplateField HeaderText="CustMobileNo">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCustMobileNo" Text='<%# Bind("CUSTMOBILENO") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                <%--     <asp:TemplateField HeaderText="CustEmailId">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblCustEmail" Text='<%# Bind("CUSTEMAILID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>

                                                    <%--Regular Column --%>
                                                    <%--Regular Column --%>
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
