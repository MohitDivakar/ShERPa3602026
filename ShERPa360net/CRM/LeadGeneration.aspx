<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/MasterCustomerRltionShipMng.Master" AutoEventWireup="true" CodeBehind="LeadGeneration.aspx.cs" Inherits="ShERPa360net.CRM.LeadGeneration" EnableEventValidation="false" %>

<%--<%@ Register Assembly="CheckBoxListExCtrl" Namespace="CheckBoxListExCtrl" TagPrefix="cc2" %>--%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Lead Generation</title>


    <script lang="javascript" type='text/javascript'>
        function ShowLoading() {
            var cust = $("#ContentPlaceHolder1_txtCustName").val();
            var cont = $("#ContentPlaceHolder1_txtContactNo").val();
            var reff = $("#ContentPlaceHolder1_ddlReference option:selected").index();
            var inqt = $("#ContentPlaceHolder1_ddlInqType option:selected").index();
            var lead = $("#ContentPlaceHolder1_ddlLeadType option:selected").index();
            if (cust != "" && cont != "" && reff > 0 && inqt > 0 && lead > 0) {
                document.getElementById("busy-holder1").style.display = "";
                document.getElementById("ContentPlaceHolder1_btnManualSave").style.display = "none";
            }
        }
    </script>

    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }

        .blockheader2 {
            width: 200px !important;
        }
    </style>

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

    <div class="page-content-wrap" runat="server" visible="false">

        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Lead  </strong>Generation</h3>
                        </div>
                        <div class="panel-body" runat="server" visible="false">
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
                                        <label align="left" class="col-md-1 control-label">Agent : </label>

                                        <asp:CheckBoxList ID="chkAgentList" runat="server" RepeatLayout="Table" CssClass="chclass" RepeatDirection="Horizontal"></asp:CheckBoxList>
                                        <%--<asp:RequiredFieldValidator ID="rfvAgentList" runat="server" ForeColor="Red" Display="Dynamic" ControlToValidate="chkAgentList"
                                                    ErrorMessage="Select Agent to Assign Calls" InitialValue="0" ValidationGroup="Upload">Select Agent to Assign Calls</asp:RequiredFieldValidator>--%>
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



    <%-- <div class="page-content-wrap">

        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">--%>
    <%--<div class="panel-heading">
        <cc1:CollapsiblePanelExtender ID="CollapsiblePanelExtender3" runat="server" TargetControlID="pnlSpec"
            ExpandControlID="pnlSpecTitle" CollapseControlID="pnlSpecTitle" TextLabelID="lblColSpecMsg"
            CollapsedText="Add Manually" ExpandedText="Hide"
            ImageControlID="imgSpecCollapsible" ExpandedImage="~/img/collapse_blue.png"
            CollapsedImage="~/img/expand_blue.png" Collapsed="true">
            
        </cc1:CollapsiblePanelExtender>

        <asp:Panel ID="pnlSpecTitle" runat="server" class="blockheader2">
            <div>
                <asp:Image ID="imgSpecCollapsible" runat="server" Width="22px" />
                <asp:Label ID="lblColSpecMsg" runat="server" Text=""></asp:Label>
            </div>
        </asp:Panel>
    </div>--%>

    <%--<asp:Panel ID="pnlSpec" runat="server">--%>
    <div class="page-content-wrap">

        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Lead  </strong>Generation</h3>
                        </div>
                        <div class="panel-body" runat="server" visible="true">

                            <div class="row">
                                <div class="col-md-12 panel-info">

                                    <div class="panel-body">

                                        <div class="row">

                                            <div class="col-md-12">

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <label class="col-md-4 control-label">Inq. Date : </label>
                                                        <div class="col-md-8 col-xs-12">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                                <asp:TextBox ID="txtInqDate" runat="server" placeholder="Inq. Date" class="form-control datepicker" AutoCompleteType="None" TabIndex="1" ReadOnly="false"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <%--<asp:RequiredFieldValidator ID="rfvMRDate" runat="server" ControlToValidate="txtMRDATE" ValidationGroup="SaveAll" Style="color: red;"
                                                ErrorMessage="Please Enter MR Date">Please Enter MR Date</asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">

                                                        <label class="col-md-4 control-label">Cust. Name : </label>
                                                        <div class="col-md-8 col-xs-12">
                                                            <asp:TextBox ID="txtCustName" runat="server" placeholder="Cust. Name" class="form-control" TabIndex="2"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ID="rfvCustName" runat="server" ControlToValidate="txtCustName" ForeColor="Red" Display="Dynamic"
                                                                ErrorMessage="Enter Customer Name" ValidationGroup="val">Enter Customer Name</asp:RequiredFieldValidator>
                                                        </div>

                                                    </div>
                                                </div>

                                                <asp:UpdatePanel ID="updas" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="col-md-3">
                                                            <div class="form-group">

                                                                <label class="col-md-4 control-label">Contact No. : </label>
                                                                <div class="col-md-8 col-xs-12">
                                                                    <%--<asp:TextBox ID="txtContactNo" runat="server" placeholder="Contact No." class="form-control" TabIndex="3" MaxLength="10" TextMode="Number" OnTextChanged="txtContactNo_TextChanged" AutoPostBack="true"></asp:TextBox>--%>
                                                                    <asp:TextBox ID="txtContactNo" runat="server" placeholder="Contact No." class="form-control" TabIndex="3" MaxLength="10" TextMode="Number"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ID="rfvContact" runat="server" ControlToValidate="txtContactNo" ForeColor="Red" Display="Dynamic"
                                                                        ErrorMessage="Enter Contact Number" ValidationGroup="val">Enter Contact Number</asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator ID="revMobileNo" runat="server" ControlToValidate="txtContactNo" ValidationGroup="val"
                                                                        Display="Dynamic" ForeColor="Red" ErrorMessage="Invalid Mobile No." ValidationExpression="^[0-9]{10}$">Invalid Mobile No.</asp:RegularExpressionValidator>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                                <div class="col-md-3">
                                                    <div class="form-group">

                                                        <label class="col-md-4 control-label">Email : </label>
                                                        <div class="col-md-8 col-xs-12">
                                                            <asp:TextBox ID="txtEmail" runat="server" placeholder="Email" class="form-control" TabIndex="3"></asp:TextBox>
                                                            <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ForeColor="Red"
                                                                ErrorMessage="Invalid Email" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">Invalid Email</asp:RegularExpressionValidator>
                                                        </div>

                                                    </div>
                                                </div>

                                            </div>

                                            <div class="col-md-12" style="padding-top: 10px !important;">

                                                <asp:UpdatePanel ID="upProduct" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="col-md-3">
                                                            <div class="form-group">

                                                                <label class="col-md-4 control-label">Product : </label>
                                                                <div class="col-md-8 col-xs-12">
                                                                    <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                </div>

                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">

                                                                <label class="col-md-4 control-label">Attribute : </label>
                                                                <div class="col-md-8 col-xs-12">
                                                                    <asp:DropDownList ID="ddlSpecs" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSpecs_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                </div>

                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">

                                                                <label class="col-md-4 control-label">Value : </label>
                                                                <div class="col-md-8 col-xs-12">
                                                                    <asp:DropDownList ID="ddlSpecValue" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                                <div class="col-md-3">
                                                    <div class="form-group">

                                                        <label class="col-md-4 control-label">Cust. Remark : </label>
                                                        <div class="col-md-8 col-xs-12">
                                                            <asp:TextBox ID="txtCustRemarks" runat="server" placeholder="Cust. Remarks" class="form-control" TabIndex="3"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>

                                            </div>

                                            <div class="col-md-12" style="padding-top: 10px !important;">

                                                <%-- <div class="col-md-3">
                                    <div class="form-group">

                                        <label class="col-md-4 control-label">Color : </label>
                                        <div class="col-md-8 col-xs-12">
                                            <asp:TextBox ID="txtColor" runat="server" placeholder="Color" class="form-control" TabIndex="3"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>--%>

                                                <div class="col-md-3">
                                                    <div class="form-group">

                                                        <label class="col-md-4 control-label">Make : </label>
                                                        <div class="col-md-8 col-xs-12">
                                                            <asp:TextBox ID="txtMake" runat="server" placeholder="Make" class="form-control" TabIndex="3"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">

                                                        <label class="col-md-4 control-label">Model : </label>
                                                        <div class="col-md-8 col-xs-12">
                                                            <asp:TextBox ID="txtModel" runat="server" placeholder="Model" class="form-control" TabIndex="3"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>

                                                <%--         <div class="col-md-3">
                                    <div class="form-group">

                                        <label class="col-md-4 control-label">RAM : </label>
                                        <div class="col-md-8 col-xs-12">
                                            <asp:TextBox ID="txtRAM" runat="server" placeholder="RAM" class="form-control" TabIndex="3"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">

                                        <label class="col-md-4 control-label">ROM : </label>
                                        <div class="col-md-8 col-xs-12">
                                            <asp:TextBox ID="txtROM" runat="server" placeholder="ROM" class="form-control" TabIndex="3"></asp:TextBox>
                                        </div>

                                    </div>
                                </div>--%>

                                                <div class="col-md-3">
                                                    <div class="form-group">

                                                        <label class="col-md-4 control-label">Price : </label>
                                                        <div class="col-md-8 col-xs-12">
                                                            <asp:TextBox ID="txtPrice" runat="server" placeholder="Price" class="form-control" TabIndex="3"></asp:TextBox>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">

                                                        <label class="col-md-4 control-label">Reference By : </label>
                                                        <div class="col-md-8 col-xs-12">
                                                            <asp:DropDownList ID="ddlReference" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvRef" runat="server" ControlToValidate="ddlReference" ErrorMessage="Select Reference by"
                                                                ValidationGroup="val" InitialValue="0" Display="Dynamic" ForeColor="Red">Select Reference by</asp:RequiredFieldValidator>
                                                        </div>

                                                    </div>
                                                </div>

                                            </div>

                                            <div class="col-md-12" style="padding-top: 10px !important;">



                                                <div class="col-md-3">
                                                    <div class="form-group">

                                                        <label class="col-md-4 control-label">Inq. Type : </label>
                                                        <div class="col-md-8 col-xs-12">
                                                            <asp:DropDownList ID="ddlInqType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvInqType" runat="server" ControlToValidate="ddlInqType" ErrorMessage="Select Inq. Type"
                                                                ValidationGroup="val" InitialValue="0" Display="Dynamic" ForeColor="Red">Select Inq. Type</asp:RequiredFieldValidator>
                                                        </div>

                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">

                                                        <label class="col-md-4 control-label">Lead Type : </label>
                                                        <div class="col-md-8 col-xs-12">
                                                            <asp:DropDownList ID="ddlLeadType" runat="server" CssClass="form-control"></asp:DropDownList>
                                                            <asp:RequiredFieldValidator ID="rfvLeadType" runat="server" ControlToValidate="ddlLeadType" ErrorMessage="Select Lead Type"
                                                                ValidationGroup="val" InitialValue="0" Display="Dynamic" ForeColor="Red">Select Lead Type</asp:RequiredFieldValidator>
                                                        </div>

                                                    </div>
                                                </div>

                                                <%--  </div>

                            <div class="col-md-12" style="padding-top: 10px !important;">--%>

                                                <asp:UpdatePanel ID="updState" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>

                                                        <div class="col-md-3">
                                                            <div class="form-group">

                                                                <label class="col-md-4 control-label">State : </label>
                                                                <div class="col-md-8 col-xs-12">
                                                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                                    <%--<asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="ddlState" ErrorMessage="Select State"
                                                        ValidationGroup="val" InitialValue="0" Display="Dynamic" ForeColor="Red">Select State</asp:RequiredFieldValidator>--%>
                                                                </div>

                                                            </div>
                                                        </div>

                                                        <div class="col-md-3">
                                                            <div class="form-group">

                                                                <label class="col-md-4 control-label">City : </label>
                                                                <div class="col-md-8 col-xs-12">
                                                                    <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                    <%--<asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="ddlCity" ErrorMessage="Select City"
                                                        ValidationGroup="val" InitialValue="0" Display="Dynamic" ForeColor="Red">Select City</asp:RequiredFieldValidator>--%>
                                                                </div>

                                                            </div>
                                                        </div>

                                                    </ContentTemplate>
                                                </asp:UpdatePanel>

                                            </div>

                                            <div class="col-md-12" style="padding-top: 10px !important;">
                                                <asp:UpdatePanel ID="updSave" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div class="col-md-12">
                                                            <center>
                                                                <div class="form-group">

                                                                    <label class="col-md-4 control-label"></label>
                                                                    <div class="col-md-8 col-xs-12">
                                                                        <asp:Button ID="btnManualSave" runat="server" CssClass="btn btn-success pull-right" Text="Save" OnClientClick="ShowLoading()" OnClick="btnManualSave_Click" ValidationGroup="val" />
                                                                        <div id="busy-holder1" style="display: none" class="clearfix inline pull-right">
                                                                            <img id="imgLoading1" alt="please wait..." runat="server" src="~/img/ajax-loader.gif" />
                                                                            <label>Please wait...</label>
                                                                        </div>
                                                                    </div>

                                                                </div>
                                                            </center>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>




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

    <%--</asp:Panel>--%>


    <%-- </div>
                </div>



            </div>
        </div>
    </div>--%>

    <div class="page-content-wrap" runat="server" visible="false">
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
                                                <asp:BoundField DataField="CUSTUPDATEDATE" HeaderText="Date" />
                                                <asp:BoundField DataField="CUSTNAME" HeaderText="Cust. Name" />
                                                <asp:BoundField DataField="CONTACTNO" HeaderText="Contact No." />
                                                <asp:BoundField DataField="EMAIL" HeaderText="Email" />
                                                <asp:BoundField DataField="MAKE" HeaderText="Make" />
                                                <asp:BoundField DataField="MODEL" HeaderText="Model" />
                                                <asp:BoundField DataField="RAM" HeaderText="RAM" />
                                                <asp:BoundField DataField="ROM" HeaderText="ROM" />
                                                <asp:BoundField DataField="COLOR" HeaderText="Color" />
                                                <asp:BoundField DataField="PRICE" HeaderText="Price" />
                                                <asp:BoundField DataField="CUSTREMARKS" HeaderText="Remarks" />
                                                <asp:BoundField DataField="REFERENCE" HeaderText="Reff." />
                                                <asp:BoundField DataField="INQTYPE" HeaderText="Inq. Type" />
                                                <asp:BoundField DataField="PRODUCT" HeaderText="Product" />
                                                <asp:BoundField DataField="LEADTYPE" HeaderText="Lead Type" />
                                                <asp:BoundField DataField="STATE" HeaderText="State" />
                                                <asp:BoundField DataField="CITY" HeaderText="City" />
                                                <%--<asp:BoundField DataField="SALESRAM" HeaderText="Ram" />
                                                <asp:BoundField DataField="SALESROM" HeaderText="Rom" />
                                                <asp:BoundField DataField="SALESCOLOR" HeaderText="Color" />
                                                <asp:BoundField DataField="SALESIMEINO" HeaderText="IMEI No" />
                                                <asp:BoundField DataField="SALESCONTACTNO" HeaderText="Contact No" />
                                                <asp:BoundField DataField="SALESEMAILADDRESS" HeaderText="Emaill" />--%>
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



    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div class="modal fade" id="modal-Check" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">

                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" style="color: #337ab7">Lead Detail</h4>
                        </div>


                        <div class="modal-body">
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:Label ID="lblDetails" runat="server"></asp:Label>
                                    </div>

                                    <div class="col-md-12" style="margin-top: 10px !important;">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Cust. Name :</label>
                                                <asp:Label ID="lblCustName" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Contact No. :</label>
                                                <asp:Label ID="lblContactNo" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Reff. :</label>
                                                <asp:Label ID="lblReff" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Status :</label>
                                                <asp:Label ID="lblStatus" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12" style="margin-top: 10px !important;">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Create By :</label>
                                                <asp:Label ID="lblCreateBy" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Assign To :</label>
                                                <asp:Label ID="lblAssignTo" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Update By :</label>
                                                <asp:Label ID="lblUpdateBy" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>


                                    </div>

                                    <div class="col-md-12" style="margin-top: 10px !important;">
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Cust. Remarks :</label>
                                                <asp:Label ID="lblHOLDCUSTREMARKS" runat="server" CssClass="form-control"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label>Agent Remarks :</label>
                                                <asp:Label ID="lblCALLREMARKS" runat="server" CssClass="form-control"></asp:Label>
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

    <input type="hidden" id="menutabid" value="tsmTranCallLogs" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" />

</asp:Content>
