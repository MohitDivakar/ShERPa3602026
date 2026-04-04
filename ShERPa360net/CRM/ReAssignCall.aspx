<%@ Page Title="" Language="C#" MasterPageFile="~/CRM/MasterCustomerRltionShipMng.Master" AutoEventWireup="true" CodeBehind="ReAssignCall.aspx.cs" Inherits="ShERPa360net.CRM.ReAssignCall" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Call Re-Assign</title>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />

    <%--<script>
        $(document).ready(function () {
            BindMakeAssociateModel();
            //$("#myInput").on("keyup", function () {
            //    var value = $(this).val().toLowerCase();
            //    $("#ContentPlaceHolder1_gvList tr ").filter(function () {
            //        $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
            //    });
            //});
        });
        function BindMakeAssociateModel() {

            $("#ContentPlaceHolder1_gvList").DataTable({
                dom: 'Bfrtip',
                buttons: [
                    {
                        extend: 'collection',
                        text: 'Export',
                        buttons: [
                            'copy',
                            'excel',
                            'csv',
                            'pdf',
                            'print'
                        ]
                    }
                ]
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Call </strong>Re-Assign</h3>
                        </div>
                        <div class="panel-body">

                            <div class="row">

                                <div class="col-md-12">
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Date From : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
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
                                            <label class="col-md-5 control-label">Agent Name : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlAgentName" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Status : </label>
                                            <div class="col-md-8 col-xs-12">
                                                <div class="input-group">
                                                    <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <%--<asp:LinkButton runat="server" ID="lnkNewVend" CssClass="btn btn-success pull-left" Text="New Vend" PostBackUrl="~/CRM/frmVendorMaster.aspx"><i class="fa fa-file-text"></i></asp:LinkButton>--%>
                                            <asp:LinkButton runat="server" ID="lnkSearhCall" CssClass="btn btn-success pull-left" Text="Search Call" OnClick="lnkSearhCall_Click">
<span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                            <%--<asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-left" Text="Export Vendor List" OnClick="lnkExport_Click"><i class="fa fa-file"></i></asp:LinkButton>--%>
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
                <div class="form-horizontal">
                    <div class="panel panel-default">

                        <div class="panel-heading">
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Assign List</strong></h3>
                            <div class="panel-body">

                                <div class="row">

                                    <div class="col-md-12">

                                        <div class="col-md-6">&nbsp;&nbsp;</div>
                                        <div class="col-md-3">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Reassign To : </label>
                                                <div class="col-md-8 col-xs-12">
                                                    <div class="input-group">
                                                        <asp:DropDownList ID="ddlGVAgedtList" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-3">
                                            <div class="form-group">

                                                <div class="col-md-4 col-xs-12">
                                                    <div class="input-group">
                                                        <asp:LinkButton runat="server" ID="lnkReAssignAll" CssClass="btn btn-success pull-right" Text="Re-Assign All" OnClick="lnkReAssignAll_Click"></asp:LinkButton>
                                                    </div>
                                                </div>
                                                <label class="col-md-4 control-label"></label>
                                            </div>
                                        </div>



                                    </div>
                                </div>

                            </div>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <fieldset class="scheduler-border">

                                            <div class="col-md-12" id="divName" runat="server" visible="true">
                                                <div class="box-body divhorizontal" style="overflow-x: scroll">
                                                    <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                        CellSpacing="0" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="true" OnRowDataBound="gvList_RowDataBound">
                                                        <EmptyDataTemplate>
                                                            No Record Found!
                                                        </EmptyDataTemplate>
                                                        <Columns>
                                                            <%-- <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                                            <asp:BoundField DataField="CUSTNAME" HeaderText="Customer Name" />
                                                            <asp:BoundField DataField="CONTACTNO" HeaderText="Contact No." />
                                                            <asp:BoundField DataField="ASSIGNTO" HeaderText="Assign To" />
                                                            <asp:BoundField DataField="ASSIGNINT" HeaderText="Assign To ID" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" />
                                                            <asp:BoundField DataField="CREATEDATE" HeaderText="Create Date" />--%>

                                                            <asp:TemplateField HeaderText="ID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGVID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Select">
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkSelectAll" runat="server" OnCheckedChanged="chkSelectAll_CheckedChanged" AutoPostBack="true" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <%--<asp:CheckBox ID="chkSelect" runat="server" OnCheckedChanged="chkSelect_CheckedChanged" AutoPostBack="true" />--%>
                                                                    <asp:CheckBox ID="chkSelect" runat="server" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Customer Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGVCustName" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Contact No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGVCONTACTNO" runat="server" Text='<%# Eval("CONTACTNO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Ref.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGVRef" runat="server" Text='<%# Eval("REFF") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Assign To">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGVASSIGNTO" runat="server" Text='<%# Eval("ASSIGNTO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Assign To ID" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGVASSIGNINT" runat="server" Text='<%# Eval("ASSIGNINT") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Create Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGVCREATEDATE" runat="server" Text='<%# Eval("CREATEDATE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Hold/Cancel Reason">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHOLDCANCELREASON" runat="server" Text='<%# Eval("HOLDCANCELREASON") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Call Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSTATUSDESC" runat="server" Text='<%# Eval("STATUSDESC") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Re-Assign To">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlGVAgedtList" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action" Visible="True">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="btnReAssign" Text="Re-Assign" OnClick="btnReAssign_Click" CssClass="btn btn-warning"></asp:LinkButton>
                                                                    <%-- | 
                                                        <asp:LinkButton runat="server" ID="btnImage" Text="Image" OnClick="btnImage_Click"></asp:LinkButton>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
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
    </div>


</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmCallReAssign" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmTranCRM" />

</asp:Content>
