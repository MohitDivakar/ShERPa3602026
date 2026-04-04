<%@ Page Title="" Language="C#" MasterPageFile="~/SD/SDMaster.Master" AutoEventWireup="true" CodeBehind="frmSODeviation.aspx.cs" Inherits="ShERPa360net.SD.frmSODeviation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>SO Deviation</title>
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; SO  </strong>Deviation</h3>
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">SO No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtSONO" runat="server" CssClass="form-control" TextMode="Number" MaxLength="10" placeholder="SO No."></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvSONO" runat="server" ControlToValidate="txtSONO" ForeColor="Red" Display="Dynamic"
                                                    ErrorMessage="Enter SO No." ValidationGroup="Search">Enter SO No.</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">SO Sr. No. : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <asp:TextBox ID="txtSOSrNo" runat="server" CssClass="form-control" TextMode="Number" MaxLength="2" placeholder="SO Sr. No."></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvSOSrNo" runat="server" ControlToValidate="txtSOSrNo" ForeColor="Red" Display="Dynamic"
                                                    ErrorMessage="Enter SO Sr. No." ValidationGroup="Search">Enter SO Sr. No.</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <%--<asp:LinkButton runat="server" ID="lnkNewSO" CssClass="btn btn-success pull-left" Text="New SO" PostBackUrl="~/SD/frmSO.aspx"><i class="fa fa-file-text"></i></asp:LinkButton>--%>
                                            <asp:LinkButton runat="server" ID="lnkSerchSO" CssClass="btn btn-success pull-left" Text="Search SO" OnClick="lnkSerchSO_Click" ValidationGroup="Search">
