<%@ Page Title="" Language="C#" MasterPageFile="~/Logistic/MasterLogistic.Master" AutoEventWireup="true" CodeBehind="frmPickupAssign.aspx.cs" Inherits="ShERPa360net.Logistic.frmPickupAssign" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Logistic Assign</title>

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
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Phy. Doc. Var. </strong>Data</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">PO Date : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">To : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">SI No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtSINo" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSerch" CssClass="btn btn-success pull-left" Text="Search" OnClick="lnkSerch_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                            <%--<asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export" OnClick="lnkExport_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>--%>
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
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; height: 500px !important;">
                                        <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="SINO" HeaderText="SI No." />
                                                <asp:BoundField DataField="SRNO" HeaderText="SI Sr. No." />
                                                <asp:BoundField DataField="SIDT" HeaderText="SI Dt." />
                                                <asp:BoundField DataField="JOBID" HeaderText="Job Id" />
                                                <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                <asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc" />
                                                <asp:BoundField DataField="PLANTCD" HeaderText="Plant" />
                                                <asp:BoundField DataField="LOCCD" HeaderText="Location" />
                                                <asp:BoundField DataField="QTY" HeaderText="Qty." />
                                                <asp:BoundField DataField="DISCAMT" HeaderText="Disc." />
                                                <asp:BoundField DataField="CAMOUNT" HeaderText="Total Amt." />
                                                <asp:BoundField DataField="REMAINAMT" HeaderText="Pending Amt." />
                                                <asp:BoundField DataField="PAYMODE" HeaderText="Pay Mode" />
                                                <asp:BoundField DataField="JOBSTATDESC" HeaderText="Job Status" />
                                                <asp:BoundField DataField="REFNO" HeaderText="Order ID" />
                                                <asp:BoundField DataField="SONO" HeaderText="SO No." />
                                                <asp:BoundField DataField="ENTITYID" HeaderText="Entity ID" />
                                                <asp:BoundField DataField="FWAYBILLNO" HeaderText="Way Bill No." />
                                                <asp:BoundField DataField="FTRANNAME" HeaderText="Courier name" />
                                                <asp:TemplateField HeaderText="Action">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnGenerateWayBill" runat="server" CssClass="btn btn-success" OnClick="btnGenerateWayBill_Click" Text="Generate WayBill" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:BoundField DataField="ESTIMATESTATUS" HeaderText="Estimate Status" />
                                                <asp:BoundField DataField="JOBSTATDESC" HeaderText="Job Status" />
                                                <asp:BoundField DataField="CREATEDATE" HeaderText="Create Date" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField DataField="QCBY" HeaderText="QC By" />
                                                <asp:BoundField DataField="QCRESULT" HeaderText="QC Result" />
                                                <asp:BoundField DataField="ONHOLDREASON" HeaderText="Hold Reason" />
                                                <asp:BoundField DataField="FAILREASON" HeaderText="Fail Reason" />
                                                <asp:BoundField DataField="JWREFNO2" HeaderText="Order Id" />--%>
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
                    <h4 class="modal-title" style="color: #337ab7"><strong>SI</strong> Details</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <%--Sender Detail Start--%>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Sender :</label>
                                        <asp:TextBox ID="lblSenderName" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Sender Address :</label>
                                        <asp:TextBox ID="lblSenderAdd1" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:TextBox ID="lblSenderAdd2" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:TextBox ID="lblSenderAdd3" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Pincode :</label>
                                        <asp:Label ID="lblSenderPincode" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Mobile :</label>
                                        <asp:Label ID="lblSenderMobile" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Telephone :</label>
                                        <asp:Label ID="lblSenderTelephone" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Email :</label>
                                        <asp:Label ID="lblSenderEmail" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <%--Sender Detail End--%>

                            <%--Consignee Detail Start--%>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Cnee. Name :</label>
                                        <asp:TextBox ID="lblConsiName" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Cnee. Address :</label>
                                        <asp:TextBox ID="lblConsiAddr1" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:TextBox ID="lblConsiAddr2" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label></label>
                                        <asp:TextBox ID="lblConsiAddr3" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Pincode :</label>
                                        <asp:Label ID="lblConsiPincode" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Mobile :</label>
                                        <asp:Label ID="lblConsiMobile" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Telephone :</label>
                                        <asp:Label ID="lblConsiTelephone" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Attention :</label>
                                        <asp:Label ID="lblConsiAttention" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <%--Consignee Detail End--%>

                            <%--Service Detail Start--%>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Piece :</label>
                                        <asp:Label ID="lblQty" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Prod. Code :</label>
                                        <asp:Label ID="lblProductCode" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Sub Prod. Code :</label>
                                        <asp:Label ID="lblSubProdCode" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Weight :</label>
                                        <asp:Label ID="lblWeight" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Invoice No. :</label>
                                        <asp:Label ID="lblInvoiceno" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Declare Value :</label>
                                        <asp:Label ID="lblDeclareValue" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Collectable Amt. :</label>
                                        <asp:Label ID="lblCollectableAmt" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Spcl. Instruction :</label>
                                        <%--<asp:Label ID="lblSpecialInstruction" runat="server" CssClass="form-control"></asp:Label>--%>
                                        <asp:TextBox ID="lblSpecialInstruction" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Pack Type :</label>
                                        <asp:Label ID="lblPackType" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Credit Ref. No. :</label>
                                        <asp:Label ID="lblCreditRefNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Regi. Pickup :</label>
                                        <asp:CheckBox ID="chkPickup" runat="server" CssClass="form-control" Checked="true" Enabled="false" />
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Is RVP :</label>
                                        <asp:CheckBox ID="chkIsRVP" runat="server" CssClass="form-control" Checked="false" Enabled="false" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Cmdt Detail1 :</label>
                                        <asp:Label ID="lblCmdtDetail1" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Product Type :</label>
                                        <asp:RadioButtonList ID="rblProductType" runat="server" CssClass="chclass" Enabled="false" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Dox" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="NDox" Value="1" Selected="True"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Waybill PDF :</label>
                                        <asp:RadioButtonList ID="rblWaybillPDF" runat="server" CssClass="chclass" Enabled="false" RepeatDirection="Horizontal">
                                            <asp:ListItem Text="Required" Value="1" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="Not Required" Value="0"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>

                                <%--<div class="col-md-3">
                                    <div class="form-group">
                                        <label>Is RVP :</label>
                                        <asp:CheckBox ID="CheckBox2" runat="server" CssClass="form-control" Checked="false" Enabled="false" />
                                    </div>
                                </div>--%>
                            </div>
                            <%--Service Detail End--%>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <br />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <br />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <br />
                                    </div>
                                </div>



                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:HiddenField ID="hfOriginArea" runat="server" />
                                        <asp:HiddenField ID="hfCustCode" runat="server" />
                                        <asp:HiddenField ID="hfPlant" runat="server" />
                                        <asp:HiddenField ID="hfSINO" runat="server" />
                                        <asp:HiddenField ID="hfjobID" runat="server" />
                                        <asp:HiddenField ID="hfSONO" runat="server" />
                                        <asp:HiddenField ID="hfEntityID" runat="server" />
                                        <asp:Button ID="btnGenerate" runat="server" CssClass="btn btn-success" Text="Generate" OnClick="btnGenerate_Click" />
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

    <input type="hidden" id="menutabid" value="tsmLogisticRequest" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmLogisticSys" runat="server" />

</asp:Content>
