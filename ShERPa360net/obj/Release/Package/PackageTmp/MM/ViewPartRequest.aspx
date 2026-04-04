<%@ Page Title="" Language="C#" MasterPageFile="~/MM/MasterMM.Master" AutoEventWireup="true" CodeBehind="ViewPartRequest.aspx.cs" Inherits="ShERPa360net.MM.ViewPartRequest" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .btnClass {
            height: 32px;
            width: 32px;
        }
    </style>
    <%--<script type="text/javascript">
        $(document).ready(function () {
            InitiatePagination();
        });

        function InitiatePagination() {
            $('[id*=gvList]').prepend($("<thead></thead>").append($('[id*=gvList]').find("tr:first"))).DataTable({
                "responsive": true,
                "scrollX": true,
                "stateSave": true,
                "stateDuration": 40 * 1,
                //"order": [[0, 'desc']],
                "ordering": false,
                "dom": '<"top"lfi<"clear">>rt<"bottom"pB<"clear">>',
                buttons: [{
                    extend: 'excel',
                    title: 'Exported List',
                    text: "Export"
                }]
            });
        }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">

                <div class="form-horizontal">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View  </strong>Part Request</h3>
                        </div>


                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Part Request Date : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">To : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group TopMarg">
                                            <label class="col-md-5 control-label">Request No. : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtRequestNo" runat="server" CssClass="form-control" placeholder="Request No."></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">Job Id : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtJobId" runat="server" CssClass="form-control" MaxLength="10" placeholder="Job Id"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-3">
                                        <div class="form-group TopMarg">
                                            <label class="col-md-5 control-label">Segment : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlSegment" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group TopMarg">
                                            <label class="col-md-5 control-label">Show Closing Stock : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">

                                                    <asp:CheckBox ID="chkClosingStock" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>


                                    <div class="col-md-3">
                                        <div class="form-group TopMarg">
                                            <label class="col-md-5 control-label">Summery Report : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <asp:CheckBox ID="chkSummeryReport" runat="server" CssClass="form-control" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group TopMarg">
                                            <label class="col-md-5 control-label"></label>
                                            <div class="col-md-7 col-xs-12">
                                                <asp:LinkButton runat="server" ID="lnkNewPR" CssClass="btn btn-success pull-left" Text="New PR" PostBackUrl="~/MM/CreatePartRequest.aspx"><i class="fa fa-file-text"></i></asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lnkSearhPR" CssClass="btn btn-success pull-left" Text="Search PR" OnClick="lnkSearhPR_Click"><i class="fa fa-search"></i></asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export PR" OnClick="lnkExport_Click"><i class="fa fa-file"></i></asp:LinkButton>
                                            </div>
                                        </div>
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

    <div class="page-content-wrap">

        <div class="row">
            <div class="col-md-12">


                <div class="panel panel-default">


                    <div class="panel-body">


                        <div class="row">
                            <div class="col-md-12">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                        <div runat="server" id="divList">
                                            <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" OnRowCommand="gvList_RowCommand">
                                                <EmptyDataTemplate>
                                                    No Record Found!
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <%--0--%><asp:BoundField DataField="ReqNo" HeaderText="Req. No." />
                                                    <%--1--%><asp:BoundField DataField="ReqDt" HeaderText="Req. Dt." />
                                                    <%--2--%><asp:BoundField DataField="SEGMENT" HeaderText="Segment" />
                                                    <%--3--%><asp:BoundField DataField="JobId" HeaderText="Job Id" />
                                                    <%--4--%><asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                    <%--5--%><asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
                                                    <%--6--%><asp:BoundField DataField="QTY" HeaderText="Qty" />
                                                    <%--7--%><asp:BoundField DataField="UNIT" HeaderText="Unit" />
                                                    <%--8--%><asp:BoundField DataField="PLANTCD" HeaderText="Plant Code" />
                                                    <%--9--%><asp:BoundField DataField="LOCCD" HeaderText="Location Code" />
                                                    <%--10--%><asp:BoundField DataField="REQBYNAME" HeaderText="Request By" />
                                                    <%--11--%><asp:BoundField DataField="STATUSDESC" HeaderText="Status" />
                                                    <%--12--%><asp:TemplateField HeaderText="Action" Visible="True">
                                                        <ItemTemplate>
                                                            <asp:Button runat="server" ID="btnPartIssue" Text="Part Issue" OnClick="btnPartIssue_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--13--%><asp:BoundField DataField="ENTRYBY" HeaderText="Create By" />
                                                    <%--14--%><asp:BoundField DataField="ENTRYDATE" HeaderText="Create Date" />
                                                    <%--15--%><asp:BoundField DataField="REMARK" HeaderText="Remarks" />
                                                    <%--16--%><asp:TemplateField HeaderText="Enter Remark" Visible="True">
                                                        <ItemTemplate>
                                                            <asp:Button runat="server" ID="btnEnterRemark" Text="Enter Remark" OnClick="btnEnterRemark_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--17--%><asp:BoundField DataField="MAKE" HeaderText="Make" />
                                                    <%--18--%><asp:BoundField DataField="MODEL" HeaderText="Model" />
                                                    <%--19--%><asp:BoundField DataField="ITEMSUBGRPDESC" HeaderText="Item Sub Group" />

                                                    <%--<asp:BoundField DataField="UPDATEBY" HeaderText="Update By" />
                                                <asp:BoundField DataField="UPDATEDATE" HeaderText="Update Dt." />
                                                <asp:BoundField DataField="DELAYS" HeaderText="Delays" />--%>


                                                    <%--20--%><asp:BoundField DataField="ITEMID" HeaderText="ITEMID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <%--21--%><asp:BoundField DataField="JOBSTATDESC" HeaderText="JOBSTATDESC" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <%--22--%><asp:BoundField DataField="JOBSTATUS" HeaderText="JOBSTATUS" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <%--23--%><asp:BoundField DataField="SEGMENTDESC" HeaderText="SEGMENTDESC" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <%--24--%><asp:BoundField DataField="UOM" HeaderText="UOM" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <%--25--%><asp:BoundField DataField="REQBY" HeaderText="REQBY" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <%--26--%><asp:BoundField DataField="ITEMSUBGRP" HeaderText="ITEM SUB GRP ID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                    <%--27--%><asp:BoundField DataField="CLSSTK" HeaderText="Closing Stock" />
                                                    <%--28--%><asp:TemplateField HeaderText="Action" Visible="True">
                                                        <ItemTemplate>
                                                            <asp:LinkButton runat="server" ID="btnEdit" Text="View" OnClick="btnEdit_Click"></asp:LinkButton>
                                                            <%-- |
                                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%#Eval("ReqNo") %>'
                                                            CommandName="eDelete" OnClientClick="return confirm('Are you sure you want to cancel this part request?');">Cancel</asp:LinkButton>--%>
                                                            <%-- | 
                                                        <asp:LinkButton runat="server" ID="bntView" Text="Details" OnClick="bntView_Click"></asp:LinkButton>
                                                        | 
                                                        <asp:LinkButton runat="server" ID="btnPrint" Text="PDF" OnClick="btnPrint_Click"></asp:LinkButton>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <div runat="server" id="divSummery">
                                            <asp:GridView ID="gvListSummery" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%" OnRowCommand="gvList_RowCommand">
                                                <EmptyDataTemplate>
                                                    No Record Found!
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <%--0--%><asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                    <%--1--%><asp:BoundField DataField="ITEMDESC" HeaderText="Item Desc." />
                                                    <%--2--%><asp:BoundField DataField="UNIT" HeaderText="Unit" />
                                                    <%--3--%><asp:BoundField DataField="REQQTY" HeaderText="Part Request" />
                                                    <%--4--%><asp:BoundField DataField="CLSSTK" HeaderText="Main Stock" />
                                                    <%--5--%><asp:BoundField DataField="ORDQTY" HeaderText="To Be Order" />
                                                    <%--6--%><asp:BoundField DataField="MAKE" HeaderText="Make" />
                                                    <%--7--%><asp:BoundField DataField="MODEL" HeaderText="Model" />
                                                    <%--8--%><asp:BoundField DataField="ITEMSUBGRPDESC" HeaderText="Item Sub Group" />
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




    <div class="modal fade" id="modal-detail" data-backdrop="static">



        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Part</strong> Request</h4>
                </div>
                <asp:UpdatePanel ID="updItemDetails" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <%--<asp:PostBackTrigger ControlID="txtItemQty" />--%>
                        <asp:AsyncPostBackTrigger ControlID="txtJobAddId" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="txtItemCode" EventName="TextChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlPLant" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="ddlUOM" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="lnnkClosePopup" EventName="Click" />
                    </Triggers>
                    <ContentTemplate>
                        <div class="modal modal-warning fade" id="modal-warningPR" data-backdrop="static">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="alert alert-warning alert-dismissible">
                                        <%--<button type="button" class="close" data-dismiss="modal" runat="server" onclick="">&times;</button>--%>
                                        <asp:LinkButton ID="lnnkClosePopup" runat="server" CssClass="btn btn-success pull-right" OnClick="lnnkClosePopup_Click"><i class="fas fa-times"></i></asp:LinkButton>

                                        <h4><i class="icon fa fa-warning"></i>&nbsp;Alert</h4>
                                        <asp:Label ID="lblAlertMsgPR" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="panel-body">

                            <div class="row">

                                <div class="col-md-12">

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Job Id : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <label class="col-md-5 control-label" id="lblReqNo" runat="server" visible="false"></label>
                                                <asp:TextBox ID="txtJobAddId" runat="server" CssClass="form-control required_text_box" placeholder="Job Id" OnTextChanged="txtJobAddId_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvJobId" runat="server" ControlToValidate="txtJobAddId" ValidationGroup="Save"
                                                    ErrorMessage="Please Enter Job Id">*</asp:RequiredFieldValidator>


                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <%--<label class="col-md-5 control-label">Segment : </label>--%>
                                            <label class="col-md-5 control-label" id="lblSegment" runat="server" visible="false"></label>
                                            <label class="col-md-12 control-label" id="lblSegmentDesc" runat="server"></label>

                                            <%--<div class="col-md-7 col-xs-12">

                                                <asp:Label ID="lblSegment" runat="server" CssClass="form-control"></asp:Label>
                                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Job Id" OnTextChanged="txtJobId_TextChanged"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtJobId" ValidationGroup="Save"
                                                            ErrorMessage="Please Enter Job Id">*</asp:RequiredFieldValidator>
                                            </div>--%>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <%--<label class="col-md-5 control-label">Job Status : </label>--%>
                                            <label class="col-md-12 control-label" id="lblJobStatus" runat="server" visible="true"></label>
                                            <%--<div class="col-md-7 col-xs-12">

                                                <asp:Label ID="lblJobStatus" runat="server" CssClass="form-control"></asp:Label>
                                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" placeholder="Job Id" OnTextChanged="txtJobId_TextChanged"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtJobId" ValidationGroup="Save"
                                                            ErrorMessage="Please Enter Job Id">*</asp:RequiredFieldValidator>
                                            </div>--%>
                                        </div>
                                    </div>

                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Item Code : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <div class="input-group">

                                                    <asp:TextBox ID="txtItemCode" runat="server" CssClass="form-control required_text_box" placeholder="Item Code" OnTextChanged="txtItemCode_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    <%--<span class="input-group-btn">
                                                        <asp:LinkButton ID="lnkOpenPoup" runat="server" CssClass="btn btn-success" OnClick="lnkOpenPoup_Click" Text="Open Popup"><span class="fa fa-search"></span></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkNewPartCode" runat="server" CssClass="btn btn-success" OnClick="lnkNewPartCode_Click" Text="Open Popup"><span class="fa fa-plus-circle"></span></asp:LinkButton>
                                                    </span>--%>
                                                </div>
                                                <asp:RequiredFieldValidator ID="rfvItemCode" runat="server" ControlToValidate="txtItemCode" ValidationGroup="Save"
                                                    ErrorMessage="Please Enter Item Code">*</asp:RequiredFieldValidator>
                                                <%--<asp:Button ID="btnPopup" runat="server" AccessKey="Z" OnClick="lnkOpenPoup_Click" CssClass="hidden" />--%>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <%--<label class="col-md-5 control-label">Item Desc. : </label>--%>
                                            <label class="col-md-7 control-label" id="lblItemDesc" runat="server"></label>
                                            <label class="col-md-5 control-label" id="lblItemId" runat="server" visible="false"></label>
                                            <label class="col-md-5 control-label" id="lblSKU" runat="server" visible="false"></label>
                                            <div class="col-md-7 col-xs-12">
                                                <%--<asp:Label ID="lblItemDesc" runat="server"></asp:Label>
                                                        <asp:Label ID="lblItemId" runat="server"></asp:Label>--%>
                                                <%--<asp:TextBox ID="TextBox1" runat="server" CssClass="form-control" placeholder="Item Code"></asp:TextBox>--%>
                                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtItemCode" ValidationGroup="Save"
                                                            ErrorMessage="Please Enter Item Code">*</asp:RequiredFieldValidator>--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Quantity : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control required_text_box" placeholder="Quantity" Text="1"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvQuantity" runat="server" ControlToValidate="txtQuantity" ValidationGroup="Save"
                                                    ErrorMessage="Please Enter Quantity">*</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">UOM :</label>
                                            <div class="col-md-9 col-xs-12">
                                                <asp:DropDownList ID="ddlUOM" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlUOM_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvUOM" ControlToValidate="ddlUOM" runat="server" ValidationGroup="Save"
                                                    ErrorMessage="Please Select Item UOM" InitialValue="0">*</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Plant Code : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <asp:DropDownList ID="ddlPLant" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlPLant_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvPLant" ControlToValidate="ddlPLant" runat="server" ValidationGroup="Save"
                                                    ErrorMessage="Please Select Plant" InitialValue="0">*</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Location : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <asp:DropDownList ID="ddlLocation" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvLocation" ControlToValidate="ddlLocation" runat="server" ValidationGroup="Save"
                                                    ErrorMessage="Please Select Location" InitialValue="0">*</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Req. By : </label>
                                            <div class="col-md-9 col-xs-12">
                                                <asp:DropDownList ID="ddlReqBy" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvReqBy" ControlToValidate="ddlReqBy" runat="server" ValidationGroup="Save"
                                                    ErrorMessage="Please Select Requested By" InitialValue="0">*</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>


                                </div>
                            </div>

                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

                <%--<div class="panel-footer" style="height: 50px;">
                    <%--<button type="button" class="btn btn-success pull-right"><i class="fa fa-plus-square"></i></button>--%>
                <%--<asp:LinkButton ID="btnSaveDet" runat="server" CssClass="btn btn-success pull-right" OnClick="btnSaveDet_Click" Text="Save Item" ValidationGroup="Save"><i class="fas fa-edit"></i></asp:LinkButton>
                </div>--%>
            </div>
        </div>
    </div>



    <div class="modal fade" id="modal-remark" data-backdrop="static">



        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Enter</strong> Remark</h4>
                </div>

                <div class="panel-body">

                    <div class="row">

                        <div class="col-md-12">

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Remark : </label>
                                    <div class="col-md-9 col-xs-12">
                                        <label class="col-md-5 control-label" id="lblRemarkReqNo" runat="server" visible="false"></label>

                                        <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control required_text_box" placeholder="Remark"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvRemark" runat="server" ControlToValidate="txtRemark" ValidationGroup="ERemark"
                                            ErrorMessage="Please Enter Remark">*</asp:RequiredFieldValidator>


                                    </div>
                                </div>
                            </div>

                        </div>



                    </div>
                    <div>
                        <%--class="panel-footer"--%>
                        <%--<button type="button" class="btn btn-success pull-right"><i class="fa fa-plus-square"></i></button>--%>
                        <asp:LinkButton ID="btnSubmitRemark" runat="server" CssClass="btn btn-success pull-right" OnClick="btnSubmitRemark_Click" Text="OK" ValidationGroup="ERemark"><i class="fas fa-edit"></i></asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>

    </div>

    <asp:ValidationSummary ID="validateRemark" runat="server" ShowMessageBox="true" ShowSummary="false" ValidationGroup="ERemark" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">
    <input type="hidden" id="menutabid" value="tsmTranMMPartReq" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranMM" runat="server" />
</asp:Content>
