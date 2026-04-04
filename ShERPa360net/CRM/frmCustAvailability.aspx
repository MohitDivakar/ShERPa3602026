<%@ Page Title="Customer Requirement Availability" Language="C#" MasterPageFile="~/CRM/MasterCustomerRltionShipMng.Master" AutoEventWireup="true" CodeBehind="frmCustAvailability.aspx.cs" Inherits="ShERPa360net.CRM.frmCustAvailability" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Customer Requirement Availability</title>

    <style type="text/css">
        .rowGreen {
            background-color: lightgreen !important;
        }
    </style>


    <script language="javascript" type="text/javascript">
        function divexpandcollapse(divname) {
            var div = document.getElementById(divname);
            var img = document.getElementById('img' + divname);
            if (div.style.display == "none") {
                div.style.display = "inline";
                img.src = "../img/collapse_blue.png";
            } else {
                div.style.display = "none";
                img.src = "../img/expand_blue.png";
            }
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>Customer Requirement / Availability</h3>
                        </div>


                        <div class="panel-body">

                            <div class="row">

                                <div class="col-md-12 pull-right">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Product :</label>
                                            <div class="col-md-8 col-xs-12 pull-right">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Cust. Name :</label>
                                            <div class="col-md-7 col-xs-12 pull-right">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtCustName" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Contact No. :</label>
                                            <div class="col-md-7 col-xs-12 pull-right">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtContactNo" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-2 control-label"></label>
                                            <div class="col-md-10 col-xs-12 pull-right">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker pull-right" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-2 control-label pull-left">To</label>
                                            <div class="col-md-10 col-xs-12 pull-right">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker pull-right" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:LinkButton runat="server" ID="lnkSearch" CssClass="btn btn-success pull-left" Text="Search" OnClick="lnkSearch_Click">
                                        <span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
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
                        <div class="row">
                            <div class="col-md-12">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                        <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" OnRowDataBound="gvList_RowDataBound">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <%--<asp:BoundField DataField="ID" HeaderText="MR Type" />--%>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <a href="JavaScript:divexpandcollapse('div<%# Eval("ID") %>');">
                                                            <img id="imgdiv<%# Eval("ID") %>" width="14px" border="0" src="../img/expand_blue.png" />
                                                        </a>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="CUSTUPDATEDATE" HeaderText="Date" />--%>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUSTUPDATEDATE" runat="server" Text='<%# Eval("CUSTUPDATEDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="CUSTNAME" HeaderText="Name" />--%>
                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUSTNAME" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="CONTACTNO" HeaderText="Contact No." />--%>
                                                <asp:TemplateField HeaderText="Contact No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCONTACTNO" runat="server" Text='<%# Eval("CONTACTNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="PRODUCT" HeaderText="Product" />--%>
                                                <asp:TemplateField HeaderText="Product">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPRODUCT" runat="server" Text='<%# Eval("PRODUCT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="ATTRIBUTE" HeaderText="Specs. Name" />--%>
                                                <asp:TemplateField HeaderText="Specs. Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblATTRIBUTE" runat="server" Text='<%# Eval("ATTRIBUTE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="ATTRVALUE" HeaderText="Specs. Value" />--%>
                                                <asp:TemplateField HeaderText="Specs. Value">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblATTRVALUE" runat="server" Text='<%# Eval("ATTRVALUE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="CREATEBY" HeaderText="Entered By" />--%>
                                                <asp:TemplateField HeaderText="Entered By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCREATEBY" runat="server" Text='<%# Eval("CREATEBY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="ASSIGNTO" HeaderText="Assign To" />--%>
                                                <asp:TemplateField HeaderText="Assign To">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblASSIGNTO" runat="server" Text='<%# Eval("ASSIGNTO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Action" Visible="True">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="bntView" Text="Details" OnClick="bntView_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <tr>
                                                            <td colspan="100%">
                                                                <div id="div<%# Eval("ID") %>" style="display: none; overflow-x: scroll; max-height: 500px !important;" class="box-body divhorizontal">
                                                                    <asp:GridView ID="gvInnerList" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-striped table-bordered nowrap" CellSpacing="0"
                                                                        ShowHeaderWhenEmpty="true" Width="100%">
                                                                        <Columns>

                                                                            <asp:TemplateField>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="btnWhatsapp" runat="server" Text="Send MSG" OnClick="btnWhatsapp_Click"><i class="fa fa-send"></i></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Date" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCUSTUPDATEDATE" runat="server" Text='<%# Eval("CUSTUPDATEDATE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Name" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCUSTNAME" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Mobile No." Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblCONTACTNO" runat="server" Text='<%# Eval("CONTACTNO") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Product" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPRODUCT" runat="server" Text='<%# Eval("PRODUCT") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Specs. Name" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblATTRIBUTE" runat="server" Text='<%# Eval("ATTRIBUTE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Specs. Value" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblATTRVALUE" runat="server" Text='<%# Eval("ATTRVALUE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Item Code" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblITEMCODE" runat="server" Text='<%# Eval("ITEMCODE") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Item Desc.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblITEMDESC" runat="server" Text='<%# Eval("ITEMDESC") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Job ID">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblJOBID" runat="server" Text='<%# Eval("JOBID") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Status" Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblJOBSTATUS" runat="server" Text='<%# Eval("JOBSTATUS") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Status Desc.">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblJOBSTATDESC" runat="server" Text='<%# Eval("JOBSTATDESC") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Plant">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblPLANTCD" runat="server" Text='<%# Eval("PLANTCD") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                            <asp:TemplateField HeaderText="Location">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLOCCD" runat="server" Text='<%# Eval("LOCCD") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>

                                                                        </Columns>
                                                                    </asp:GridView>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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

    <div class="modal fade" id="modal-detail" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Customer Req. Availability</strong></h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Cust. Name :</label>
                                        <asp:Label ID="lblCUSTNAMEPOP" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Contact No. :</label>
                                        <asp:Label ID="lblCONTACTNOPOP" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Product :</label>
                                        <asp:Label ID="lblPRODUCTPOP" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Specs. Name :</label>
                                        <asp:Label ID="lblSpecNamePOP" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Specs. Value :</label>
                                        <asp:Label ID="lblSpecValuePOP" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll">
                                        <asp:GridView ID="gvInnerListNew" runat="server" AutoGenerateColumns="false" CssClass="table table-hover table-striped table-bordered nowrap" CellSpacing="0"
                                            ShowHeaderWhenEmpty="true" Width="100%">
                                            <Columns>

                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnWhatsappNew" runat="server" Text="Send MSG" OnClick="btnWhatsappNew_Click"><i class="fa fa-send"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Date" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUSTUPDATEDATE" runat="server" Text='<%# Eval("CUSTUPDATEDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Name" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUSTNAME" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Mobile No." Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCONTACTNO" runat="server" Text='<%# Eval("CONTACTNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Product" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPRODUCT" runat="server" Text='<%# Eval("PRODUCT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Specs. Name" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblATTRIBUTE" runat="server" Text='<%# Eval("ATTRIBUTE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Specs. Value" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblATTRVALUE" runat="server" Text='<%# Eval("ATTRVALUE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Item Code" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblITEMCODE" runat="server" Text='<%# Eval("ITEMCODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Item Desc.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblITEMDESC" runat="server" Text='<%# Eval("ITEMDESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Job ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJOBID" runat="server" Text='<%# Eval("JOBID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Status" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJOBSTATUS" runat="server" Text='<%# Eval("JOBSTATUS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Status Desc.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblJOBSTATDESC" runat="server" Text='<%# Eval("JOBSTATDESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Plant">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPLANTCD" runat="server" Text='<%# Eval("PLANTCD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Location">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLOCCD" runat="server" Text='<%# Eval("LOCCD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

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

    <input type="hidden" id="menutabid" value="tsmTranCRMNotification" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" />

</asp:Content>
