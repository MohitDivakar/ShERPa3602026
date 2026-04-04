<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="ViewPO.aspx.cs" Inherits="ShERPa360net.MM.ViewPO" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>Purchase Order</h3>
                        </div>


                        <div class="panel-body">

                            <div class="row">

                                <div class="col-md-12">

                                    <div class="col-md-3">
                                        <div class="form-group">



                                            <label class="col-md-3 control-label">PO Date : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">

                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>



                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">



                                            <label class="col-md-3 control-label">To : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>



                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">



                                            <label class="col-md-4 control-label">PO No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtPONO" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>

                                                </div>
                                            </div>



                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">



                                            <label class="col-md-4 control-label">PR No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtPRNO" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>

                                                </div>
                                            </div>



                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkNewPO" CssClass="btn btn-success pull-left" Text="New PO" PostBackUrl="~/MM/CreatePO.aspx"><span tooltip="Add New" flow="down"><i class="fa fa-plus"  aria-hidden="true"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkSerchPO" CssClass="btn btn-success pull-left" Text="Search PO" OnClick="lnkSerchPO_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export PO" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
                                        </div>
                                    </div>

                                </div>
                                <div id="mobile_View">
                                    <div class="col-md-1" style="margin-top: 5px;">&nbsp;</div>
                                    <div class="col-md-3" style="margin-top: 5px;">
                                        <div class="form-group">
                                            <div class="col-md-9 col-xs-12">
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


                        <div class="row">
                            <div class="col-md-12">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" OnRowDataBound="gvList_RowDataBound">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="POTYPE" HeaderText="PO Type" />
                                                <asp:BoundField DataField="PONO" HeaderText="PO No." />
                                                <asp:BoundField DataField="PODTD" HeaderText="PO Date" DataFormatString="{0:dd-MM-yyyy}" />
                                                <asp:BoundField DataField="VENDCODE" HeaderText="Vendor Code" />
                                                <asp:BoundField DataField="VENDNAME" HeaderText="Vendor Name" />
                                                <asp:BoundField DataField="TRANCODE" HeaderText="Transporter Code" />
                                                <asp:BoundField DataField="TRANNAME" HeaderText="Transporter Name" />
                                                <asp:BoundField DataField="PLANTCD" HeaderText="Plant Code" />
                                                <asp:BoundField DataField="REMARK" HeaderText="Remark" />
                                                <asp:BoundField DataField="APRVSTATUS" HeaderText="Approve Status" />
                                                <asp:BoundField DataField="STATUS" HeaderText="Status" Visible="false" />
                                                <asp:TemplateField HeaderText="Action" Visible="true">
                                                    <ItemTemplate>

                                                        <%--<asp:LinkButton runat="server" ID="btnEdit" Text="Edit" OnClick="btnEdit_Click"></asp:LinkButton>
                                                        <asp:Label ID="lblStick" runat="server" Text="|"></asp:Label>--%>
                                                        <asp:LinkButton runat="server" ID="bntView" Text="Details" OnClick="bntView_Click"></asp:LinkButton>
                                                        <asp:Label ID="lblLine" runat="server" Text=" | "></asp:Label>
                                                        <asp:LinkButton runat="server" ID="btnPrint" Text="PDF" OnClick="btnPrint_Click"></asp:LinkButton>
                                                        <%-- | 
                                                        <asp:LinkButton runat="server" ID="btnPrint" Text="PDF" OnClick="btnPrint_Click"></asp:LinkButton>--%>
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
        <div class="modal-dialog modal-lg top-0">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>PO</strong> Details</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Doc Type :</label>
                                    <asp:Label ID="lblDoctype" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>PO No. :</label>
                                    <asp:Label ID="lblPONo" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>PO Date :</label>
                                    <asp:Label ID="lblPODate" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>


                            <div class="col-md-4" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Payment Terms :</label>
                                    <asp:Label ID="lblPOPayTerms" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-8" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Payment Terms Desc. :</label>
                                    <asp:Label ID="lblPOPayTermsDesc" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>


                            <div class="col-md-6" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Vendor :</label>
                                    <asp:Label ID="lblPOVendor" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-6" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Transporter :</label>
                                    <asp:Label ID="lblPOTransporter" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-8" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Remark :</label>
                                    <asp:Label ID="lblRemark" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>


                            <div class="col-md-4" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Net PO Amount :</label>
                                    <asp:Label ID="lblNetPOAmt" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-3" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Material Amount :</label>
                                    <asp:Label ID="lblMaterialAmt" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Tax Amount :</label>
                                    <asp:Label ID="lblTaxAmt" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Discount Amount :</label>
                                    <asp:Label ID="lblDiscountAmt" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Other Charges :</label>
                                    <asp:Label ID="lblOtherChg" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>



                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="box">

                                    <div class="page-content-wrap">


                                        <div class="row">
                                            <div class="col-md-12">

                                                <div class="panel panel-default tabs">
                                                    <ul class="nav nav-tabs" role="tablist">
                                                        <li class="active"><a href="#tab-first" role="tab" data-toggle="tab">Material</a></li>
                                                        <li><a href="#tab-second" role="tab" data-toggle="tab">Taxation </a></li>
                                                        <li><a href="#tab-third" role="tab" data-toggle="tab">Other Charges </a></li>
                                                    </ul>
                                                    <div class="panel-body tab-content">

                                                        <div class="tab-pane active" id="tab-first">

                                                            <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                                <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                                                    CssClass="table table-hover table-striped table-bordered nowrap">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="ID" HeaderText="Sr. No." />
                                                                        <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                                        <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
                                                                        <asp:BoundField DataField="ITEMGROUP" HeaderText="Item Group" />
                                                                        <asp:BoundField DataField="ITEMUOM" HeaderText="UOM" />
                                                                        <asp:BoundField DataField="POQTY" HeaderText="Qty" />
                                                                        <asp:BoundField DataField="ITEMRATE" HeaderText="Rate" />
                                                                        <asp:BoundField DataField="ITEMAMOUNT" HeaderText="Amount" />
                                                                        <asp:BoundField DataField="DELIVERYDATE" HeaderText="Delivery Date" DataFormatString="{0:MM/dd/yyyy}" />
                                                                        <asp:BoundField DataField="GLCODE" HeaderText="GL Code" />
                                                                        <asp:BoundField DataField="COSTCENTER" HeaderText="Cost Center" />
                                                                        <asp:BoundField DataField="ITEMPLANTCD" HeaderText="Plant Code" />
                                                                        <asp:BoundField DataField="ITEMLOCCD" HeaderText="Location Code" />
                                                                        <asp:BoundField DataField="PROFITCENTER" HeaderText="Profit Center" />
                                                                        <asp:BoundField DataField="ASSETCODE" HeaderText="Asset Code" />
                                                                        <asp:BoundField DataField="TRACKNO" HeaderText="Tracking No." />
                                                                        <asp:BoundField DataField="ITEMREMARKS" HeaderText="Item Text" />
                                                                        <asp:BoundField DataField="PRNO" HeaderText="PR No." />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>

                                                        </div>

                                                        <div class="tab-pane" id="tab-second">
                                                            <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                                <asp:GridView ID="grvTaxation" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                                                    CssClass="table table-hover table-striped table-bordered nowrap">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="TAXSRNO" HeaderText="Sr. No." />
                                                                        <asp:BoundField DataField="POSRNO" HeaderText="PO Sr. No." />
                                                                        <asp:BoundField DataField="CONDTYPE" HeaderText="Cond. Type" />
                                                                        <asp:BoundField DataField="TAXRATE" HeaderText="Rate" />
                                                                        <asp:BoundField DataField="TAXBASEAMOUNT" HeaderText="Base Amt." />
                                                                        <asp:BoundField DataField="TAXAMOUNT" HeaderText="Tax Amt." />
                                                                        <asp:BoundField DataField="OPERATOR" HeaderText="Operator" />
                                                                    </Columns>
                                                                </asp:GridView>
                                                            </div>
                                                        </div>

                                                        <div class="tab-pane" id="tab-third">
                                                            <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                                <asp:GridView ID="grvOtherChg" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                                                    CssClass="table table-hover table-striped table-bordered nowrap">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="CHRGSRNO" HeaderText="Sr. No." />
                                                                        <asp:BoundField DataField="CHRGTYPE" HeaderText="Charge Type" />
                                                                        <asp:BoundField DataField="CHRGAMOUNT" HeaderText="Charge Amount" />
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

                            <div class="col-md-5" style="margin-top: 5px;">
                                <div class="form-group">
                                    <asp:Label ID="lblAPRV1" runat="server" Visible="false"></asp:Label>
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
    <input type="hidden" id="menutabid" value="tsmTranMMPO" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />
    <input type="hidden" id="menuRptid" value="tsmRptDocPO" runat="server" />
</asp:Content>
