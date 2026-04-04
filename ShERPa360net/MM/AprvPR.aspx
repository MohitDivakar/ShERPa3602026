<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="AprvPR.aspx.cs" Inherits="ShERPa360net.MM.AprvPR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="page-content-wrap">

        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>Requisition</h3>
                        </div>


                        <div class="panel-body">

                            <div class="row">

                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">



                                            <label class="col-md-3 control-label">PR Date : </label>
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

                                    <div class="col-md-3">
                                        <div class="form-group">



                                            <label class="col-md-3 control-label">PR No. : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtPrno" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>

                                                </div>
                                            </div>



                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:LinkButton runat="server" ID="lnkSearhPR" CssClass="btn btn-success pull-left" Text="Search PR" OnClick="lnkSearhPR_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export PR"><i class="fa fa-file"></i></asp:LinkButton>
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
                                    <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 500px !important;">
                                        <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="PRTYPE" HeaderText="PR Type" />
                                                <asp:BoundField DataField="PRNO" HeaderText="PR No." />
                                                <asp:BoundField DataField="PRDTD" HeaderText="PR Date" />
                                                <asp:BoundField DataField="DEPTNAME" HeaderText="Department Name" />
                                                <asp:BoundField DataField="PLANTCD" HeaderText="Plant Code" />
                                                <asp:BoundField DataField="REMARK" HeaderText="Remark" />
                                                <asp:BoundField DataField="LISTDESC" HeaderText="Status" />
                                                <asp:BoundField DataField="ENTEREDBY" HeaderText="Entered By" />
                                                <asp:BoundField DataField="UPDATEBY" HeaderText="Updated By" />
                                                <asp:TemplateField HeaderText="Action" Visible="True">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" ID="btnDetails" Text="Details" OnClick="btnDetails_Click"></asp:LinkButton>
                                                        | 
                                                        <asp:LinkButton runat="server" ID="btnApprove" Text="Approve" OnClick="btnApprove_Click"></asp:LinkButton>
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
                    <h4 class="modal-title" style="color: #337ab7"><strong>PR</strong> Details</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Doc Type :</label>
                                    <asp:Label ID="lblDoctype" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>PR No. :</label>
                                    <asp:Label ID="lblPRNo" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>PR Date :</label>
                                    <asp:Label ID="lblPRDate" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Remark :</label>
                                    <asp:Label ID="lblRemark" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll">
                                        <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                            CssClass="table table-hover table-striped table-bordered nowrap" OnRowEditing="gvDetail_RowEditing" OnRowCancelingEdit="gvDetail_RowCancelingEdit" OnRowUpdating="gvDetail_RowUpdating">
                                            <Columns>
                                                <%--<asp:BoundField DataField="ID" HeaderText="Sr. No." />--%>
                                                <asp:TemplateField HeaderText="Sr. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />--%>
                                                <asp:TemplateField HeaderText="Item Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblITEMCODE" runat="server" Text='<%# Bind("ITEMCODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />--%>
                                                <asp:TemplateField HeaderText="Item Desc.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblITEMDESC" runat="server" Text='<%# Bind("ITEMDESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="ITEMGROUP" HeaderText="Item Group" />--%>
                                                <asp:TemplateField HeaderText="Item Group">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblITEMGROUP" runat="server" Text='<%# Bind("ITEMGROUP") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="ITEMUOM" HeaderText="UOM" />--%>
                                                <asp:TemplateField HeaderText="UOM">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblITEMUOM" runat="server" Text='<%# Bind("ITEMUOM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="ITEMQTY" HeaderText="Qty" />--%>
                                                <asp:TemplateField HeaderText="Qty">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblITEMQTY" runat="server" Text='<%# Bind("ITEMQTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="ITEMRATE" HeaderText="Rate" />--%>
                                                <asp:TemplateField HeaderText="Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblITEMRATE" runat="server" Text='<%# Bind("ITEMRATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="ITEMAMOUNT" HeaderText="Amount" />--%>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblITEMAMOUNT" runat="server" Text='<%# Bind("ITEMAMOUNT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="DELIVERYDATE" HeaderText="Delivery Date" DataFormatString="{0:MM/dd/yyyy}" ApplyFormatInEditMode="false" />--%>
                                                <asp:TemplateField HeaderText="Delivery Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDELIVERYDATE" runat="server" Text='<%# Bind("DELIVERYDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="GLCODE" HeaderText="GL Code" />--%>
                                                <asp:TemplateField HeaderText="GL Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGLCODE" runat="server" Text='<%# Bind("GLCODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="COSTCENTER" HeaderText="Cost Center" />--%>
                                                <asp:TemplateField HeaderText="Cost Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCOSTCENTER" runat="server" Text='<%# Bind("COSTCENTER") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="ITEMPLANTCD" HeaderText="Plant Code" />--%>
                                                <asp:TemplateField HeaderText="Plant Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblITEMPLANTCD" runat="server" Text='<%# Bind("ITEMPLANTCD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="ITEMLOCCD" HeaderText="Location Code" />--%>
                                                <asp:TemplateField HeaderText="Location Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblITEMLOCCD" runat="server" Text='<%# Bind("ITEMLOCCD") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="PROFITCENTER" HeaderText="Profit Center" />--%>
                                                <asp:TemplateField HeaderText="Profit Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPROFITCENTER" runat="server" Text='<%# Bind("PROFITCENTER") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="ASSETCODE" HeaderText="Asset Code" />--%>
                                                <asp:TemplateField HeaderText="Asset Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblASSETCODE" runat="server" Text='<%# Bind("ASSETCODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="REQUISITIONER" HeaderText="Requisitioner" />--%>
                                                <asp:TemplateField HeaderText="Requisitioner">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblREQUISITIONER" runat="server" Text='<%# Bind("REQUISITIONER") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="TRACKNO" HeaderText="Tracking No." />--%>
                                                <asp:TemplateField HeaderText="Tracking No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTRACKNO" runat="server" Text='<%# Bind("TRACKNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="ITEMTEXT" HeaderText="Item Text" />--%>
                                                <asp:TemplateField HeaderText="Item Text">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblITEMTEXT" runat="server" Text='<%# Bind("ITEMTEXT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:BoundField DataField="PARTREQNO" HeaderText="Part Req. No." />--%>
                                                <asp:TemplateField HeaderText="Part Req. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPARTREQNO" runat="server" Text='<%# Bind("PARTREQNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("STATUS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:RadioButtonList ID="rblTrueFalse" runat="server">
                                                            <asp:ListItem Text="Approve" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="Reject" Value="0"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <asp:CommandField ShowEditButton="true" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>


                            <div class="col-md-5" style="margin-top: 5px;">
                                <div class="form-group">
                                    <asp:Label ID="lblAPRV1" runat="server" Visible="false"></asp:Label>
                                    <br />
                                    <asp:Label ID="lblAPRV2" runat="server" Visible="false"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3" style="margin-top: 5px;">
                                <div class="form-group">
                                    <asp:TextBox ID="txtApRejDetReason" runat="server" CssClass="form-control" placeholder="Approve / Reject Reason"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtApRejDetReason" ValidationGroup="ValDetRej">Enter Reject Reason</asp:RequiredFieldValidator>
                                    <br />
                                    <asp:LinkButton runat="server" ID="lnkReject" CssClass="btn btn-success pull-left" Text="Reject PR" OnClick="lnkReject_Click" Visible="false" ValidationGroup="ValDetRej"><i class="fa fa-times-circle"></i> Reject</asp:LinkButton>
                                    <asp:LinkButton runat="server" ID="lnkApprove" CssClass="btn btn-success pull-left" Text="Approve PR" OnClick="lnkApprove_Click" Visible="false"><i class="fa fa-check-circle"></i> Approve</asp:LinkButton>

                                </div>
                            </div>



                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="message-box animated fadeIn" data-sound="alert" id="mb-aprv">

        <div class="mb-container">

            <button type="button" class="close" data-dismiss="modal" aria-label="Close" style="color: #ffffff;">
                <span aria-hidden="true">&times;</span></button>
            <h4 class="modal-title" style="color: #faa61a;"><strong>PR</strong> Approval</h4>
            <div class="mb-middle">
                <div class="mb-title" style="color: #faa61a"><span class="fa fa-check"></span>Approve <strong>PR</strong> ?</div>
                <div class="mb-content">
                    <h4 style="color: #ffffff;">PR No. : <strong>
                        <asp:Label runat="server" ID="lblPopupPRNO"></asp:Label></strong></h4>
                    <h4 style="color: #ffffff;">Are you sure you want to Approve this PR?</h4>
                </div>

                <%--<div class="col-md-3" style="margin-top: 5px;">
                    <div class="form-group">
                        <asp:Label ID="lblPopAPRV1" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="lblPopAPRV2" runat="server"></asp:Label>
                    </div>
                </div>--%>

                <div class="mb-footer">
                    <div class="pull-right">
                        <%--<a id="btnApprove" class="btn btn-success btn-lg">Yes</a>
                        <button class="btn btn-default btn-lg mb-control-close">No</button>--%>
                        <asp:TextBox ID="txtAPREJReason" runat="server" CssClass="form-control" placeholder="Approve / Reject Reason"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvVal" runat="server" ControlToValidate="txtAPREJReason" ValidationGroup="ValRej">Enter Reject Reason</asp:RequiredFieldValidator>
                        <br />
                        <asp:LinkButton runat="server" ID="lnkPopReject" CssClass="btn btn-success pull-left" Text="Reject PR" OnClick="lnkPopReject_Click" ValidationGroup="ValRej"><i class="fa fa-times-circle"></i> Reject</asp:LinkButton>
                        <asp:LinkButton runat="server" ID="lnkPopApprove" CssClass="btn btn-success pull-left" Text="Approve PR" OnClick="lnkPopApprove_Click"><i class="fa fa-check-circle"></i> Approve</asp:LinkButton>
                        <asp:Label runat="server" ID="lblRightsMessage" Visible="false"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmTranMMPR" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />
</asp:Content>