<span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                            <%--<asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export SO" OnClick="lnkExport_Click"><i class="fa fa-file"></i></asp:LinkButton>--%>
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
                                    <asp:Table ID="tblDT" runat="server" CssClass="table table-hover table-striped table-bordered nowrap" Visible="false" Width="100%">
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lbltxtSONO" runat="server" CssClass="form-control" Text="SO No. :"></asp:Label>
                                            </asp:TableCell>

                                            <asp:TableCell>
                                                <asp:Label ID="lblSONO" runat="server" CssClass="form-control"></asp:Label>
                                            </asp:TableCell>

                                            <asp:TableCell>
                                                <asp:Label ID="lbltxtSOSRNO" runat="server" CssClass="form-control" Text="SO Sr. No. :"></asp:Label>
                                            </asp:TableCell>

                                            <asp:TableCell>
                                                <asp:Label ID="lblSOSRNO" runat="server" CssClass="form-control"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>

                                        <asp:TableHeaderRow>
                                            <asp:TableHeaderCell ColumnSpan="2" HorizontalAlign="Center" Style="text-align-last: center; width: 50% !important; background-color: #FAA61A !important; border: inset;">
                                                Old Details
                                            </asp:TableHeaderCell>
                                            <asp:TableHeaderCell ColumnSpan="2" HorizontalAlign="Center" Style="text-align-last: center; width: 50% !important; background-color: #FAA61A !important; border: inset;">
                                                New Details
                                            </asp:TableHeaderCell>
                                        </asp:TableHeaderRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lbltxtOldItemCode" runat="server" CssClass="form-control" Text="Old Item Code :"></asp:Label>
                                            </asp:TableCell>

                                            <asp:TableCell>
                                                <asp:Label ID="lblOldItemCode" runat="server" CssClass="form-control"></asp:Label>
                                                <asp:Label ID="lblOldItemID" runat="server" CssClass="form-control" Visible="false"></asp:Label>
                                            </asp:TableCell>

                                            <asp:TableCell>
                                                <asp:Label ID="lbltxtNewItemCode" runat="server" CssClass="form-control" Text="New Item Code :"></asp:Label>
                                            </asp:TableCell>

                                            <asp:TableCell>
                                                <%--<asp:TextBox ID="txtItemcode" runat="server" CssClass="form-control" OnTextChanged="txtItemcode_TextChanged" AutoPostBack="true"></asp:TextBox>--%>
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtItemcode" runat="server" placeholder="Item Code" AutoPostBack="true" CssClass="form-control required_text_box" OnTextChanged="txtItemcode_TextChanged" Enabled="true"></asp:TextBox>
                                                    <span class="input-group-btn">
                                                        <asp:LinkButton ID="lnkOpenPoup" runat="server" CssClass="btn btn-success" OnClick="lnkOpenPoup_Click" Text="Open Popup" Enabled="true"><span class="fa fa-search"></span></asp:LinkButton>
                                                    </span>
                                                </div>
                                                <%--<cc:AutoCompleteExtender ServiceMethod="GetItemCode" MinimumPrefixLength="1" CompletionInterval="10"
                                                    EnableCaching="false" CompletionSetCount="1" TargetControlID="txtItemcode" ID="Auto1" runat="server"
                                                    FirstRowSelected="false" ShowOnlyCurrentWordInCompletionListItem="true">
                                                </cc:AutoCompleteExtender>--%>
                                                <asp:RequiredFieldValidator ID="rfvNewItemCode" runat="server" ControlToValidate="txtItemcode" ForeColor="Red"
                                                    Display="Dynamic" ErrorMessage="Enter Item Code" ValidationGroup="Save">Enter Item Code</asp:RequiredFieldValidator>
                                                <asp:Label ID="txtItemID" runat="server" CssClass="form-control" Visible="false"></asp:Label>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lbltxtOldItemDesc" runat="server" CssClass="form-control" Text="Old Item Desc. :"></asp:Label>
                                            </asp:TableCell>

                                            <asp:TableCell>
                                                <asp:Label ID="lblOldItemDesc" runat="server" CssClass="form-control"></asp:Label>
                                            </asp:TableCell>

                                            <asp:TableCell>
                                                <asp:Label ID="lbltxtNewNewItemDesc" runat="server" CssClass="form-control" Text="New Item Desc. :"></asp:Label>
                                            </asp:TableCell>

                                            <asp:TableCell>
                                                <asp:TextBox ID="lblNewItemDesc" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvNewItemDesc" runat="server" ControlToValidate="lblNewItemDesc" ForeColor="Red"
                                                    Display="Dynamic" ErrorMessage="Enter Item Desc." ValidationGroup="Save">Enter Item Desc.</asp:RequiredFieldValidator>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lbltxtOldItemGrade" runat="server" CssClass="form-control" Text="Old Item Grade :"></asp:Label>
                                            </asp:TableCell>

                                            <asp:TableCell>
                                                <asp:Label ID="lblOldItemGrade" runat="server" CssClass="form-control"></asp:Label>
                                            </asp:TableCell>

                                            <asp:TableCell>
                                                <asp:Label ID="lbltxtNewItemGrade" runat="server" CssClass="form-control" Text="New Item Grade :"></asp:Label>
                                            </asp:TableCell>

                                            <asp:TableCell>
                                                <asp:DropDownList ID="ddlItemGrade" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <asp:RequiredFieldValidator ID="rfvNewGrade" runat="server" ControlToValidate="ddlItemGrade" ForeColor="Red" Display="Dynamic"
                                                    InitialValue="0" ErrorMessage="Select Grade" ValidationGroup="Save">Select Grade</asp:RequiredFieldValidator>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableRow>
                                            <asp:TableCell>
                                                <asp:Label ID="lbltxtReason" runat="server" CssClass="form-control" Text="Reason to change :"></asp:Label>
                                            </asp:TableCell>
                                            <asp:TableCell ColumnSpan="3">
                                                <asp:TextBox ID="txtReason" runat="server" CssClass="form-control" placeholder="Reason to change"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvReason" runat="server" ControlToValidate="txtReason" ForeColor="Red"
                                                    Display="Dynamic" ErrorMessage="Enter Reason to Change" ValidationGroup="Save">Enter Reason to Change</asp:RequiredFieldValidator>
                                            </asp:TableCell>
                                        </asp:TableRow>
                                        <asp:TableFooterRow>
                                            <asp:TableCell ColumnSpan="4" HorizontalAlign="Right">
                                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btnSave_Click" ValidationGroup="Save" />
                                            </asp:TableCell>
                                        </asp:TableFooterRow>
                                    </asp:Table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-item" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7">Item List</h4>
                </div>



                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Item Code :</label>
                                        <asp:TextBox ID="txtpopItemCode" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Item Desc. :</label>
                                        <asp:TextBox ID="txtPopupItemDesc" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Category :</label>
                                        <asp:DropDownList ID="ddlpopCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Group :</label>
                                        <asp:DropDownList ID="ddlpopGroup" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Sub Group :</label>
                                        <asp:DropDownList ID="ddlpopSubGroup" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-12">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Make :</label>
                                        <asp:DropDownList ID="ddlpopMake" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlpopMake_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Model :</label>
                                        <asp:DropDownList ID="ddlpopModel" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12" style="margin-top: 5px;">
                                <%--<asp:ImageButton ID="imgFind" CssClass="btnClass" runat="server" OnClick="imgFind_Click" ImageUrl="~/img/icons/find.png" AlternateText="Show Data" ToolTip="Show PR Data" />--%>
                                <asp:LinkButton ID="btnShowItem" runat="server" CssClass="btn btn-success pull-left" OnClick="btnShowItem_Click" Text="Show Item"><span class="fa fa-search"></span></asp:LinkButton>
                            </div>

                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="box">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll">
                                        <asp:GridView ID="grvPopItem" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                            CssClass="table table-hover table-striped table-bordered nowrap" AutoGenerateSelectButton="true" OnSelectedIndexChanged="grvPopItem_SelectedIndexChanged">
                                            <EmptyDataTemplate>
                                                <div style="text-align: center; color: red; font-size: 18px;">
                                                    No Record Not Found !
                                                </div>
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="ITEMCODE" HeaderText="Item Code" />
                                                <asp:BoundField DataField="Desciption" HeaderText="Item Desc." />
                                                <asp:BoundField DataField="MAKE" HeaderText="Make" />
                                                <asp:BoundField DataField="MODEL" HeaderText="Model" />
                                                <asp:BoundField DataField="DisplayName" HeaderText="Display Name" />
                                                <asp:BoundField DataField="ItemCategory" HeaderText="Item Category" />
                                                <asp:BoundField DataField="ItemGroup" HeaderText="Item Group" />
                                                <asp:BoundField DataField="ItemSubGroup" HeaderText="Item Sub Group" />
                                                <asp:BoundField DataField="HSNGroup" HeaderText="HSN Group" />
                                                <asp:BoundField DataField="ItemStatus" HeaderText="Item Status" />
                                                <asp:BoundField DataField="CREATEBY" HeaderText="Create By" />
                                                <asp:BoundField DataField="CREATEDATE" HeaderText="Create Date" DataFormatString="{0:d}" />
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

    <input type="hidden" id="menutabid" value="tsmTranSDSO" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranSD" runat="server" />

</asp:Content>
