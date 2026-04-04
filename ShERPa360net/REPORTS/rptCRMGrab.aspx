<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptCRMGrab.aspx.cs" Inherits="ShERPa360net.REPORTS.rptCRMGrab" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>CRM Report (Grab It)</title>


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

            if ($("#ContentPlaceHolder1_gvAllGrab tr").length > 2) {
                $("#ContentPlaceHolder1_gvAllGrab").DataTable({
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
            if ($("#ContentPlaceHolder1_gvGrabbedByAgent tr").length > 2) {
                $("#ContentPlaceHolder1_gvGrabbedByAgent").DataTable({
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
            if ($("#ContentPlaceHolder1_gvGrabPending tr").length > 2) {
                $("#ContentPlaceHolder1_gvGrabPending").DataTable({
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
            if ($("#ContentPlaceHolder1_gvGrabDate tr").length > 2) {
                $("#ContentPlaceHolder1_gvGrabDate").DataTable({
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

            if ($("#ContentPlaceHolder1_gvGrabbSummary tr").length > 2) {
                $("#ContentPlaceHolder1_gvGrabbSummary").DataTable({
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

            if ($("#ContentPlaceHolder1_grvCancelled tr").length > 2) {
                $("#ContentPlaceHolder1_grvCancelled").DataTable({
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
            if ($("#ContentPlaceHolder1_grvAllPending tr").length > 2) {
                $("#ContentPlaceHolder1_grvAllPending").DataTable({
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
            if ($("#ContentPlaceHolder1_grvAllCancelled tr").length > 2) {
                $("#ContentPlaceHolder1_grvAllCancelled").DataTable({
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
            if ($("#ContentPlaceHolder1_gvSO tr").length > 2) {

                $("#ContentPlaceHolder1_gvSO").DataTable({
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
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Grab  </strong>Data</h3>
                        <div class="col-md-12 pull-right">
                            <div class="col-md-2">&nbsp;</div>
                            <div class="col-md-2">&nbsp;</div>
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
                                    <label class="col-md-2 control-label pull-left" style="padding-top: 7px;">To</label>
                                    <div class="col-md-10 col-xs-12 pull-right">
                                        <div class="input-group">
                                            <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control datepicker pull-right" MaxLength="10"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--<div class="col-md-2">
                                <div class="form-group">
                                    <label class="col-md-4 control-label pull-left" style="padding-top: 7px;">Agent : </label>
                                    <div class="col-md-8 col-xs-12 pull-right">
                                        <asp:DropDownList ID="ddlAgent" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>--%>
                            <div class="col-md-2">
                                <asp:LinkButton runat="server" ID="lnkSearch" CssClass="btn btn-success pull-left" Text="Search" OnClick="lnkSearch_Click">

<span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="lnkDownLoadAll" CssClass="btn btn-success pull-right" Text="Download All" OnClick="lnkDownLoadAll_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <%--<div class="panel-body">

                        <div class="panel-body">

                            <div class="row">
                              
                            </div>
                        </div>

                    </div>--%>



                    <div class="row" style="margin-top: 10px !important;">
                        <div class="col-md-12">
                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkAllGrab" runat="server" CssClass="tile tile-warning" OnClick="lnkAllGrab_Click">
                                    <asp:Label ID="lblAllGrab" runat="server" Text="0"></asp:Label>
                                    <p>All Generated</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkGrabByDate" runat="server" CssClass="tile tile-info" OnClick="lnkGrabByDate_Click" BackColor="#3E8768" BorderColor="#3E8768">
                                    <asp:Label ID="lblGrabByDate" runat="server" Text="0"></asp:Label>
                                    <asp:Label ID="lblGrabbedOutOf" runat="server" Text=" / 0" Font-Size="X-Large"></asp:Label>
                                    <p>Grabbed by Agent</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkPending" runat="server" CssClass="tile tile-default" OnClick="lnkPending_Click" BackColor="Red" BorderColor="Red">
                                    <asp:Label ID="lblPending" runat="server" Text="0" ForeColor="White"></asp:Label>
                                    <asp:Label ID="lblPendingOutOf" runat="server" Text=" / 0" Font-Size="X-Large" ForeColor="White"></asp:Label>
                                    <p style="color: white;">Pending</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkCancelledCall" runat="server" CssClass="tile tile-primary" OnClick="lnkCancelledCall_Click">
                                    <asp:Label ID="lblCancelledCall" runat="server" Text="0"></asp:Label>
                                    <asp:Label ID="lblCancelledOutOf" runat="server" Text=" / 0" Font-Size="X-Large"></asp:Label>
                                    <p>Cancelled Calls</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkConverted" runat="server" CssClass="tile tile-default" OnClick="lnkConverted_Click" BackColor="Violet" BorderColor="Violet">
                                    <asp:Label ID="lblConvertedCall" runat="server" Text="0" ForeColor="#e8e8e8"></asp:Label>
                                    <asp:Label ID="lblConvertedOutOf" runat="server" Text=" / 0" Font-Size="X-Large" ForeColor="#e8e8e8"></asp:Label>
                                    <p style="color: #e8e8e8;">Converted Calls</p>
                                </asp:LinkButton>
                            </div>

                        </div>
                        <div class="col-md-12">
                            <div class="col-md-2" style="margin-top: 10px !important;">
                                &nbsp;&nbsp;
                            </div>
                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkGrabBy" runat="server" CssClass="tile tile-info" OnClick="lnkGrabBy_Click" BackColor="#3E8768" BorderColor="#3E8768">
                                    <asp:Label ID="lblGrabBy" runat="server" Text="0"></asp:Label>
                                    <p>Total Grabbed by Agent</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkAllPending" runat="server" CssClass="tile tile-default" OnClick="lnkAllPending_Click" BackColor="Red" BorderColor="Red">
                                    <asp:Label ID="lblAllGrabPending" runat="server" Text="0" ForeColor="White"></asp:Label>
                                    <p style="color: white;">All Pending</p>
                                </asp:LinkButton>
                            </div>

                            <div class="col-md-2" style="margin-top: 10px !important;">
                                <asp:LinkButton ID="lnkAllCancelledCall" runat="server" CssClass="tile tile-primary" OnClick="lnkAllCancelledCall_Click">
                                    <asp:Label ID="lblAllCancelled" runat="server" Text="0"></asp:Label>
                                    <p>All Cancelled Calls</p>
                                </asp:LinkButton>
                            </div>
                        </div>
                    </div>



                    <div class="row">
                        <div class="col-md-12">
                            <div id="divAllLead" class="box" runat="server" visible="false">
                                <div class="col-md-12">
                                    <asp:Label ID="lblHeadAllGenerated" runat="server" Text="All Generated" Font-Size="20px" ForeColor="#faa61a"></asp:Label>
                                    <asp:LinkButton runat="server" ID="lnkNewAllGenerated" CssClass="btn btn-success pull-right" Text="Download" OnClick="lnkNewAllGenerated_Click"><i class="fa fa-download"></i></asp:LinkButton>
                                </div>
                                <div class="col-md-12" style="margin-top: 10px !important;">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvAllGrab" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>

                                                <asp:TemplateField HeaderText="Cust. Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUSTNAME" runat="server" Text='<%#Eval("CUSTNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust. Contact">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCONTACTNO" runat="server" Text='<%#Eval("CONTACTNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust. Mail">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEMAIL" runat="server" Text='<%#Eval("EMAIL") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="DEVICE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDEVICE" runat="server" Text='<%#Eval("DEVICE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Price Range">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPRICERANGE" runat="server" Text='<%#Eval("PRICERANGE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Grabbed By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblASSIGNEDTO" runat="server" Text='<%#Eval("ASSIGNTO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Status Desc.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTATUSDESC" runat="server" Text='<%#Eval("STATUSDESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Hold/Cancell Reason">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHOLDREASON" runat="server" Text='<%#Eval("HOLDREASON") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust. Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHOLDCUSTREMARKS" runat="server" Text='<%#Eval("HOLDCUSTREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Agent Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCALLREMARKS" runat="server" Text='<%#Eval("CALLREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Enter By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblENTEREDBY" runat="server" Text='<%#Eval("CREATEBY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Grabbed In (Minutes)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGRABBEDWITHIN" runat="server" Text='<%#Eval("GRABBEDWITHIN") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Enter Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCREATEDATE" runat="server" Text='<%#Eval("CREATEDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Grabbed Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGRABBEDDATE" runat="server" Text='<%#Eval("GRABBEDDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Reff.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblREFF" runat="server" Text='<%#Eval("REFF") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Inq. Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblINQUIRYTYPE" runat="server" Text='<%#Eval("INQTYPE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div id="divGrabbedDate" class="box" runat="server" visible="false">
                                <div class="col-md-12">
                                    <asp:Label ID="lblHeadGrabbedDate" runat="server" Text="Grabbed by Agent" Font-Size="20px" ForeColor="#faa61a"></asp:Label>
                                    <asp:LinkButton runat="server" ID="lnkNewGrabbedDate" CssClass="btn btn-success pull-right" Text="Download" OnClick="lnkNewGrabbedDate_Click"><i class="fa fa-download"></i></asp:LinkButton>
                                </div>
                                <div class="col-md-12" style="margin-top: 10px !important;">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvGrabDate" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>

                                                <asp:TemplateField HeaderText="Cust. Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUSTNAME" runat="server" Text='<%#Eval("CUSTNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust. Contact">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCONTACTNO" runat="server" Text='<%#Eval("CONTACTNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust. Mail">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEMAIL" runat="server" Text='<%#Eval("EMAIL") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="DEVICE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDEVICE" runat="server" Text='<%#Eval("DEVICE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Price Range">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPRICERANGE" runat="server" Text='<%#Eval("PRICERANGE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Grabbed By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblASSIGNEDTO" runat="server" Text='<%#Eval("ASSIGNTO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Status Desc.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTATUSDESC" runat="server" Text='<%#Eval("STATUSDESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Hold/Cancell Reason">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHOLDREASON" runat="server" Text='<%#Eval("HOLDREASON") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust. Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHOLDCUSTREMARKS" runat="server" Text='<%#Eval("HOLDCUSTREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Agent Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCALLREMARKS" runat="server" Text='<%#Eval("CALLREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Enter By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblENTEREDBY" runat="server" Text='<%#Eval("CREATEBY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Grabbed In (Minutes)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGRABBEDWITHIN" runat="server" Text='<%#Eval("GRABBEDWITHIN") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Enter Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCREATEDATE" runat="server" Text='<%#Eval("CREATEDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Grabbed Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGRABBEDDATE" runat="server" Text='<%#Eval("GRABBEDDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Reff.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblREFF" runat="server" Text='<%#Eval("REFF") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Inq. Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblINQUIRYTYPE" runat="server" Text='<%#Eval("INQTYPE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div id="divGrabbed" class="box" runat="server" visible="false">
                                <div class="col-md-12">
                                    <asp:Label ID="lblHeadGrabbed" runat="server" Text="Total Grabbed by Agent" Font-Size="20px" ForeColor="#faa61a"></asp:Label>
                                    <asp:LinkButton runat="server" ID="lnkNewGrabbedBy" CssClass="btn btn-success pull-right" Text="Download" OnClick="lnkNewGrabbedBy_Click"><i class="fa fa-download"></i></asp:LinkButton>
                                </div>
                                <div class="col-md-12" style="margin-top: 10px !important;">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvGrabbedByAgent" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>

                                                <asp:TemplateField HeaderText="Cust. Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUSTNAME" runat="server" Text='<%#Eval("CUSTNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust. Contact">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCONTACTNO" runat="server" Text='<%#Eval("CONTACTNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust. Mail">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEMAIL" runat="server" Text='<%#Eval("EMAIL") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="DEVICE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDEVICE" runat="server" Text='<%#Eval("DEVICE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Price Range">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPRICERANGE" runat="server" Text='<%#Eval("PRICERANGE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Grabbed By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblASSIGNEDTO" runat="server" Text='<%#Eval("ASSIGNTO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Status Desc.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTATUSDESC" runat="server" Text='<%#Eval("STATUSDESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Hold/Cancell Reason">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHOLDREASON" runat="server" Text='<%#Eval("HOLDREASON") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust. Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHOLDCUSTREMARKS" runat="server" Text='<%#Eval("HOLDCUSTREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Agent Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCALLREMARKS" runat="server" Text='<%#Eval("CALLREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Enter By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblENTEREDBY" runat="server" Text='<%#Eval("CREATEBY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Grabbed In (Minutes)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGRABBEDWITHIN" runat="server" Text='<%#Eval("GRABBEDWITHIN") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Enter Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCREATEDATE" runat="server" Text='<%#Eval("CREATEDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Grabbed Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGRABBEDDATE" runat="server" Text='<%#Eval("GRABBEDDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Reff.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblREFF" runat="server" Text='<%#Eval("REFF") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Inq. Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblINQUIRYTYPE" runat="server" Text='<%#Eval("INQTYPE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>



                            <div id="divPending" class="box" runat="server" visible="false">
                                <div class="col-md-12">
                                    <asp:Label ID="lblHeadPending" runat="server" Text="Pending" Font-Size="20px" ForeColor="#faa61a"></asp:Label>
                                    <asp:LinkButton runat="server" ID="lnkNewPending" CssClass="btn btn-success pull-right" Text="Download" OnClick="lnkNewPending_Click"><i class="fa fa-download"></i></asp:LinkButton>
                                </div>
                                <div class="col-md-12" style="margin-top: 10px !important;">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvGrabPending" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>

                                                <asp:TemplateField HeaderText="Cust. Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUSTNAME" runat="server" Text='<%#Eval("CUSTNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust. Contact">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCONTACTNO" runat="server" Text='<%#Eval("CONTACTNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust. Mail">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEMAIL" runat="server" Text='<%#Eval("EMAIL") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="DEVICE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDEVICE" runat="server" Text='<%#Eval("DEVICE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Price Range">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPRICERANGE" runat="server" Text='<%#Eval("PRICERANGE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:TemplateField HeaderText="Grabbed By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblASSIGNEDTO" runat="server" Text='<%#Eval("ASSIGNTO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>


                                                <%--<asp:TemplateField HeaderText="Penging (Minutes)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGRABBEDWITHIN" runat="server" Text='<%#Eval("GRABBEDWITHIN") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="Status Desc.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTATUSDESC" runat="server" Text='<%#Eval("STATUSDESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Enter By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblENTEREDBY" runat="server" Text='<%#Eval("CREATEBY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Enter Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCREATEDATE" runat="server" Text='<%#Eval("CREATEDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Reff.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblREFF" runat="server" Text='<%#Eval("REFF") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Inq. Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblINQUIRYTYPE" runat="server" Text='<%#Eval("INQTYPE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Aging">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAGING" runat="server" Text='<%#Eval("AGING") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div id="divAllPending" class="box" runat="server" visible="false">
                                <div class="col-md-12">
                                    <asp:Label ID="lblHeaderAllPending" runat="server" Text="All Pending" Font-Size="20px" ForeColor="#faa61a"></asp:Label>
                                    <asp:LinkButton runat="server" ID="lblAllPending" CssClass="btn btn-success pull-right" Text="Download" OnClick="lblAllPending_Click"><i class="fa fa-download"></i></asp:LinkButton>
                                </div>
                                <div class="col-md-12" style="margin-top: 10px !important;">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                        <asp:GridView ID="grvAllPending" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>

                                                <asp:TemplateField HeaderText="Cust. Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUSTNAME" runat="server" Text='<%#Eval("CUSTNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust. Contact">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCONTACTNO" runat="server" Text='<%#Eval("CONTACTNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust. Mail">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEMAIL" runat="server" Text='<%#Eval("EMAIL") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="DEVICE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDEVICE" runat="server" Text='<%#Eval("DEVICE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Price Range">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPRICERANGE" runat="server" Text='<%#Eval("PRICERANGE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:TemplateField HeaderText="Grabbed By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblASSIGNEDTO" runat="server" Text='<%#Eval("ASSIGNTO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>


                                                <%--<asp:TemplateField HeaderText="Penging (Minutes)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGRABBEDWITHIN" runat="server" Text='<%#Eval("GRABBEDWITHIN") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="Status Desc.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTATUSDESC" runat="server" Text='<%#Eval("STATUSDESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Enter By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblENTEREDBY" runat="server" Text='<%#Eval("CREATEBY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Enter Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCREATEDATE" runat="server" Text='<%#Eval("CREATEDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Reff.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblREFF" runat="server" Text='<%#Eval("REFF") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Inq. Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblINQUIRYTYPE" runat="server" Text='<%#Eval("INQTYPE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Aging">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAGING" runat="server" Text='<%#Eval("AGING") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>


                            <div id="divCancelled" class="box" runat="server" visible="false">
                                <div class="col-md-12">
                                    <asp:Label ID="lblHeaderCancelled" runat="server" Text="Cancelled" Font-Size="20px" ForeColor="#faa61a"></asp:Label>
                                    <asp:LinkButton runat="server" ID="lnkDateCancelled" CssClass="btn btn-success pull-right" Text="Download" OnClick="lnkDateCancelled_Click"><i class="fa fa-download"></i></asp:LinkButton>
                                </div>
                                <div class="col-md-12" style="margin-top: 10px !important;">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                        <asp:GridView ID="grvCancelled" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>

                                                <asp:TemplateField HeaderText="Cust. Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUSTNAME" runat="server" Text='<%#Eval("CUSTNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust. Contact">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCONTACTNO" runat="server" Text='<%#Eval("CONTACTNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust. Mail">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEMAIL" runat="server" Text='<%#Eval("EMAIL") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="DEVICE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDEVICE" runat="server" Text='<%#Eval("DEVICE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Price Range">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPRICERANGE" runat="server" Text='<%#Eval("PRICERANGE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Grabbed By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblASSIGNEDTO" runat="server" Text='<%#Eval("ASSIGNTO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Status Desc.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTATUSDESC" runat="server" Text='<%#Eval("STATUSDESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Hold/Cancell Reason">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHOLDREASON" runat="server" Text='<%#Eval("HOLDREASON") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust. Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHOLDCUSTREMARKS" runat="server" Text='<%#Eval("HOLDCUSTREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Agent Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCALLREMARKS" runat="server" Text='<%#Eval("CALLREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Enter By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblENTEREDBY" runat="server" Text='<%#Eval("CREATEBY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Grabbed In (Minutes)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGRABBEDWITHIN" runat="server" Text='<%#Eval("GRABBEDWITHIN") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Enter Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCREATEDATE" runat="server" Text='<%#Eval("CREATEDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Grabbed Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGRABBEDDATE" runat="server" Text='<%#Eval("GRABBEDDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Reff.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblREFF" runat="server" Text='<%#Eval("REFF") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Inq. Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblINQUIRYTYPE" runat="server" Text='<%#Eval("INQTYPE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div id="divAllCancelled" class="box" runat="server" visible="false">
                                <div class="col-md-12">
                                    <asp:Label ID="lblHeaderAllCancelled" runat="server" Text="All Cancelled" Font-Size="20px" ForeColor="#faa61a"></asp:Label>
                                    <asp:LinkButton runat="server" ID="lnkAllCancelled" CssClass="btn btn-success pull-right" Text="Download" OnClick="lnkAllCancelled_Click"><i class="fa fa-download"></i></asp:LinkButton>
                                </div>
                                <div class="col-md-12" style="margin-top: 10px !important;">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                        <asp:GridView ID="grvAllCancelled" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>

                                                <asp:TemplateField HeaderText="Cust. Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUSTNAME" runat="server" Text='<%#Eval("CUSTNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust. Contact">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCONTACTNO" runat="server" Text='<%#Eval("CONTACTNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust. Mail">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEMAIL" runat="server" Text='<%#Eval("EMAIL") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="DEVICE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDEVICE" runat="server" Text='<%#Eval("DEVICE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Price Range">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPRICERANGE" runat="server" Text='<%#Eval("PRICERANGE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Grabbed By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblASSIGNEDTO" runat="server" Text='<%#Eval("ASSIGNTO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Status Desc.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTATUSDESC" runat="server" Text='<%#Eval("STATUSDESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Hold/Cancell Reason">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHOLDREASON" runat="server" Text='<%#Eval("HOLDREASON") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust. Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHOLDCUSTREMARKS" runat="server" Text='<%#Eval("HOLDCUSTREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Agent Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCALLREMARKS" runat="server" Text='<%#Eval("CALLREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Enter By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblENTEREDBY" runat="server" Text='<%#Eval("CREATEBY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Grabbed In (Minutes)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGRABBEDWITHIN" runat="server" Text='<%#Eval("GRABBEDWITHIN") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Enter Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCREATEDATE" runat="server" Text='<%#Eval("CREATEDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Grabbed Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGRABBEDDATE" runat="server" Text='<%#Eval("GRABBEDDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Reff.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblREFF" runat="server" Text='<%#Eval("REFF") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Inq. Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblINQUIRYTYPE" runat="server" Text='<%#Eval("INQTYPE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div id="divSO" class="box" runat="server" visible="false">
                                <div class="col-md-12">
                                    <asp:Label ID="lblHeadSO" runat="server" Text="Converted" Font-Size="20px" ForeColor="#faa61a"></asp:Label>
                                    <asp:LinkButton runat="server" ID="lnkSODwn" CssClass="btn btn-success pull-right" Text="Download" OnClick="lnkSODwn_Click"><i class="fa fa-download"></i></asp:LinkButton>
                                </div>
                                <div class="col-md-12" style="margin-top: 10px !important;">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvSO" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:TemplateField HeaderText="CMPID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCMPID" runat="server" Text='<%#Eval("CMPID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust. Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUSTNAME" runat="server" Text='<%#Eval("CUSTNAME") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust. Contact">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCONTACTNO" runat="server" Text='<%#Eval("CONTACTNO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cust. Mail">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEMAIL" runat="server" Text='<%#Eval("EMAIL") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="DEVICE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDEVICE" runat="server" Text='<%#Eval("DEVICE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Status" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTATUS" runat="server" Text='<%#Eval("STATUS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Status Desc.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSTATUSDESC" runat="server" Text='<%#Eval("STATUSDESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Update By ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUPDATEBY" runat="server" Text='<%#Eval("UPDATEBY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Updated By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUPDATEDBY" runat="server" Text='<%#Eval("UPDATEDBY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Updated Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUPDATEDATE" runat="server" Text='<%#Eval("UPDATEDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Inq. Type ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblINQTYPE" runat="server" Text='<%#Eval("INQTYPE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Inq. Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblINQUIRYTYPE" runat="server" Text='<%#Eval("INQUIRYTYPE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="SO/Inq. No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblINQSO" runat="server" Text='<%#Eval("INQSO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Start Dt.&Time">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCALLSTART" runat="server" Text='<%#Eval("CALLSTART") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="End Dt.&Time">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCALLEND" runat="server" Text='<%#Eval("CALLEND") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Duration">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDURATION" runat="server" Text='<%#Eval("DURATION") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div id="divSummary" class="box" runat="server" visible="true" style="margin-top: 20px !important;">
                                <div class="col-md-12">
                                    <asp:Label ID="lblGrabSummary" runat="server" Text="Grabb Summary" Font-Size="20px" ForeColor="#faa61a" Style="margin-top: 20px !important;"></asp:Label>
                                    <%--<asp:LinkButton runat="server" ID="LinkButton1" CssClass="btn btn-success pull-right" Text="Download" OnClick="lnkNewPending_Click"><i class="fa fa-download"></i></asp:LinkButton>--%>
                                </div>
                                <div class="col-md-12" style="margin-top: 10px !important;">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvGrabbSummary" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
                                            CellSpacing="0" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" Width="100%">
                                            <EmptyDataTemplate>
                                                No Record Found!
                                            </EmptyDataTemplate>
                                            <Columns>
                                                <asp:BoundField HeaderText="" DataField="ASSIGNTO" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                                <asp:BoundField HeaderText="User Name" DataField="USERNAME" />
                                                <asp:BoundField HeaderText="Grabbed" DataField="GRABBED" />
                                                <asp:BoundField HeaderText="Save" DataField="SAVED" />
                                                <asp:BoundField HeaderText="Converted" DataField="CONVERTED" />
                                                <asp:BoundField HeaderText="Hold" DataField="HOLD" />
                                                <asp:BoundField HeaderText="Cancelled" DataField="CANCELLED" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>







                        </div>
                    </div>

                    <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
                </div>
            </div>
        </div>

    </div>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmRptCRMCallLogs" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptCRM" />

</asp:Content>
