<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptAllCallData.aspx.cs" Inherits="ShERPa360net.REPORTS.rptAllCallData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>All Call Report</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />


    <script>
        $(document).ready(function () {
            BindMakeAssociateModel();
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
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="page-content-wrap">
        <div class="row">
            <div class="col-md-12">
                <div class="panel panel-default">

                    <div class="panel-heading">
                        <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Call  </strong>Data</h3>
                        <div class="col-md-12 pull-right">
                            
                            <div class="col-md-3">
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
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="col-md-2 control-label pull-left" style="padding-top: 7px;">To</label>
                                    <div class="col-md-10 col-xs-12 pull-right">
                                        <div class="input-group">
                                            <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker pull-right" MaxLength="10"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="col-md-4 control-label pull-left" style="padding-top: 7px;">Inq. Type : </label>
                                    <div class="col-md-8 col-xs-12 pull-right">
                                        <asp:DropDownList ID="ddlInqType" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <asp:LinkButton runat="server" ID="lnkSearch" CssClass="btn btn-success pull-left" Text="Search" OnClick="lnkSearch_Click">

<span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="lnkDownLoadAll" CssClass="btn btn-success pull-right" Text="Download All" OnClick="lnkDownLoadAll_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp;Call List</strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="row">
                                        <fieldset class="scheduler-border">

                                            <div class="col-md-12" id="divName" runat="server" visible="true">
                                                <div class="box-body divhorizontal" style="overflow-x: scroll">

                                                    <asp:GridView ID="gvList" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                                        CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                                        <EmptyDataTemplate>
                                                            <div style="text-align: center; color: red; font-size: 18px;">
                                                                List is empty !
                                                            </div>
                                                        </EmptyDataTemplate>
                                                        <Columns>

                                                            <asp:TemplateField HeaderText="ID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Cust. Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCUSTNAME" runat="server" Text='<%# Eval("CUSTNAME") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Contact No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCONTACTNO" runat="server" Text='<%# Eval("CONTACTNO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Mail">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblEMAIL" runat="server" Text='<%# Eval("EMAIL") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Make">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMAKE" runat="server" Text='<%# Eval("MAKE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Model">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMODEL" runat="server" Text='<%# Eval("MODEL") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Inq. Type">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblINQTYPE" runat="server" Text='<%# Eval("INQTYPE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Reff.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblREFF" runat="server" Text='<%# Eval("REFF") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCALLSTATUS" runat="server" Text='<%# Eval("CALLSTATUS") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                              <asp:TemplateField HeaderText="Assign To">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblASSIGNTO" runat="server" Text='<%# Eval("ASSIGNTO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Call Dt.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCALLDATE" runat="server" Text='<%# Eval("CALLDATE") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Hold/Cancel Reason">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblREASON" runat="server" Text='<%# Eval("REASON") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Cust. Remarks">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCUSTREMARKS" runat="server" Text='<%# Eval("CUSTREMARKS") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Agent Remarks">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCALLREMARKS" runat="server" Text='<%# Eval("CALLREMARKS") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Call By">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCALLBY" runat="server" Text='<%# Eval("CALLBY") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Inq./SO No.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblINQSO" runat="server" Text='<%# Eval("INQSO") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Call Start Time">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCALLSTART" runat="server" Text='<%# Eval("CALLSTART") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Call End Time">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCALLEND" runat="server" Text='<%# Eval("CALLEND") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Time Duration (in sec.)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblElapsedTime" runat="server" Text='<%# Eval("ElapsedTime") %>'></asp:Label>
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

    <input type="hidden" id="menutabid" value="tsmRptCRMCallLogs" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptCRM" />

</asp:Content>
