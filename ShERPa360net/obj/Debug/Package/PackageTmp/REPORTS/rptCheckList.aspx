<%@ Page Title="View Laptop Check List Report" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptCheckList.aspx.cs" Inherits="ShERPa360net.REPORTS.rptCheckList" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845DCD8080CC91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>View Laptop Check List Report</title>

    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.7.2/jquery.min.js"></script>
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.20/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://editor.datatables.net/extensions/Editor/css/editor.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.3.1/css/select.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.6.1/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/colreorder/1.5.2/css/colReorder.dataTables.min.css" type="text/css" />

    <style>
        body:nth-of-type(1) img[src*="Blank.gif"] {
            display: none;
        }
    </style>

    <script>
        $(document).ready(function () {
            BindMakeAssociateModel();
        });
        function BindMakeAssociateModel() {
            if ($("#ContentPlaceHolder1_grvCheckListData tr").length > 2) {
                $("#ContentPlaceHolder1_grvCheckListData").DataTable({
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

            if ($("#ContentPlaceHolder1_gvDetail tr").length > 2) {
                $("#ContentPlaceHolder1_gvDetail").DataTable({
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
                            <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; View Laptop Check List Data </strong></h3>
                        </div>

                        <div class="panel-body">
                            <div class="row">

                                <div class="col-md-12">

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">From Date : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvFromDate" runat="server" ControlToValidate="txtFromDate"
                                                        ErrorMessage="Enter From Date" ValidationGroup="valSearch" Display="Dynamic">Enter From Date</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-5 control-label">To Date : </label>
                                            <div class="col-md-7 col-xs-12">
                                                <div class="input-group">
                                                    <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                                    <asp:TextBox ID="txtTodate" runat="server" CssClass="form-control datepicker" MaxLength="10"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvToDate" runat="server" ControlToValidate="txtTodate"
                                                        ErrorMessage="Enter To Date" ValidationGroup="valSearch" Display="Dynamic">Enter To Date</asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label class="col-md-6 control-label">Job Id : </label>
                                            <div class="col-md-6 col-xs-12">
                                                <div class="input-group">
                                                    <asp:TextBox ID="txtJobid" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <%--<asp:LinkButton runat="server" ID="lnkSummary" CssClass="btn btn-success pull-right" Text="Report Summary" PostBackUrl="~/REPORTS/rptSafetyReportSummary.aspx"></asp:LinkButton>
                                            <asp:LinkButton runat="server" ID="lnkExport" CssClass="btn btn-success pull-right" Text="Export Report" OnClick="lnkExport_Click">
                                            <span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>--%>
                                            <%--<asp:LinkButton runat="server" ID="lnkNewMR" CssClass="btn btn-success pull-left" Text="New MR" PostBackUrl="~/CRM/frmLaptopCheckList.aspx"><span tooltip="Add New" flow="down"><i class="fa fa-plus"  aria-hidden="true"></i> </span></asp:LinkButton>--%>
                                            <asp:LinkButton runat="server" ID="lnkSearhSR" CssClass="btn btn-success pull-right" Text="Search Report" OnClick="lnkSearhSR_Click" ValidationGroup="valSearch"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>

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
                                        <asp:GridView ID="grvCheckListData" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="false" Width="100%" ShowHeaderWhenEmpty="true">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField DataField="ID" HeaderText="ID" />
                                                <asp:BoundField DataField="DOCDATE" HeaderText="Check Date" />
                                                <asp:BoundField DataField="JOBID" HeaderText="Job ID" />
                                                <asp:BoundField DataField="SERIALNO" HeaderText="Serial No." />
                                                <asp:BoundField DataField="PROJECT" HeaderText="Project" />
                                                <asp:BoundField DataField="STATUS" HeaderText="Status" />
                                                <asp:BoundField DataField="GRADE" HeaderText="Grade" />
                                                <asp:BoundField DataField="MAKE" HeaderText="Make" />
                                                <asp:BoundField DataField="MODEL" HeaderText="Model" />
                                                <asp:BoundField DataField="COLOR" HeaderText="Color" />
                                                <asp:BoundField DataField="CHECKBY" HeaderText="Checked By" />
                                                <asp:BoundField DataField="VERIFIEDBY" HeaderText="Verified By" />
                                                <asp:BoundField DataField="CREATEDBY" HeaderText="Created By" />
                                                <asp:BoundField DataField="CREATEDATE" HeaderText="Created Date" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkView" runat="server" Text="View Details" OnClick="lnkView_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDownload" runat="server" Text="View PDF" OnClick="lnkDownload_Click"></asp:LinkButton>
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
                    <h4 class="modal-title" style="color: #337ab7"><strong>Laptop Check List</strong> </h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Job ID :</label>
                                    <asp:Label ID="lblJOBID" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Serial No. :</label>
                                    <asp:Label ID="lblSERIALNO" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-3" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Make :</label>
                                    <asp:Label ID="lblMAKE" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-3" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Model :</label>
                                    <asp:Label ID="lblMODEL" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Doc Date :</label>
                                    <asp:Label ID="lblDOCDATE" runat="server" CssClass="form-control"></asp:Label>
                                    <asp:HiddenField ID="hfChkID" runat="server" />
                                </div>
                            </div>
                            <div class="col-md-3" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Project :</label>
                                    <asp:Label ID="lblPROJECT" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-3" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Status :</label>
                                    <asp:Label ID="lblSTATUS" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>


                            <div class="col-md-3" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Grade :</label>
                                    <asp:Label ID="lblGRADE" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>



                            <div class="col-md-3" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Color :</label>
                                    <asp:Label ID="lblCOLOR" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-3" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Checked By :</label>
                                    <asp:Label ID="lblCHECKBY" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Verified By :</label>
                                    <asp:Label ID="lblVERIFIEDBY" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Created By :</label>
                                    <asp:Label ID="lblCREATEDBY" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-3" style="margin-top: 5px;">
                                <div class="form-group">
                                    <label>Create Date :</label>
                                    <asp:Label ID="lblCREATEDATE" runat="server" CssClass="form-control"></asp:Label>
                                </div>
                            </div>

                            <div class="col-md-12" style="margin-top: 5px;">
                                <div class="box">
                                    <div class="page-content-wrap">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="panel panel-default tabs">
                                                    <div class="panel-body tab-content">
                                                        <div class="box-body divhorizontal" style="overflow-x: scroll; overflow-y: scroll; height: 400px">
                                                            <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                                                CssClass="table table-hover table-striped table-bordered nowrap">
                                                                <Columns>
                                                                    <asp:BoundField DataField="DTLID" HeaderText="ID" />
                                                                    <asp:BoundField DataField="QUESTIONID" HeaderText="Que. ID" Visible="false" />
                                                                    <asp:BoundField DataField="QUESTION" HeaderText="Question" />
                                                                    <asp:BoundField DataField="RESULT" HeaderText="Result" />
                                                                    <asp:BoundField DataField="REMARKS" HeaderText="Remarks" />
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

                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modal-report" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7"><strong>Laptop Check List Report</strong> </h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="box-body divhorizontal" style="overflow-x: scroll; max-height: 1000px !important;">
                                <rpt:ReportViewer ID="ReportViewer1" runat="server" SizeToReportContent="true" Visible="false"></rpt:ReportViewer>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmRptCheckList" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptCRM" runat="server" />

</asp:Content>
