<%@ Page Title="" Language="C#" MasterPageFile="~/REPORTS/Report.Master" AutoEventWireup="true" CodeBehind="rptCRM.aspx.cs" Inherits="ShERPa360net.REPORTS.rptCRM" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>CRM Report</title>
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

            $("#ContentPlaceHolder1_gvNewLead").DataTable({
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

            $("#ContentPlaceHolder1_gvFollowUps").DataTable({
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

            $("#ContentPlaceHolder1_gvConverted").DataTable({
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

            $("#ContentPlaceHolder1_gvPending").DataTable({
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

            $("#ContentPlaceHolder1_gvCancelled").DataTable({
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

            $("#ContentPlaceHolder1_gvSOINQ").DataTable({
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

            $("#ContentPlaceHolder1_gvReassign").DataTable({
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
                    <%--<asp:UpdatePanel ID="upd" runat="server" UpdateMode="Conditional">
                        <Triggers>
                            <asp:PostBackTrigger ControlID="lnkSearch" />
                            <asp:PostBackTrigger ControlID="lnkNewLead" />
                            <asp:PostBackTrigger ControlID="lnkFollowUp" />
                            <asp:PostBackTrigger ControlID="lnkConvertedCall" />
                            <asp:PostBackTrigger ControlID="lnkCancelledCall" />
                            <asp:PostBackTrigger ControlID="lnkSOGene" />
                            <asp:PostBackTrigger ControlID="lnkReassignCall" />
                            <asp:AsyncPostBackTrigger ControlID="lnkSearch" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="lnkNewLead" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="lnkFollowUp" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="lnkConvertedCall" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="lnkCancelledCall" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="lnkSOGene" EventName="Click" />
                            <asp:AsyncPostBackTrigger ControlID="lnkReassignCall" EventName="Click" />
                        </Triggers>
                        <ContentTemplate>--%>
                    <div class="panel-heading">
                        <h3 class="panel-title"><strong><span class="fa fa-file"></span>&nbsp; Call  </strong>Data</h3>
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
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label class="col-md-4 control-label pull-left" style="padding-top: 7px;">Agent : </label>
                                    <div class="col-md-8 col-xs-12 pull-right">
                                        <asp:DropDownList ID="ddlAgent" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <asp:LinkButton runat="server" ID="lnkSearch" CssClass="btn btn-success pull-left" Text="Search" OnClick="lnkSearch_Click"><span tooltip="Search" flow="down"><i class="fa fa-search"></i> </span></asp:LinkButton>
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

                        <div class="col-md-2" style="margin-top: 10px !important;">

                            <%--<a >--%>
                            <asp:LinkButton ID="lnkNewLead" runat="server" CssClass="tile tile-warning" OnClick="lnkNewLead_Click">
                                <asp:Label ID="lblNewLead" runat="server" Text="0"></asp:Label>
                                <p>Lead Generated</p>
                            </asp:LinkButton>
                            <%--</a>--%>
                        </div>

                        <div class="col-md-2" style="margin-top: 10px !important;">
                            <asp:LinkButton ID="lnkFollowUp" runat="server" CssClass="tile tile-info" OnClick="lnkFollowUp_Click" BackColor="#3E8768" BorderColor="#3E8768">
                                <asp:Label ID="lblFollowUp" runat="server" Text="0"></asp:Label>
                                <p>Follow Up Calls</p>
                                <%--<div class="informer informer-default dir-bl"><span class="fa fa-globe"></span>Lates Somethink</div>--%>
                            </asp:LinkButton>
                        </div>

                        <div class="col-md-1" style="margin-top: 10px !important;">
                            <asp:LinkButton ID="lnkSO" runat="server" CssClass="tile tile-danger" OnClick="lnkSO_Click">
                                <asp:Label ID="lblSOGeneratedCall" runat="server" Text="0"></asp:Label>
                                <p>SO</p>
                                <%--<div class="informer informer-default dir-tr"><span class="fa fa-calendar"></span></div>--%>
                            </asp:LinkButton>
                        </div>



                        <div class="col-md-1" style="margin-top: 10px !important;">
                            <asp:LinkButton ID="lnkSOGene" runat="server" CssClass="tile tile-default" OnClick="lnkSOGene_Click" BackColor="Violet" BorderColor="Violet">
                                <asp:Label ID="lblSOGene" runat="server" Text="0" ForeColor="White"></asp:Label>
                                <p style="color: white;">Inquiry</p>
                                <%--<div class="informer informer-default"><span class="fa fa-shopping-cart"></span></div>--%>
                            </asp:LinkButton>
                        </div>

                        <div class="col-md-2" style="margin-top: 10px !important;">
                            <asp:LinkButton ID="lnkPending" runat="server" CssClass="tile tile-default" OnClick="lnkPending_Click" BackColor="Red" BorderColor="Red">
                                <asp:Label ID="lblPending" runat="server" Text="0" ForeColor="White"></asp:Label>
                                <p style="color: white;">Pending</p>
                                <%--<div class="informer informer-default"><span class="fa fa-shopping-cart"></span></div>--%>
                            </asp:LinkButton>
                        </div>

                        <div class="col-md-2" style="margin-top: 10px !important;">
                            <asp:LinkButton ID="lnkCancelledCall" runat="server" CssClass="tile tile-primary" OnClick="lnkCancelledCall_Click">
                                <asp:Label ID="lblCancelledCall" runat="server" Text="0"></asp:Label>
                                <p>Cancelled Calls</p>
                                <%--<div class="informer informer-danger dir-tr"><span class="fa fa-caret-down"></span></div>--%>
                            </asp:LinkButton>
                        </div>

                        <div class="col-md-2" style="margin-top: 10px !important;">
                            <asp:LinkButton ID="lnkReassignCall" runat="server" CssClass="tile tile-success" OnClick="lnkReassignCall_Click">
                                <asp:Label ID="lblReassignCall" runat="server" Text="0"></asp:Label>
                                <p>Re-Assigned Calls</p>
                            </asp:LinkButton>
                        </div>

                    </div>



                    <div class="row">
                        <div class="col-md-12">
                            <div id="divLead" class="box" runat="server" visible="false">
                                <div class="col-md-12">
                                    <asp:Label ID="lblHeadNewLead" runat="server" Text="Lead Generated" Font-Size="20px" ForeColor="#faa61a"></asp:Label>
                                    <asp:LinkButton runat="server" ID="lnkNewLeadDwn" CssClass="btn btn-success pull-right" Text="Download" OnClick="lnkNewLeadDwn_Click"><i class="fa fa-download"></i></asp:LinkButton>
                                </div>
                                <div class="col-md-12" style="margin-top: 10px !important;">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvNewLead" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
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

                                                <asp:TemplateField HeaderText="Inq. Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUSTUPDATEDATE" runat="server" Text='<%#Eval("CUSTUPDATEDATE") %>'></asp:Label>
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

                                                <asp:TemplateField HeaderText="Product">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPRODUCT" runat="server" Text='<%#Eval("PRODUCT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Specs.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSPECS" runat="server" Text='<%#Eval("SPECS") %>'></asp:Label>
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

                                                <asp:TemplateField HeaderText="Assign To ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblASSIGNTO" runat="server" Text='<%#Eval("ASSIGNTO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Assigned To">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblASSIGNEDTO" runat="server" Text='<%#Eval("ASSIGNEDTO") %>'></asp:Label>
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

                                                <asp:TemplateField HeaderText="Enter By ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCREATEBY" runat="server" Text='<%#Eval("CREATEBY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Enter By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblENTEREDBY" runat="server" Text='<%#Eval("ENTEREDBY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Enter Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCREATEDATE" runat="server" Text='<%#Eval("CREATEDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Cust. Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUSTREMARKS" runat="server" Text='<%#Eval("CUSTREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Reff.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblREFF" runat="server" Text='<%#Eval("REFF") %>'></asp:Label>
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

                                                <%--<asp:TemplateField HeaderText="Hold/Cancel Reason">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHOLDREASON" runat="server" Text='<%#Eval("HOLDREASON") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Hold/Cancel Reason by Cust.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHOLDCUSTREMARKS" runat="server" Text='<%#Eval("HOLDCUSTREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CC Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCALLREMARKS" runat="server" Text='<%#Eval("CALLREMARKS") %>'></asp:Label>
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
                                                </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div id="divFollowUp" class="box" runat="server" visible="false">
                                <div class="col-md-12">
                                    <asp:Label ID="lblHeadFollowup" runat="server" Text="Follow Up Calls" Font-Size="20px" ForeColor="#faa61a"></asp:Label>
                                    <asp:LinkButton runat="server" ID="lnkFollowUpDwn" CssClass="btn btn-success pull-right" Text="Download" OnClick="lnkFollowUpDwn_Click"><span tooltip="Download" flow="down"><i class="fa fa-download"></i> </span></asp:LinkButton>
                                </div>
                                <div class="col-md-12" style="margin-top: 10px !important;">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvFollowUps" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
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

                                                <asp:TemplateField HeaderText="Inq. Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUSTUPDATEDATE" runat="server" Text='<%#Eval("CUSTUPDATEDATE") %>'></asp:Label>
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

                                                <asp:TemplateField HeaderText="Product">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPRODUCT" runat="server" Text='<%#Eval("PRODUCT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Specs.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSPECS" runat="server" Text='<%#Eval("SPECS") %>'></asp:Label>
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

                                                <asp:TemplateField HeaderText="Assign To ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblASSIGNTO" runat="server" Text='<%#Eval("ASSIGNTO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Assigned To">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblASSIGNEDTO" runat="server" Text='<%#Eval("ASSIGNEDTO") %>'></asp:Label>
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

                                                <asp:TemplateField HeaderText="Cust. Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUSTREMARKS" runat="server" Text='<%#Eval("CUSTREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Reff.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblREFF" runat="server" Text='<%#Eval("REFF") %>'></asp:Label>
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

                                                <asp:TemplateField HeaderText="Hold Reason">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHOLDREASON" runat="server" Text='<%#Eval("HOLDREASON") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Hold Reason by Cust.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHOLDCUSTREMARKS" runat="server" Text='<%#Eval("HOLDCUSTREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CC Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCALLREMARKS" runat="server" Text='<%#Eval("CALLREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--  <asp:TemplateField HeaderText="Start Dt.&Time">
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
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="History">
                                                    <ItemTemplate>
                                                        <center>
                                                            <asp:LinkButton ID="lnkFolloupHistory" runat="server" OnClick="lnkFolloupHistory_Click"
                                                                Text="<i id='spaninfo' class='fa fa-info-circle' style='color: #3a1772;font-size: initial;'></i>   <asp:Label ID='lblCallText' runat='server' Text='Details'></asp:Label>">
                                                            </asp:LinkButton>
                                                        </center>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <%--                            <div id="divConverted" class="box" runat="server" visible="false">
                                <div class="col-md-12">
                                    <asp:Label ID="lblHeadConverted" runat="server" Text="Converted Calls" Font-Size="20px" ForeColor="#e04b4a"></asp:Label>
                                    <asp:LinkButton runat="server" ID="lnkConvertedDwn" CssClass="btn btn-success pull-right" Text="Download" OnClick="lnkConvertedDwn_Click"><i class="fa fa-download"></i></asp:LinkButton>
                                </div>
                                <div class="col-md-12" style="margin-top: 10px !important;">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvConverted" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
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

                                <asp:TemplateField HeaderText="Product">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPRODUCT" runat="server" Text='<%#Eval("PRODUCT") %>'></asp:Label>
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

                                                <asp:TemplateField HeaderText="Assign To" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblASSIGNTO" runat="server" Text='<%#Eval("ASSIGNTO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Assigned To">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblASSIGNEDTO" runat="server" Text='<%#Eval("ASSIGNEDTO") %>'></asp:Label>
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

                                                <asp:TemplateField HeaderText="Update By" Visible="false">
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

                                                <asp:TemplateField HeaderText="Cust. Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUSTREMARKS" runat="server" Text='<%#Eval("CUSTREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Reff.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblREFF" runat="server" Text='<%#Eval("REFF") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Inq. Tyep" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblINQTYPE" runat="server" Text='<%#Eval("INQTYPE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Inq. Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblINQUIRYTYPE" runat="server" Text='<%#Eval("INQUIRYTYPE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="Hold Reason">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHOLDREASON" runat="server" Text='<%#Eval("HOLDREASON") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="Hold Reason by Cust.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHOLDCUSTREMARKS" runat="server" Text='<%#Eval("HOLDCUSTREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="CC Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCALLREMARKS" runat="server" Text='<%#Eval("CALLREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="History">
                                                    <ItemTemplate>
                                                        <center>
                                                            <asp:LinkButton ID="lnkFolloupHistory" runat="server" OnClick="lnkFolloupHistory_Click"
                                                                Text="<i id='spaninfo' class='fa fa-info-circle' style='color: #3a1772;font-size: initial;'></i>   <asp:Label ID='lblCallText' runat='server' Text='Details'></asp:Label>">
                                                            </asp:LinkButton>
                                                        </center>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>--%>

                            <div id="divSO" class="box" runat="server" visible="false">
                                <div class="col-md-12">
                                    <asp:Label ID="lblHeadSO" runat="server" Text="SO Generated" Font-Size="20px" ForeColor="#faa61a"></asp:Label>
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

                                                <asp:TemplateField HeaderText="Inq. Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUSTUPDATEDATE" runat="server" Text='<%#Eval("CUSTUPDATEDATE") %>'></asp:Label>
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

                                                <asp:TemplateField HeaderText="Product">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPRODUCT" runat="server" Text='<%#Eval("PRODUCT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Specs.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSPECS" runat="server" Text='<%#Eval("SPECS") %>'></asp:Label>
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

                                                <asp:TemplateField HeaderText="SO No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblINQSO" runat="server" Text='<%#Eval("INQSO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%-- <asp:TemplateField HeaderText="Hold Reason">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHOLDREASON" runat="server" Text='<%#Eval("HOLDREASON") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="Hold Reason by Cust.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHOLDCUSTREMARKS" runat="server" Text='<%#Eval("HOLDCUSTREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="CC Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCALLREMARKS" runat="server" Text='<%#Eval("CALLREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <%--  <asp:TemplateField HeaderText="Start Dt.&Time">
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
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="History">
                                                    <ItemTemplate>
                                                        <center>
                                                            <asp:LinkButton ID="lnkFolloupHistory" runat="server" OnClick="lnkFolloupHistory_Click"
                                                                Text="<i id='spaninfo' class='fa fa-info-circle' style='color: #3a1772;font-size: initial;'></i>   <asp:Label ID='lblCallText' runat='server' Text='Details'></asp:Label>">
                                                            </asp:LinkButton>
                                                        </center>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div id="divSOINQ" class="box" runat="server" visible="false">
                                <div class="col-md-12">
                                    <asp:Label ID="lblHeadINQ" runat="server" Text="INQ Generated" Font-Size="20px" ForeColor="#faa61a"></asp:Label>
                                    <asp:LinkButton runat="server" ID="lnkInqSODwn" CssClass="btn btn-success pull-right" Text="Download" OnClick="lnkInqSODwn_Click"><i class="fa fa-download"></i></asp:LinkButton>
                                </div>
                                <div class="col-md-12" style="margin-top: 10px !important;">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvSOINQ" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
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

                                                <asp:TemplateField HeaderText="Inq. Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUSTUPDATEDATE" runat="server" Text='<%#Eval("CUSTUPDATEDATE") %>'></asp:Label>
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

                                                <asp:TemplateField HeaderText="Specs.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSPECS" runat="server" Text='<%#Eval("SPECS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="DEVICE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDEVICE" runat="server" Text='<%#Eval("DEVICE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Product">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPRODUCT" runat="server" Text='<%#Eval("PRODUCT") %>'></asp:Label>
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

                                                <asp:TemplateField HeaderText="Inq No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblINQSO" runat="server" Text='<%#Eval("INQSO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--  <asp:TemplateField HeaderText="Hold Reason">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHOLDREASON" runat="server" Text='<%#Eval("HOLDREASON") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Hold Reason by Cust.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHOLDCUSTREMARKS" runat="server" Text='<%#Eval("HOLDCUSTREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CC Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCALLREMARKS" runat="server" Text='<%#Eval("CALLREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                <%--  <asp:TemplateField HeaderText="Start Dt.&Time">
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
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="History">
                                                    <ItemTemplate>
                                                        <center>
                                                            <asp:LinkButton ID="lnkFolloupHistory" runat="server" OnClick="lnkFolloupHistory_Click"
                                                                Text="<i id='spaninfo' class='fa fa-info-circle' style='color: #3a1772;font-size: initial;'></i>   <asp:Label ID='lblCallText' runat='server' Text='Details'></asp:Label>">
                                                            </asp:LinkButton>
                                                        </center>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div id="divPending" class="box" runat="server" visible="false">
                                <div class="col-md-12">
                                    <asp:Label ID="lblHealPending" runat="server" Text="Pending Lead" Font-Size="20px" ForeColor="#faa61a"></asp:Label>
                                    <asp:LinkButton runat="server" ID="lnkPendingDwn" CssClass="btn btn-success pull-right" Text="Download" OnClick="lnkPendingDwn_Click"><i class="fa fa-download"></i></asp:LinkButton>
                                </div>
                                <div class="col-md-12" style="margin-top: 10px !important;">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvPending" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
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

                                                <asp:TemplateField HeaderText="Inq. Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUSTUPDATEDATE" runat="server" Text='<%#Eval("CUSTUPDATEDATE") %>'></asp:Label>
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

                                                <asp:TemplateField HeaderText="Product">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPRODUCT" runat="server" Text='<%#Eval("PRODUCT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Specs.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSPECS" runat="server" Text='<%#Eval("SPECS") %>'></asp:Label>
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

                                                <asp:TemplateField HeaderText="Assign To ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblASSIGNTO" runat="server" Text='<%#Eval("ASSIGNTO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Assigned To">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblASSIGNEDTO" runat="server" Text='<%#Eval("ASSIGNEDTO") %>'></asp:Label>
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

                                                <%-- <asp:TemplateField HeaderText="Enter By" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCREATEBY" runat="server" Text='<%#Eval("CREATEBY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Enter By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblENTEREDBY" runat="server" Text='<%#Eval("ENTEREDBY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Enter Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCREATEDATE" runat="server" Text='<%#Eval("CREATEDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>



                                                <asp:TemplateField HeaderText="Cust. Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUSTREMARKS" runat="server" Text='<%#Eval("CUSTREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Reff.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblREFF" runat="server" Text='<%#Eval("REFF") %>'></asp:Label>
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
                                                <%--  <asp:TemplateField HeaderText="Start Dt.&Time">
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
                                                </asp:TemplateField>--%>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div id="divCancelled" class="box" runat="server" visible="false">
                                <div class="col-md-12">
                                    <asp:Label ID="lblHeadCancelled" runat="server" Text="Cancelled Calls" Font-Size="20px" ForeColor="#1b1e24"></asp:Label>
                                    <asp:LinkButton runat="server" ID="lnkCancelledDwn" CssClass="btn btn-success pull-right" Text="Download" OnClick="lnkCancelledDwn_Click"><i class="fa fa-download"></i></asp:LinkButton>
                                </div>
                                <div class="col-md-12" style="margin-top: 10px !important;">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvCancelled" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
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

                                                <asp:TemplateField HeaderText="Inq. Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUSTUPDATEDATE" runat="server" Text='<%#Eval("CUSTUPDATEDATE") %>'></asp:Label>
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

                                                <asp:TemplateField HeaderText="Specs.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSPECS" runat="server" Text='<%#Eval("SPECS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="DEVICE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDEVICE" runat="server" Text='<%#Eval("DEVICE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Product">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPRODUCT" runat="server" Text='<%#Eval("PRODUCT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Price Range">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPRICERANGE" runat="server" Text='<%#Eval("PRICERANGE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Assign To ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblASSIGNTO" runat="server" Text='<%#Eval("ASSIGNTO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Assigned To">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblASSIGNEDTO" runat="server" Text='<%#Eval("ASSIGNEDTO") %>'></asp:Label>
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

                                                <asp:TemplateField HeaderText="Cust. Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUSTREMARKS" runat="server" Text='<%#Eval("CUSTREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Reff.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblREFF" runat="server" Text='<%#Eval("REFF") %>'></asp:Label>
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

                                                <asp:TemplateField HeaderText="Cancel Reason">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHOLDREASON" runat="server" Text='<%#Eval("HOLDREASON") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cancel Reason by Cust.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHOLDCUSTREMARKS" runat="server" Text='<%#Eval("HOLDCUSTREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="CC Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCALLREMARKS" runat="server" Text='<%#Eval("CALLREMARKS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:TemplateField HeaderText="Start Dt.&Time">
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
                                                </asp:TemplateField>--%>

                                                <asp:TemplateField HeaderText="History">
                                                    <ItemTemplate>
                                                        <center>
                                                            <asp:LinkButton ID="lnkFolloupHistory" runat="server" OnClick="lnkFolloupHistory_Click"
                                                                Text="<i id='spaninfo' class='fa fa-info-circle' style='color: #3a1772;font-size: initial;'></i>   <asp:Label ID='lblCallText' runat='server' Text='Details'></asp:Label>">
                                                            </asp:LinkButton>
                                                        </center>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>

                            <div id="divReassign" class="box" runat="server" visible="false">
                                <div class="col-md-12">
                                    <asp:Label ID="lblHeadReassign" runat="server" Text="Re-Assigned Calls" Font-Size="20px" ForeColor="#3a1772"></asp:Label>
                                    <asp:LinkButton runat="server" ID="lnkReassignDwn" CssClass="btn btn-success pull-right" Text="Download" OnClick="lnkReassignDwn_Click"><i class="fa fa-download"></i></asp:LinkButton>
                                </div>
                                <div class="col-md-12" style="margin-top: 10px !important;">
                                    <div class="box-body divhorizontal" style="overflow-x: scroll;">
                                        <asp:GridView ID="gvReassign" runat="server" CssClass="table table-hover table-striped table-bordered nowrap"
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

                                                <asp:TemplateField HeaderText="Lead ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLEADID" runat="server" Text='<%#Eval("LEADID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Inq. Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCUSTUPDATEDATE" runat="server" Text='<%#Eval("CUSTUPDATEDATE") %>'></asp:Label>
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

                                                <asp:TemplateField HeaderText="Product">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPRODUCT" runat="server" Text='<%#Eval("PRODUCT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Specs.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSPECS" runat="server" Text='<%#Eval("SPECS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="DEVICE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDEVICE" runat="server" Text='<%#Eval("DEVICE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Old Assign To">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOLDASSIGNTO" runat="server" Text='<%#Eval("OLDASSIGNTO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Re-Assign To">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblREASSIGNTO" runat="server" Text='<%#Eval("REASSIGNTO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Re-Assign By">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblREASSIGNBY" runat="server" Text='<%#Eval("REASSIGNBY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Re-Assign Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblREASSIGNDATE" runat="server" Text='<%#Eval("REASSIGNDATE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="History">
                                                    <ItemTemplate>
                                                        <center>
                                                            <asp:LinkButton ID="lnkReassign" runat="server" OnClick="lnkReassign_Click"
                                                                Text="<i id='spaninfo' class='fa fa-info-circle' style='color: #3a1772;font-size: initial;'></i>   <asp:Label ID='lblCallText' runat='server' Text='Details'></asp:Label>">
                                                            </asp:LinkButton>
                                                        </center>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
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

    <div class="modal fade" id="modal-CallHistory" data-backdrop="static">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" style="color: #337ab7">Call History</h4>
                </div>
                <div class="modal-body">
                    <div class="box-body">
                        <div class="row">
                            <div class="col-md-12">

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Cust. Name</label>
                                        <asp:HiddenField ID="hfHisID" runat="server" />
                                        <asp:Label ID="lblHisCustName" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Contact No. :</label>
                                        <asp:Label ID="lblHisContactNo" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Email :</label>
                                        <asp:Label ID="lblHisMail" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12">


                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Make :</label>
                                        <asp:Label ID="lblHisMake" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Model :</label>
                                        <asp:Label ID="lblHisModel" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                                <div class="col-md-4">
                                    <div class="form-group">
                                        <label>Curr. Status :</label>
                                        <asp:Label ID="lblHisCurrStatus" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>

                            </div>

                            <div class="col-md-12" style="margin-top: 10px !important;">
                                <%--<div class="col-md-6">--%>
                                <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="false" Width="100%" CellSpacing="0" ShowHeaderWhenEmpty="true"
                                    CssClass="table table-hover table-striped table-bordered nowrap">
                                    <EmptyDataTemplate>
                                        No Record Found!
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:BoundField DataField="CALLDATE" HeaderText="Call Date" />
                                        <asp:BoundField DataField="REASON" HeaderText="Hold/Cancel Reason" />
                                        <asp:BoundField DataField="CUSTREMARKS" HeaderText="Cust. Remarks" />
                                        <asp:BoundField DataField="CALLREMARKS" HeaderText="Our Remarks" />
                                        <asp:BoundField DataField="CALLBY" HeaderText="Call By" />
                                        <asp:BoundField DataField="CALLSTATUS" HeaderText="Call Status" />
                                        <%--<asp:TemplateField HeaderText="Image Type">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblImageType" runat="server" Text='<%# Eval("LISTDESC") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                                <%--</div>--%>
                            </div>
                        </div>
                    </div>
                    <%--<div class="box-footer text-center">
                                <i class="fa fa-spin fa-refresh fa-lg" id="faLoading3" style="display: none"></i>
                                <p style="margin-bottom: 0px; margin-top: 5px">
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" Text="Submit" OnClick="btnCancel_Click" UseSubmitBehavior="false"
                                        OnClientClick="if (window.Page_ClientValidate('VgCancelStatus') == true) {this.disabled = true; $('#faLoading1').show();}" />
                                </p>
                            </div>--%>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderMenu" runat="server">

    <input type="hidden" id="menutabid" value="tsmRptCRMCallLogs" runat="server" />
    <input type="hidden" id="mainmenuid" value="tsmRptCRM" />

</asp:Content>
